using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace TudoMario
{
    public class LogicController
    {
        Stopwatch stopwatch = new Stopwatch();
        int desiredMilisec = 16;
        float avg = 0;
        int cnt = 0;

        public LogicController()
        {
            if (desiredMilisec < 1)
                throw new Exception();
            stopwatch.Start();
        }

        private void Tick(object sender, EventArgs e)
        {
            Tick();
        }

        private void Tick()
        {
            float sum = avg * cnt;
            sum += stopwatch.ElapsedMilliseconds;
            cnt++;
            avg = sum / cnt;
            stopwatch.Restart();

            if (cnt % (1000 / desiredMilisec) == 0)
            {
                Debug.WriteLine("Avg tick lag: " + (avg - desiredMilisec) + " ms");
                avg = 0;
                cnt = 0;
            }
        }

        public async void Start()
        {
            // System.Diagnostics.Stopwatch
            await Task.Run(() =>
            {
                while (true)
                {
                    if (stopwatch.ElapsedMilliseconds < desiredMilisec)
                        continue;
                    Tick();
                }
            });
        }
    }
}
