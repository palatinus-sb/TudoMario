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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace TudoMario.Map
{
    public sealed partial class Chunk : UserControl
    {
        public Tile[,] Tiles = new Tile[16, 16];
        public Chunk()
        {
            InitializeComponent();
            Height = 512;
            Width = 512;
        }

        /// <summary>
        /// Sets the tile at the given coordinates in this chunk to the given tile type.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void SetTileAt(int x, int y, Tile tile)
        {
            /////////////////////////////////////////////////
            ///HashSet TO UNBIND PREVIOUS CHILD INCOMPLETE
            /////////////////////////////////////////////////

            //0 is the top since we go from top left so it has to be mirrored
            x = 15 - x;



            Tiles[y, x] = tile;
            //ChunkCanvas.Background = new SolidColorBrush(Windows.UI.Colors.Green);
            ChunkCanvas.Children.Add(tile);
            Canvas.SetLeft(tile, (y * 32));
            Canvas.SetTop(tile, (x * 32));
        }
        public void SetTileAt(int x, int y, BitmapImage texture)
        {
            /////////////////////////////////////////////////
            ///HashSet TO UNBIND PREVIOUS CHILD INCOMPLETE
            /////////////////////////////////////////////////

            //0 is the top since we go from top left so it has to be mirrored
            x = 15 - x;

            Tile tile = new Tile();
            tile.Texture = texture;

            Tiles[y, x] = tile;
            //ChunkCanvas.Background = new SolidColorBrush(Windows.UI.Colors.Green);
            ChunkCanvas.Children.Add(tile);
            Canvas.SetLeft(tile, (y * 32));
            Canvas.SetTop(tile, (x * 32));
        }

        /// <summary>
        /// Fills this chunk with the given tiletype
        /// </summary>
        /// <param name="tileType"></param>
        /// <param name="imagePath"></param>
        public void FillChunkWith(BitmapImage texture)
        {
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    i = 15 - i;

                    var _tile = (Tile)Activator.CreateInstance(typeof(Tile));
                    //_tile.SetBackgroundColor(TileFillerBrush);
                    _tile.Texture = texture;

                    Tiles[j, i] = _tile;
                    //ChunkCanvas.Background = new SolidColorBrush(Windows.UI.Colors.Green);
                    ChunkCanvas.Children.Add(_tile);
                    Canvas.SetLeft(_tile, (j * 32));
                    Canvas.SetTop(_tile, (i * 32));
                }
            }
        }
    }
}
