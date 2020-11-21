using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TudoMario.Map;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using TudoMario.Rendering;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Xaml.Media.Imaging;

namespace TudoMario.Ui
{
    internal class UiController
    {
        private MainPage _main;
        private Renderer _renderer;
        private CameraObject camera;

        private PlayerActor testPlayer;

        private Canvas DefaultHud = new Canvas();

        public UiController(MainPage mainpage, Renderer renderer)
        {
            camera = new CameraObject();

            _main = mainpage;
            _renderer = renderer;
            _renderer.Camera = camera;

            Init();

            ShowMap();
        }

        public void ShowHud()
        {
            _renderer.ShowHud();
        }
        public void HideHud()
        {

        }

        public CoreApplicationView CurrentView { get; set; }

        /// <summary>
        /// Only for UI testing;
        /// </summary>
        public void ShowMap()
        {

            testPlayer = new PlayerActor(new Vector2(0, 0), new Vector2(64, 64));
            testPlayer.SetTexture(TextureHandler.GetImageByName("playermodel2"));

            camera.BindActor(testPlayer);

            MapBase mapBase = new MapBase(new Vector2(0, 0));
            Chunk airchunkMissingTexturetest = new Chunk();
            Chunk airChunk = new Chunk();

            BitmapImage ground = TextureHandler.GetImageByName("GroundBase");
            BitmapImage air = TextureHandler.GetImageByName("BaseBackGroung");

            BitmapImage missing = TextureHandler.GetImageByName("kekekekek");

            airchunkMissingTexturetest.FillChunkWith(air);

            mapBase.AddActor(testPlayer);

            for (int i = 0; i < 16; i++)
            {
                airchunkMissingTexturetest.SetTileAt(0, i, missing);
            }

            airChunk.FillChunkWith(air);

            mapBase.AddChunkAt(airchunkMissingTexturetest, 0, 0);
            mapBase.AddChunkAt(airChunk, 1, 0);

            Chunk groundChunk = new Chunk();
            groundChunk.FillChunkWith(ground);

            mapBase.AddChunkAt(groundChunk, 0, -1);

            _renderer.CurrentMap = mapBase;
        }

        /// <summary>
        /// Only for UI testing;
        /// </summary>
        public void Testf(string cont)
        {
            if (cont == "Left")
            {
                camera.CameraX -= 20;
            }
            if (cont == "Right")
            {
                camera.CameraX += 20;
            }
            if (cont == "Up")
            {
                camera.CameraY = camera.CameraY + 20;
            }
            if (cont == "Down")
            {
                camera.CameraY = camera.CameraY - 20;
            }

            if (cont == "pUp")
            {
                testPlayer.Position.Y += 10f;
            }
            if (cont == "pDown")
            {
                testPlayer.Position.Y -= 10f;
            }
            if (cont == "pLeft")
            {
                testPlayer.Position.X -= 10f;
            }
            if (cont == "pRight")
            {
                testPlayer.Position.X += 10f;
            }
        }

        /// <summary>
        /// Initialise Ui HUD.
        /// </summary>
        private void Init()
        {
            Button testb = new Button();
            testb.Content = "asdsada";
            DefaultHud.Children.Add(testb);
        }
    }
}


