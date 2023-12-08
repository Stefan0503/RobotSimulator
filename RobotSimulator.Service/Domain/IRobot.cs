using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("RobotSimulator.Service.Tests")]
namespace RobotSimulator.Service.Domain
{
    internal interface IRobot
    {
        void Place(RobotPosition position);
        void Move();
        void Left();
        void Right();
        string Report();
    }
}
