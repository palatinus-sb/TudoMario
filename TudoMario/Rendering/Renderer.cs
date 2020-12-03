using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TudoMario.Map;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using TudoMario.Ui;

namespace TudoMario.Rendering
{
    internal class Renderer : Page
    {
        private MainPage Main;
        /// <summary>
        /// Main canvas contains all the chunks can be used to set renderdistance.
        /// </summary>
        private Canvas MainCanvas;
        /// <summary>
        /// This element is STRICTLY used to make/allow transforms on the main canvas.
        /// </summary>
        private Canvas MainCanvasTransform = new Canvas();

        private Hud hud;

        private static CameraObject Camera = new CameraObject();

        /// <summary>
        /// The current map to render at.
        /// </summary>
        private MapBase _currentMap;

        private List<ActorRender> ActorRenderAll = new List<ActorRender>();
        private List<ActorRender> ActorRenderActive = new List<ActorRender>();

        /// <summary>
        /// Represents the rendered chunks which are in view range.
        /// </summary>
        private List<SortedDictionary<int, Chunk>> ChunkColumnActive = new List<SortedDictionary<int, Chunk>>();
        private List<Tile> RenderedTiles = new List<Tile>();

        private double ZoomLevel = 1;

        //Changes the render distance. If minus you can see the unrendering
        private double RenderDistance = 50;

        private static float ChunkSize = 512;
        private static float TileSize = 32;


        //private float ZoomLevel = 1;

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
                ResizeTilesForZoomLevel();

                ShowHud();


            }
        }

        public Hud Hud
        {
            set
            {
                if (hud != null)
                {
                    Hud current = hud;
                    MainCanvasTransform.Children.Remove(current);
                    hud = value;
                }
                else
                {
                    hud = value;
                }
            }
        }

        public Renderer(MainPage main)
        {
            TextureHandler.Init();

            Main = main;
            InitializeComponent();
        }
        private void InitializeComponent()
        {
            ChunkSize = ChunkSize * (float)ZoomLevel;
            TileSize = TileSize * (float)ZoomLevel;

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

        /// <summary>
        /// Binds the Camera to the given actor
        /// </summary>
        /// <param name="act"></param>
        public static void BindCameraAtActor(ActorBase target)
        {
            Camera.BindActor(target);
        }

        /// <summary>
        /// Unbinds the camera from an actor therefore makes it possible to focus on fixed coordinates.
        /// </summary>
        public static void UnbindCameraAtActor()
        {
            Camera.UnbindActor();
        }

        public void RenderTileAt(Tile tile, int x, int y)
        {
            y = TranslateFromYToMonitorY(y);

            MainCanvas.Children.Add(tile);
            Canvas.SetLeft(tile, x);
            Canvas.SetTop(tile, y);
        }

        private void UnrenderTiles(List<Tile> Targets)
        {
            foreach (var target in Targets)
            {
                MainCanvas.Children.Remove(target);
            }
        }

        /// <summary>
        /// Renders the map around the given coordinates.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void RenderAround(Vector2 Position)
        {
            //RenderChunks();
            RenderTiles();
            RenderActors();

            RefreshHudValues();

            float x = Position.X;
            float y = Position.Y;

            //Monitor y + is down from top left corner
            y = TranslateFromYToMonitorY(y);

            var centerAtX = Main.ActualWidth / 2;
            var centerAtY = Main.ActualHeight / 2;

            Canvas.SetLeft(MainCanvas, (centerAtX - x));
            Canvas.SetTop(MainCanvas, (centerAtY - y));
        }

        /// <summary>
        /// Renders the scene around the camera.
        /// </summary>
        public void RenderAtCamera()
        {
            if (Camera != null)
            {
                Vector2 PositionToRenderAt = new Vector2(Camera.CameraX * (float)ZoomLevel, Camera.CameraY * (float)ZoomLevel);
                RenderAround(PositionToRenderAt);
            }
        }

        /// <summary>
        /// Makes HUD visible.
        /// </summary>
        public void ShowHud()
        {
            if (hud != null && !MainCanvas.Children.Contains(hud))
            {
                MainCanvasTransform.Children.Add(hud);
            }
        }

        /// <summary>
        /// Hides HUD.
        /// </summary>
        public void HideHud()
        {
            if (MainCanvas.Children.Contains(hud))
                MainCanvas.Children.Remove(hud);
        }

        /// <summary>
        /// Moves camera to left. Can be negative value for Right.
        /// </summary>
        /// <param name="x"></param>
        public void MoveCameraLeft(int x)
        {
            Camera.CameraX = Camera.CameraX - x;
        }
        /// <summary>
        /// Moves Camera up. Can be negative value for Down.
        /// </summary>
        /// <param name="y"></param>
        public void MoveCameraUp(int y)
        {
            Camera.CameraY = Camera.CameraY + y;
        }

        /// <summary>
        /// Shows a menu object on top of everything untill RemoveMenuObject(Usercontrol obj) is given.
        /// </summary>
        /// <param name="obj"></param>
        public void ShowMenuObject(UserControl obj)
        {
            if (!MainCanvasTransform.Children.Contains(obj))
                MainCanvasTransform.Children.Add(obj);
        }
        /// <summary>
        /// Removes the given menu object from the screen.
        /// </summary>
        /// <param name="obj"></param>
        public void RemoveMenuObject(UserControl obj)
        {
            MainCanvasTransform.Children.Remove(obj);
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
                ActorRenderAll.Add(new ActorRender(actorbase, ZoomLevel));
            }
        }

        private void ResizeTilesForZoomLevel()
        {
            var map = _currentMap.Chunks.Values;
            foreach (var column in map)
            {
                foreach (var chunk in column)
                {
                    //chunk.Value.Tiles
                    for (int x = 0; x < 16; x++)
                    {
                        for (int y = 0; y < 16; y++)
                        {
                            Tile t = chunk.Value.Tiles[x, y];
                            if (t != null)
                            {
                                t.Height = TileSize;
                                t.Width = TileSize;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Clears scene at request drops old references.
        /// </summary>
        private void CleanRendererCanvas()
        {
            MainCanvas = new Canvas();
            MainCanvasTransform.Background = new SolidColorBrush(Windows.UI.Colors.LightBlue);

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
            float xDistance = Math.Abs(acrender.Position.X - Camera.CameraX);
            float cameraXWidthWithExtraBufferRange = cameraSize.X + 150;

            //Render distance is only applied on x coord
            return xDistance <= cameraXWidthWithExtraBufferRange;
        }

        /// <summary>
        /// Returns x coord that is the buffered render range on the left.
        /// </summary>
        /// <returns></returns>
        private float GetRenderBorderOnLeftXCoord()
        {
            Vector2 cameraSize = GetCameraRenderSize();
            float RenderedDistanceOnLeft = Camera.CameraX * (float)ZoomLevel - (cameraSize.X / 2);

            //- makes the range bigger
            float RenderBorderOnLeftWithExtraBufferRange = RenderedDistanceOnLeft - (float)RenderDistance;

            return RenderBorderOnLeftWithExtraBufferRange;
        }
        /// <summary>
        /// Returns x coord that is the buffered render range on the right.
        /// </summary>
        /// <returns></returns>
        private float GetRenderBorderOnRightXCoord()
        {
            Vector2 cameraSize = GetCameraRenderSize();
            float RenderedDistanceOnLeft = Camera.CameraX * (float)ZoomLevel + (cameraSize.X / 2);

            //+ makes the range bigger
            float RenderBorderOnLeftWithExtraBufferRange = RenderedDistanceOnLeft + (float)RenderDistance;

            return RenderBorderOnLeftWithExtraBufferRange;
        }

        private List<Tile> GetTilesToRender()
        {
            List<Tile> retList = new List<Tile>();

            var LeftTileEdge = (int)Math.Round(GetRenderBorderOnLeftXCoord() / TileSize);
            if (LeftTileEdge < 0)
                LeftTileEdge = 0;
            var RightTileEdge = (int)Math.Round(GetRenderBorderOnRightXCoord() / TileSize);

            for (int i = LeftTileEdge; i <= RightTileEdge; i++)
            {
                int rowInChunk = i % 16;
                int chunkFromI = i / 16;

                var TileColumn = GetTilesFromChunkColumnAt(CurrentMap.GetColumnAsDictionary(chunkFromI), rowInChunk);
                if (TileColumn != null)
                    retList.AddRange(TileColumn);
            }

            return retList;
        }

        private IEnumerable<Tile> GetTilesFromChunkColumnAt(SortedDictionary<int, Chunk> column, int row)
        {
            if (column == null)
                return null;
            List<Tile> retList = new List<Tile>();
            foreach (var chunk in column)
            {
                for (int y = 0; y < 16; y++)
                {
                    if (chunk.Value != null)
                        retList.Add(chunk.Value.Tiles[row, y]);
                }
            }
            return retList;
        }

        public void RenderActors()
        {
            try
            {
                foreach (var activeActorRender in ActorRenderActive)
                {
                    var translatedPos = GetTranslatedActorPosForRender(activeActorRender);
                    Canvas.SetLeft(activeActorRender, translatedPos.X * (float)ZoomLevel);
                    Canvas.SetTop(activeActorRender, TranslateFromYToMonitorY(translatedPos.Y * (float)ZoomLevel));
                    Canvas.SetZIndex(activeActorRender, 1000);
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
        private void RenderTiles()
        {
            List<Tile> unRenderQueue = new List<Tile>();
            var TileListToRenderWithX = GetTilesToRender();

            //Get all the tiles that are rendered but not in the newly calculated DesiredRenderedTiles
            foreach (var renderedTile in RenderedTiles)
            {
                if (renderedTile != null && !TileListToRenderWithX.Contains(renderedTile))
                    unRenderQueue.Add(renderedTile);
            }
            int tmpcounter = 0;
            //Get all the tiles that needs to be rendered but are not yet rendered
            foreach (var toRenderTile in TileListToRenderWithX)
            {
                if (toRenderTile != null && !RenderedTiles.Contains(toRenderTile))
                {
                    tmpcounter++;
                    int relativeX = (int)Math.Round(toRenderTile.ChunkParent.ChunkPosition.X * ChunkSize + toRenderTile.TilePosition.X * TileSize);
                    int relativeY = (int)Math.Round(toRenderTile.ChunkParent.ChunkPosition.Y * ChunkSize + (ChunkSize - toRenderTile.TilePosition.Y * TileSize)) - (int)Math.Round(ChunkSize);
                    RenderTileAt(toRenderTile, relativeX, relativeY);

                }
            }
            RenderedTiles = TileListToRenderWithX;
            UnrenderTiles(unRenderQueue);
        }

        private Vector2 GetTranslatedActorPosForRender(ActorRender acRender)
        {
            var translatedX = acRender.Position.X - acRender.Size.X / 2;
            var translatedY = acRender.Position.Y + acRender.Size.Y / 2;

            return new Vector2(translatedX, translatedY);
        }

        private void RefreshHudValues()
        {
            if (_currentMap.MainPlayer != null)
                hud.SetStressLevel(_currentMap.MainPlayer.StressLevel);
        }
    }
}
