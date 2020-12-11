using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using System.IO;
using TudoMario.Map;
using TudoMario.Rendering;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using TudoMario.Ui;
using Windows.UI.Core;
using TudoMario.AiActors;

namespace TudoMario
{
    public static class LoadMap
    {
        public static Vector2 actorStartingPoint = new Vector2();
        private static Vector2 mapStartingPoint = new Vector2(0, 0);
        public static MapBase map = new MapBase(mapStartingPoint);
        public static int CurrentLevel { get; set; } = 0;
        internal static UiController UiControl { get; set; }
        public static string path = @"Assets//gameSave.txt";

        public static event EventHandler SwitchMap;

        /// <summary>
        /// Contains the maps for the game in order
        /// </summary>
        public static readonly IList<string> levels = new ReadOnlyCollection<string>
        (new List<string> { "map01.csv", "map02.csv", "map03.csv", "map06.csv" });

        #region map6 variables
        internal static DumbEnemy enemy = null;
        internal static bool triggerOnce = true;
        #endregion

        public static MapBase PreLoad(int level)
        {
            map = new MapBase(new Vector2(100, 0));
            if (0 > level)
            {
                throw new IndexOutOfRangeException();
            }
            if (level > levels.Count - 1)
            {
                throw new IndexOutOfRangeException();
            }
            var fileName = levels[level];
            ReadFile(fileName);
            return map;
        }

        private static void OnTouchKillZone(ColliderBase sender, ColliderBase collider)
        {
            map.MainPlayer.ApplyDamage(1000);
            ShowDialog("You died!");
        }

        private static void OnFinishLevel(ColliderBase sender, ColliderBase collider)
        {
            CurrentLevel++;
            SwitchMap?.Invoke(null, EventArgs.Empty);
        }

        public static void ModifyMap1()
        {
            StaticCollider KillZone = new StaticCollider(new Vector2(10000, 10), new Vector2(280, -500), false);
            KillZone.CollisionStarted += OnTouchKillZone;

            StaticCollider NextMapTrigger = new StaticCollider(new Vector2(64, 64), new Vector2(1983, 1), false);
            NextMapTrigger.CollisionStarted += OnFinishLevel;

            DumbEnemy de = new DumbEnemy(new Vector2(653, -127), new Vector2(64, 64));
            de.Texture = TextureHandler.GetImageByName("enemy1-r");
            de.canMove = true;
            de.IsVisible = true;
            map.AddActor(de);
        }
        public static void ModifyMap2()
        {
            StaticCollider KillZone = new StaticCollider(new Vector2(10000, 10), new Vector2(280, -500), false);
            KillZone.CollisionStarted += OnTouchKillZone;

            StaticCollider NextMapTrigger = new StaticCollider(new Vector2(64, 64), new Vector2(3007, -160), false);
            NextMapTrigger.CollisionStarted += OnFinishLevel;
        }
        public static void ModifyMap3()
        {
            StaticCollider KillZone = new StaticCollider(new Vector2(10000, 10), new Vector2(280, -500), false);
            KillZone.CollisionStarted += OnTouchKillZone;

            StaticCollider NextMapTrigger = new StaticCollider(new Vector2(64, 64), new Vector2(4031, 321), false);
            NextMapTrigger.CollisionStarted += OnFinishLevel;
        }
        public static void ModifyMap4() { }
        public static void ModifyMap5() { }
        public static void ModifyMap6()
        {
            ///Defining collision mechanics for maps
            ///MAP 6 Deadline

            static void OnDeadlineCollison(ColliderBase sender, ColliderBase collidor)
            {
                if (collidor == map.MainPlayer)
                {
                    ShowDialog("Deadline of Tools of software projects caught you! You failed!\nPress Esc to close!");
                    enemy.IsStatic = true;
                    map.MainPlayer.ApplyDamage(999);
                    map.MainPlayer.IsStatic = true;
                }
            }
            static void OnRandomText(ColliderBase sender, ColliderBase collidor)
            {
                if (collidor == map.MainPlayer && triggerOnce)
                {
                    ShowDialog("The end of the semester is coming! Quick run before the deadlines are catching up!\nPress Esc to continue...");
                    map.MainPlayer.IsStatic = true;
                    triggerOnce = false;
                }
            }
            static void OnDialogClose(object sender, DialogEndedEventArgs e)
            {
                if (e.Dialog == "The end of the semester is coming! Quick run before the deadlines are catching up!\nPress Esc to continue...")
                {
                    enemy.canMove = true;
                    enemy.IsVisible = true;
                    map.MainPlayer.IsStatic = false;
                }
                else if (e.Dialog == "Deadline of Tools of software projects caught you! You failed!\nPress Esc to close!")
                {
                    Environment.Exit(1);
                }
            }

            UiControl.DialogClosed += OnDialogClose;

            StaticCollider RandomTextPopupHitbox = new StaticCollider(new Vector2(50, 500), new Vector2(2007, -127), false);
            RandomTextPopupHitbox.CollisionStarted += OnRandomText;

            DumbEnemy narrator = new DumbEnemy(new Vector2(2007, -127), new Vector2(70, 70));
            narrator.Texture = TextureHandler.GetImageByName("narrator2");
            narrator.canMove = false;

            DumbEnemy de = new DumbEnemy(new Vector2(450, 200), new Vector2(100, 500));
            de.Texture = TextureHandler.GetImageByName("dl");
            de.CollisionStarted += OnDeadlineCollison;
            de.canMove = false;
            de.IsVisible = false;
            enemy = de;
            map.AddActor(narrator);
            map.AddActor(enemy);
        }

        public static void PostLoad()
        {
            switch (levels[CurrentLevel])
            {
                case "map01.csv":
                    ModifyMap1();
                    break;
                case "map02.csv":
                    ModifyMap2();
                    break;
                case "map03.csv":
                    ModifyMap3();
                    break;
                case "map06.csv":
                    ModifyMap6();
                    break;
            }
        }

        public static void SaveCurrentLevel()
        {
            ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.Values["mapSave"] = CurrentLevel.ToString();
        }

        public static void LevelCompleted()
        {
            CurrentLevel++;
            SaveCurrentLevel();
        }

        public static void LoadCurrentLevel()
        {
            ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            int currentLevel = 0;
            int.TryParse((string)localSettings.Values["mapSave"], out currentLevel);
            CurrentLevel = currentLevel;
        }

        private static Windows.UI.Xaml.Media.Imaging.BitmapImage Texture(string Initial)
        {
            string textureInitial = Initial;
            var texture = TextureHandler.GetImageByName("missing");
            switch (textureInitial)
            {
                case "g":
                    texture = TextureHandler.GetImageByName("ground");
                    break;
                case "g1":
                    texture = TextureHandler.GetImageByName("groundbase");
                    break;

                case "m":
                    texture = TextureHandler.GetImageByName("mud02");
                    break;

                case "s":
                    texture = TextureHandler.GetImageByName("basebackgroung");
                    break;

                case "i":
                    texture = TextureHandler.GetImageByName("ice02");
                    break;

                case "t":
                case "t1":
                    texture = TextureHandler.GetImageByName("ground");
                    break;
            }
            return texture;
        }

        private static void SetTileAndCollider(int row, int column, int chunksInAColumn, string type)
        {
            switch (type)
            {
                case "g":
                    map.GetChunkAt((row / 16), chunksInAColumn - ((column / 16) + 1)).SetTileAt(row % 16, 15 - (column % 16), Texture(type));
                    break;

                case "g1":
                    map.GetChunkAt(row / 16, chunksInAColumn - ((column / 16) + 1)).SetTileAt(row % 16, 15 - (column % 16), Texture(type), solid: true);
                    break;

                case "m":
                    map.GetChunkAt(row / 16, chunksInAColumn - ((column / 16) + 1)).SetTileAt(row % 16, 15 - (column % 16), Texture(type), solid: false, MovementModifier.SwampWalk);
                    break;

                case "s":
                    map.GetChunkAt(row / 16, chunksInAColumn - ((column / 16) + 1)).SetTileAt(row % 16, 15 - (column % 16), Texture(type));
                    break;

                case "i":
                    map.GetChunkAt(row / 16, chunksInAColumn - ((column / 16) + 1)).SetTileAt(row % 16, 15 - (column % 16), Texture(type), solid: false, MovementModifier.IceWalk);
                    break;

                case "t":
                    map.GetChunkAt(row / 16, chunksInAColumn - ((column / 16) + 1)).SetTileAt(row % 16, 15 - (column % 16), Texture(type));
                    break;

                case "t1":
                    map.GetChunkAt(row / 16, chunksInAColumn - ((column / 16) + 1)).SetTileAt(row % 16, 15 - (column % 16), Texture(type), true);
                    break;
            }
        }

        /// <summary>
        /// Reads the file and fills the map with content
        /// </summary>
        private static void ReadFile(string fileName)
        {
            DirectoryInfo dir = new DirectoryInfo("Assets");
            var files = dir.GetFiles();
            string path = "";
            foreach (var item in files)
            {
                if (fileName == item.Name)
                {
                    path = Path.Combine("Assets", fileName);
                }
            }
            using StreamReader reader = new StreamReader(path);
            string[] config = reader.ReadLine().Split(';');
            actorStartingPoint.X = float.Parse(config[1]) * 16;
            actorStartingPoint.Y = float.Parse(config[2]) * 16;
            int mapLength = int.Parse(config[4]);
            int mapHeight = int.Parse(config[5]);
            int chunksInAColumn = int.Parse(config[8]);
            Vector2 actorSize = new Vector2(64, 64);
            PlayerActor player = new PlayerActor(actorStartingPoint, actorSize);
            player.Texture = TextureHandler.GetImageByName("player1-r");
            player.AddMovingTexture("player1-move0-l,player1-move1-l", 0);
            player.AddMovingTexture("player1-move0-r,player1-move1-r", 1);
            map.AddActor(player);
            Renderer.BindCameraAtActor(player);
            int column = 0;
            while (!reader.EndOfStream)
            {
                string[] line = reader.ReadLine().Split(';');
                for (int row = 0; row < line.Length; row++)
                {
                    if (column < mapHeight)
                    {
                        if (0 == row % 16 && 0 == column % 16)
                        {
                            map.SetChunkAt(row / 16, chunksInAColumn - ((column / 16) + 1), new Chunk());
                        }
                        SetTileAndCollider(row, column, chunksInAColumn, line[row]);
                    }
                }
                column++;
            }
            reader.Close();
        }


        private static void ShowDialog(string Text)
        {
            UiControl.ShowDialog(Text).Wait();
        }
    }
}
