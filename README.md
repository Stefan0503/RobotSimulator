# Robot Simulator


## Description

The application is a simulation of a toy robot moving on a square tabletop, of dimensions 5 units x 5 
units. 

There are no other obstructions on the table surface. 

The robot is free to roam around the surface of the table but must be prevented from falling to 
destruction. Any movement that would result in the robot falling from the table must be prevented, 
however further valid movement commands must still be allowed. 

The application reads in commands of the following form - 

PLACE X,Y,F 
MOVE 
LEFT 
RIGHT 
REPORT 

PLACE will put the toy robot on the table in position X,Y and facing NORTH, SOUTH, EAST or WEST. 

The origin (0,0) can be considered to be the SOUTH WEST most corner. 

The first valid command to the robot is a PLACE command, after that, any sequence of commands may 
be issued, in any order, including another PLACE command. The application discards all 
commands in the sequence until a valid PLACE command has been executed.

MOVE will move the toy robot one unit forward in the direction it is currently facing. 

LEFT and RIGHT will rotate the robot 90 degrees in the specified direction without changing the position 
of the robot. 

REPORT will announce the X,Y and F of the robot. 

A robot that is not on the table will ignore the MOVE, LEFT, RIGHT and REPORT commands. 


## Inputs

You can enter inputs eiter via command line prompts or via an input file. The input file needs to reside as a .txt file in the same directory as the application exe.
There is a commands.txt file included in the solution which can be used

Example Inputs

PLACE 1,1,EAST
MOVE
LEFT
MOVE
REPORT

Output - 2,2,NORTH

PLACE 2,2,NORTH
MOVE
LEFT
MOVE
LEFT
RIGHT
REPORT

Output - 1,3,WEST


## Setup

Clone this repo locally and open the application using Visual Studio. This app is written in c# using .net 6.

The application has been split into two projects. The Robot simulator has been made into a service which has been injected into the Console app via Dependency Injection.
This makes it easy to reuse the service for other applications.

Build the solution in Release mode.

In File Explorer, navigate to the ..\RobotSimulator\bin\Release\net6.0 folder.


## Running the app

In the folder above, either

1. Click on RobotSimulator.exe. Manually start entering commands to control the robot. Remember to place the robot on the table first.

2. Open up command prompt. CD (change directory) to the same ..\RobotSimulator\bin\Release\net6.0 folder. 
Enter 'RobotSimulator commands.txt', this will execute all the commands in this file.
Feel free to edit this commands.txt file, or create a new .txt file in the same folder, add some commands and execute this file instead -  'RobotSimulator [filename].txt'


## Customise the table size.

5 x 5 table just too small for your friendly robot?
No problem, edit the appsettings.json file in Visual Studio and change the Table dimensions width and height to make the table surface bigger (or as small as 0,0 if you are feeling vindictive). 
Rebuild the application and let your metal friend roam!


