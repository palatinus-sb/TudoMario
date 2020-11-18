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

        private List<ActorRender> ActorRenderAll = new List<ActorRender>();
        private List<ActorRender> ActorRenderActive = new List<ActorRender>();

        /// <summary>
        /// Represents the rendered chunks which are in view range.
        /// </summary>
        private List<Tuple<Chunk, int, int>> ChunkRenderActive = new List<Tuple<Chunk, int, int>>();

        private static float ChunkSize = 512;


        //private float ZoomLevel = 1;
        public CameraObject Camera
        {
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
                CleanRendererCanvas();
                InitActorRenderBindingList();
            }
        }

        public Renderer(MainPage main)
        {
            TextureHandler.Init();

            Main = main;
            this.InitializeComponent();
        }

        /// <summary>
        /// Render is the base Renderer function Render should be called each gametick for rendering the current state of the game.
        /// </summary>
        public void Render()
        {
            if (_currentMap != null)
            {
                RenderAtCamera();
            }
            else
            {

            }
        }

        private void InitializeComponent()
        {
            /// MAINPAGE->MainGrid->MainCanvasTransform->MainCanvas(contains chunks)->Chunks->Tiles

            //Contains all the chunks
            MainCanvas = new Canvas();
            // MainCanvasTransform.Background = new SolidColorBrush(Windows.UI.Colors.LightBlue);

            //A parent canvas to make map transforms(camera movement) easier
            MainCanvasTransform.Children.Add(MainCanvas);

            //At the actual xaml main page bind the drawn scene
            Main.MainGrid.Children.Add(MainCanvasTransform);

        }

        /// <summary>
        /// Renders the chunk at the desired position.
        /// NOT MAP COORDINATES THIS TRANSLATES TO CHUNK POSITIONS
        /// </summary>
        /// <param name="chunk"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void RenderChunkAt(Chunk chunk, int x, int y)
        {
            y = TranslateFromYToMonitorY(y);
            MainCanvas.Children.Add(chunk);
            Canvas.SetLeft(chunk, x * 512);
            Canvas.SetTop(chunk, y * 512);
        }

        /// <summary>
        /// Unrenders the given chunks for performance.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void UnrenderChunkAt(Chunk target)
        {
            MainCanvas.Children.Remove(target);
        }

        /// <summary>
        /// Renders the map around the given coordinates.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void RenderAround(Vector2 Position)
        {
            RenderChunks();
            RenderActors();


            float x = Position.X;
            float y = Position.Y;

            //Monitor y + is down from top left corner
            y = TranslateFromYToMonitorY(y);

            var centerAtX = Main.ActualWidth / 2;
            var centerAtY = Main.ActualHeight / 2;

            Canvas.SetLeft(MainCanvas, centerAtX - x);
            Canvas.SetTop(MainCanvas, centerAtY - y);
        }

        /// <summary>
        /// Renders the scene around the camera.
        /// </summary>
        public void RenderAtCamera()
        {
            Vector2 PositionToRenderAt = new Vector2(camera.CameraX, camera.CameraY);
            RenderAround(PositionToRenderAt);
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
        private int TranslateFromYToMonitorY(int y)
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

        /// <summary>
        /// Returns the actual size in which the camera can see
        /// </summary>
        /// <returns></returns>
        private Vector2 GetCameraRenderSize()
        {
            return new Vector2((float)Main.ActualWidth, (float)Main.ActualHeight);
        }

        /// <summary>
        /// Initialisis ActorRenderList based on CurrentMap
        /// </summary>
        private void InitActorRenderBindingList()
        {
            ActorRenderAll.Clear();

            foreach (var actorbase in _currentMap.MapActorList)
            {
                //ActorRender ar = new ActorRender(actorbase);
                //ar.Texture = 
                ActorRenderAll.Add(new ActorRender(actorbase));
            }
        }

        /// <summary>
        /// Clears scene at request drops old references.
        /// </summary>
        private void CleanRendererCanvas()
        {
            MainCanvas = new Canvas();
            //MainCanvas.Background = new SolidColorBrush(Windows.UI.Colors.Gray);

            //A parent canvas to make map transforms(camera movement) easier
            MainCanvasTransform.Children.Clear();
            MainCanvasTransform.Children.Add(MainCanvas);
        }

        /// <summary>
        /// True if actor is in camera view range
        /// </summary>
        /// <param name="acrender"></param>
        /// <returns></returns>
        private bool IsActorInRenderRange(ActorRender acrender)
        {
            Vector2 cameraSize = GetCameraRenderSize();
            float xDistance = Math.Abs(acrender.Position.X - camera.CameraX);
            float cameraXWidthWithExtraBufferRange = cameraSize.X + 150;

            //Render distance is only applied on x coord
            return xDistance <= cameraXWidthWithExtraBufferRange;
        }
        private bool IsChunkInRenderRange(int x, int y)
        {
            Vector2 cameraSize = GetCameraRenderSize();
            float middleOFChunk = (x * ChunkSize) + (ChunkSize / 2);
            float xDistance = Math.Abs(middleOFChunk - camera.CameraX);

            //Add one extra chunk for prebuffering
            float cameraXWidthWithExtraBufferRange = cameraSize.X + 10000;
            return xDistance <= cameraXWidthWithExtraBufferRange;
        }

        private void RenderActors()
        {
            try
            {
                foreach (var activeActorRender in ActorRenderActive)
                {
                    var translatedPos = GetTranslatedActorPosForRender(activeActorRender);
                    Canvas.SetLeft(activeActorRender, translatedPos.X);
                    Canvas.SetTop(activeActorRender, TranslateFromYToMonitorY(translatedPos.Y));
                }

                //Remove actors that got out of render range
                foreach (var activeAcRender in ActorRenderActive)
                {
                    if (!IsActorInRenderRange(activeAcRender))
                    {
                        ActorRenderActive.Remove(activeAcRender);
                        MainCanvas.Children.Remove(activeAcRender);
                    }
                }

                //Add new actors that got in render range
                foreach (var acRender in ActorRenderAll)
                {
                    if (!ActorRenderActive.Contains(acRender) && IsActorInRenderRange(acRender))
                    {
                        ActorRenderActive.Add(acRender);
                        MainCanvas.Children.Add(acRender);
                    }
                }
            }
            catch (Exception) { }
        }

        private void RenderChunks()
        {
            try
            {
                //Remove chunks that got out of render range
                foreach (var chunkWithCoords in ChunkRenderActive)
                {
                    if (!IsChunkInRenderRange(chunkWithCoords.Item2, chunkWithCoords.Item3))
                    {
                        ChunkRenderActive.Remove(chunkWithCoords);
                        MainCanvas.Children.Remove(chunkWithCoords.Item1);
                    }
                }

                int x = 0;
                //Add new actors that got in render range
                foreach (var chunksOnLockedX in _currentMap.Map)
                {
                    foreach (var chunkWithYCoord in chunksOnLockedX)
                    {
                        var MatchedChunkList = ChunkRenderActive.Where(chunkWithCoords => chunkWithCoords.Item1 == chunkWithYCoord.Item1);
                        if (!MatchedChunkList.Any() && IsChunkInRenderRange(x, chunkWithYCoord.Item2))
                        {
                            ChunkRenderActive.Add(new Tuple<Chunk, int, int>(chunkWithYCoord.Item1, x, chunkWithYCoord.Item2));
                            RenderChunkAt(chunkWithYCoord.Item1, x, chunkWithYCoord.Item2);
                        }
                    }
                    x++;
                }
            }
            catch (Exception) { }
        }

        private Vector2 GetTranslatedActorPosForRender(ActorRender acRender)
        {
            var translatedX = acRender.Position.X - acRender.Size.X / 2;
            var translatedY = acRender.Position.Y + acRender.Size.Y / 2;

            return new Vector2(translatedX, translatedY);
        }
    }
}
