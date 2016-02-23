using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using CopThis.Data.EntityFramework;
using CopThis.Domain.Commands;
using CopThis.Domain.Entities;

namespace CopThis.Controllers
{
    public class TicketRow
    {
        public int Id { get; set; }
        public int SpeedExceededBy { get; set; }
        public string LicensePlate { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }

        public static readonly Expression<Func<Ticket, TicketRow>> Projection = ticket => new TicketRow
        {
            Id = ticket.Id,
            SpeedExceededBy = ticket.ActualSpeed - ticket.SpeedLimit,
            LicensePlate = ticket.Vehicle.LicensePlate,
            Make = ticket.Vehicle.Make,
            Model = ticket.Vehicle.Model
        };
    }

    public class TicketView
    {
        public int Id { get; set; }
        public int SpeedLimit { get; set; }
        public int ActualSpeed { get; set; }
        public string LicensePlate { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }

        public static readonly Expression<Func<Ticket, TicketView>> Projection = ticket => new TicketView
        {
            Id = ticket.Id,
            SpeedLimit = ticket.SpeedLimit,
            ActualSpeed = ticket.ActualSpeed,
            LicensePlate = ticket.Vehicle.LicensePlate,
            Make = ticket.Vehicle.Make,
            Model = ticket.Vehicle.Model
        };

        public static readonly Func<Ticket, TicketView> Create = Projection.Compile();
    }

    public class TicketsController : Controller
    {
        private readonly TicketCommandHandler _ticketCommandHandler;
        private readonly IUnitOfWork _unitOfWork;
        private readonly CopContext _context;

        public TicketsController(TicketCommandHandler ticketCommandHandler, IUnitOfWork unitOfWork, CopContext context)
        {
            _ticketCommandHandler = ticketCommandHandler;
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public ActionResult Index()
        {
            IEnumerable<TicketRow> tickets = _context.Tickets.Include("Vehicle")
                .Where(t => !t.IsPaid)
                .Select(TicketRow.Projection);

            return View(tickets);
        }

        public ActionResult View(int id)
        {
            var ticket = _context.Tickets.Include("Vehicle").FirstOrDefault(t => t.Id == id);

            var ticketView = TicketView.Create(ticket);
            return View(ticketView);
        }

        public ActionResult Create()
        {
            return View(new IssueTicketCommand());
        }

        [ValidateAntiForgeryToken, HttpPost]
        public ActionResult Create([Bind]IssueTicketCommand issueTicket)
        {
            var ticket = _ticketCommandHandler.Handle(issueTicket);
            _unitOfWork.Commit();

            return RedirectToAction("View", new {id = ticket.Id});
        }

        [ValidateAntiForgeryToken]
        public ActionResult Pay([Bind]PayTicketCommand payTicket)
        {
            _ticketCommandHandler.Handle(payTicket);
            _unitOfWork.Commit();

            return Redirect("Index");
        }

        public ActionResult About()
        {
            return View();
        }
    }
}