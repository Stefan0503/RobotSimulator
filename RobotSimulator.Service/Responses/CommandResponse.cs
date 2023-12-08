namespace RobotSimulator.Service.Responses
{
    public class CommandResponse
    {
        public bool Success { get; internal set; } = false;
        public List<string> Messages { get; internal set; } = new List<string>();
    }
}
