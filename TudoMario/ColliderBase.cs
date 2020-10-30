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

        public virtual Vector2 Position { get; set; } = new Vector2();
        public virtual Vector2 Size { get; set; } = new Vector2();
        public ICollideable Parent { get; private set; }
        public bool IsActive { get; set; }

        public ColliderBase(ICollideable parent) { Parent = parent; }
        /*public ColliderBase(float CoordinateX, float CoordinateY, float Width, float Height)
        {
            Position.X = CoordinateX;
            Position.Y = CoordinateY;
            Size.X = Width;
            Size.Y = Height;
        }*/

        /// <summary>
        /// Checks if two colliders are colliding.
        /// </summary>
        /// <param name="other"> The other collider. </param>
        /// <returns> Returns true if the param and this collider are colliding. </returns>
        public bool IsCollidingWith(ColliderBase other)
        {
            double ThisRight = this.Position.X + this.Size.X / 2;
            double ThisTop = this.Position.Y + this.Size.Y / 2;
            double ThisLeft = this.Position.X - this.Size.X / 2;
            double ThisBot = this.Position.Y - this.Size.Y / 2;
            double OtherRight = other.Position.X + other.Size.X / 2;
            double OtherTop = other.Position.Y + other.Size.Y / 2;
            double OtherLeft = other.Position.X - other.Size.X / 2;
            double OtherBot = other.Position.Y - other.Size.Y / 2;

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
            if (OtherLeft >= ThisLeft && OtherRight <= ThisRight && OtherBot >= ThisBot && OtherTop <= ThisTop)
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
        
        // These methods were made for testing but maybe they will be useful later
        public void AddToColliders(ColliderBase item)
        {
            colliders.Add(item);
        }
        public bool CollidersContains(ColliderBase item)
        {
            return colliders.Contains(item);
        }
    }
}
