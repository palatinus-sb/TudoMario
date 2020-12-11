using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TudoMario.AiActors
{
    internal sealed class DumbEnemy : ActorBase
    {
        private int stationaryTickCount = 0;
        private int direction = 1;
        private float lastPostionX = 0;
        public bool canMove = true;

        public DumbEnemy(Vector2 position, Vector2 size, string id = "") : base(position, size, id, false) { }

        protected override void PerformBehaviour()
        {
            if (canMove)
            {
                MovementSpeed.X += direction * PhysicsController.friction * 1.25f;

                if (lastPostionX == Position.X)
                    stationaryTickCount++;
                else
                    stationaryTickCount = 0;

                if (stationaryTickCount == 3)
                {
                    MovementSpeed.Y = 12.5f;
                    CanJump = false;
                }
                else if (stationaryTickCount > 9)
                {
                    stationaryTickCount = 0;
                    direction = -1 * direction;
                }
                lastPostionX = Position.X;
            }
        }
    }
}
