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
    public class ColliderBase
    {
        private static readonly List<ColliderBase> colliders = new List<ColliderBase>();

        public virtual Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public ICollideable Parent { get; private set; }
        public bool IsActive { get; set; }

        public ColliderBase(ICollideable parent) { Parent = parent; }

        /// <summary>
        /// Checks if two colliders are colliding.
        /// </summary>
        /// <param name="other"> The other collider. </param>
        /// <returns> Returns true if the param and this collider are colliding. </returns>
        public bool IsCollidingWith(ColliderBase other)
        {
            double ThisRight = this.Position.X + this.Position.X / 2;
            double ThisTop = this.Position.Y + this.Position.Y / 2;
            double ThisLeft = this.Position.X - this.Position.X / 2;
            double ThisBot = this.Position.Y - this.Position.Y / 2;
            double OtherRight = other.Position.X + other.Position.X / 2;
            double OtherTop = other.Position.Y + other.Position.Y / 2;
            double OtherLeft = other.Position.X - other.Position.X / 2;
            double OtherBot = other.Position.Y - other.Position.Y / 2;

            if (OtherLeft <= ThisRight && OtherTop >= ThisBot && OtherRight >= ThisRight && OtherBot <= ThisBot)
            {
                return true;
            }
            if (OtherLeft <= ThisRight && OtherBot <= ThisTop && OtherRight >= ThisRight && OtherTop >= ThisTop)
            {
                return true;
            }
            if (OtherRight >= ThisLeft && OtherBot <= ThisTop && OtherLeft <= ThisLeft && OtherTop >= ThisTop)
            {
                return true;
            }
            if (OtherRight >= ThisLeft && OtherTop >= ThisBot && OtherLeft <= ThisLeft && OtherBot <= ThisBot)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns the list of Colliders that collides with this class.
        /// </summary>
        public IEnumerable<ColliderBase> GetColliders()
        {
            List<ColliderBase> CollidingColliders = new List<ColliderBase>();
            if (this.IsActive)
            {
                foreach (ColliderBase collider in colliders)
                {
                    if (collider.IsActive)
                    {
                        if (this.IsCollidingWith(collider))
                        {
                            CollidingColliders.Add(collider);
                        }
                    }
                }
            }
            return CollidingColliders;
        }
    }
}
