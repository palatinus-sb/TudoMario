using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TudoMario.Map;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace TudoMario.Rendering
{
    class Renderer : Page
    {
        MainPage Main;
        /// <summary>
        /// Main canvas contains all the chunks can be used to set renderdistance.
        /// </summary>
        Canvas MainCanvas;
        /// <summary>
        /// This element is STRICTLY used to make/allow transforms on the main canvas.
        /// </summary>
        Canvas MainCanvasTransform = new Canvas();

        CameraObject camera;
        public CameraObject Camera {
            get
            {
                return camera;
            }
            set
            {
                camera = value;
            } 
        }

        public Renderer(MainPage main)
        {
            Main = main;
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            MainCanvas = new Canvas();
            MainCanvas.Background = new SolidColorBrush(Windows.UI.Colors.Gray);

            MainCanvasTransform.Children.Add(MainCanvas);

            Main.MainGrid.Children.Add(MainCanvasTransform);
        }

        public void RenderChunkAt(Chunk chunk,int x,int y)
        {
            MainCanvas.Children.Add(chunk);
            Canvas.SetLeft(chunk,x * 512);
            Canvas.SetTop(chunk,y * 512);
        }

        public void RenderAround(float x,float y)
        {
            //Monitor y + is down from top left corner
            y = TranslateFromYToMonitorY(y);

            var centerAtX = Main.ActualWidth/2; 
            var centerAtY = Main.ActualHeight / 2;

            Canvas.SetLeft(MainCanvas, centerAtX + x);
            Canvas.SetTop(MainCanvas, centerAtY + y);
        }

        public void RenderAtCamera()
        {
            
            RenderAround(camera.CameraX, camera.CameraY);
        }

        private float TranslateFromYToMonitorY(float y)
        {
            return (-1 * y);
        }
    }
}
