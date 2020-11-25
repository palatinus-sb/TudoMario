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

namespace TudoMario
{
    public static class LoadMap
    {


        private static string fileName;
        private static List<string> mapCsv = new List<string>();
        public static Vector2 actorStartingPoint = new Vector2();
        private static Vector2 mapStartingPoint = new Vector2(0, 0);
        private static MapBase map = new MapBase(mapStartingPoint);

        public static MapBase Load(string FileName)
        {
            fileName = FileName;
            ReadFile();
            return map;
        }

        private static Tile TileWithSetTexture(string Initial)
        {
            Tile tile = new Tile();
            string textureInitial = Initial;
            var texture = TextureHandler.GetImageByName("missing");
            switch (textureInitial)
            {
                case "g":
                    texture = TextureHandler.GetImageByName("groundbase");
                    break;

                case "m":
                    texture = TextureHandler.GetImageByName("groundbase");
                    break;

                case "s":
                    texture = TextureHandler.GetImageByName("basebackgroung");
                    break;

                case "i":
                    texture = TextureHandler.GetImageByName("groundbase");
                    break;

                case "t":
                    texture = TextureHandler.GetImageByName("groundbase");
                    break;
            }
            tile.Texture = texture;
            return tile;
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
            actorStartingPoint.X = float.Parse(config[1]);
            actorStartingPoint.Y = float.Parse(config[2]);
            int mapLength = int.Parse(config[4]);
            int mapHeight = int.Parse(config[5]);
            int chunksInARow = int.Parse(config[7]);
            int chunksInAColumn = int.Parse(config[8]);
            Vector2 actorSize = new Vector2(64, 64);
            map.AddActor(new PlayerActor(actorStartingPoint, actorSize));
            Renderer.BindCameraAtActor(map.MainPlayer);
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
                        if (row % 16 == 0 && column % 16 == 0)
                        {
                            map.SetChunkAt(row / 16, chunksInAColumn - ((column / 16) + 1), new Chunk());
                        }
                        map.GetChunkAt((row / 16), chunksInAColumn - ((column / 16) + 1)).SetTileAt(row % 16, 15 - (column % 16), TileWithSetTexture(line[row]));
                    }
                }
                column++;
            }
            reader.Close();
        }
    }
}
