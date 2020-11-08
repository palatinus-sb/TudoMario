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

        /// <summary>
        /// The current map to render at.
        /// </summary>
        private MapBase _currentMap;
        public static TextureHandler TextureHandler;


        private float ZoomLevel = 1;
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

        public MapBase CurrentMap
        {
            get
            {
                return _currentMap;
            }
            set
            {
                _currentMap = value;
            }
        }

        public Renderer(MainPage main)
        {
            TextureHandler = new TextureHandler();

            Main = main;
            this.InitializeComponent();
        }

        /// <summary>
        /// Render is the base Renderer function Render should be called each gametick for rendering the current state of the game.
        /// </summary>
        public void Render()
        {
            if (CurrentMap != null)
            {
                RenderAtCamera();
            }
            else
            {

            }
        }

        private void InitializeComponent()
        {
            MainCanvas = new Canvas();
            MainCanvas.Background = new SolidColorBrush(Windows.UI.Colors.Gray);

            MainCanvasTransform.Children.Add(MainCanvas);

            Main.MainGrid.Children.Add(MainCanvasTransform);
        }

        public void AddActorToRenderScene(ActorBase actor)
        {
            //MainCanvas.Children.Add(actor);
        }

        /// <summary>
        /// Renders the chunk at the desired position.
        /// NOT MAP COORDINATES THIS TRANSLATES TO CHUNK POSITIONS
        /// </summary>
        /// <param name="chunk"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void RenderChunkAt(Chunk chunk,int x,int y)
        {
            MainCanvas.Children.Add(chunk);
            Canvas.SetLeft(chunk,x * 512);
            Canvas.SetTop(chunk,y * 512);
        }

        /// <summary>
        /// Unrenders the given chunks for performance.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void UnrenderChunkAt(int x, int y)
        {
            //Map.getc
        }

        /// <summary>
        /// Renders the map around the given coordinates.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void RenderAround(Vector2 Position)
        {
            float x = Position.X;
            float y = Position.Y;

            //Monitor y + is down from top left corner
            y = TranslateFromYToMonitorY(y);

            var centerAtX = Main.ActualWidth/2; 
            var centerAtY = Main.ActualHeight / 2;

            Canvas.SetLeft(MainCanvas, centerAtX + x);
            Canvas.SetTop(MainCanvas, centerAtY + y);
        }

        /// <summary>
        /// Renders the scene around the camera.
        /// </summary>
        public void RenderAtCamera()
        {
            
            RenderAround(new Vector2(camera.CameraX, camera.CameraY));
        }

        /// <summary>
        /// Translates from top right coordinates to normal coordinate system Y.
        /// </summary>
        /// <param name="y"></param>
        /// <returns></returns>
        private float TranslateFromYToMonitorY(float y)
        {
            return (-1 * y);
        }
        /// <summary>
        /// Tells the actual render width in pixels
        /// </summary>
        /// <returns></returns>
        private float DetermineRenderWidth()
        {
            return 0f;
        }
        /// <summary>
        /// Returns the objects that are out of render distance therefore have to be unrendered.
        /// </summary>
        /// <returns></returns>
        private List<Panel> GetAllUnrenderableObjects()
        {
            return new List<Panel>();
        }
    }
}
