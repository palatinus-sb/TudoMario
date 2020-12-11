using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TudoMario.AiActors
{
    internal class DeadlineEnemy : ActorBase
    {
        private bool move = true;

        public DeadlineEnemy(Vector2 position)
        {
            Position = position;
            Size = new Vector2(64, 1024);
            IsStatic = true;
        }

        protected override void PerformBehaviour()
        {
            if (move)
                Position.X += 8f;
        }

        public void StopMovement()
        {
            move = false;
        }
    }
}
