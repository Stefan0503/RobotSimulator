namespace RobotSimulator.Service.Constants
{
    public static class ValidationMessageConstants
    {
        public const string CommandIgnored = "Command ignored.";
        public const string InvalidDirection = "Invalid direction, must be NORTH, SOUTH, EAST or WEST. " + CommandIgnored;
        public const string InvalidPlacement = "Invalid Robot placement. " + CommandIgnored;
        public const string RobotNotPlaced = "Cannot process command until Robot is placed. " + CommandIgnored;
        public const string RobotOutOfBounds = "Cannot process command as Robot will be out of bounds. " + CommandIgnored;
    }
}
