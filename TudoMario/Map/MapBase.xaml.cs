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
            this.InitializeComponent();
            StartingPoint = startingpoint;
        }
        public List<ActorBase> MapActorList = new List<ActorBase>();
        private List<List<Tuple<Chunk, int>>> Map = new List<List<Tuple<Chunk, int>>>();

        public Vector2 StartingPoint { get; set; }

        /// <summary>
        /// Inserts the given chunk into the given x,y coordinate in logic layer.
        /// </summary>
        /// <param name="chunk">Chunk to insert</param>
        /// <param name="x">X coord to insert at</param>
        /// <param name="y">Y coord to insert at</param>
        public void AddChunkAt(Chunk chunk, int x, int y)
        {
            if (Map.Count > x)
            {
                Map[x].Add((new Tuple<Chunk, int>(chunk, y)));
            }
            else
            {
                Map.Add(new List<Tuple<Chunk, int>>());
                Map[x].Add((new Tuple<Chunk, int>(chunk, y)));
            }
        }

        /// <summary>
        /// Returns the chunk at given x,y. Null if not exists.
        /// </summary>
        /// <param name="x">X coord</param>
        /// <param name="y">Y coord</param>
        public Chunk GetChunkAt(int x, int y)
        {
            if (Map.Count > x)
            {
                var ChunkAtY = Map[x].Where(ChunksAtFixedXCoord => ChunksAtFixedXCoord.Item2 == y).SingleOrDefault();
                if (ChunkAtY != null)
                {
                    return ChunkAtY.Item1;
                }
            }
            return null;
        }

        /// <summary>
        /// Binds the actor to the map which registers it for rendering.
        /// </summary>
        public void AddActor(ActorBase actor)
        {
            MapActorList.Add(actor);
        }
    }
}

