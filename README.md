# TEST TASK SWISS TIMING – C# BOWLING



## Solution:
The solution includes 3 projects:
- Game: Class Library containing all logic regarding the Bowling game.
- Application: Console Application including the Bowling Game UI
- Test: Unit Test for Game and Frames


## Description:
Please create an application that will calculate and display score of a bowling game for one player.
The number of fallen pins in each frame will be entered by application user.

Short bowling rules overview:

A bowling game consists of rolls to clear 10 pins at the end of the bowling lane. This means that 0
to 10 pins can be knocked down with each throw. A player has a maximum of 2 throws per round
to clear the 10 pins.

Throws are grouped into frames, each of which is assigned a score. A game consists of 10 rounds or
frames. 

The number of throws a player may make per round depends on how many pins are thrown with
them:  
If a player's first throw clears all 10 pins (strike), it is the only throw in the round.
Otherwise, the frames always contain two throws, each with 0 to 10 pins.

Exception: If the 10th frame contains a strike as the first throw or both throws together result in a
spare (see below), then a third throw can be made. There are some rules for calculating the score
of a frame:
- A frame whose two throws together have broken a maximum of 9 pins receives the sum of the pins as its score.
- A frame whose two throws together have broken 10 pins (spare) receives a score of 10 + the number of pins of the next throw.
- A frame with one strike receives a score of 10 + the sum of the pins of the next two throws.

Each frame shows the first and second and, in the case of the last, even the third throw with their pins as well as the cumulative score. If the box for the second throw is half filled, there is a spare, if it is completely filled, the first throw was a strike.

## Programming Language / Technology:
- .NET 6 & C# 
- Unit Tests
- Exception Handling
