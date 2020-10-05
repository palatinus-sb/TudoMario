using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TudoMario
{
    /// <summary>
    /// 
    /// </summary>
    public class ActorBase
    {
        /// <summary>
        /// Relative X coord to the Map.
        /// </summary>
        public float RelativeXPos { get; set; }
        public float RelativeYPos { get; set; }
        public float BaseJumpHeight { get; set; }
        public float BaseVerticalSpeed { get; set; }
        public float BaseHorizontalSpeed { get; set; }

        /// <summary>
        /// Actor healthpoints. 0 is perfectly fine, 100 is dead.
        /// </summary>
        public int StressLevel { get; set; }

        
        public int TakeDamage()
        {
            throw new NotImplementedException();
        }

        public void Attack()
        {
            throw new NotImplementedException();
        }
    }
}
