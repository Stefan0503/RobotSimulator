using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("RobotSimulator.Service.Tests")]
namespace RobotSimulator.Service.Domain
{
    public class TableDimensions
    {
        public int Width { get; set; }
        public int Length { get; set; }

        public TableDimensions(int width, int length)
        {
            Width = width;
            Length = length;
        }
    }

    internal class Table : ITable
    {
        private readonly TableDimensions _tableDimensions;
        public Table(TableDimensions tableDimensions)
        {
            _tableDimensions = tableDimensions;
        }

        public bool IsValidLocation(int x, int y)
        {
            return x >= 0 && x < _tableDimensions.Width && y >= 0 && y < _tableDimensions.Length;
        }
    }
}
