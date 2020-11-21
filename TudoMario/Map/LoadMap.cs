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
        static List<string> mapCsv = new List<string>();
        public static Vector2 actorStartingPoint = new Vector2();
        private static Vector2 mapStartingPoint = new Vector2(0, 0);
        private static MapBase map = new MapBase(mapStartingPoint); // I have to make this accesible I guess

        public static MapBase Load(string FileName)
        {
            fileName = FileName;
            ReadFile();
            return map;
        }

        private static Tile SetTexture(string Initial)
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
        public static void ReadFile() //can be private tho
        {
            DirectoryInfo dir = new DirectoryInfo("Assets");
            var files = dir.GetFiles();
            string[] text = new string[] { };
            string path = "";
            foreach (var item in files)
            {
                if (fileName == item.Name)
                {
                    path = Path.Combine("Assets", fileName);
                    text = File.ReadAllLines(path); // Have to find a better solution later / maybe size in the map
                    /* actorStartingPoint.X = float.Parse(text[0].Split(';')[1]);
                     actorStartingPoint.Y = float.Parse(text[0].Split(';')[2]);*/
                }
            }
            //bool passedLast = false;
            /*int lengthOfARow = text[0].Split(';').Count();
            int chunksInARow = lengthOfARow / 16;
            int chunksInAColumn = (text.Count() - 1) / 16; // had to extract(?) the config line
            /*int x = 0;
            int y = 0;*/
            /*foreach (var line in text) // goes through the lines
            {
                foreach  (var word in line.Split(';')) // goes through the columns
                {
                    if (!passedLast && "last" == word)
                    {
                        passedLast = true;
                    }
                    if (passedLast)
                    {
                        mapCsv.Add(word); // stores the type of the tiles
                        Tile tile = new Tile(); // this is just temporary
                        
                        //this should be a texture handler
                        /*switch (word)
                        {
                            case "s": 
                                tile = new Tile();
                                break;
                            default:
                                break;
                        }*/

            /*if (0 == x % 16 && 0 == y % 16) // checks if a new chunk is required
            {
                chunks.Add(new Chunk());
                map.AddChunkAt(chunks[y / 16], x / 16, chunksInAColumn - (y / 16)); // should use the index var here?
            }
            int index = (x / 16) * chunksInARow + y / 16;
            chunks[index].SetTileAt(x, y, tile);
        }
        ++y;
    }
    ++x;
}*/





            using StreamReader reader = new StreamReader(path);
            string[] config = reader.ReadLine().Split(';');
            actorStartingPoint.X = float.Parse(config[1]);
            actorStartingPoint.Y = float.Parse(config[2]);
            Vector2 actorSize = new Vector2(64, 64);
            map.AddActor(new PlayerActor(actorStartingPoint, actorSize));
            int column = 0;
            int numberOfRows = text.Count();
            //int row = 0;
            List<List<Tile>> tiles = new List<List<Tile>>();
            //List<List<Chunk>> chunks = new List<List<Chunk>>();
            // TODO
            // texture from the picture
            // correct chunks indexes
            // correct tile indexes
            while (!reader.EndOfStream)
            {
                string[] line = reader.ReadLine().Split(';');
                for (int row = 0; row < line.Length; row++)
                {
                    if (column + 16 < numberOfRows - 1) //had to substract 1 coz of the config line
                    {
                        if (row % 16 == 0 && column % 16 == 0)
                        {
                            map.SetChunkAt(row / 16, (column / 16) + 1, new Chunk());
                        }
                        map.GetChunkAt((row / 16), ((column / 16) + 1)).SetTileAt(row % 16, 16 - (column % 16), SetTexture(line[row])); // need a Tile  maybe? with type? Is the Tile
                    }

                }
                column++;
            }
            reader.Close();




            Debug.Write("Breakpoint");
        }
    }
}
