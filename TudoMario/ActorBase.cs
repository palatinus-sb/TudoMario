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
        private BitmapImage texture = TextureHandler.GetMissingTexture();
        private BitmapImage[] StandingSprites = new BitmapImage[2];
        private BitmapImage[][] MovementSprites = new BitmapImage[2][];
        private bool FacingDirection = true;
        private int TickCounter = 0;

        /// <summary>
        /// Actor healthpoints. 0 is perfectly fine, 1000 is dead.
        /// </summary>
        public int StressLevel { get; set; }
        public BitmapImage Texture
        {
            get => texture;
            set
            {
                texture = value;
                StandingSprites = new BitmapImage[] { texture, texture };
                OnTextureChanged();
            }
        }
        public bool IsVisible { get; set; } = true;

        public readonly string id;
        public event EventHandler Died;
        public event EventHandler TextureChanged;

        private IEnumerable<ColliderBase> colliders;

        public bool IsStatic { get; set; } = false;
        public bool IsAffectedByGravity { get; set; } = true;
        public Vector2 MovementSpeed { get; set; } = new Vector2(0, 0);
        public Vector2 SpeedLimits { get; private set; } = new Vector2(10, 20);
        public HashSet<MovementModifier> MovementModifiers { get; private set; } = new HashSet<MovementModifier>();
        public int AttackDamage { get; set; } = 0;
        public bool IsAlive { get; private set; } = true;
        public bool CanJump { get; set; }
        public bool HasMovementSprites { get; private set; } = false;

        public ActorBase(string id = "", bool isSolid = true) : base(isSolid)
        {
            this.id = string.IsNullOrEmpty(id) ? $"Actor-{GetType().Name}-{instances}" : $"Actor-{id}";
            instances++;
            colliders = base.GetColliders();
        }


        public ActorBase(Vector2 position, Vector2 size, string id = "", bool isSolid = true) : this(id, isSolid)
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

        public void ResetStats()
        {
            IsAlive = true;
            StressLevel = 0;
            MovementSpeed = new Vector2(0, 0);
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
            if (HasMovementSprites && TickCounter == 4)
            {
                SetMovementTexture();
                TickCounter = 0;
            }
            else
                TickCounter++;
        }

        protected void SetMovementTexture()
        {
            if (MovementSpeed.X == 0)
            {
                Texture = StandingSprites[GetFacingDirection()];
            }
            else if (MovementSpeed.X > 0)
            {
                int index = Array.FindIndex(MovementSprites[1], row => row.Equals(Texture)) + 1;
                if (index >= MovementSprites[1].Length)
                    index = 0;
                Texture = MovementSprites[1][index];
                FacingDirection = true;
            }
            else if (MovementSpeed.X < 0)
            {
                int index = Array.FindIndex(MovementSprites[0], row => row.Equals(Texture)) + 1;
                if (index >= MovementSprites[0].Length)
                    index = 0;
                Texture = MovementSprites[0][index];
                FacingDirection = false;
            }
        }

        /// <summary>
        /// Defines the unique behaviour realated to this type of Actor.
        /// Implement this function when creating a new type of Actor.
        /// </summary>
        protected abstract void PerformBehaviour();

        public override IEnumerable<ColliderBase> GetColliders() => colliders;

        public override string ToString() => id;

        public void OnTextureChanged()
        {
            TextureChanged?.Invoke(this, EventArgs.Empty);
        }

        public void AddMovingTexture(string data, int x)
        {
            HasMovementSprites = true;
            string[] datas = data.Split(",");
            BitmapImage[] sprites = new BitmapImage[datas.Length];
            for (int i = 0; i < datas.Length; i++)
                sprites[i] = TextureHandler.GetImageByName(datas[i]);
            MovementSprites[x] = sprites;
        }

        public int GetFacingDirection()
        {
            return FacingDirection ? 1 : 0;
        }
    }
}
