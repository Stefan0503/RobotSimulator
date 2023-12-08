using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("RobotSimulator.Service.Tests")]
namespace RobotSimulator.Service.Domain
{
    internal enum Direction
    {
        NORTH,
        EAST,
        SOUTH,
        WEST
    }

    internal class RobotPosition
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Direction Direction { get; set; }

        public RobotPosition(int x, int y, Direction direction)
        {
            X = x;
            Y = y;
            Direction = direction;
        }
    }

    internal class Robot : IRobot
    {
        public RobotPosition? Position { get; private set; }

        public bool Placed => Position!=null;

        public Robot() {}

        public void Place(RobotPosition position)
        {
            Position = position;
        }
        public void Move()
        {
            switch (Position?.Direction)
            {
                case Direction.EAST:
                    Position.X += 1;
                    break;
                case Direction.WEST:
                    Position.X -= 1;
                    break;
                case Direction.NORTH:
                    Position.Y += 1;
                    break;
                case Direction.SOUTH:
                    Position.Y -= 1;
                    break;
            }
        }

        public void Left()
        {
            switch (Position?.Direction)
            {
                case Direction.EAST:
                    Position.Direction = Direction.NORTH;
                    break;
                case Direction.WEST:
                    Position.Direction = Direction.SOUTH;
                    break;
                case Direction.NORTH:
                    Position.Direction = Direction.WEST;
                    break;
                case Direction.SOUTH:
                    Position.Direction = Direction.EAST;
                    break;
            }
        }

        public void Right()
        {
            switch (Position?.Direction)
            {
                case Direction.EAST:
                    Position.Direction = Direction.SOUTH;
                    break;
                case Direction.WEST:
                    Position.Direction = Direction.NORTH;
                    break;
                case Direction.NORTH:
                    Position.Direction = Direction.EAST;
                    break;
                case Direction.SOUTH:
                    Position.Direction = Direction.WEST;
                    break;
            }
        }

        public string Report()
        {
            return Placed ? $"{Position?.X.ToString()},{Position?.Y.ToString()},{Position?.Direction.ToString()}" : "Robot has not been placed.";
        }


    }
}
