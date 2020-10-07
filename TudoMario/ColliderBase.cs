using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TudoMario
{
    /// <summary>
    /// Represent the base of Collider fields. Used for hitboxes.
    /// </summary>
    class ColliderBase
    {
        private static readonly List<ColliderBase> colliders = new List<ColliderBase>();

        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }

        public bool IsCollidingWith(ColliderBase other)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ColliderBase> GetColliders()
        {
            // TODO: logic
            return new List<ColliderBase>();
        }
    }
}
