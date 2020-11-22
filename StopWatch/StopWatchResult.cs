using System;

namespace StopWatch
{
   public class StopWatchResult
   {
      public decimal Ticks { get; set; }
      public decimal Miliseconds { get; set; }
      public TimeSpan EslapsedTime { get; set; }

      public void DisplayTime()
      {
         if(Math.Round(EslapsedTime.TotalSeconds, 0, MidpointRounding.AwayFromZero) > 0)
         {
            Console.WriteLine("Time elapsed: {0:hh\\:mm\\:ss}", EslapsedTime);
         }
         else if(Miliseconds > 0)
         {
            Console.WriteLine("Time elapsed: {0}ms", Miliseconds);
         }
         else
         {
            Console.WriteLine("Time elapsed: {0}ticks", Ticks);
         }
      }
   }
}
