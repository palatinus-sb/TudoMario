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

        private PlayerActor testPlayer;

        private Hud CurrentHud = new Hud();

        public UiController(MainPage mainpage, Renderer renderer)
        {
            _main = mainpage;
            _renderer = renderer;
            //_renderer.Camera = camera;

            Init();

            ShowMap();
        }

        /// <summary>
        /// Prints the text into the Hud dialogbox. Returns false if it was not possible.
        /// </summary>
        /// <param name="dialog"></param>
        public void ShowDialog(string dialog)
        {
            CurrentHud.ShowDialog(dialog);
        }

        public void RemoveDialog()
        {
            CurrentHud.RemoveDialog();
        }
        public CoreApplicationView CurrentView { get; set; }

        /// <summary>
        /// Only for UI testing;
        /// </summary>
        public void ShowMap()
        {

            testPlayer = new PlayerActor(new Vector2(0, 0), new Vector2(64, 64));
            testPlayer.SetTexture(TextureHandler.GetImageByName("playermodel2"));

            _renderer.BindCameraAtActor(testPlayer);

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
                airchunkMissingTexturetest.SetTileAt(i, 0, missing);
            }

            airChunk.FillChunkWith(air);

            mapBase.SetChunkAt(0, 0, airchunkMissingTexturetest);
            mapBase.SetChunkAt(1, 0, airChunk);

            Chunk groundChunk = new Chunk();
            groundChunk.FillChunkWith(ground);

            mapBase.SetChunkAt(0, -1, groundChunk);

            _renderer.CurrentMap = mapBase;

            ShowDialog("TEST DYNAMIC DIALOG THAT IS VERY VERY LONG TO TEST THE LENGHT OF THIS DAMN THING AND I HOPE IT WILL WORK CAUSE I AM SO DONE WITH THIS PROJECT AT FKING SATURDAY 6PM REEEEEEEEE. OK IT WORKS HF GUYS");

            // RemoveDialog();
        }

        /// <summary>
        /// Only for UI testing;
        /// </summary>
        public void Testf(string cont)
        {
            if (cont == "Left")
            {
                _renderer.MoveCameraLeft(20);
            }
            if (cont == "Right")
            {
                _renderer.MoveCameraLeft(-20);
            }
            if (cont == "Up")
            {
                _renderer.MoveCameraUp(20);
            }
            if (cont == "Down")
            {
                _renderer.MoveCameraUp(-20);
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

            _renderer.Hud = CurrentHud;

        }
    }
}


