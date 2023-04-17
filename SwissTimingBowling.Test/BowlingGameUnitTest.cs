using System.Collections.Generic;
using NUnit.Framework;
using SwissTimingBowling.Game.Bowling;
using SwissTimingBowling.Game.Roll;
using SwissTimingBowling.Game.Scorer;

namespace SwissTimingBowling.Test;

public class BowlingGameUnitTest
{
    private IScorer _scorer;
    private IRoll _roll;
    private IBowlingGame _bowlingGame;
    
    [SetUp]
    public void SetUp()
    {
        _scorer = new Scorer();
        _roll = new TestRoll();
        _bowlingGame = new BowlingGame(_scorer, _roll);
    }
    
    [Test]
    public void TotalScore_After_All_Strikes_TotalScoreIs300()
    {
        _roll.SetNextRoll(10);

        for (var i = 0; i < 12; i++)
        {
            _bowlingGame.RollBall();
        }
        
        Assert.That(_bowlingGame.TotalScore, Is.EqualTo(300));
    }

    [Test]
    public void TotalScore_After_All_5s_Total_Score_Is_150()
    {
        var rolls = new List<int>(){ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        
        foreach (var roll in rolls)
        {
            _roll.SetNextRoll(roll);
            _bowlingGame.RollBall();
        }
        
        Assert.That(_bowlingGame.TotalScore, Is.EqualTo(150));
    }
    
    [Test]
    public void TotalScore_After_All_GutterBalls_TotalScore_Is_0()
    {
        _roll.SetNextRoll(0);
        
        for (var i = 0; i < 20; i++)
        {
            _bowlingGame.RollBall();
        }
        
        Assert.That(_bowlingGame.TotalScore, Is.EqualTo(0));
    }
}