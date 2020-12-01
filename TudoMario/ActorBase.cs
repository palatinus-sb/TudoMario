using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TudoMario.Rendering;
using Windows.UI.Xaml.Media.Imaging;

namespace TudoMario
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class ActorBase : ColliderBase, ITextured
    {
        private static uint instances = 0;
        private BitmapImage Texture = TextureHandler.GetMissingTexture();

        /// <summary>
        /// Actor healthpoints. 0 is perfectly fine, 1000 is dead.
        /// </summary>
        public int StressLevel { get; set; }

        public readonly string id;
        public event EventHandler Died;
        private IEnumerable<ColliderBase> colliders;

        public bool IsStatic { get; set; } = false;
        public bool IsAffectedByGravity { get; set; } = true;
        public Vector2 MovementSpeed { get; set; } = new Vector2(0, 0);
        public Vector2 SpeedLimits { get; private set; } = new Vector2(10, 20);
        public HashSet<MovementModifier> MovementModifiers { get; private set; } = new HashSet<MovementModifier>();
        public int AttackDamage { get; set; } = 0;
        public bool IsAlive { get; private set; } = true;
        public bool CanJump { get; set; }

        public ActorBase(string id = "")
        {
            this.id = string.IsNullOrEmpty(id) ? $"Actor-{GetType().Name}-{instances}" : $"Actor-{id}";
            instances++;
            colliders = base.GetColliders();
        }

        public ActorBase(Vector2 position, Vector2 size, string id = "") : this(id)
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
            if (IsCollidingWith(target))
                target.ApplyDamage(AttackDamage);
        }

        /// <summary>
        /// Called every game-tick. Responsible for performing all tasks that the Actor needs to do.
        /// </summary>
        public void Tick()
        {
            // Signaling changes to Colliders and caching new Colliders
            IEnumerable<ColliderBase> newColliders = base.GetColliders();
            foreach (var collider in colliders.Except(newColliders))
                collider.SignalCollisionEnd(this);
            foreach (var collider in newColliders.Except(colliders))
                collider.SignalCollisionStart(this);
            colliders = newColliders;

            // Applying MovementModifiers
            MovementModifiers.Clear();
            foreach (var collider in colliders)
                if (collider is ColliderWithModifier cwm && cwm.Modifier != null)
                    MovementModifiers.Add(cwm.Modifier);

            // Perform type-specific Behaviour
            PerformBehaviour();
            // Move actor
            PhysicsController.ApplyPhysics(this);
        }

        /// <summary>
        /// Defines the unique behaviour realated to this type of Actor.
        /// Implement this function when creating a new type of Actor.
        /// </summary>
        protected abstract void PerformBehaviour();

        public BitmapImage GetTexture()
        {
            return Texture;
        }

        public void SetTexture(BitmapImage texture)
        {
            Texture = texture;
        }

        public override IEnumerable<ColliderBase> GetColliders() => colliders;

        public override string ToString() => id;
    }
}
