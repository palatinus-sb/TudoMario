using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TudoMario
{
    /// <summary>
    /// Represent the base of Collider fields. Used for hitboxes.
    /// </summary>
    public abstract class ColliderBase : IEquatable<ColliderBase>
    {
        private static readonly List<ColliderBase> instances = new List<ColliderBase>();

        protected ColliderBase(bool isSolid = true)
        {
            instances.Add(this);
            IsSolid = isSolid;
        }

        public event CollisionArgs CollisionStarted;
        public event CollisionArgs CollisionEnded;

        public virtual Vector2 Position { get; set; } = new Vector2();
        public virtual Vector2 Size { get; set; } = new Vector2();
        public bool IsCollisionEnabled { get; set; } = true;
        public bool IsSolid { get; protected set; } = true; // prevents actors from entering it's collision box

        public static IEnumerable<ColliderBase> GetProbeColliders(ColliderBase collider)
        {
            ProbeCollider probe = new ProbeCollider(collider);
            List<ColliderBase> colliders = probe.GetColliders().ToList();
            colliders.Remove(collider);
            instances.Remove(probe);
            return colliders;
        }

        /// <summary>
        /// Checks if two colliders are colliding.
        /// </summary>
        /// <param name="other"> The other collider. </param>
        /// <returns> Returns true if the param and this collider are colliding. </returns>
        public bool IsCollidingWith(ColliderBase other)
        {
            if (!this.IsCollisionEnabled || !other.IsCollisionEnabled || this.Equals(other))
                return false;

            float centerXDistance = Math.Abs(this.Position.X - other.Position.X);
            float actualXDistance = centerXDistance - (this.Size.X / 2 + other.Size.X / 2);
            float centerYDistance = Math.Abs(this.Position.Y - other.Position.Y);
            float actualYDistance = centerYDistance - (this.Size.Y / 2 + other.Size.Y / 2);

            return actualXDistance < 0 && actualYDistance < 0;
        }

        /// <summary>
        /// Returns the list of Colliders that collides with this class.
        /// </summary>
        public virtual IEnumerable<ColliderBase> GetColliders()
        {
            if (!IsCollisionEnabled)
                return new List<ColliderBase>();

            List<ColliderBase> CollidingColliders = new List<ColliderBase>();
            foreach (ColliderBase collider in instances)
            {
                if (IsCollidingWith(collider))
                    CollidingColliders.Add(collider);
            }

            return CollidingColliders;
        }

        public void SignalCollisionStart(ColliderBase collider) => CollisionStarted?.Invoke(this, collider);

        public void SignalCollisionEnd(ColliderBase collider) => CollisionEnded?.Invoke(this, collider);

        public bool Equals(ColliderBase other) => ReferenceEquals(this, other);

        private class ProbeCollider : ColliderBase
        {
            public ProbeCollider(ColliderBase collider)
            {
                Position = collider.Position;
                Size = collider.Size;
            }
        }
    }

    public class ColliderWithModifier : ColliderBase
    {
        public MovementModifier Modifier { get; }

        public ColliderWithModifier(MovementModifier modifier, bool isSolid) : base(isSolid)
        {
            Modifier = modifier;
        }
    }

    public delegate void CollisionArgs(ColliderBase sender, ColliderBase collider);
}
