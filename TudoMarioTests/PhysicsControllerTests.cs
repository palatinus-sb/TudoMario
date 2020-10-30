/*using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TudoMario;

namespace TudoMarioTests
{
    [TestClass]
    public class PhysicsControllerTests
    {
        [TestMethod]
        public void TestGravity()
        {
            var ActualGravity = 5;
            ActorBase actor = new ActorBase();
            actor.MovementSpeed = new Vector2(0, ActualGravity);
            var expected = ActualGravity + PhysicsController.Gravity.Y;
            PhysicsController.ApplyGravity(actor);
            Assert.AreEqual(actor.MovementSpeed.Y, expected);
        }

        [DataRow(0, 0)]
        [DataRow(1, 1)]
        [DataRow(5, 5)]
        [DataRow(100, 5)]
        [DataRow(-1, -1)]
        [DataRow(-5, -5)]
        [DataRow(-100, -5)]
        [DataTestMethod]
        public void TestHorizontalSpeedLimits(float number, float result)
        {
            var ActualSpeedLimit = 5;
            ActorBase actor = new ActorBase();
            actor.MovementSpeed = new Vector2(number, 0);
            actor.SpeedLimits = new Vector2(ActualSpeedLimit, 0);
            PhysicsController.ApplySpeedLimits(actor, ActualSpeedLimit);
            Assert.AreEqual(result, actor.MovementSpeed.X);
        }

        [DataRow(0, 0)]
        [DataRow(1, 1)]
        [DataRow(3, 3)]
        [DataRow(100, 3)]
        [DataRow(-1, -1)]
        [DataRow(-3, -3)]
        [DataRow(-100, -3)]
        [DataTestMethod]
        public void TestVerticalSpeedLimits(float number, float result)
        {
            var ActualSpeedLimit = 3;
            ActorBase actor = new ActorBase();
            actor.MovementSpeed = new Vector2(0, number);
            actor.SpeedLimits = new Vector2(0, ActualSpeedLimit);
            PhysicsController.ApplySpeedLimits(actor, ActualSpeedLimit);
            Assert.AreEqual(result, actor.MovementSpeed.Y);
        }

        [DataRow(0, 0)]
        [DataRow(1, 1)]
        [DataRow(3, 3)]
        [DataRow(100, 3)]
        [DataRow(-1, -1)]
        [DataRow(-3, -3)]
        [DataRow(-100, -3)]
        [DataTestMethod]
        public void TestSpeedLimitOnGround(float number, float result)
        {
            var ActualSpeedLimit = 3;
            ActorBase actor = new ActorBase();
            actor.MovementSpeed = new Vector2(number, 0);
            actor.SpeedLimits = new Vector2(ActualSpeedLimit, 0);
            PhysicsController.ApplySpeedLimitOnGround(actor);
            Assert.AreEqual(result, actor.MovementSpeed.X);
        }

        [DataRow(0, 0)]
        [DataRow(1, 1)]
        [DataRow(100, 6)]
        [DataRow(-1, -1)]
        [DataRow(-100, -6)]
        [DataTestMethod]
        public void TestSpeedLimitOnIce(float number, float result)
        {
            var ActualSpeedLimit = 3;
            ActorBase actor = new ActorBase();
            actor.MovementSpeed = new Vector2(number, 0);
            actor.SpeedLimits = new Vector2(ActualSpeedLimit, 0);
            PhysicsController.ApplySpeedLimitOnIce(actor);
            Assert.AreEqual(result, actor.MovementSpeed.X);
        }

        [DataRow(0, 0)]
        [DataRow(1, 1)]
        [DataRow(100, 2.1f)]
        [DataRow(-1, -1)]
        [DataRow(-100, -2.1f)]
        [DataTestMethod]
        public void TestSpeedLimitInAir(float number, float result)
        {
            var ActualSpeedLimit = 3;
            ActorBase actor = new ActorBase();
            actor.MovementSpeed = new Vector2(number, 0);
            actor.SpeedLimits = new Vector2(ActualSpeedLimit, 0);
            PhysicsController.ApplySpeedLimitInAir(actor);
            Assert.AreEqual(result, actor.MovementSpeed.X);
        }

        [DataRow(0, 0)]
        [DataRow(1, 1)]
        [DataRow(100, 2.4f)]
        [DataRow(-1, -1)]
        [DataRow(-100, -2.4f)]
        [DataTestMethod]
        public void TestSpeedLimitInSwamp(float number, float result)
        {
            var ActualSpeedLimit = 3;
            ActorBase actor = new ActorBase();
            actor.MovementSpeed = new Vector2(number, 0);
            actor.SpeedLimits = new Vector2(ActualSpeedLimit, 0);
            PhysicsController.ApplySpeedLimitInSwamp(actor);
            Assert.AreEqual(result, actor.MovementSpeed.X);
        }

        [DataRow(0, 0)]
        [DataRow(1, 2)]
        [DataRow(-1, -2)]
        [DataRow(5, 3)]
        [DataRow(-5, -3)]
        [DataTestMethod]
        public void TestFrictionOnGround(float number, float result) 
        {
            var ActualSpeedLimit = 3;
            ActorBase actor = new ActorBase();
            actor.MovementSpeed = new Vector2(number, 0);
            actor.SpeedLimits = new Vector2(ActualSpeedLimit, 0);
            PhysicsController.ApplyFrictionOnGround(actor);
            PhysicsController.ApplySpeedLimitOnGround(actor);
            Assert.AreEqual(actor.MovementSpeed.X, result);
        }

        [DataRow(0, 0)]
        [DataRow(1, 2.5f)]
        [DataRow(-1, -2.5f)]
        [DataRow(5, 6)]
        [DataRow(-5, -6)]
        [DataTestMethod]
        public void TestFrictionOnIce(float number, float result)
        {
            var ActualSpeedLimit = 3;
            ActorBase actor = new ActorBase();
            actor.MovementSpeed = new Vector2(number, 0);
            actor.SpeedLimits = new Vector2(ActualSpeedLimit, 0);
            PhysicsController.ApplyFrictionOnIce(actor);
            PhysicsController.ApplySpeedLimitOnIce(actor);
            Assert.AreEqual(actor.MovementSpeed.X, result);
        }

        [DataRow(0, 0)]
        [DataRow(1, 1.7f)]
        [DataRow(-1, -1.7f)]
        [DataRow(5, 2.1f)]
        [DataRow(-5, -2.1f)]
        [DataTestMethod]
        public void TestFrictionInAir(float number, float result)
        {
            var ActualSpeedLimit = 3;
            ActorBase actor = new ActorBase();
            actor.MovementSpeed = new Vector2(number, 0);
            actor.SpeedLimits = new Vector2(ActualSpeedLimit, 0);
            PhysicsController.ApplyFrictionInAir(actor);
            PhysicsController.ApplySpeedLimitInAir(actor);
            Assert.AreEqual(actor.MovementSpeed.X, result);
        }

        [DataRow(0, 0)]
        [DataRow(1, 1.5f)]
        [DataRow(-1, -1.5f)]
        [DataRow(5, 2.4f)]
        [DataRow(-5, -2.4f)]
        [DataTestMethod]
        public void TestFrictionInSwamp(float number, float result)
        {
            var ActualSpeedLimit = 3;
            ActorBase actor = new ActorBase();
            actor.MovementSpeed = new Vector2(number, 0);
            actor.SpeedLimits = new Vector2(ActualSpeedLimit, 0);
            PhysicsController.ApplyFrictionInSwamp(actor);
            PhysicsController.ApplySpeedLimitInSwamp(actor);
            Assert.AreEqual(actor.MovementSpeed.X, result);
        }
    }
}*/
