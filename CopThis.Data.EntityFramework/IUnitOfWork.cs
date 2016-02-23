namespace CopThis.Data.EntityFramework
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}