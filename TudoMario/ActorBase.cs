using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TudoMario
{
    /// <summary>
    /// 
    /// </summary>
    public class ActorBase : ICollideable
    {
        public ActorBase(Vector2 position, Vector2 size) { Position = position; Size = size; }

        /// <summary>
        /// Relative coordinates on the map.
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// The actor's speed vector.
        /// </summary>
        public Vector2 MovementSpeed { get; set; }
        /// <summary>
        /// The maximum speed vector the actor is allowed to go.
        /// </summary>
        public Vector2 SpeedLimits { get; set; }
        /// <summary>
        /// The actor's size.
        /// </summary>
        public Vector2 Size { get; set; }
        /// <summary>
        /// Actor healthpoints. 0 is perfectly fine, 100 is dead.
        /// </summary>
        public int StressLevel { get; set; }
        public bool CanMove { get; set; }
        private ColliderBase Collider { get; set; }
        
        public int TakeDamage()
        {
            throw new NotImplementedException();
        }

        public void Attack()
        {
            throw new NotImplementedException();
        }

        public void CreateCollider(ColliderBase target)
        {
            Collider = target;
        }

        /*public void UnBindCollider(ColliderBase target)
        {
            ColliderActor = null;
        }*/
    }
}
