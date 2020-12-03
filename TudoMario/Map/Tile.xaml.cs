using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TudoMario.Rendering;
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
    public sealed partial class Tile : UserControl
    {
        public Vector2 TilePosition { get; set; }
        public Chunk ChunkParent { get; set; }
        private BitmapImage texture;
        public BitmapImage Texture
        {
            set
            {
                texture = value;
                ImageControl.Source = value;
            }
        }
        public Tile()
        {
            InitializeComponent();
            Width = 32;
            Height = 32;


            //ImageControl.Width = 200;
            //ImageControl.Height = 200;
        }
        public Tile(Vector2 Position) : this()
        {
            TilePosition = Position;
        }
        public Tile(Vector2 Position, Chunk Parent) : this(Position)
        {
            ChunkParent = Parent;
        }
    }
}
