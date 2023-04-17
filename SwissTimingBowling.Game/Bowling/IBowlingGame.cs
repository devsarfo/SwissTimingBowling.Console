using SwissTimingBowling.Game.Frame;

namespace SwissTimingBowling.Game.Bowling;

public interface IBowlingGame
{
    void RollBall();
    List<IFrameModel> Frames();
    int TotalScore { get; }
    bool IsGameCompleted();
}