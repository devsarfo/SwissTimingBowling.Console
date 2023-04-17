namespace SwissTimingBowling.Game.Frame;

public class FrameModel : IFrameModel
{
    public int? FirstRoll { get; set; }
    public int? SecondRoll { get; set; }
    public int? ThirdRoll { get; set; }
    public int? FrameScore { get; set; }
    public int? TotalScore { get; set; }

    public FrameModel(int? firstRoll = null, int? secondRoll = null, int? thirdRoll = null, int? frameScore = null, int? totalScore = null)
    {
        FirstRoll = firstRoll;
        SecondRoll = secondRoll;
        ThirdRoll = thirdRoll;
        FrameScore = frameScore;
        TotalScore = totalScore;
    }
}