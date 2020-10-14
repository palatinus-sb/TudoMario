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
    public sealed partial class Chunk : UserControl
    {
        public Tile[,] Tiles = new Tile[16, 16];
        public Chunk()
        {
            this.InitializeComponent();
            this.Height = 512;
            this.Width = 512;
        }
        
        /// <summary>
        /// Sets the tile at the given coordinates in this chunk to the given tile type.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="tileType"></param>
        /// <param name="imagePath"></param>
        public void SetTileAt(int x, int y, Type tileType, string imagePath)
        {
            /////////////////////////////////////////////////
            ///HashSet TO UNBIND PREVIOUS CHILD INCOMPLETE
             /////////////////////////////////////////////////

            //0 is the top since we go from top left so it has to be mirrored
            x = 15 - x;

            var _tile = (Tile)Activator.CreateInstance(tileType);
            //_tile.SetBackgroundColor(TileFillerBrush);
            _tile.ImagePath = imagePath;

            Tiles[y, x] = _tile;
            ChunkCanvas.Background = new SolidColorBrush(Windows.UI.Colors.Green);
            ChunkCanvas.Children.Add(_tile);
            Canvas.SetLeft(_tile, (y * 32));
            Canvas.SetTop(_tile, (x * 32));
        }

        /// <summary>
        /// Fills this chunk with the given tiletype
        /// </summary>
        /// <param name="tileType"></param>
        /// <param name="imagePath"></param>
        public void FillChunkWith(Type tileType, string imagePath)
        {
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    i = 15 - i;

                    var _tile = (Tile)Activator.CreateInstance(tileType);
                    //_tile.SetBackgroundColor(TileFillerBrush);
                    _tile.ImagePath = imagePath;

                    Tiles[j, i] = _tile;
                    ChunkCanvas.Background = new SolidColorBrush(Windows.UI.Colors.Green);
                    ChunkCanvas.Children.Add(_tile);
                    Canvas.SetLeft(_tile, (j * 32));
                    Canvas.SetTop(_tile, (i * 32));
                }
            }
        }
    }
}
