using SwissTimingBowling.Application;
using SwissTimingBowling.Game.Bowling;
using SwissTimingBowling.Game.Roll;
using SwissTimingBowling.Game.Scorer;

Console.WriteLine("========================");
Console.WriteLine("SwissTiming Bowling Game");
Console.WriteLine("========================\n");

string? player = null;
while (player is null || player.Length == 0)
{
    Console.Write("Enter Name of Player: ");
    player = Console.ReadLine()?.Trim();
    
    if(player is null || player.Length == 0) Console.Write("Invalid Player Name! ");
}

Console.WriteLine($"\nWelcome to the Bowling Game, {player}");

var scorer = new Scorer();
var roll = new Roll();
var game = new BowlingGame(scorer, roll);
var gameUi = new BowlingGameUi();

//Show Initial Score Card
gameUi.DisplayGame(game.Frames());

while (!game.IsGameCompleted())
{ 
    game.RollBall(); 
    gameUi.DisplayGame(game.Frames());
}

Console.WriteLine("==================================================");
Console.WriteLine($"Game Over {player}, Final Score: {game.TotalScore}");
Console.WriteLine("==================================================");
