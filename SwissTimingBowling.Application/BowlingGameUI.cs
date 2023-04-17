using SwissTimingBowling.Game.Frame;

namespace SwissTimingBowling.Application;

public class BowlingGameUi
{
    public void DisplayGame(List<IFrameModel> frames)
    {
        var seriesView = "";
        for (var i = 0; i < frames.Count; i++)
        {
            var isLast = i == frames.Count - 1;
            var frameView = GetFrameString(frames[i], isLast);
            seriesView += frameView;
            if (!isLast)
            {
                seriesView += "  ";
            }
        }
        
        Console.WriteLine($"\nSCORE: {seriesView}\n");
    }

    private string GetFrameString(IFrameModel frame, bool isCurrentFrame = false, bool isLastFrame = false)
    {
        var frameString = ParseFrame(frame, isCurrentFrame, isLastFrame);
        frameString += $" => {frame.FrameScore}({frame.TotalScore})";

        return frameString;
    }
    
    private string ParseFrame(IFrameModel frame, bool isCurrent = false, bool isLast = false)
    {
        var firstRoll = frame.FirstRoll switch
        {
            null => " ",
            10 => "X",
            _ => frame.FirstRoll.ToString()
        };
        
        var secondRoll = " ";
        if (frame.SecondRoll != null)
        {
            if (frame.FirstRoll + frame.SecondRoll == 10)
            {
                secondRoll = "/";
            }
            else if (frame.SecondRoll == 10)
            {
                secondRoll = "X";
            }
            else
            {
                secondRoll = frame.SecondRoll.ToString();
            }
        }
        
        if (isCurrent)
        {
            if (frame.FirstRoll == null)
            {
                firstRoll = "[]";
            }

            if (frame.SecondRoll == null)
            {
                secondRoll = "[]";
            }
        }

        var partialString = $"[{firstRoll}|{secondRoll}";
        
        if (isLast)
        {
            var thirdRoll = frame.ThirdRoll switch
            {
                null => " ",
                10 => "X",
                _ => frame.ThirdRoll.ToString()
            };
            
            if (isCurrent)
            {
                thirdRoll = "[]";
            }
            
            partialString += $"|{thirdRoll}";
        }

        partialString += "]";
        return partialString;
    }
}