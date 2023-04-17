using SwissTimingBowling.Game.Scorer;

namespace SwissTimingBowling.Game.Frame;

public class Frame : IFrame
{
    private int MaxRolls => _isLastFrame ? 3 : 2;
    private readonly List<int?> _rolls = new();
    private readonly IScorer _scorer;
    private int _pinsLeft = 10;
    private readonly bool _isLastFrame;
    public IFrame? Next { set; get; }
    public IFrame? Previous { set; get; }
    
    public Frame(IScorer scorer, IFrame? prev = null, IFrame? next = null, bool isIsLastFrame = false)
    {
        Previous = prev;
        Next = next;
        _scorer = scorer;
        _isLastFrame = isIsLastFrame;
    }

    public bool IsOpenFrame => Type == FrameType.Open;

    public void AddRoll(int pinsHit)
    {
        if (Type != FrameType.Open) throw new Exception("This frame is already closed");
        if (_rolls.Count >= MaxRolls) throw new Exception($"No more rolls can be added to this frame");
        
        ValidateRollInRange(pinsHit:pinsHit);
        _rolls.Add(pinsHit);
        _pinsLeft -= pinsHit;
        
        if(_pinsLeft == 0 && _isLastFrame)  ResetPins();
    }

    public List<int?> Rolls => _rolls;
    
    public int PinsLeft => _pinsLeft;
    
    private void ResetPins()
    {
        _pinsLeft = 10;
    }

    public int FrameScore => _scorer.ScoreFrame(this);

    public int TotalScore => (Previous?.TotalScore ?? 0) + FrameScore;

    public FrameType Type
    {
        get 
        {
            var frameType = FrameType.Open;
            switch (Rolls.Count)
            {
                case <= 0:
                    return frameType;
                case >= 1:
                {
                    if (Rolls[0] == 10) frameType = FrameType.Strike;
                    break;
                }
            }

            if (Rolls.Count >= 2) 
            { 
                if (Rolls[0] + Rolls[1] == 10) frameType = FrameType.Spare;
                if (Rolls[0] + Rolls[1] < 10) frameType = FrameType.Mark;
                
            }
            
            if (_isLastFrame && frameType is FrameType.Strike or FrameType.Spare && Rolls.Count < MaxRolls)
            {
                frameType = FrameType.Open;
            }

            return frameType;
        }
    }

    
    public IFrameModel ToFrame()
    {
        return new FrameModel(Rolls.FirstOrDefault(), Rolls.ElementAtOrDefault(1), Rolls.ElementAtOrDefault(2), FrameScore, TotalScore);
    }

    private void ValidateRollInRange(int pinsHit)
    {
        if (pinsHit < 0) throw new ArgumentOutOfRangeException(nameof(pinsHit), "You can not hit negative pins");
        if (pinsHit > _pinsLeft) throw new ArgumentOutOfRangeException(nameof(pinsHit), $"You can not hit more pins than the {_pinsLeft} left on the frame");
    }
}