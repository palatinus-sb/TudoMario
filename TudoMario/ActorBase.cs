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
    public class ActorBase : ColliderBase, ITextured
    {
        private static uint instances = 0;
        private BitmapImage Texture = TextureHandler.GetMissingTexture();

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

        public readonly string id;
        public event EventHandler Died;
        public bool CanMove { get; set; }
        public Vector2 MovementSpeed { get; set; } = new Vector2(0, 0);
        public Vector2 SpeedLimits { get; set; } = new Vector2(0, 0);

        /// <summary>
        /// Actor healthpoints. 0 is perfectly fine, 1000 is dead.
        /// </summary>
        public int StressLevel { get; set; }
        public int AttackDamage { get; set; }
        
        public void ApplyDamage(int amount)
        {
            StressLevel += amount;
            if (StressLevel >= 1000)
                Died.Invoke(this, EventArgs.Empty);
        }

        public void Attack(ActorBase target)
        {
            if (GetColliders().ToList().Contains(target))
                target.ApplyDamage(AttackDamage);
        }

        public override string ToString()
        {
            return id;
        }

        public BitmapImage GetTexture()
        {
            return Texture;
        }

        public void SetTexture(BitmapImage texture)
        {
            Texture = texture;
        }
    }
}
