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
        public CameraObject() { }
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

        public void BindActor(ActorBase target)
        {
            Target = target;
            Position = target.Position;
        }
        public void UnbindActor()
        {
            Target = null;
        }
    }
}
