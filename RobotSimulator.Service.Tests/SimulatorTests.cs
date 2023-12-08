using RobotSimulator.Service.Constants;
using RobotSimulator.Service.Domain;

namespace RobotSimulator.Service.Tests
{
    public class SimulatorTests
    {
        private TableDimensions _tableDimensions;
        public SimulatorTests()
        {
            _tableDimensions = new TableDimensions(5,5);
        }

        #region Place
        [Fact]
        public void Simulator_PlaceSet_Return_Success()
        {
            Simulator simulator = new(_tableDimensions);

            var response = simulator.Place(1,1,"NORTH");

            Assert.NotNull(response);
            Assert.True(response.Success);
            Assert.Empty(response.Messages);
        }

        [Fact]
        public void Simulator_PlaceSet_Invalid_Direction_Return_Message()
        {
            Simulator simulator = new(_tableDimensions);

            var response = simulator.Place(1, 1, "INVALID");

            Assert.NotNull(response);
            Assert.False(response.Success);
            Assert.Single(response.Messages);
            Assert.Equal(ValidationMessageConstants.InvalidDirection, response.Messages[0]);
        }

        [Fact]
        public void Simulator_PlaceSet_Invalid_Placement_Return_Message()
        {
            Simulator simulator = new(_tableDimensions);

            var response = simulator.Place(19, 1, "NORTH");

            Assert.NotNull(response);
            Assert.False(response.Success);
            Assert.Single(response.Messages);
            Assert.Equal(ValidationMessageConstants.InvalidPlacement, response.Messages[0]);
        }
        #endregion Place

        #region Turn
        [Fact]
        public void Simulator_Left_Turn_Return_Success()
        {
            Simulator simulator = new(_tableDimensions);

            simulator.Place(1, 1, "NORTH");

            var response = simulator.Left();

            Assert.NotNull(response);
            Assert.True(response.Success);
            Assert.Empty(response.Messages);
        }

        [Fact]
        public void Simulator_Left_Not_Placed_ReturnMessage()
        {
            Simulator simulator = new(_tableDimensions);

     
            var response = simulator.Left();

            Assert.NotNull(response);
            Assert.False(response.Success);
            Assert.Single(response.Messages);
            Assert.Equal(ValidationMessageConstants.RobotNotPlaced, response.Messages[0]);
        }

        [Fact]
        public void Simulator_Right_Turn_Return_Success()
        {
            Simulator simulator = new(_tableDimensions);

            simulator.Place(1, 1, "NORTH");

            var response = simulator.Right();

            Assert.NotNull(response);
            Assert.True(response.Success);
            Assert.Empty(response.Messages);
        }

        [Fact]
        public void Simulator_Right_Not_Placed_ReturnMessage()
        {
            Simulator simulator = new(_tableDimensions);

            var response = simulator.Right();

            Assert.NotNull(response);
            Assert.False(response.Success);
            Assert.Single(response.Messages);
            Assert.Equal(ValidationMessageConstants.RobotNotPlaced, response.Messages[0]);
        }

        #endregion Turn

        #region Move
        [Fact]
        public void Simulator_Move_Valid_Return_Success()
        {
            Simulator simulator = new(_tableDimensions);

            simulator.Place(1, 1, "NORTH");

            var response = simulator.Move();

            Assert.NotNull(response);
            Assert.True(response.Success);
            Assert.Empty(response.Messages);
        }

        [Fact]
        public void Simulator_Move_Not_Placed_ReturnMessage()
        {
            Simulator simulator = new(_tableDimensions);

            var response = simulator.Move();

            Assert.NotNull(response);
            Assert.False(response.Success);
            Assert.Single(response.Messages);
            Assert.Equal(ValidationMessageConstants.RobotNotPlaced, response.Messages[0]);
        }

        [Fact]
        public void Simulator_Move_Get_OutOfBounds_Message()
        {
            Simulator simulator = new(_tableDimensions);

            simulator.Place(4, 4, "NORTH");

            var response = simulator.Move();

            Assert.NotNull(response);
            Assert.False(response.Success);
            Assert.Equal(ValidationMessageConstants.RobotOutOfBounds, response.Messages[0]);
        }

        [Theory]
        [InlineData(4, 3, "NORTH", true)]
        [InlineData(4, 4, "NORTH", false)]
        [InlineData(3, 4, "EAST", true)]
        [InlineData(4, 4, "EAST", false)]
        [InlineData(4, 1, "SOUTH", true)]
        [InlineData(4, 0, "SOUTH", false)]
        [InlineData(1, 1, "WEST", true)]
        [InlineData(0, 0, "WEST", false)]
        public void Simulator_Move_Check_OutOfBounds(int x, int y, string direction, bool success)
        {
            Simulator simulator = new(_tableDimensions);

            simulator.Place(x, y, direction);

            var response = simulator.Move();

            Assert.NotNull(response);
            Assert.Equal(response.Success, success);
        }

        #endregion Move

        #region Report
        [Fact]
        public void Simulator_Report_Valid_Return_Report()
        {
            Simulator simulator = new(_tableDimensions);

            simulator.Place(1, 1, "NORTH");

            var response = simulator.Report();

            Assert.NotNull(response);
            Assert.True(response.Success);
            Assert.Single(response.Messages);
            Assert.Equal("1,1,NORTH", response.Messages[0]);
        }

        [Fact]
        public void Simulator_Report_Not_Placed_ReturnMessage()
        {
            Simulator simulator = new(_tableDimensions);

            var response = simulator.Report();

            Assert.NotNull(response);
            Assert.False(response.Success);
            Assert.Single(response.Messages);
            Assert.Equal(ValidationMessageConstants.RobotNotPlaced, response.Messages[0]);
        }
        #endregion Report
    }
}

