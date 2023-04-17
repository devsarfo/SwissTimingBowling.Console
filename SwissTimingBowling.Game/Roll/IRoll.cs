namespace SwissTimingBowling.Game.Roll;

public interface IRoll
{
    int NextRoll(int frame, int roll, int max);
    void SetNextRoll(int roll);
}