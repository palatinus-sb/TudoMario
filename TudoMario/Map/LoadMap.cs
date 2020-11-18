using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using System.IO;

namespace TudoMario.Map
{
    public class LoadMap
    {
        public LoadMap(string fileName)
        {
            this.fileName = fileName;
            ReadFile();
        }

        private string fileName;
        List<string> mapCsv = new List<string>();
        public Vector2 actorStartingPoint = new Vector2();
        private static Vector2 mapStartingPoint = new Vector2(0, 0);
        public MapBase map = new MapBase(mapStartingPoint); // I have to make this accesible I guess

        public void ReadFile() //can be private tho
        {
            DirectoryInfo dir = new DirectoryInfo("Assets");
            var files = dir.GetFiles();
            string[] text = new string[] {};
            string path = "";
            foreach (var item in files)
            {
                if (fileName == item.Name)
                {
                    path = Path.Combine("Assets", fileName);
                    text = File.ReadAllLines(path);
                    actorStartingPoint.X = float.Parse(text[0].Split(';')[1]);
                    actorStartingPoint.Y = float.Parse(text[0].Split(';')[2]);
                }
            }
            bool passedLast = false;
            int lengthOfARow = text[0].Split(';').Count();
            int chunksInARow = lengthOfARow / 16;
            int chunksInAColumn = (text.Count() - 1) / 16; // had to extract(?) the config line
            int x = 0;
            int y = 0;
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
            int column = 0;
            List<List<Tile>> tiles = new List<List<Tile>>(); 
            List<List<Chunk>> chunks = new List<List<Chunk>>();
            while (!reader.EndOfStream)
            {
                string[] line = reader.ReadLine().Split(';');
                for (int row = 0; row < line.Length; row++)
                {
                    if (row % 16 == 0 && column % 16 == 0)
                    {
                        map.AddChunkAt(new Chunk(), row % 16, column % 16);
                    }
                }
            }




            Debug.Write("Breakpoint");
        }
    }
}
