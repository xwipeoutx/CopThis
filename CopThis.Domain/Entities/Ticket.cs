namespace CopThis.Domain.Entities
{
    public class Ticket
    {
        public int Id { get; set; }
        public Vehicle Vehicle { get; set; }
        public int SpeedLimit { get; set; }
        public int ActualSpeed { get; set; }
        public bool IsPaid { get; set; }
    }
}
