namespace SwissTimingBowling.Game.Frame;

public interface IFrame
{
    IFrame? Next { get; set; }
    IFrame? Previous { get; set; }
    List<int?> Rolls { get; }
    int FrameScore { get; }
    int TotalScore { get; }
    FrameType Type { get; }
    bool IsOpenFrame { get; }
    IFrameModel ToFrame();
    void AddRoll(int pinsHit);
    int PinsLeft { get; }
}
