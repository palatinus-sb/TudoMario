using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace TudoMario.Map
{
    public sealed partial class MapBase : UserControl
    {
        public MapBase(Vector2 startingpoint)
        {
            InitializeComponent();
            StartingPoint = startingpoint;
            MapActorList.Clear();
            Chunks.Clear();
            StartingPoint = new Vector2(0, 0);
            MainPlayer = null;
        }

        public List<ActorBase> MapActorList = new List<ActorBase>();
        public SortedDictionary<int, SortedDictionary<int, Chunk>> Chunks = new SortedDictionary<int, SortedDictionary<int, Chunk>>();
        //public SortedDictionary<int, SortedDictionary<int, Chunk>> ChunksY = new SortedDictionary<int, SortedDictionary<int, Chunk>>();

        public Vector2 StartingPoint { get; set; }

        public PlayerActor MainPlayer { get; set; }

        /// <summary>
        /// Set the chunk at the specified coordinates. Previous chunk will be overwritten if one was already present at specified coordinates.
        /// </summary>
        /// <param name="x">The chunk column</param>
        /// <param name="y">The chunk row</param>
        /// <param name="chunk">the chunk to be set</param>
        public void SetChunkAt(int x, int y, Chunk chunk)
        {
            if (!Chunks.ContainsKey(x))
                Chunks.Add(x, new SortedDictionary<int, Chunk>());

            if (!Chunks[x].ContainsKey(y))
            {
                Chunks[x].Add(y, chunk);
                chunk.ChunkPosition = new Vector2(x, y);
            }
            else
            {
                Chunks[x][y] = chunk;
                chunk.ChunkPosition = new Vector2(x, y);
            }
        }

        /// <summary>
        /// Gets the chunk at the specified coordinates. Returns null if no chunk is present at that coordinates.
        /// </summary>
        /// <param name="x">The chunk column</param>
        /// <param name="y">The chunk row</param>
        /// <returns>The chunk at the specified coordinates</returns>
        public Chunk GetChunkAt(int x, int y)
        {
            if (Chunks.ContainsKey(x))
                if (Chunks[x].ContainsKey(y))
                    return Chunks[x][y];

            return null;
        }

        /// <summary>
        /// Gets all the chunks in a specified column ordered ascending.
        /// </summary>
        /// <param name="x">The column number</param>
        /// <returns></returns>
        public IEnumerable<Chunk> GetColumn(int x)
        {
            if (x < 0)
                return null;
            if (x >= Chunks.Count)
                return null;
            return Chunks[x].Values;
        }

        /// <summary>
        /// Returns the whole dictionary for the given x coord.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public SortedDictionary<int, Chunk> GetColumnAsDictionary(int x)
        {
            if (x < 0)
                return null;
            if (x >= Chunks.Count)
                return null;
            SortedDictionary<int, Chunk> ret;
            Chunks.TryGetValue(x, out ret);

            return ret;
        }

        /// <summary>
        /// Gets all the chunks in a specified column ordered ascending.
        /// </summary>
        /// <param name="y">The row number</param>
        /// <returns></returns>
        public IEnumerable<Chunk> GetRow(int y)
        {
            List<Chunk> row = new List<Chunk>();
            foreach (var column in Chunks.Values)
                if (column.ContainsKey(y))
                    row.Add(column[y]);
            return row;
        }

        /// <summary>
        /// Binds the actor to the map which registers it for rendering.
        /// </summary>
        public void AddActor(ActorBase actor)
        {
            if (actor.GetType() == typeof(PlayerActor))
                MainPlayer = actor as PlayerActor;

            MapActorList.Add(actor);
        }

        /// <summary>
        /// Removes the actor to the map.
        /// </summary>
        public void RemoveActor(ActorBase actor) => MapActorList.Remove(actor);
    }
}

