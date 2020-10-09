using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TudoMario
{
    class PhysicsController
    {
        /// <summary>
        /// The friction on ground.
        /// </summary>
        public const float FrictionOnGround = 1.0f;

        /// <summary>
        /// The friction on ice.
        /// </summary>
        public const float FrictionOnIce = 1.5f;

        /// <summary>
        /// The friction in the air.
        /// </summary>
        public const float FrictionInAir = 0.7f;

        /// <summary>
        /// The friction in the swamp.
        /// </summary>
        public const float FrictionInSwamp = 0.5f;

        /// <summary>
        /// Actor's maximum speed on ground.
        /// </summary>
        public const float MaxSpeedMultiplierOnGround = 1.0f;

        /// <summary>
        /// Actor's maximum speed on ice.
        /// </summary>
        public const float MaxSpeedMultiplierOnIce = 2.0f;

        /// <summary>
        /// Actor's maximum speed in air.
        /// </summary>
        public const float MaxSpeedMultiplierInAir = 0.7f;

        /// <summary>
        /// Actor's maximum speed in swamp.
        /// </summary>
        public const float MaxSpeedMultiplierInSwamp = 0.8f;

        /// <summary>
        /// Maximum movement speed in the game.
        /// </summary>
        public const float MaxMovementSpeed = 5.0f;

        /// <summary>
        /// Maximum jump height in the game.
        /// </summary>
        public const float JumpHeight = 6.0f;

        /// <summary>
        /// Declares the gravity.
        /// </summary>
        public Vector2 Gravity = new Vector2(0.0f, -0.7f);

        /// <summary>
        /// Apply the ground friction.
        /// </summary>
        public void ApplyFrictionOnGround(ActorBase actor) {
            if (actor.MovementSpeed.X == 0.0f) actor.MovementSpeed = new Vector2(0.0f, actor.MovementSpeed.Y);
            else if (actor.MovementSpeed.X > 0.0f) actor.MovementSpeed = new Vector2((actor.MovementSpeed.X + FrictionOnGround), actor.MovementSpeed.Y);
            else if (actor.MovementSpeed.X < 0.0f) actor.MovementSpeed = new Vector2((actor.MovementSpeed.X - FrictionOnGround), actor.MovementSpeed.Y);
        }

        /// <summary>
        /// Apply the ice friction.
        /// </summary>
        public void ApplyFrictionOnIce(ActorBase actor) {
            if (actor.MovementSpeed.X == 0.0f) actor.MovementSpeed = new Vector2(0.0f, actor.MovementSpeed.Y);
            else if (actor.MovementSpeed.X > 0.0f) actor.MovementSpeed = new Vector2((actor.MovementSpeed.X + FrictionOnIce), actor.MovementSpeed.Y);
            else if (actor.MovementSpeed.X < 0.0f) actor.MovementSpeed = new Vector2((actor.MovementSpeed.X - FrictionOnIce), actor.MovementSpeed.Y);
        }

        /// <summary>
        /// Apply the air friction.
        /// </summary>
        public void ApplyFrictionInAir(ActorBase actor) {
            if (actor.MovementSpeed.X == 0.0f) actor.MovementSpeed = new Vector2(0.0f, actor.MovementSpeed.Y);
            else if (actor.MovementSpeed.X > 0.0f) actor.MovementSpeed = new Vector2((actor.MovementSpeed.X + FrictionInAir), actor.MovementSpeed.Y);
            else if (actor.MovementSpeed.X < 0.0f) actor.MovementSpeed = new Vector2((actor.MovementSpeed.X - FrictionInAir), actor.MovementSpeed.Y);
        }

        /// <summary>
        /// Apply the swamp friction.
        /// </summary>
        public void ApplyFrictionInSwamp(ActorBase actor) {
            if (actor.MovementSpeed.X == 0.0f) actor.MovementSpeed = new Vector2(0.0f, actor.MovementSpeed.Y);
            else if (actor.MovementSpeed.X > 0.0f) actor.MovementSpeed = new Vector2((actor.MovementSpeed.X + FrictionInSwamp), actor.MovementSpeed.Y);
            else if (actor.MovementSpeed.X < 0.0f) actor.MovementSpeed = new Vector2((actor.MovementSpeed.X - FrictionInSwamp), actor.MovementSpeed.Y);
        }

        /// <summary>
        /// Apply the actor's maximum movement speeds.
        /// </summary>
        private void ApplySpeedLimits(ActorBase actor, float NewSpeedLimit) {
            if (actor.MovementSpeed.X > NewSpeedLimit) actor.MovementSpeed = new Vector2(NewSpeedLimit, actor.MovementSpeed.Y);
            else if (actor.MovementSpeed.X < -NewSpeedLimit) actor.MovementSpeed = new Vector2(-NewSpeedLimit, actor.MovementSpeed.Y);
            if (actor.MovementSpeed.Y > actor.SpeedLimits.Y) actor.MovementSpeed = new Vector2(actor.MovementSpeed.Y, actor.SpeedLimits.Y);
            else if (actor.MovementSpeed.X < -actor.SpeedLimits.X) actor.MovementSpeed = new Vector2(actor.MovementSpeed.X, -actor.SpeedLimits.Y);
        }

        /// <summary>
        /// Apply the actor's maximum movement speed on ground.
        /// </summary>
        public void ApplySpeedLimitOnGround(ActorBase actor) {
            float NewSpeedLimit = actor.SpeedLimits.X * MaxSpeedMultiplierOnGround;
            ApplySpeedLimits(actor, NewSpeedLimit);
        }

        /// <summary>
        /// Apply the actor's maximum movement speed on ice.
        /// </summary>
        public void ApplySpeedLimitOnIce(ActorBase actor) {
            float NewSpeedLimit = actor.SpeedLimits.X * MaxSpeedMultiplierOnIce;
            ApplySpeedLimits(actor, NewSpeedLimit);
        }

        /// <summary>
        /// Apply the actor's maximum movement speed in air.
        /// </summary>
        public void ApplySpeedLimitInAir(ActorBase actor) {
            float NewSpeedLimit = actor.SpeedLimits.X * MaxSpeedMultiplierInAir;
            ApplySpeedLimits(actor, NewSpeedLimit);
        }

        /// <summary>
        /// Apply the actor's maximum movement speed in swamp.
        /// </summary>
        public void ApplySpeedLimitInSwamp(ActorBase actor) {
            float NewSpeedLimit = actor.SpeedLimits.X * MaxSpeedMultiplierInSwamp;
            ApplySpeedLimits(actor, NewSpeedLimit);
        }

        /// <summary>
        /// Apply the gravity on the actor.
        /// </summary>
        public void ApplyGravity(ActorBase actor) {
            if (actor.MovementSpeed.Y > -MaxMovementSpeed) actor.MovementSpeed = new Vector2(Gravity.X, (actor.MovementSpeed.Y + Gravity.Y));
        }
    }
}
