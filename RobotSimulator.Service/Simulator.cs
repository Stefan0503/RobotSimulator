using RobotSimulator.Service.Constants;
using RobotSimulator.Service.Domain;
using RobotSimulator.Service.Responses;

namespace RobotSimulator.Service
{
    public class Simulator : ISimulator
    {
        private readonly Robot _robot;
        private readonly Table _table;

        public Simulator(TableDimensions tableDimensions)
        {
            _robot = new Robot();
            _table = new Table(tableDimensions);
        }

        public CommandResponse Place(int x, int y, string direction)
        {
            var response = new CommandResponse();

            if (!Enum.TryParse(direction.ToUpper(), out Direction _direction))
            {
                response.Messages.Add(ValidationMessageConstants.InvalidDirection);
                return response;
            }

            if (IsValidPlacement(response, ValidationMessageConstants.InvalidPlacement, x, y))
            {
                response.Success = true;
                _robot.Place(new RobotPosition(x, y, _direction));
            }

            return response;
        }

        public CommandResponse Left()
        {
            return Turn(_robot.Left);
        }

        public CommandResponse Right()
        {
            return Turn(_robot.Right);
        }

        private CommandResponse Turn(Action turn)
        {
            var response = new CommandResponse();

            if (IsRobotPlaced(response))
            {
                response.Success = true;
                turn();
            }
            return response;
        }

        public CommandResponse Move()
        {
            var response = new CommandResponse();

            if (ValidateMove(response))
            {
                response.Success = true;
                _robot.Move();
            }
            return response;
        }

        public CommandResponse Report()
        {
            var response = new CommandResponse();

            if (IsRobotPlaced(response))
            {
                response.Success = true;
                response.Messages.Add(_robot.Report());
            }

            return response;
        }

        private bool ValidateMove(CommandResponse response)
        {
            return IsRobotPlaced(response) && CanProcessMoveCommand(response);
        }

        private bool IsRobotPlaced(CommandResponse response)
        {
            if (!_robot.Placed)
            {
                response.Messages.Add(ValidationMessageConstants.RobotNotPlaced);
            }

            return _robot.Placed;
        }

        private bool CanProcessMoveCommand(CommandResponse response)
        {
            var newX = _robot.Position != null ? _robot.Position.X : -1;
            var newY = _robot.Position != null ? _robot.Position.Y : -1;

            switch (_robot.Position?.Direction)
            {
                case Direction.EAST:
                    newX += 1;
                    break;
                case Direction.WEST:
                    newX -= 1;
                    break;
                case Direction.NORTH:
                    newY += 1;
                    break;
                case Direction.SOUTH:
                    newY -= 1;
                    break;
            }

            return IsValidPlacement(response, ValidationMessageConstants.RobotOutOfBounds, newX, newY);
        }

        private bool IsValidPlacement(CommandResponse response, string message, int x, int y)
        {
            var valid = _table.IsValidLocation(x, y);
            if (!valid)
            {
                response.Messages.Add(message);
            }

            return valid;
        }
    }
}
