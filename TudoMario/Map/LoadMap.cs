using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using System.IO;

namespace TudoMario
{
    public class LoadMap
    {
        public LoadMap(string fileName)
        {
            this.fileName = fileName;
            ReadFile();
            /*// Get the app's installation folder.
            StorageFolder appFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;

            // Print the folder's path to the Visual Studio Output window.
            Debug.WriteLine(appFolder.Name + " folder path: " + appFolder.Path);*/
        }

        private string fileName;
        string[] mapCsv = new string[4160]; //need better solution

        public async void ReadFile() // need filepath
        {
            DirectoryInfo dir = new DirectoryInfo("Assets");
            var files = dir.GetFiles();
            foreach(var item in files)
            {
                if (fileName == item.Name)
                {
                   /* Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
                    Windows.Storage.StorageFile sampleFile = await storageFolder.GetFileAsync(fileName);*/
                    //string text = await Windows.Storage.FileIO.ReadTextAsync(@"ms-appx:/Assets//fileName");
                    var valami = File.ReadAllLines(@"Assets\TestMap.csv");
                    //mapCsv = text.Split(';');
                }
            }
            Windows.Storage.StorageFolder storageFolder2 = Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile file = await storageFolder2.CreateFileAsync("teszt123.txt", Windows.Storage.CreationCollisionOption.ReplaceExisting);
            int lengthOfFile = 0;
            for (int i = 0; i < mapCsv.Length; i++)
            {
                await Windows.Storage.FileIO.AppendTextAsync(file, mapCsv[i]);
                lengthOfFile = i + 1;
            }
            Debug.WriteLine(lengthOfFile);
        }
    }
}
