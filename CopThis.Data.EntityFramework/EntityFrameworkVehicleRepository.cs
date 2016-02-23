using System.Data.Entity;
using System.Linq;
using CopThis.Domain.Entities;
using CopThis.Domain.Repositories;

namespace CopThis.Data.EntityFramework
{
    public class EntityFrameworkVehicleRepository : IVehicleRepository
    {
        private readonly CopContext _context;

        public EntityFrameworkVehicleRepository(CopContext context)
        {
            _context = context;
        }

        public Vehicle Find(string licensePlate)
        {
            return _context.Vehicles.AsNoTracking().FirstOrDefault(
                v => v.LicensePlate == licensePlate
                );
        }

        public void Create(Vehicle vehicle)
        {
            _context.Vehicles.Add(vehicle);
        }

        public void Save(Vehicle vehicle)
        {
            _context.Vehicles.Attach(vehicle);
            _context.Entry(vehicle).State = EntityState.Modified;
        }
    }
}