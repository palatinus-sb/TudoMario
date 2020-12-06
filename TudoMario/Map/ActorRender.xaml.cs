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

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace TudoMario.Map
{
    public sealed partial class ActorRender : UserControl
    {
        private ActorBase self;
        public Vector2 Position { get => self.Position; }
        public Vector2 Size { get => self.Size; }

        public BitmapImage Texture { get => self.Texture; set => self.Texture = value; }

        public ActorRender(ActorBase self)
        {
            this.self = self;

            InitializeComponent();
            Width = Size.X;
            Height = Size.Y;

            ImageControl.Width = Size.X;
            ImageControl.Height = Size.Y;

            ImageControl.Source = self.Texture;

            self.TextureChanged += TextureChanged;
        }
        public ActorRender(ActorBase self, double sizeMultiply) : this(self)
        {
            Width = Size.X * sizeMultiply;
            Height = Size.Y * sizeMultiply;

            ImageControl.Width = Size.X * sizeMultiply;
            ImageControl.Height = Size.Y * sizeMultiply;
        }

        public void TextureChanged(object sender, EventArgs e)
        {
#pragma warning disable CS4014
            Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                CoreDispatcherPriority.Normal,
                () => ImageControl.Source = self.Texture);
#pragma warning restore CS4014
        }

    }
}
