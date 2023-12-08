using RobotSimulator.Service.Responses;

namespace RobotSimulator.Service
{
    public interface ISimulator
    {
        CommandResponse Place(int x, int y, string direction);
        CommandResponse Move();
        CommandResponse Left();
        CommandResponse Right();
        CommandResponse Report();
    }
}
