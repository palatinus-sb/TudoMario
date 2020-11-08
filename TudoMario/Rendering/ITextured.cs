using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace TudoMario.Rendering
{
    interface ITextured
    {
        BitmapImage GetTexture();
        void SetTexture();
    }
}
