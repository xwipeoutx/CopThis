using CopThis.Domain.Entities;

namespace CopThis.Domain.Repositories
{
    public interface IVehicleRepository
    {
        Vehicle Find(string licensePlate);
        void Create(Vehicle ticket);
        void Save(Vehicle ticket);
    }
}