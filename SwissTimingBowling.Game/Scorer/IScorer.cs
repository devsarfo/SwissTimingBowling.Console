using SwissTimingBowling.Game.Frame;

namespace SwissTimingBowling.Game.Scorer;

public interface IScorer
{
    int ScoreFrame(IFrame frame);
}