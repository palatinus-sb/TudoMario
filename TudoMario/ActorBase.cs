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
    public class ActorBase : ColliderBase
    {
        private static uint instances = 0;

        /// <summary>
        /// Actor healthpoints. 0 is perfectly fine, 1000 is dead.
        /// </summary>
        public int StressLevel { get; set; }

        public readonly string id;
        public event EventHandler Died;
        private IEnumerable<ColliderBase> colliders;

        public bool CanMove { get; set; }
        public Vector2 MovementSpeed { get; set; } = new Vector2(0, 0);
        public Vector2 SpeedLimits { get; private set; } = new Vector2(5, 5);
        public HashSet<MovementModifier> MovementModifiers { get; private set; } = new HashSet<MovementModifier>();
        public int AttackDamage { get; set; } = 0;
        public bool IsAlive { get; private set; } = true;

        public ActorBase(string id = "")
        {
            if (string.IsNullOrEmpty(id))
                this.id = $"Actor-{GetType().Name}-{instances}";
            instances++;
        }

        public ActorBase(Vector2 position, Vector2 size) : this()
        {
            Position = position;
            Size = size;
        }
        
        /// <summary>
        /// Applies damage (increases stress) to this Actor.
        /// </summary>
        /// <param name="amount">the amount of stress</param>
        public void ApplyDamage(int amount)
        {
            StressLevel += amount;
            if (StressLevel >= 1000)
            {
                Died?.Invoke(this, EventArgs.Empty);
                IsAlive = false;
            }
        }

        /// <summary>
        /// Deals damage (increases stress) to an Actor.
        /// </summary>
        /// <param name="target">the target of the attack</param>
        public void Attack(ActorBase target)
        {
            if (GetColliders().ToList().Contains(target))
                target.ApplyDamage(AttackDamage);
        }

        /// <summary>
        /// Called every game-tick. Responsible for performing all tasks that the Actor needs to do.
        /// </summary>
        public void Tick()
        {
            // caching Colliders
            colliders = base.GetColliders();

            // applying MovementModifiers
            foreach (var collider in colliders)
            {
                if (collider is ColliderWithModifiers cwm)
                {
                    MovementModifiers.Clear();
                    foreach (var modifier in cwm.Modifiers)
                        MovementModifiers.Add(modifier);
                }
            }

            // Perform type-specific Behaviour
            PerformBehaviour();
        }

        /// <summary>
        /// Defines the unique behaviour realated to this type of Actor.
        /// Implement this function when creating a new type of Actor.
        /// </summary>
        protected virtual void PerformBehaviour() { }

        public override IEnumerable<ColliderBase> GetColliders()
        {
            return colliders;
        }

        public override string ToString() => id;
    }
}
