using RobotSimulator.Service.Domain;

namespace RobotSimulator.Service
{
    public class RobotSimulatorService : IRobotSimulatorService
    {
        public RobotSimulatorService() { }
        public ISimulator GetSimulator(int width = 5, int length = 5)
        {
            return new Simulator(new TableDimensions(width, length));
        }
    }
}