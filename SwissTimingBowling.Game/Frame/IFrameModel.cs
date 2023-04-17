namespace SwissTimingBowling.Game.Frame;

public interface IFrameModel
{
    int? FirstRoll { get; }
    int? SecondRoll { get; }
    int? ThirdRoll { get; }
    int? FrameScore { get; }
    int? TotalScore { get; }
}