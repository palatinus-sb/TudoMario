﻿using System;
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

namespace TudoMario
{
    public static class LoadMap
    {


        private static string fileName;
        public static Vector2 actorStartingPoint = new Vector2();
        private static Vector2 mapStartingPoint = new Vector2(0, 0);
        private static MapBase map = new MapBase(mapStartingPoint);
        public static int currentLevel = 0;

        /// <summary>
        /// Contains the maps for the game in order
        /// </summary>
        public static readonly IList<string> levels = new ReadOnlyCollection<string>
        (new List<string> { "TestMap.csv", "TestMapv2.0.csv", "map01.csv", "map02.csv", "map03.csv", "map06.csv" });

        public static MapBase PreLoad(int level)
        {
            if (0 > level)
            {
                throw new IndexOutOfRangeException();
            }
            if (level > levels.Count - 1)
            {
                throw new IndexOutOfRangeException();
            }
            fileName = levels[level];
            ReadFile();
            return map;
        }

        public static void ModifyMap1() { }
        public static void ModifyMap2() { }
        public static void ModifyMap3() { }
        public static void ModifyMap4() { }
        public static void ModifyMap5() { }
        public static void ModifyMap6() { }

        public static void PostLoad()
        {
            switch (levels[currentLevel])
            {
                case "TestMap.csv":
                    ModifyMap1();
                    break;
                case "TestMapv2.0.csv":
                    ModifyMap2();
                    break;
                case "map02.csv":
                    ModifyMap3();
                    break;
                case "map03.csv":
                    ModifyMap4();
                    break;
                case "map04.csv":
                    ModifyMap5();
                    break;
                case "placeholder":
                    ModifyMap6();
                    break;
            }
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
                    texture = TextureHandler.GetImageByName("mud01");
                    break;

                case "s":
                    texture = TextureHandler.GetImageByName("basebackgroung");
                    break;

                case "i":
                    texture = TextureHandler.GetImageByName("ice01");
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
        private static void ReadFile()
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
            //int chunksInARow = int.Parse(config[7]);
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
    }
}
