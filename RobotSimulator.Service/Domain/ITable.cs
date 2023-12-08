using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("RobotSimulator.Service.Tests")]
namespace RobotSimulator.Service.Domain
{
    internal interface ITable
    {
        bool IsValidLocation(int x, int y);
    }
}
