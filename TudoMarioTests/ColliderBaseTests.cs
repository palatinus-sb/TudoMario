using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TudoMario;

namespace TudoMarioTests
{
    [TestClass]
    public class ColliderBaseTests
    {
        //  ╔═══╦═══╦═══╗  ┬  3
        //  ║ 1 ║ 2 ║ 3 ║  ┤
        //  ╠═══╬═══╬═══╣  ┤
        //  ║ 4 ║ 5 ║ 6 ║  ┼  0
        //  ╠═══╬═══╬═══╣  ┤
        //  ║ 7 ║ 8 ║ 9 ║  ┤
        //  ╚═══╩═══╩═══╝  ┴ -3

        private DummyActor actor123 = new DummyActor(new Vector2(0, 2), new Vector2(6, 2));
        private DummyActor actor14 = new DummyActor(new Vector2(-2, 1), new Vector2(2, 4));
        private DummyActor actor258 = new DummyActor(new Vector2(0, 0), new Vector2(2, 6));
        private DummyActor actor6 = new DummyActor(new Vector2(2, 0), new Vector2(2, 2));
        private DummyActor actor89 = new DummyActor(new Vector2(1, -2), new Vector2(4, 2));

        private bool eventRaised = false;

        [TestInitialize]
        public void Setup()
        {
            ColliderBase.ClearAllColliders();
            actor123 = new DummyActor(new Vector2(0, 2), new Vector2(6, 2));
            actor14 = new DummyActor(new Vector2(-2, 1), new Vector2(2, 4));
            actor258 = new DummyActor(new Vector2(0, 0), new Vector2(2, 6));
            actor6 = new DummyActor(new Vector2(2, 0), new Vector2(2, 2));
            actor89 = new DummyActor(new Vector2(1, -2), new Vector2(4, 2));
        }

        [TestMethod]
        public void TestIsCollidingWith()
        {
            // self-collision
            Assert.IsFalse(actor123.IsCollidingWith(actor123));
            Assert.IsFalse(actor6.IsCollidingWith(actor6));

            // should collide
            Assert.IsTrue(actor123.IsCollidingWith(actor14));
            Assert.IsTrue(actor123.IsCollidingWith(actor258));
            Assert.IsTrue(actor258.IsCollidingWith(actor89));

            // should not collide
            Assert.IsFalse(actor123.IsCollidingWith(actor89));
            Assert.IsFalse(actor123.IsCollidingWith(actor6));
            Assert.IsFalse(actor14.IsCollidingWith(actor6));
            Assert.IsFalse(actor14.IsCollidingWith(actor258));
            Assert.IsFalse(actor14.IsCollidingWith(actor89));
            Assert.IsFalse(actor258.IsCollidingWith(actor6));
            Assert.IsFalse(actor6.IsCollidingWith(actor89));
        }

        [TestMethod]
        public void TestIsCollidingWith_IsCollisionEnabledDisabled()
        {
            Assert.IsTrue(actor123.IsCollidingWith(actor14)); // should collide
            actor123.IsCollisionEnabled = false;
            Assert.IsFalse(actor123.IsCollidingWith(actor14)); // should not collide because actor123's collision is disabled
            actor123.IsCollisionEnabled = true;
        }

        [TestMethod]
        public void TestGetColliders()
        {
            actor123.Tick(); // refresh cached value
            var colliders123 = actor123.GetColliders();
            Assert.IsTrue(colliders123.Contains(actor14));
            Assert.IsTrue(colliders123.Contains(actor258));
            Assert.IsFalse(colliders123.Contains(actor6));
            Assert.IsFalse(colliders123.Contains(actor89));
            Assert.IsFalse(colliders123.Contains(actor123));
        }

        [TestMethod]
        public void TestCollisionStartedEvent()
        {
            eventRaised = false;
            List<DummyActor> dummies = new List<DummyActor>();
            dummies.Add(new DummyActor(new Vector2(0, 0), new Vector2(2, 2)) { IsStatic = true });
            dummies.Add(new DummyActor(new Vector2(0, 3), new Vector2(2, 2)) { IsStatic = true });
            dummies[0].CollisionStarted += Actor1_CollisionStarted;
            dummies.ForEach(d => d.Tick());
            Assert.IsFalse(eventRaised);
            dummies[1].Position = new Vector2(0, 0); // move to same positions
            dummies.ForEach(d => d.Tick());
            Assert.IsTrue(eventRaised);
        }

        private void Actor1_CollisionStarted(ColliderBase sender, ColliderBase collider)
        {
            eventRaised = true;
        }

        [TestMethod]
        public void TestCollisionEndedEvent()
        {
            eventRaised = false;
            List<DummyActor> dummies = new List<DummyActor>();
            dummies.Add(new DummyActor(new Vector2(0, 0), new Vector2(2, 2)) { IsStatic = true });
            dummies.Add(new DummyActor(new Vector2(0, 0), new Vector2(2, 2)) { IsStatic = true });
            dummies[0].CollisionEnded += ColliderBaseTests_CollisionEnded;
            dummies.ForEach(d => d.Tick());
            Assert.IsFalse(eventRaised);
            dummies[1].Position = new Vector2(0, 3); // move to different positions
            dummies.ForEach(d => d.Tick());
            Assert.IsTrue(eventRaised);
        }

        private void ColliderBaseTests_CollisionEnded(ColliderBase sender, ColliderBase collider)
        {
            eventRaised = true;
        }
    }
}
