namespace RobotSimulator.Service
{
    public interface IRobotSimulatorService
    {
        ISimulator GetSimulator(int width, int length);
    }
}
