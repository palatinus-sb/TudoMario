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

namespace TudoMario
{
    public static class LoadMap
    {


        private static string fileName;
        public static Vector2 actorStartingPoint = new Vector2();
        private static Vector2 mapStartingPoint = new Vector2(0, 0);
        private static MapBase map = new MapBase(mapStartingPoint);

        public static MapBase Load(string FileName)
        {
            fileName = FileName;
            ReadFile();
            return map;
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
            player.SetTexture(TextureHandler.GetImageByName("player1-r"));
            map.AddActor(player);
            Renderer.BindCameraAtActor(player);
            // TODO
            // add texture images and textures to ice, terra and mud
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
