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
    public sealed partial class ActorRender : UserControl
    {
        ActorBase self = new ActorBase();
        public Vector2 Position { get => self.Position;}
        public Vector2 Size { get => self.Size; }

        private BitmapImage texture;
        public BitmapImage Texture
        {
            set
            {
                texture = value;
                ImageControl.Source = value;
            }
        }
        public ActorRender(ActorBase self)
        {
            this.self = self;

            this.InitializeComponent();
            this.Width = Size.X;
            this.Height = Size.Y;

            ImageControl.Width = Size.X;
            ImageControl.Height = Size.Y;
        }
    }
}
