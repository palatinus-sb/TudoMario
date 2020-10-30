using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TudoMario;

namespace TudoMarioTests
{
    [TestClass]
    public class ColliderBaseTests
    {
        [TestMethod]
        public void TestIsCollidingWith()
        {
            ActorBase a = new PlayerActor(new Vector2(10, 10), new Vector2(6, 4));
            ColliderBase center = new ActorCollider(a);
            ActorBase b = new PlayerActor(new Vector2(12.5f, 12.5f), new Vector2(1, 1));
            ColliderBase one = new ActorCollider(b);
            ActorBase c = new PlayerActor(new Vector2(17.5f, 17.5f), new Vector2(5, 5));
            ColliderBase two = new ActorCollider(c);
            ActorBase d = new PlayerActor(new Vector2(10.5f, 17.5f), new Vector2(5, 3));
            ColliderBase three = new ActorCollider(d);
            ActorBase e = new PlayerActor(new Vector2(6.5f, 13), new Vector2(1, 2));
            ColliderBase four = new ActorCollider(e);
            ActorBase f = new PlayerActor(new Vector2(3, 16), new Vector2(2, 2));
            ColliderBase five = new ActorCollider(f);
            ActorBase g = new PlayerActor(new Vector2(3, 9), new Vector2(2, 4));
            ColliderBase six = new ActorCollider(g);
            ActorBase h = new PlayerActor(new Vector2(6, 6.5f), new Vector2(2, 3));
            ColliderBase seven = new ActorCollider(h);
            ActorBase i = new PlayerActor(new Vector2(2, 2), new Vector2(2, 2));
            ColliderBase eight = new ActorCollider(i);
            ActorBase j = new PlayerActor(new Vector2(10, 3.5f), new Vector2(2, 3));
            ColliderBase nine = new ActorCollider(j);
            ActorBase k = new PlayerActor(new Vector2(13, 7), new Vector2(2, 2));
            ColliderBase ten = new ActorCollider(k);
            ActorBase l = new PlayerActor(new Vector2(16.5f, 2.5f), new Vector2(1, 3));
            ColliderBase eleven = new ActorCollider(l);
            ActorBase m = new PlayerActor(new Vector2(19, 8.5f), new Vector2(2, 3));
            ColliderBase twelve = new ActorCollider(m);
            ActorBase n = new PlayerActor(new Vector2(9.5f, 10), new Vector2(3, 2));
            ColliderBase thirteen = new ActorCollider(n);
            
            Assert.IsTrue(center.IsCollidingWith(one));
            Assert.IsTrue(center.IsCollidingWith(four));
            Assert.IsTrue(center.IsCollidingWith(seven));
            Assert.IsTrue(center.IsCollidingWith(ten));
            Assert.IsTrue(center.IsCollidingWith(thirteen));
            Assert.IsTrue(center.IsCollidingWith(center));
            Assert.IsFalse(center.IsCollidingWith(two));
            Assert.IsFalse(center.IsCollidingWith(three));
            Assert.IsFalse(center.IsCollidingWith(five));
            Assert.IsFalse(center.IsCollidingWith(six));
            Assert.IsFalse(center.IsCollidingWith(eight));
            Assert.IsFalse(center.IsCollidingWith(nine));
            Assert.IsFalse(center.IsCollidingWith(eleven));
            Assert.IsFalse(center.IsCollidingWith(twelve));
            
        }
        [TestMethod]
        public void TestGetColliders()
        {
            ActorBase a = new PlayerActor(new Vector2(10, 10), new Vector2(6, 4));
            ColliderBase center = new ActorCollider(a);
            ActorBase b = new PlayerActor(new Vector2(12.5f, 12.5f), new Vector2(1, 1));
            ColliderBase one = new ActorCollider(b);
            ActorBase c = new PlayerActor(new Vector2(17.5f, 17.5f), new Vector2(5, 5));
            ColliderBase two = new ActorCollider(c);
            ActorBase d = new PlayerActor(new Vector2(10.5f, 17.5f), new Vector2(5, 3));
            ColliderBase three = new ActorCollider(d);
            ActorBase e = new PlayerActor(new Vector2(6.5f, 13), new Vector2(1, 2));
            ColliderBase four = new ActorCollider(e);
            ActorBase f = new PlayerActor(new Vector2(3, 16), new Vector2(2, 2));
            ColliderBase five = new ActorCollider(f);
            ActorBase g = new PlayerActor(new Vector2(3, 9), new Vector2(2, 4));
            ColliderBase six = new ActorCollider(g);
            ActorBase h = new PlayerActor(new Vector2(6, 6.5f), new Vector2(2, 3));
            ColliderBase seven = new ActorCollider(h);
            ActorBase i = new PlayerActor(new Vector2(2, 2), new Vector2(2, 2));
            ColliderBase eight = new ActorCollider(i);
            ActorBase j = new PlayerActor(new Vector2(10, 3.5f), new Vector2(2, 3));
            ColliderBase nine = new ActorCollider(j);
            ActorBase k = new PlayerActor(new Vector2(13, 7), new Vector2(2, 2));
            ColliderBase ten = new ActorCollider(k);
            ActorBase l = new PlayerActor(new Vector2(16.5f, 2.5f), new Vector2(1, 3));
            ColliderBase eleven = new ActorCollider(l);
            ActorBase m = new PlayerActor(new Vector2(19, 8.5f), new Vector2(2, 3));
            ColliderBase twelve = new ActorCollider(m);
            ActorBase n = new PlayerActor(new Vector2(9.5f, 10), new Vector2(3, 2));
            ColliderBase thirteen = new ActorCollider(n);

            center.AddToColliders(one);
            center.AddToColliders(two);
            center.AddToColliders(three);
            center.AddToColliders(four);
            center.AddToColliders(five);
            center.AddToColliders(six);
            center.AddToColliders(seven);
            center.AddToColliders(eight);
            center.AddToColliders(nine);
            center.AddToColliders(ten);
            center.AddToColliders(eleven);
            center.AddToColliders(twelve);
            center.AddToColliders(thirteen);

            center.IsActive = true;
            one.IsActive = true;
            two.IsActive = true;
            three.IsActive = true;
            four.IsActive = true;
            five.IsActive = true;
            six.IsActive = true;
            seven.IsActive = true;
            eight.IsActive = true;
            nine.IsActive = true;
            ten.IsActive = true;
            eleven.IsActive = true;
            twelve.IsActive = true;
            thirteen.IsActive = true;

            //Test with all colliders active
            Assert.IsTrue(center.GetColliders().Contains(one));
            Assert.IsFalse(center.GetColliders().Contains(two));
            Assert.IsFalse(center.GetColliders().Contains(three));
            Assert.IsTrue(center.GetColliders().Contains(four));
            Assert.IsFalse(center.GetColliders().Contains(five));
            Assert.IsFalse(center.GetColliders().Contains(six));
            Assert.IsTrue(center.GetColliders().Contains(seven));
            Assert.IsFalse(center.GetColliders().Contains(eight));
            Assert.IsFalse(center.GetColliders().Contains(nine));
            Assert.IsTrue(center.GetColliders().Contains(ten));
            Assert.IsFalse(center.GetColliders().Contains(eleven));
            Assert.IsFalse(center.GetColliders().Contains(twelve));
            Assert.IsTrue(center.GetColliders().Contains(thirteen));

            one.IsActive = false;
            two.IsActive = false;
            three.IsActive = false;
            four.IsActive = false;
            five.IsActive = false;
            six.IsActive = false;
            seven.IsActive = false;
            eight.IsActive = false;
            nine.IsActive = false;
            ten.IsActive = false;
            eleven.IsActive = false;
            twelve.IsActive = false;
            thirteen.IsActive = false;

            //Test with all colliders not active
            Assert.IsFalse(center.GetColliders().Contains(one));
            Assert.IsFalse(center.GetColliders().Contains(two));
            Assert.IsFalse(center.GetColliders().Contains(three));
            Assert.IsFalse(center.GetColliders().Contains(four));
            Assert.IsFalse(center.GetColliders().Contains(five));
            Assert.IsFalse(center.GetColliders().Contains(six));
            Assert.IsFalse(center.GetColliders().Contains(seven));
            Assert.IsFalse(center.GetColliders().Contains(eight));
            Assert.IsFalse(center.GetColliders().Contains(nine));
            Assert.IsFalse(center.GetColliders().Contains(ten));
            Assert.IsFalse(center.GetColliders().Contains(eleven));
            Assert.IsFalse(center.GetColliders().Contains(twelve));
            Assert.IsFalse(center.GetColliders().Contains(thirteen));

            center.IsActive = false;
            one.IsActive = true;
            two.IsActive = true;
            three.IsActive = true;
            four.IsActive = true;
            five.IsActive = true;
            six.IsActive = true;
            seven.IsActive = true;
            eight.IsActive = true;
            nine.IsActive = true;
            ten.IsActive = true;
            eleven.IsActive = true;
            twelve.IsActive = true;
            thirteen.IsActive = true;

            //Test with all colliders active but center is not active
            Assert.IsFalse(center.GetColliders().Contains(one));
            Assert.IsFalse(center.GetColliders().Contains(two));
            Assert.IsFalse(center.GetColliders().Contains(three));
            Assert.IsFalse(center.GetColliders().Contains(four));
            Assert.IsFalse(center.GetColliders().Contains(five));
            Assert.IsFalse(center.GetColliders().Contains(six));
            Assert.IsFalse(center.GetColliders().Contains(seven));
            Assert.IsFalse(center.GetColliders().Contains(eight));
            Assert.IsFalse(center.GetColliders().Contains(nine));
            Assert.IsFalse(center.GetColliders().Contains(ten));
            Assert.IsFalse(center.GetColliders().Contains(eleven));
            Assert.IsFalse(center.GetColliders().Contains(twelve));
            Assert.IsFalse(center.GetColliders().Contains(thirteen));
        }
    }
}
