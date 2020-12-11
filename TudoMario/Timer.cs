using System;
using System.Diagnostics;
using System.Threading;

namespace TudoMario
{
    internal class Timer
    {
        private readonly Stopwatch stopwatch;
        private readonly int desiredMiliseconds;
        private bool run;

        public event EventHandler Tick;

        /// <summary>
        /// Creates a new timer with the desired interal.
        /// </summary>
        /// <param name="interval">The interval in miliseconds.</param>
        public Timer(int interval)
        {
            if (interval < 1)
                throw new ArgumentException("Timer interval cannot be less than 1.");
            stopwatch = new Stopwatch();
            desiredMiliseconds = interval;
            run = false;
        }

        /// <summary>
        /// Starts the timer.
        /// </summary>
        public void Start()
        {
            if (run)
                return;
            run = true;
            stopwatch.Start();
            Run();
        }

        /// <summary>
        /// Stops the timer.
        /// </summary>
        public void Stop()
        {
            run = false;
        }

        /// <summary>
        /// This method will start the ticking mechanic which will run on it's own;
        /// </summary>
        private void Run()
        {
            new Thread(() =>
            {
                while (run)
                {
                    if (stopwatch.ElapsedMilliseconds < desiredMiliseconds)
                        continue;
                    Tick.Invoke(this, EventArgs.Empty);
                    stopwatch.Restart();
                }
                stopwatch.Stop();
                stopwatch.Reset();
            }).Start();
        }
    }
}
