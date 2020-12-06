using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace TudoMario.Rendering
{
    public interface ITextured
    {
        public event EventHandler TextureChanged;
        public BitmapImage Texture { get; set; }

        /// <summary>
        /// Fires TextureChanged event when SetTexture is performed.
        /// </summary>
        public void OnTextureChanged();
    }
}
