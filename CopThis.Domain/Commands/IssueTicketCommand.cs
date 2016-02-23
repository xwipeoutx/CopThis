namespace CopThis.Domain.Commands
{
    public class IssueTicketCommand
    {
        public string LicensePlate { get; set; }
        public int SpeedLimit { get; set; }
        public int ActualSpeed { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
    }
}