using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TudoMario.Rendering;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using System.Threading.Tasks;

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
                SetImageSource(value).Wait();
            }
        }
        public Tile()
        {
            InitializeComponent();
            Width = 32;
            Height = 32;
        }
#pragma warning disable CS1998
        public static async Task<Tile> TileAsync()
        {
            Tile t = null;
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                CoreDispatcherPriority.Normal,
                () => t = new Tile());
            return t;
        }
#pragma warning restore CS1998
        public Tile(Vector2 Position) : this()
        {
            TilePosition = Position;
        }
        public Tile(Vector2 Position, Chunk Parent) : this(Position)
        {
            ChunkParent = Parent;
        }
#pragma warning disable CS1998
#pragma warning disable CS4014
        private async Task SetImageSource(BitmapImage bmi)
        {
            Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                CoreDispatcherPriority.Normal,
                () => ImageControl.Source = bmi);
        }
    }
}
