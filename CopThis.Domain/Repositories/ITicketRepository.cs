using CopThis.Domain.Entities;

namespace CopThis.Domain.Repositories
{
    public interface ITicketRepository
    {
        Ticket Get(int ticketId);
        void Create(Ticket ticket);
        void Save(Ticket ticket);
    }
}