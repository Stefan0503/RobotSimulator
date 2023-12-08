using RobotSimulator.Service.Domain;

namespace RobotSimulator.Service.Tests
{
    public class TableTests
    {
        [Theory]
        [InlineData(4, 4, true)]
        [InlineData(5, 5, false)]
        [InlineData(0, 0, true)]
        [InlineData(1, 6, false)]
        [InlineData(6, 1, false)]
        [InlineData(-1, 1, false)]
        [InlineData(1, -1, false)]
        [InlineData(1, 1, true)]
        public void Table_Check_Is_Valid_Location(int width, int length, bool result)
        {
            TableDimensions tableDimensions = new(5, 5);

            Table table = new(tableDimensions);

            var _result = table.IsValidLocation(width, length);

            Assert.Equal(result, _result);
        }
    }
}