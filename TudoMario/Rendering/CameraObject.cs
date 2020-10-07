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
        private float positionX = 0;
        private float positionY = 0;
        public CameraObject() { }
        public CameraObject(ActorBase target) { Target = target; }

        public float CameraX 
        { 
            get
            {
                return positionX;
            }
            set
            {
                positionX = value;
            }
        }
        public float CameraY
        {
            get
            {
                return positionY;
            }
            set
            {
                positionY = value;
            }
        }

        public void BindActor(ActorBase target)
        {

        }
        public void UnbindActor()
        {

        }
    }
}
