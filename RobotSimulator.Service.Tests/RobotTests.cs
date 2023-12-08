using RobotSimulator.Service.Domain;

namespace RobotSimulator.Service.Tests
{
    public class RobotTests
    {
        [Fact]
        public void Robot_PlaceNotSet()
        {
            Robot robot = new();

            Assert.False(robot.Placed);
        }

        [Fact]
        public void Robot_PlaceSet()
        {
            Robot robot = new();
            RobotPosition position = new(1, 3, Direction.SOUTH);

            robot.Place(position);

            Assert.True(robot.Placed);
            Assert.Equal(position, robot.Position);

        }


        [Theory]
        [InlineData(0, 2, 3)]
        [InlineData(1, 3, 2)]
        [InlineData(2, 2, 1)]
        [InlineData(3, 1, 2)]
        public void Robot_Move_Position_IsSet(int direction, int x, int y)
        {
            Robot robot = new();
            RobotPosition position = new(2, 2, (Direction)direction);

            robot.Place(position);

            robot.Move();

            Assert.Equal(x, robot.Position?.X);
            Assert.Equal(y, robot.Position?.Y);
        }

        [Theory]
        [InlineData(0, 3)]
        [InlineData(1, 0)]
        [InlineData(2, 1)]
        [InlineData(3, 2)]
        public void Robot_Left_Direction_IsSet(int direction, int newDirection)
        {
            Robot robot = new();
            RobotPosition position = new(2, 2, (Direction)direction);

            robot.Place(position);

            robot.Left();

            Assert.Equal((Direction)newDirection, robot.Position?.Direction);
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 2)]
        [InlineData(2, 3)]
        [InlineData(3, 0)]
        public void Robot_Right_Direction_IsSet(int direction, int newDirection)
        {
            Robot robot = new();
            RobotPosition position = new(2, 2, (Direction)direction);

            robot.Place(position);

            robot.Right();

            Assert.Equal((Direction)newDirection, robot.Position?.Direction);
        }

        [Fact]
        public void Robot_Report_PlaceNotSet_Return_Message()
        {
            Robot robot = new();

            var result = robot.Report();

            Assert.Equal("Robot has not been placed.", result);
        }

        [Fact]
        public void Robot_Report_PlaceSet_Return_Report()
        {
            Robot robot = new();
            RobotPosition position = new(1, 3, Direction.SOUTH);

            robot.Place(position);

            var result = robot.Report();

            Assert.Equal("1,3,SOUTH", result);
        }
    }
}



