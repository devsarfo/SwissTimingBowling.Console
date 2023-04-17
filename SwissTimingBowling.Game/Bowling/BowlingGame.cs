using SwissTimingBowling.Game.Frame;
using SwissTimingBowling.Game.Roll;
using SwissTimingBowling.Game.Scorer;

namespace SwissTimingBowling.Game.Bowling;

public class BowlingGame : IBowlingGame
{
    private readonly IRoll _roll;
    private readonly IScorer _scorer;
    private readonly IFrame _head;
    private IFrame _currentFrame;
    private int _numberOfFrames;

    public BowlingGame(IScorer scorer, IRoll rollProvider)
    {
        _roll = rollProvider;
        _scorer = scorer;
        _head = new Frame.Frame(_scorer);
        _numberOfFrames = 1;
        _currentFrame = _head;
    }
    
    public int TotalScore => _currentFrame.TotalScore;

    public List<IFrameModel> Frames()
    { 
        var frames = new List<IFrameModel>();
        var frame = _head;
        while (frame != null)
        {
            frames.Add(frame.ToFrame());
            frame = frame.Next;
        }

        while (frames.Count < 10)
        {
            frames.Add(new FrameModel());
        }
        
        return frames;
    }
    
    public void RollBall()
    {
        if (!_currentFrame.IsOpenFrame) throw new Exception();
        
        var currentRollsRecordedOnFrame = _currentFrame.Rolls.Count;
        while (_currentFrame.Rolls.Count <= currentRollsRecordedOnFrame)
        {
            try
            {
                var newRoll = _roll.NextRoll(_numberOfFrames, currentRollsRecordedOnFrame+1, _currentFrame.PinsLeft);
               _currentFrame.AddRoll(newRoll);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine($"ERROR: {e.Message}");
            }
        }
        
        if (_currentFrame.IsOpenFrame || _numberOfFrames > 9) return;
        
        var nextFrame = new Frame.Frame(scorer: _scorer, isIsLastFrame: _numberOfFrames >= 9);
            
        _currentFrame.Next = nextFrame;
        nextFrame.Previous = _currentFrame;
        _currentFrame = nextFrame;
        _numberOfFrames++;

    }

    public bool IsGameCompleted()
    {
        return _numberOfFrames == 10 && !_currentFrame.IsOpenFrame;
    }
}