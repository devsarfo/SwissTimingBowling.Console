using SwissTimingBowling.Game.Frame;

namespace SwissTimingBowling.Game.Scorer;

public class Scorer : IScorer
{
    public int ScoreFrame(IFrame frame)
    {
        return frame.Type switch
        {
            FrameType.Strike => CalculateStrikeScore(frame),
            FrameType.Spare => CalculateSpareScore(frame),
            _ => frame.Rolls.Sum(roll => roll ?? 0)
        };
    }

    private static int CalculateSpareScore(IFrame frame)
    {
        //Ten plus bonus of the next roll
        var nextRoll = GetNextRoll(frame);
        return 10 + nextRoll;
    }

    private static int GetNextRoll(IFrame frame)
    {
        var nextFrame = frame.Next;
        if (nextFrame == null)
        {
            //this is the last frame 1 additional roll has been made, will be the sum of all the rolls
            return frame.Rolls.LastOrDefault() ?? 0;
        }

        //this frame isn't the last frame
        return nextFrame.Rolls.FirstOrDefault() ?? 0;
    }


    private static int CalculateStrikeScore(IFrame frame)
    {
        //10 plus the bonus of the next two rolls
        var nextTwoRolls = GetNextTwoRolls(frame);
        return 10 + nextTwoRolls.Sum(roll => roll ?? 0);
    }

    private static IEnumerable<int?> GetNextTwoRolls(IFrame frame)
    {
        var nextFrame = frame.Next;
        if (nextFrame is null)
        {
            //this is the last frame 2 additional rolls has been made, will be the sum of all the rolls
            return frame.Rolls.Skip(1).Take(2).ToList();
        }
        
        if (nextFrame.Type != FrameType.Strike)
        {
            //if we have a spare or nonMarked on the next frame the bonus is just the sum of the next frame
            return nextFrame.Rolls.Take(2).ToList();
        }
        
        //The next frame is also a strike, so we need another frame to calculate the bonus
        var twoFrameFromMe = nextFrame.Next;
        if (twoFrameFromMe is null)
        {
            // the next frame is the last frame so we take two first rolls as the bonus
            return nextFrame.Rolls.Take(2).ToList();
        }
            
        var firstRoll = nextFrame.Rolls[0];
        var secondRoll = twoFrameFromMe.Rolls.Count > 0  ? twoFrameFromMe.Rolls[0] : 0;
        
        return new List<int?>() { firstRoll, secondRoll };
    }
}