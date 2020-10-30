using System;
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

        ActorBase actor123 = null;
        ActorBase actor14 = null;
        ActorBase actor258 = null;
        ActorBase actor6 = null;
        ActorBase actor89 = null;

        [TestInitialize]
        public void BeforEach()
        {
            actor123 = new ActorBase(new Vector2(0, 2), new Vector2(6, 2));
            actor14 = new ActorBase(new Vector2(-2, 1), new Vector2(2, 4));
            actor258 = new ActorBase(new Vector2(0, 0), new Vector2(2, 6));
            actor6 = new ActorBase(new Vector2(2, 0), new Vector2(2, 2));
            actor89 = new ActorBase(new Vector2(1, -2), new Vector2(4, 2));
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
        }

        [TestMethod]
        public void TestGetColliders()
        {
            var colliders123 = actor123.GetColliders();
            Assert.IsTrue(colliders123.Contains(actor14));
            Assert.IsTrue(colliders123.Contains(actor258));
            Assert.IsFalse(colliders123.Contains(actor6));
            Assert.IsFalse(colliders123.Contains(actor89));
            Assert.IsFalse(colliders123.Contains(actor123));

            var colliders6 = actor6.GetColliders();
            foreach (var item in colliders6)
            {
                Console.WriteLine(item);
            }
            Assert.AreEqual(0, colliders6.Count());
        }
    }
}
