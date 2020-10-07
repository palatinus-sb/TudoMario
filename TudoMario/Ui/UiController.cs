using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TudoMario.Map;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using TudoMario.Rendering;

namespace TudoMario.Ui
{
    class UiController
    {
        MainPage _main;
        Renderer _renderer;
        CameraObject camera;
        public UiController(MainPage mainpage,Renderer renderer)
        {
            camera = new CameraObject();

            _main = mainpage;
            _renderer = renderer;
            _renderer.Camera = camera;
            ShowMap();
        }
        public void ShowMap()
        {
            MapBase mapBase = new MapBase();
            Chunk testchunk = new Chunk();
            Chunk testchunk2 = new Chunk();

            testchunk.FillChunkWith(typeof(Tile), @"ms-appx:/Assets//BaseBackGroung.png");

            for (int i = 0; i < 16; i++)
            {
                testchunk.SetTileAt(0, i, typeof(Tile), @"ms-appx:/Assets//GroundBase.png");
            }

            testchunk2.FillChunkWith(typeof(Tile), @"ms-appx:/Assets//BaseBackGroung.png");

            mapBase.AddChunkAt(testchunk, 0, 0);
            mapBase.AddChunkAt(testchunk2, 1, 0);

            _renderer.RenderChunkAt(testchunk, 0, 0);
            _renderer.RenderChunkAt(testchunk2, 1, 0);

            Chunk testchunk3 = new Chunk();
            testchunk3.FillChunkWith(typeof(Tile), @"ms-appx:/Assets//GroundBase.png");

            _renderer.RenderChunkAt(testchunk3, -1, 0);
        }

        public void Testf(string cont)
        {
            if (cont == "Left")
            {
                camera.CameraX-= 20;
            }
            if (cont == "Right")
            {
                camera.CameraX+=20;
            }
            if (cont == "Up")
            {
                camera.CameraY = camera.CameraY + 20;
            }
            if (cont == "Down")
            {
                camera.CameraY = camera.CameraY -20;
            }

            _renderer.RenderAtCamera();
        }


    }
}
