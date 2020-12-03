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
        private ActorBase self;
        public Vector2 Position { get => self.Position; }
        public Vector2 Size { get => self.Size; }

        public BitmapImage Texture { get => self.GetTexture(); set => self.SetTexture(value); }

        public ActorRender(ActorBase self)
        {
            this.self = self;

            this.InitializeComponent();
            this.Width = Size.X;
            this.Height = Size.Y;

            ImageControl.Width = Size.X;
            ImageControl.Height = Size.Y;

            ImageControl.Source = self.GetTexture();
        }
        public ActorRender(ActorBase self, double sizeMultiply) : this(self)
        {
            this.Width = Size.X * sizeMultiply;
            this.Height = Size.Y * sizeMultiply;

            ImageControl.Width = Size.X * sizeMultiply;
            ImageControl.Height = Size.Y * sizeMultiply;
        }
    }
}
