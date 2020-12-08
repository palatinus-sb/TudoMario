using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TudoMario
{
    internal class EnemyTestActor : ActorBase
    {
        public EnemyTestActor(Vector2 position, Vector2 size) : base(position, size, "DeadlineEnemy", false)
        {

        }
        protected override void PerformBehaviour()
        {

        }
    }
}
