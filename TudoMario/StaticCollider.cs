using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TudoMario
{
    public class StaticCollider : ColliderBase
    {
        public StaticCollider(Vector2 size, Vector2 pos, bool isSolid = false) : base(isSolid)
        {
            Size = size;
            Position = pos;
        }
    }
}
