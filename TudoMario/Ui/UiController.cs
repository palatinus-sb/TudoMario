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
    class UiController
    {
        MainPage _main;
        Renderer _renderer;
        CameraObject camera;
        public UiController(MainPage mainpage, Renderer renderer)
        {
            camera = new CameraObject();

            _main = mainpage;
            _renderer = renderer;
            _renderer.Camera = camera;

            ShowMap();
        }

        public CoreApplicationView CurrentView { get; set; }

        /// <summary>
        /// Only for UI testing;
        /// </summary>
        public void ShowMap()
        {
            PlayerActor testPlayer = new PlayerActor(new Vector2(0, 0), new Vector2(0, 0));

            MapBase mapBase = new MapBase(new Vector2(0,0));
            Chunk testchunk = new Chunk();
            Chunk testchunk2 = new Chunk();

            BitmapImage ground = _renderer.TextureHandler.GetImageByName("GroundBase");
            BitmapImage air = _renderer.TextureHandler.GetImageByName("BaseBackGroung");

            BitmapImage missing = _renderer.TextureHandler.GetImageByName("kekekekek");

            testchunk.FillChunkWith(air);

            for (int i = 0; i < 16; i++)
            {
                testchunk.SetTileAt(0, i, missing);
            }

            testchunk2.FillChunkWith(air);

            mapBase.AddChunkAt(testchunk, 0, 0);
            mapBase.AddChunkAt(testchunk2, 1, 0);

            _renderer.CurrentMap = mapBase;

            _renderer.RenderChunkAt(testchunk, 0, 0);
            _renderer.RenderChunkAt(testchunk2, 1, 0);

            Chunk testchunk3 = new Chunk();
            testchunk3.FillChunkWith(ground);

            _renderer.RenderChunkAt(testchunk3, -1, 0);
            _renderer.RenderAround(new Vector2(0, 0));
            _renderer.Render();
            
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

            //_renderer.RenderAtCamera();
        }

    }
}
    

