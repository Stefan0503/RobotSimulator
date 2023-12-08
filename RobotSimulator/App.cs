using Microsoft.Extensions.Configuration;
using RobotSimulator.Service;
using RobotSimulator.Service.Responses;

namespace RobotSimulator
{
    public class App
    {
        private readonly IRobotSimulatorService _robotSimulatorService;
        private ISimulator _robotSimulator;
        private readonly int _tableWidth = 5;
        private readonly int _tableLength = 5;

        public App(IRobotSimulatorService robotSimulatorService)
        {
            _robotSimulatorService = robotSimulatorService;

            var config = GetConfig();

            _ = int.TryParse(config.GetSection("TableDimensions:Width").Value, out _tableWidth);
            _ = int.TryParse(config.GetSection("TableDimensions:Length").Value, out _tableLength);

            _robotSimulator = _robotSimulatorService.GetSimulator(_tableWidth, _tableLength);
        }

        static IConfiguration GetConfig()
        {
            var builder = new ConfigurationBuilder()
                         .SetBasePath(Directory.GetCurrentDirectory())
                         .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);


            return builder.Build();
        }

        public void Run(string[] args)
        {

            if (args == null || args.Length == 0)
            {
                StartNewSession();
                return;
            }

            if (File.Exists(args[0]) && (Path.GetExtension(args[0]) == ".txt"))
            {
                string[] commands = File.ReadAllLines(args[0]);

                foreach (string command in commands)
                {
                    var response = ProcessCommand(command);
                    response.Messages.ForEach(x => Console.WriteLine(x));
                }
            }
            else
            {
                Console.WriteLine("Not a .txt file. Please try again.");
            }

        }

        private void StartNewSession()
        {
            Console.Write("\n");
            Console.Write("Please begin the Robot Simulator by entering a 'PLACE X,Y,DIRECTION' command to place the Robot on the table.\n");
            Console.Write("\n");
            Console.Write("You may then enter these commands :\n");
            Console.Write("\n");
            Console.Write("PLACE X,Y,DIRECTION - Place robot on the table in position X,Y and facing NORTH, SOUTH, EAST or WEST\n");
            Console.Write("MOVE -  Move the  robot one unit forward in the direction it is currently facing\n");
            Console.Write("RIGHT - Rotate the robot 90 degrees in the specified direction without changing the position of the robot\n");
            Console.Write("LEFT - Rotate the robot 90 degrees in the specified direction without changing the position of the robot\n");
            Console.Write("REPORT - Announce X,Y and position of the robot\n");
            Console.Write("\n");
            Console.Write("Enter N to start new session.\n");
            Console.Write("\n");
            Console.Write("Enter E to end session.\n");

            _robotSimulator = _robotSimulatorService.GetSimulator(_tableWidth, _tableLength);

            AcceptInput();
        }


        private void AcceptInput()
        {
            var line = Console.ReadLine();

            switch (line?.ToUpper())
            {
                case "E":
                    break;
                case "N":
                    StartNewSession();
                    break;
                default:
                    var response = ProcessCommand(line);
                    response.Messages.ForEach(x => Console.WriteLine(x));
                    AcceptInput();
                    break;
            }

        }

        private CommandResponse ProcessCommand(string command)
        {
            var _command = command.Trim().ToUpper();
            CommandResponse response = new();

            if (_command.StartsWith("PLACE")) 
            {
                response = ProcessPlace(_command);
                return response;
            }

            if (_command.StartsWith("MOVE"))
            {
                response = _robotSimulator.Move();
                return response;
            }

            if (_command.StartsWith("LEFT"))
            {
                response = _robotSimulator.Left();
                return response; 
            }

            if (_command.StartsWith("RIGHT"))
            {
                response = _robotSimulator.Right();
                return response;
            }

            if (_command.StartsWith("REPORT"))
            {
                response = _robotSimulator.Report();
                return response;
            }

            return response;
        }

        private CommandResponse ProcessPlace(string command = "")
        {
            CommandResponse response;
            bool validEast = int.TryParse(command.AsSpan(6, 1), out int east);
            bool validNorth = int.TryParse(command.AsSpan(8, 1), out int north);

            if (!validEast || !validNorth) {
                response = new CommandResponse();
                response.Messages.Add("Invalid X, Y coordinates for PLACE command");
                return response;
            };

            response = _robotSimulator.Place(east, north, command[10..]);
            return response;
        }
    }
}
