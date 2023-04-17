using System;
using NUnit.Framework;
using SwissTimingBowling.Game.Frame;
using SwissTimingBowling.Game.Scorer;

namespace SwissTimingBowling.Test;

public class FrameUnitTest
{
    private IFrame _frame;
    private IScorer _scorer;
    
    [SetUp]
    public void SetUp()
    {
        _scorer = new Scorer();
        _frame = new Frame(scorer: _scorer);
    }

    [Test]
    public void AddRoll_FrameIsAlreadyFilled_ThrowsException()
    {
        //arrange
        _frame.AddRoll(2);
        _frame.AddRoll(5);

        //act and assert
        Assert.Throws<Exception>(() => _frame.AddRoll(3));
    }
    
    //TODO: one for strike and one for spare and nonMarked
    [Test]
    public void Add_Roll_Is_A_Strike_First_Roll_Is_Strike_Second_Roll_Throws_Exception()
    {
        _frame.AddRoll(10);
        
        Assert.That(_frame.Type, Is.EqualTo(FrameType.Strike));
        Assert.Throws<Exception>(() => _frame.AddRoll(2));
    }
    
    [TestCase(5, 5)]
    [TestCase(7, 3)]
    public void Add_Roll_Is_A_Spare_First_And_Second_Roll_Is_Spare_Third_Roll_Throws_Exception(int firstRoll, int secondRoll)
    {
        _frame.AddRoll(firstRoll);
        _frame.AddRoll(secondRoll);
        
        Assert.That(_frame.Type, Is.EqualTo(FrameType.Spare));
        Assert.Throws<Exception>(() => _frame.AddRoll(2));
    }
    
    [TestCase(5, 2)]
    [TestCase(4, 5)]
    public void Add_Roll_Is_Marked_Frame_First_And_Second_Is_Mark_Third_Roll_Throws_Exception(int firstRoll, int secondRoll)
    {
        _frame.AddRoll(firstRoll);
        _frame.AddRoll(secondRoll);
        
        Assert.That(_frame.Type, Is.EqualTo(FrameType.Mark));
        Assert.Throws<Exception>(() => _frame.AddRoll(2));
    }
    
    [Test]
    public void Total_Score_For_Two_Frames_With_Score_Of_7_Is_14_Total_Score_On_Second_Frame()
    {
        var firstFrame = new Frame(_scorer);
        firstFrame.AddRoll(7);
        firstFrame.AddRoll(0);
        
        var secondFrame = new Frame(_scorer);
        firstFrame.Next = secondFrame;
        secondFrame.Previous = firstFrame;
        secondFrame.AddRoll(0);
        secondFrame.AddRoll(7);

        Assert.That(firstFrame.FrameScore, Is.EqualTo(7));
        Assert.That(firstFrame.TotalScore, Is.EqualTo(7));
        Assert.That(secondFrame.FrameScore, Is.EqualTo(7));
        Assert.That(secondFrame.TotalScore, Is.EqualTo(14));
    }
    
    [Test]
    public void Add_Roll_Pins_Hit_Is_Less_Than_Zero_Throws_ArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _frame.AddRoll(-3));
    }
}