using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TudoMario
{
    public static class PhysicsController
    {
        public const float JumpHeight = 6.0f;

        public static Vector2 Gravity = new Vector2(0.0f, -0.7f);

        public static float Movement = 0.1f;

        private const float friction = 0.1f;
        private const float gravity = 0.5f;

        public static void ApplyFrictionOnGround(ActorBase actor) {
            throw new Exception("Obsolete method");
        }

        public static void ApplySpeedLimitOnGround(ActorBase actor) {
            throw new Exception("Obsolete method");
        }

        public static void ApplyGravity(ActorBase actor) {
            throw new Exception("Obsolete method");
        }

        /// <summary>
        /// Moves the actor based on it's speed and properties, factoring in gravity and friction.
        /// </summary>
        /// <param name="actor">the Actor to be moved</param>
        public static void ApplyPhysics(ActorBase actor)
        {
            if (actor.IsStatic)
                return;
            Vector4 speedLimits = CalculateSpeedLimit(actor);

            // apply friction
            actor.MovementSpeed.X = Math.Sign(actor.MovementSpeed.X) * (Math.Abs(actor.MovementSpeed.X) - friction);
            // apply gravity
            actor.MovementSpeed.Y = Math.Abs(actor.MovementSpeed.X) - gravity;

            // enforce speed limits
            if (Math.Sign(actor.MovementSpeed.Y) > 0)
                actor.MovementSpeed.Y = Math.Min(actor.MovementSpeed.Y, speedLimits.X); // up (+y)
            else
                actor.MovementSpeed.Y = Math.Max(actor.MovementSpeed.Y, speedLimits.Y); // down (-y)
            if (Math.Sign(actor.MovementSpeed.X) > 0)
                actor.MovementSpeed.X = Math.Min(actor.MovementSpeed.X, speedLimits.W); // right (+x)
            else
                actor.MovementSpeed.X = Math.Max(actor.MovementSpeed.X, speedLimits.Z); // left (-y)

            // move actor
            actor.Position += actor.MovementSpeed;
        }

        /// <summary>
        /// Calculates an actor's speed limits based on it's properties and modifiers.
        /// </summary>
        /// <param name="actor">the actor who's speed limits are to be calculated</param>
        /// <returns>The speed limits in Vector4(Up, Down, Left, Right) representation.</returns>
        private static Vector4 CalculateSpeedLimit(ActorBase actor)
        {
            Vector4 speedLimits = new Vector4(actor.SpeedLimits.Y, actor.SpeedLimits.Y, actor.SpeedLimits.X, actor.SpeedLimits.X);
            speedLimits = ApplyAdditiveModifiers(actor, speedLimits);
            speedLimits = ApplyMultiplicativeModifiers(actor, speedLimits);
            speedLimits = ApplyAbsoluteModifiers(actor, speedLimits);
            return speedLimits;
        }

        private static Vector4 ApplyAdditiveModifiers(ActorBase actor, Vector4 speedLimits)
        {
            var newLimits = speedLimits;
            var additives = actor.MovementModifiers.Where(m => m.Mode == Mode.Additive);
            foreach (var modifier in additives)
                newLimits = ApplyModifier(modifier, newLimits, (x, y) => x + y);
            return newLimits;
        }

        private static Vector4 ApplyMultiplicativeModifiers(ActorBase actor, Vector4 speedLimits)
        {
            var newLimits = speedLimits;
            var multiplicatives = actor.MovementModifiers.Where(m => m.Mode == Mode.Multiplicative);
            foreach (var modifier in multiplicatives)
                newLimits = ApplyModifier(modifier, newLimits, (x, y) => x * y);
            return newLimits;
        }

        private static Vector4 ApplyAbsoluteModifiers(ActorBase actor, Vector4 speedLimits)
        {
            var newLimits = speedLimits;
            var absolutes = actor.MovementModifiers.Where(m => m.Mode == Mode.Absolute);
            foreach (var modifier in absolutes)
                newLimits = ApplyModifier(modifier, newLimits, (x, y) => y);
            return newLimits;
        }

        /// <summary>
        /// Applies a MovementModifier to a Vector4 represented speed limit set. Vector4(Up, Down, Left, Right)
        /// </summary>
        /// <param name="modifier">The modifier to apply</param>
        /// <param name="speedLimits">The speed limits to modify</param>
        /// <param name="func">The arithmetic operation to perform. Param 1 is base-value, param 2 is the modifier-value.</param>
        /// <returns>The new set of speed limits in the same Vector4 representation.</returns>
        private static Vector4 ApplyModifier(MovementModifier modifier, Vector4 speedLimits, Func<float, float, float> func)
        {
            float up = speedLimits.X;
            if ((modifier.Direction & Direction.Up) != 0)
                up = func(speedLimits.X, modifier.Value);

            float down = speedLimits.Y;
            if ((modifier.Direction & Direction.Down) != 0)
                down = func(speedLimits.Y, modifier.Value);

            float left = speedLimits.Z;
            if ((modifier.Direction & Direction.Left) != 0)
                left = func(speedLimits.Z, modifier.Value);

            float right = speedLimits.W;
            if ((modifier.Direction & Direction.Right) != 0)
                right = func(speedLimits.W, modifier.Value);

            return new Vector4(up, down, left, right);
        }
    }
}
