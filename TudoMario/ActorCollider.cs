using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TudoMario
{
    public class ActorCollider : ColliderBase, ICollideable
    {
        public ActorCollider(ICollideable parent) : base(parent) { }
        //public ActorCollider(float CoordinateX, float CoordinateY, float Width, float Height) : base(CoordinateX, CoordinateY, Width, Height) { }
        public override Vector2 Position { get => ((ActorBase)Parent).Position; }
        public override Vector2 Size { get => ((ActorBase)Parent).Size; }
    }
}
