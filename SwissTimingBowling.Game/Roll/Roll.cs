namespace SwissTimingBowling.Game.Roll;

public class Roll : IRoll
{
    public int NextRoll(int frame, int roll, int max = 10)
    {
        while (true)
        {
            try
            {
                Console.Write($"Frame: {frame} - Roll: {roll} => Enter number of pins hit (max. {max}): ");
                return Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("ERROR: number of pins hit is required!");
            }
        }
    }

    public void SetNextRoll(int roll)
    {
        throw new NotImplementedException();
    }
}