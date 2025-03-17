using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data.Entities;
using Proekt;
using FlightManager.Filters;
using System.Net.Mail;
using System.Text;

namespace FlightManager.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly FlightDbContext _context;

        public ReservationsController(FlightDbContext context)
        {
            _context = context;
        }

        // GET: Reservations
        [LoggedUserFilter]
        public async Task<IActionResult> Index()
        {
            var flightDbContext = _context.Reservations.Include(r => r.Flight);
            return View(await flightDbContext.ToListAsync());
        }

        // GET: Reservations/Details/5
        [LoggedUserFilter]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.Flight)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Reservations/Create
        public IActionResult Create(int id)
        {
            ViewData["FlightId"] = new SelectList(_context.Flights, "Id", "Id");
            Test.Flight = _context.Flights.FirstOrDefault(f => f.Id == id);
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, [Bind("FirstName,MiddleName,LastName,Email,Egn,Telephone,Nationality,TicketType")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                reservation.FlightId = id;
                _context.Add(reservation);
                Flight flight = _context.Flights.FirstOrDefault(f => f.Id == id);
                //Test.Flight = flight;

                if (reservation.TicketType == Data.Enums.TicketType.Economy)
                {
                    flight.CapacityOfPassengers--;
                }
                else
                {
                    flight.CapacityOfBusinessClass--;
                }

                //SendEmail(reservation);

                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            ViewData["FlightId"] = new SelectList(_context.Flights, "Id", "Id", reservation.FlightId);
            return View(reservation);
        }

        private static void SendEmail(Reservation reservation)
        {
            string to = reservation.Email; //To address    
            string from = "mhadzhikovakeva@gmail.com"; //From address    
            MailMessage message = new MailMessage(from, to);

            string mailbody = $"Your reservation for flight from {reservation.Flight.LocationFrom} to " +
                $"{reservation.Flight.LocationFrom} is confirmed. Now you can not cancel it!{Environment.NewLine}" +
                $"Ticket is on name : {reservation.FirstName} {reservation.MiddleName} {reservation.LastName}{Environment.NewLine}" +
                $"It is {reservation.TicketType} class{Environment.NewLine}" +
                $"Enjoy your Flight!";
            message.Subject = "Confirmation of flight reservation";
            message.Body = mailbody;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
            System.Net.NetworkCredential basicCredential1 = new
            System.Net.NetworkCredential("mhadzhikovakeva", "mihi*hadz");
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = basicCredential1;
            client.Send(message);
        }

        // GET: Reservations/Edit/5
        [AdminFilter]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            ViewData["FlightId"] = new SelectList(_context.Flights, "Id", "Id", reservation.FlightId);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AdminFilter]
        public async Task<IActionResult> Edit(int id, [Bind("FirstName,MiddleName,LastName,Email,Egn,Telephone,Nationality,TicketType,FlightId,Id")] Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["FlightId"] = new SelectList(_context.Flights, "Id", "Id", reservation.FlightId);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        [AdminFilter]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.Flight)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AdminFilter]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            _context.Reservations.Remove(reservation);

            Flight flight = await _context.Flights.FirstOrDefaultAsync(f => f.Id == reservation.FlightId);
            if (reservation.TicketType == Data.Enums.TicketType.Economy)
            {
                flight.CapacityOfPassengers++;
            }
            else
            {
                flight.CapacityOfBusinessClass++;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.Id == id);
        }
    }
}
