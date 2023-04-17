namespace SwissTimingBowling.Game.Roll;

public class TestRoll : IRoll
{
    private int _nextRoll;
    
    public void SetNextRoll(int roll) => _nextRoll = roll;
    public int NextRoll(int frame, int roll, int max) => _nextRoll;
}