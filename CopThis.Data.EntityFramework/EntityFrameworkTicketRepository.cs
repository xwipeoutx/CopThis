using System.Data.Entity;
using System.Linq;
using CopThis.Domain.Entities;
using CopThis.Domain.Repositories;

namespace CopThis.Data.EntityFramework
{
    public class EntityFrameworkTicketRepository : ITicketRepository
    {
        private readonly CopContext _context;

        public EntityFrameworkTicketRepository(CopContext context)
        {
            _context = context;
        }

        public Ticket Get(int id)
        {
            return _context.Tickets.AsNoTracking().FirstOrDefault(t => t.Id == id);
        }

        public void Create(Ticket ticket)
        {
            _context.Tickets.Add(ticket);
        }

        public void Save(Ticket ticket)
        {
            _context.Tickets.Attach(ticket);
            _context.Entry(ticket).State = EntityState.Modified;
        }
    }
}