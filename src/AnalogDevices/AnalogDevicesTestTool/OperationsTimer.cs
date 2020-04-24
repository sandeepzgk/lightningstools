using System;
using System.Diagnostics;

namespace StopWatchSample
{
    class OperationsTimer
    {
        //public static void Main()
        //{
        //    DisplayTimerProperties();
        //
        //    Console.WriteLine();
        //    Console.WriteLine("Press the Enter key to begin:");
        //    Console.ReadLine();
        //    Console.WriteLine();
        //
        //    TimeOperations();
        //}

        public static void DisplayTimerProperties()
        {
            // Display the timer frequency and resolution.
            if (Stopwatch.IsHighResolution)
            {
                Console.WriteLine("Operations timed using the system's high-resolution performance counter.");
            }
            else
            {
                Console.WriteLine("Operations timed using the DateTime class.");
            }

            long frequency = Stopwatch.Frequency;
            Console.WriteLine("  Timer frequency in ticks per second = {0}",
                frequency);
            long nanosecPerTick = (1000L * 1000L * 1000L) / frequency;
            Console.WriteLine("  Timer is accurate within {0} nanoseconds",
                nanosecPerTick);
        }
        private static Stopwatch stopwatch;
        public static void startTimer()
        {
            if (stopwatch == null || stopwatch.IsRunning == false)
            {
                stopwatch = new Stopwatch();
                stopwatch.Start();
            }
            else
                Console.WriteLine("Stopwatch already running");
        }

        public static void stopTimer()
        {
            stopwatch.Stop();
            var elapsedTicks = stopwatch.ElapsedTicks;
            long frequency = Stopwatch.Frequency;
            long nanosecPerTick = (1000L * 1000L * 1000L) / frequency;

            var elapsedNanoSeconds = elapsedTicks * nanosecPerTick;

            Console.WriteLine("Elapsed Time:"+elapsedNanoSeconds );
           // Console.WriteLine("  Timer is accurate within {0} nanoseconds", nanosecPerTick);

        }
       
        
    }
}
