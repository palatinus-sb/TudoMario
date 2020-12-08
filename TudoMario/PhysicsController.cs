using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TudoMario
{
    public static class PhysicsController
    {
        public const float friction = 2f;
        public const float gravity = 2.5f;

        /// <summary>
        /// Moves the actor based on it's speed and properties, factoring in gravity and friction.
        /// </summary>
        /// <param name="actor">the Actor to be moved</param>
        public static void ApplyPhysics(ActorBase actor)
        {
            if (actor.IsStatic)
                return;

            // apply friction
            actor.MovementSpeed.X = Math.Sign(actor.MovementSpeed.X) * Math.Max(Math.Abs(actor.MovementSpeed.X) - friction, 0f);

            // apply gravity
            if (actor.IsAffectedByGravity)
                actor.MovementSpeed.Y -= gravity;

            // enforce speed limits
            Vector4 speedLimits = CalculateSpeedLimit(actor);
            if (Math.Sign(actor.MovementSpeed.X) >= 0)
                actor.MovementSpeed.X = 1 * Math.Min(Math.Abs(actor.MovementSpeed.X), speedLimits.W); // right (+x)
            else
                actor.MovementSpeed.X = -1 * Math.Min(Math.Abs(actor.MovementSpeed.X), speedLimits.Z); // left (-x)
            if (Math.Sign(actor.MovementSpeed.Y) >= 0)
                actor.MovementSpeed.Y = 1 * Math.Min(Math.Abs(actor.MovementSpeed.Y), speedLimits.X); // up (+y)
            else
                actor.MovementSpeed.Y = -1 * Math.Min(Math.Abs(actor.MovementSpeed.Y), speedLimits.Y); // down (-y)

            // move actor
            {
                Probe probe = new Probe(actor);
                probe.Position.X += actor.MovementSpeed.X;
                IEnumerable<ColliderBase> colliders = probe.GetColliders().Where(c => c.IsSolid);
                if (colliders.Count() > 0)
                {
                    probe.Position.X -= actor.MovementSpeed.X;
                    ColliderBase closest = GetClosestCollider(probe, colliders, DistanceX);
                    if (closest is null)
                        actor.Position.X += actor.MovementSpeed.X;
                    else
                    {
                        actor.Position.X += Math.Sign(actor.MovementSpeed.X) * (DistanceX(actor, closest) - 1);
                        actor.MovementSpeed.X = 0;
                    }
                }
            }
            {
                Probe probe = new Probe(actor);
                probe.Position.Y += actor.MovementSpeed.Y;
                IEnumerable<ColliderBase> colliders = probe.GetColliders().Where(c => c.IsSolid);
                if (colliders.Count() > 0)
                {
                    probe.Position.Y -= actor.MovementSpeed.Y;
                    ColliderBase closest = GetClosestCollider(probe, colliders, DistanceY);
                    if (closest is null)
                        actor.Position.Y += actor.MovementSpeed.Y;
                    else
                    {
                        actor.Position.Y += Math.Sign(actor.MovementSpeed.Y) * (DistanceY(actor, closest) - 1);
                        actor.MovementSpeed.Y = 0;
                        actor.CanJump = true;
                    }
                }
            }
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
                newLimits = ApplyModifier(modifier, newLimits);
            return newLimits;
        }

        private static Vector4 ApplyMultiplicativeModifiers(ActorBase actor, Vector4 speedLimits)
        {
            var newLimits = speedLimits;
            var multiplicatives = actor.MovementModifiers.Where(m => m.Mode == Mode.Multiplicative);
            foreach (var modifier in multiplicatives)
                newLimits = ApplyModifier(modifier, newLimits);
            return newLimits;
        }

        private static Vector4 ApplyAbsoluteModifiers(ActorBase actor, Vector4 speedLimits)
        {
            var newLimits = speedLimits;
            var absolutes = actor.MovementModifiers.Where(m => m.Mode == Mode.Absolute);
            foreach (var modifier in absolutes)
                newLimits = ApplyModifier(modifier, newLimits);
            return newLimits;
        }

        /// <summary>
        /// Applies a MovementModifier to a Vector4 represented speed limit set. Vector4(Up, Down, Left, Right)
        /// </summary>
        /// <param name="modifier">The modifier to apply</param>
        /// <param name="speedLimits">The speed limits to modify</param>
        /// <returns>The new set of speed limits in the same Vector4 representation.</returns>
        private static Vector4 ApplyModifier(MovementModifier modifier, Vector4 speedLimits)
        {
            float up = speedLimits.X;
            if ((modifier.Direction & Direction.Up) != 0)
                up = modifier.Function(speedLimits.X, modifier.Value);

            float down = speedLimits.Y;
            if ((modifier.Direction & Direction.Down) != 0)
                down = modifier.Function(speedLimits.Y, modifier.Value);

            float left = speedLimits.Z;
            if ((modifier.Direction & Direction.Left) != 0)
                left = modifier.Function(speedLimits.Z, modifier.Value);

            float right = speedLimits.W;
            if ((modifier.Direction & Direction.Right) != 0)
                right = modifier.Function(speedLimits.W, modifier.Value);

            return new Vector4(up, down, left, right);
        }

        private static float DistanceX(ColliderBase first, ColliderBase second)
        {
            return Math.Abs(first.Position.X - second.Position.X) - (first.Size.X + second.Size.X) / 2;
        }

        private static float DistanceY(ColliderBase first, ColliderBase second)
        {
            return Math.Abs(first.Position.Y - second.Position.Y) - (first.Size.Y + second.Size.Y) / 2;
        }

        private static ColliderBase GetClosestCollider(ColliderBase collider, IEnumerable<ColliderBase> colliders, Func<ColliderBase, ColliderBase, float> distance)
        {
            if (colliders.Count() == 0)
                return null;

            ColliderBase closest = colliders.First();
            float min = distance(collider, closest);
            foreach (var other in colliders)
            {
                float dist = distance(collider, other);
                if (dist < min)
                {
                    closest = other;
                    min = dist;
                }
            }
            return closest;
        }
    }
}
