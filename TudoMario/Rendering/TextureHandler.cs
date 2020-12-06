using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;

namespace TudoMario.Rendering
{
    public static class TextureHandler
    {
        private static List<Tuple<string, BitmapImage>> TextureList = new List<Tuple<string, BitmapImage>>();
        private static BitmapImage MissingTexture;

        /// <summary>
        /// Loads the static textures. Only RENDERER should use this method.
        /// </summary>
        public static void Init()
        {
            MissingTexture = new BitmapImage(new Uri(@"ms-appx:/Assets//missing.png"));

            string uriName = @"ms-appx:/Assets//" + "missing.png";
            MissingTexture = new BitmapImage(new Uri(uriName));

            LoadTexturesFromLocal();
        }

        private static void LoadTexturesFromLocal()
        {
            TextureList.Clear();

            DirectoryInfo dir = new DirectoryInfo("Assets");
            var files = dir.GetFiles();
            foreach (var file in files)
            {
                string name = file.Name.ToLower().Replace(".png", "");
                name = name.ToLower().Replace(".jpg", "");

                string uriName = @"ms-appx:/Assets//" + file.Name;

                Tuple<string, BitmapImage> toAdd = new Tuple<string, BitmapImage>(name, new BitmapImage(new Uri(uriName)));
                TextureList.Add(toAdd);
            }
        }

        public static BitmapImage GetImageByName(string searchedName)
        {
            searchedName = searchedName.ToLower();

            var _tuple = TextureList.Where(tuple => tuple.Item1 == searchedName).SingleOrDefault();
            if (_tuple == null)
                return MissingTexture;

            var bitmap = _tuple.Item2;
            return bitmap;
        }
        public static BitmapImage GetMissingTexture()
        {
            return MissingTexture;
        }
    }
}
