using System;
using System.Numerics;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TudoMario;

namespace TudoMarioTests
{
    [TestClass]
    public class PhysicsControllerTests
    {
        private readonly Type physicsReflection = typeof(PhysicsController);

        [TestMethod]
        public void TestApplyModifier_IceWalk()
        {
            MovementModifier modifier = MovementModifier.IceWalk;
            Vector4 speedLimits = new Vector4(1f, 1f, 1f, 1f);
            MethodInfo applyModifier = physicsReflection.GetMethod("ApplyModifier", BindingFlags.NonPublic | BindingFlags.Static);
            Vector4 result = (Vector4)applyModifier.Invoke(null, new object[] { modifier, speedLimits });

            Assert.AreEqual(speedLimits.X, result.X);
            Assert.AreEqual(speedLimits.Y, result.Y);
            Assert.AreEqual(modifier.Function(speedLimits.Z, modifier.Value), result.Z);
            Assert.AreEqual(modifier.Function(speedLimits.W, modifier.Value), result.W);
        }

        [TestMethod]
        public void TestApplyModifier_SwampWalk()
        {
            MovementModifier modifier = MovementModifier.SwampWalk;
            Vector4 speedLimits = new Vector4(1f, 1f, 1f, 1f);
            MethodInfo applyModifier = physicsReflection.GetMethod("ApplyModifier", BindingFlags.NonPublic | BindingFlags.Static);
            Vector4 result = (Vector4)applyModifier.Invoke(null, new object[] { modifier, speedLimits });

            Assert.AreEqual(speedLimits.X, result.X);
            Assert.AreEqual(modifier.Function(speedLimits.Y, modifier.Value), result.Y);
            Assert.AreEqual(modifier.Function(speedLimits.Z, modifier.Value), result.Z);
            Assert.AreEqual(modifier.Function(speedLimits.W, modifier.Value), result.W);
        }

        [TestMethod]
        public void TestApplyMultiplicativeModifiers()
        {
            DummyActor actor = new DummyActor();
            actor.MovementModifiers.Add(MovementModifier.IceWalk);
            Vector4 speedLimits = new Vector4(actor.SpeedLimits.Y, actor.SpeedLimits.Y, actor.SpeedLimits.X, actor.SpeedLimits.X);
            MethodInfo applyMultiplicative = physicsReflection.GetMethod("ApplyMultiplicativeModifiers", BindingFlags.NonPublic | BindingFlags.Static);
            Vector4 result = (Vector4)applyMultiplicative.Invoke(null, new object[] { actor, speedLimits });

            Assert.AreEqual(speedLimits.X, result.X);
            Assert.AreEqual(speedLimits.Y, result.Y);
            Assert.AreEqual(MovementModifier.IceWalk.Function(speedLimits.Z, MovementModifier.IceWalk.Value), result.Z);
            Assert.AreEqual(MovementModifier.IceWalk.Function(speedLimits.W, MovementModifier.IceWalk.Value), result.W);
        }

        [TestMethod]
        public void TestCalculateSpeedLimit()
        {
            DummyActor actor = new DummyActor();
            actor.IsCollisionEnabled = false;
            actor.MovementModifiers.Add(MovementModifier.IceWalk);
            actor.MovementModifiers.Add(MovementModifier.JumpBoost);
            MethodInfo applyMultiplicative = physicsReflection.GetMethod("CalculateSpeedLimit", BindingFlags.NonPublic | BindingFlags.Static);
            Vector4 result = (Vector4)applyMultiplicative.Invoke(null, new object[] { actor });

            Assert.AreEqual(MovementModifier.JumpBoost.Function(actor.SpeedLimits.Y, MovementModifier.JumpBoost.Value), result.X); // modified by: JumpBoost
            Assert.AreEqual(actor.SpeedLimits.Y, result.Y); // modified by: none
            Assert.AreEqual(MovementModifier.IceWalk.Function(actor.SpeedLimits.X, MovementModifier.IceWalk.Value), result.W); // modified by: IceWalk
        }

        [TestMethod]
        public void TestApplyPhysics_StaticActor()
        {
            DummyActor actor = new DummyActor() { IsStatic = true };
            var initialPosition = actor.Position;
            actor.MovementSpeed = new TudoMario.Vector2(1f, -5f);
            PhysicsController.ApplyPhysics(actor);

            Assert.AreEqual(initialPosition, actor.Position);
        }

        [TestMethod]
        public void TestApplyPhysics_ActorNotAffectedByGravity()
        {
            DummyActor actor = new DummyActor() { IsAffectedByGravity = false };
            var initialPosition = actor.Position;
            PhysicsController.ApplyPhysics(actor);

            Assert.AreEqual(initialPosition, actor.Position);
        }

        [TestMethod]
        public void TestApplyPhysics_Friction()
        {
            DummyActor actor = new DummyActor() { IsAffectedByGravity = false };

            actor.MovementSpeed.X = 1f;
            var initialSpeed = actor.MovementSpeed.X;
            PhysicsController.ApplyPhysics(actor);
            Assert.IsTrue(Math.Abs(actor.MovementSpeed.X) < Math.Abs(initialSpeed));

            actor.MovementSpeed.X = -1f;
            initialSpeed = actor.MovementSpeed.X;
            PhysicsController.ApplyPhysics(actor);
            Assert.IsTrue(Math.Abs(actor.MovementSpeed.X) < Math.Abs(initialSpeed));
        }

        [TestMethod]
        public void TestApplyPhysics_Gravity()
        {
            DummyActor actor = new DummyActor();

            actor.MovementSpeed.Y = 1f;
            var initialSpeed = actor.MovementSpeed.Y;
            PhysicsController.ApplyPhysics(actor);
            Assert.IsTrue(actor.MovementSpeed.Y < initialSpeed);

            actor.MovementSpeed.Y = -1f;
            initialSpeed = actor.MovementSpeed.Y;
            PhysicsController.ApplyPhysics(actor);
            Assert.IsTrue(actor.MovementSpeed.Y < initialSpeed);
        }
    }
}
