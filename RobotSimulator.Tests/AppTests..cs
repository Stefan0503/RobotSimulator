using RobotSimulator.Service;
using RobotSimulator.Service.Responses;

namespace RobotSimulator.Tests
{
    public class AppTests
    {
        private readonly Mock<IRobotSimulatorService> _robotSimulatorService;
        private readonly App _app;
        private Mock<ISimulator> _simulator;

        public AppTests()
        {
            _simulator = new Mock<ISimulator>();
            _simulator.Setup(x => x.Place(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>())).Returns(new CommandResponse());
            _simulator.Setup(x => x.Move()).Returns(new CommandResponse());
            _simulator.Setup(x => x.Left()).Returns(new CommandResponse());
            _simulator.Setup(x => x.Right()).Returns(new CommandResponse());
            _simulator.Setup(x => x.Report()).Returns(new CommandResponse());

            _robotSimulatorService = new Mock<IRobotSimulatorService>();
            _robotSimulatorService.Setup(x => x.GetSimulator(It.IsAny<int>(), It.IsAny<int>())).Returns(_simulator.Object);
            _app = new App(_robotSimulatorService.Object);

            var output = new StringWriter();
            Console.SetOut(output);
        }

       
        [Fact]
        public void App_Run_Place_Command_Calls_Simulator_Place_Method_Correct_Params()
        {
            var input = string.Join(Environment.NewLine, new[]
            {
                "PLACE 1,1,EAST",
                "E",
            });

            Console.SetIn(new StringReader(input));

            _app.Run(new string[] { });

            _simulator.Verify(x => x.Place(1, 1, "EAST"), Times.Once());
        }

        [Theory]
        [InlineData("PLACE x,1,EAST")]
        [InlineData("PLACE 1,y,EAST")]
        public void App_Run_Place_Command_Calls_Simulator_Place_Method_InCorrect_Param(string command)
        {
            var output = new StringWriter();
            Console.SetOut(output);

            var input = string.Join(Environment.NewLine, new[]
            {
                command,
                "E",
            });

            Console.SetIn(new StringReader(input));
            _app.Run(new string[] { });

            _simulator.Verify(x => x.Place(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()), Times.Never());
            Assert.Contains("Invalid X, Y coordinates for PLACE command", output.ToString());
        }

        [Fact]
        public void App_Commands_Calls_Simulator_Method_Correct_Times()
        {
            var input = string.Join(Environment.NewLine, new[]
            {
                "PLACE 1,1,EAST",
                "LEFT",
                "RIGHT",
                "MOVE",
                "REPORT",
                "PLACE 3,1,NORTH",
                "MOVE",
                "MOVE",
                "RIGHT",
                "RIGHT",
                "RIGHT",
                "RIGHT",
                "REPORT",
                "REPORT",
                "REPORT",
                "E",
            });

            Console.SetIn(new StringReader(input));

            _app.Run(new string[] { });

            _simulator.Verify(x => x.Place(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()), Times.Exactly(2));
            _simulator.Verify(x => x.Left(), Times.Once());
            _simulator.Verify(x => x.Right(), Times.Exactly(5));
            _simulator.Verify(x => x.Move(), Times.Exactly(3));
            _simulator.Verify(x => x.Report(), Times.Exactly(4));
        }


        [Fact]
        public void App_Commands_Displays_Response_Messages()
        {
            var moveResponse = new CommandResponse();
            moveResponse.Messages.Add("Move Command processed");

            var leftResponse = new CommandResponse();
            leftResponse.Messages.Add("Left Command processed");

            _simulator.Setup(x => x.Move()).Returns(moveResponse);
            _simulator.Setup(x => x.Left()).Returns(leftResponse);

            var output = new StringWriter();
            Console.SetOut(output);

            var input = string.Join(Environment.NewLine, new[]
            {
                "PLACE 1,1,EAST",
                "LEFT",
                "MOVE",
                "E",
            });

            Console.SetIn(new StringReader(input));

            _app.Run(new string[] { });

            Assert.Contains("Move Command processed", output.ToString());
            Assert.Contains("Left Command processed", output.ToString());
        }


    }
}