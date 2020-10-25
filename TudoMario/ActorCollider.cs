using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TudoMario
{
    class ActorCollider : ColliderBase, ICollideable
    {
        public ActorCollider(ICollideable parent) : base(parent) { }
        public override Vector2 Position { get => ((ActorBase)Parent).Position; }
    }
}
