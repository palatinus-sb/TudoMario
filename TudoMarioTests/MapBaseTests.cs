using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TudoMario.Rendering;
using TudoMario;
using TudoMario.Map;

namespace TudoMarioTests
{
    [TestClass]
    public class MapBaseTests
    {
        [TestMethod]
        public void SetChunkAtZero()
        {
            MapBase mb = new MapBase(new Vector2(0, 0));
            Chunk cu = new Chunk();
            cu.FillChunkWith(typeof(Tile), @"ms-appx:/Assets//BaseBackGroung.png");
            mb.AddChunkAt(cu,0, 0);

            Assert.AreEqual(cu, mb.GetChunkAt(0,0));
        }

        [TestMethod]
        public void SetChunkFarFromZeroNothingBetween()
        {
            MapBase mb = new MapBase(new Vector2(0, 0));
            Chunk cu = new Chunk();
            cu.FillChunkWith(typeof(Tile), @"ms-appx:/Assets//BaseBackGroung.png");
            mb.AddChunkAt(cu, 20, 5);

            Assert.AreEqual(cu, mb.GetChunkAt(20, 5));
        }
        [TestMethod]
        public void StartingPointSetupAtConstructor()
        {
            MapBase mb = new MapBase(new Vector2(250, 458));
            var sp = mb.StartingPoint;
            Assert.AreEqual(sp.X, 250);
            Assert.AreEqual(sp.Y, 458);
        }
    }
}