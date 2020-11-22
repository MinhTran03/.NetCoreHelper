using StopWatch;
using System;

namespace ConsoleMain
{
   class Program
   {
      static void Main(string[] args)
      {
         Action action = () =>
         {
            for (int i = 0; i < 100; i++)
            {
               Console.WriteLine(i);
            }
         };

         var result = action.CalculateRunTime();
         result.DisplayTime();

         Console.ReadKey();
      }
   }
}
