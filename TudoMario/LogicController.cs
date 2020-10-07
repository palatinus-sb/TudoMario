using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace TudoMario
{
    public class LogicController
    {
        Timer testtimer = new Timer();

        public LogicController()
        {
            testtimer.Interval = 16;
            testtimer.Elapsed += Tick;
            testtimer.Start();
        }

        private void Tick(object sender, EventArgs e)
        {
            Debug.WriteLine(DateTime.Now.Millisecond);
        }
    }
}
