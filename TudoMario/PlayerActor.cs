using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TudoMario
{
    public class PlayerActor : ActorBase
    {
        public PlayerActor(Vector2 position, Vector2 size) : base(position, size, "Player")
        {
            
        }

        protected override void PerformBehaviour()
        {
            // TODO: User-control handling
        }
    }
}
