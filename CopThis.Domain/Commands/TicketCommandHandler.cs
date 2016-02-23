using CopThis.Domain.Entities;
using CopThis.Domain.Exceptions;
using CopThis.Domain.Repositories;

namespace CopThis.Domain.Commands
{
    public class TicketCommandHandler
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IVehicleRepository _vehicleRepository;

        public TicketCommandHandler(ITicketRepository ticketRepository, IVehicleRepository vehicleRepository)
        {
            _ticketRepository = ticketRepository;
            _vehicleRepository = vehicleRepository;
        }

        public Ticket Handle(IssueTicketCommand command)
        {
            if (command.ActualSpeed <= command.SpeedLimit)
                throw new SpeedLimitNotExceededException();

            var vehicle = _vehicleRepository.Find(command.LicensePlate)
                          ?? CreateVehicle(command.LicensePlate, command.Make, command.Model);

            var ticket = new Ticket
            {
                Vehicle = vehicle,
                SpeedLimit = command.SpeedLimit,
                ActualSpeed = command.SpeedLimit
            };
            _ticketRepository.Create(ticket);
            return ticket;
        }

        private Vehicle CreateVehicle(string licensePlate, string make, string model)
        {
            var vehicle = new Vehicle
            {
                LicensePlate = licensePlate,
                Make = make,
                Model = model
            };

            _vehicleRepository.Create(vehicle);

            return vehicle;
        }

        public void Handle(PayTicketCommand command)
        {
            var ticket = _ticketRepository.Get(command.TicketId);

            if (ticket == null)
                throw new TicketDoesNotExistException();

            if (ticket.IsPaid)
                throw new TicketIsAlreadyPaidException();

            ticket.IsPaid = true;
            _ticketRepository.Save(ticket);
        }
    }
}