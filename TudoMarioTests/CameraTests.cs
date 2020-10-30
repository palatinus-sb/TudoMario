using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TudoMario.Rendering;
using TudoMario;

namespace TudoMarioTests
{
    [TestClass]
    public class CameraTests
    {
        [TestMethod]
        public void CameraSetPosition()
        {
            CameraObject CO = new CameraObject();
            CO.CameraX = 15;
            CO.CameraY = 200;
            Assert.AreEqual(15, CO.CameraX);
            Assert.AreEqual(200, CO.CameraY);
        }

        [TestMethod]
        public void CameraPositionIsEqualWithBindedActor()
        {
            PlayerActor player = new PlayerActor(new Vector2(200, 100), new Vector2(32, 32));
            CameraObject CO = new CameraObject(player);
            Assert.AreEqual(200, CO.CameraX);
            Assert.AreEqual(100, CO.CameraY);
        }

        [TestMethod]
        public void CameraPositionCannotMoveIfBindedToActor()
        {
            PlayerActor player = new PlayerActor(new Vector2(200, 100), new Vector2(32, 32));
            CameraObject CO = new CameraObject(player);
            CO.CameraX = 1000;
            CO.CameraY = 0;

            Assert.AreEqual(200, CO.CameraX);
            Assert.AreEqual(100, CO.CameraY);
        }

        [TestMethod]
        public void FreeCameraDynamicBind()
        {
            PlayerActor player = new PlayerActor(new Vector2(200, 100), new Vector2(32, 32));
            CameraObject CO = new CameraObject();

            CO.CameraX = 2;
            CO.CameraY = 4;

            CO.BindActor(player);

            Assert.AreEqual(200, CO.CameraX);
            Assert.AreEqual(100, CO.CameraY);
        }

        [TestMethod]
        public void BindedCameraUnbindAndMove()
        {
            PlayerActor player = new PlayerActor(new Vector2(200, 100), new Vector2(32, 32));
            CameraObject CO = new CameraObject(player);

            CO.UnbindActor();

            CO.CameraX = 10;
            CO.CameraY = 20;

            Assert.AreEqual(10, CO.CameraX);
            Assert.AreEqual(20, CO.CameraY);
        }
    }
}
