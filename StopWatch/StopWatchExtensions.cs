using System;
using System.Diagnostics;

namespace StopWatch
{
   public static class StopWatchExtensions
   {
      public static StopWatchResult CalculateRunTime(this Action actionToCaculate, short loopTimes = 1)
      {
         var result = new StopWatchResult();

         var stopWatch = new Stopwatch();
         for (int time = 0; time < loopTimes; time++)
         {
            stopWatch.Start();
            actionToCaculate();
            stopWatch.Stop();

            result.Ticks += stopWatch.ElapsedTicks;
            result.EslapsedTime += stopWatch.Elapsed;
            result.Miliseconds += stopWatch.ElapsedMilliseconds;
         }

         return result;
      }
   }
}
