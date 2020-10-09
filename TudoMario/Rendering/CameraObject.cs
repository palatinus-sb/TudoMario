using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TudoMario.Rendering
{
    class CameraObject
    {
        private ActorBase Target;
        private Vector2 Position { get; set; }
        public CameraObject() { Position = new Vector2(0, 0); }
        public CameraObject(ActorBase target) { Target = target; }

        public float CameraX 
        { 
            get
            {
                if (Target != null)
                {
                    Position.X = Target.Position.X;
                }
                return Position.X;
            }
            set
            {
                if (Target == null)
                {
                    Position.X = value;
                }
            }
        }
        public float CameraY
        {
            get
            {
                if (Target != null)
                {
                    Position.Y = Target.Position.Y;
                }
                return Position.Y;
            }
            set
            {
                if (Target == null)
                {
                    Position.Y = value;
                }
            }
        }

        /// <summary>
        /// Binds the actor to the camera. The camera will render around the target each Render() tick.
        /// </summary>
        /// <param name="target"></param>
        public void BindActor(ActorBase target)
        {
            Target = target;
            Position = target.Position;
        }
        /// <summary>
        /// Unbinds the actor and lets the camera stay in position.
        /// </summary>
        public void UnbindActor()
        {
            Target = null;
        }
    }
}
