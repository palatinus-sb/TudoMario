using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TudoMario.Map;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using TudoMario.Rendering;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Xaml.Media.Imaging;

namespace TudoMario.Ui
{
    internal class UiController
    {
        private MainPage _main;
        private Renderer _renderer;

        private Hud CurrentHud = new Hud();
        private MainMenu MainMenu;
        private bool isMainMenuShown;
        private bool isDialogShown;

        public bool IsMainMenuShown { get => isMainMenuShown; }
        public bool IsDialogShown { get => isDialogShown; private set => isDialogShown = value; }

        public UiController(MainPage mainpage, Renderer renderer)
        {
            _main = mainpage;
            _renderer = renderer;
            this.MainMenu = new MainMenu(this);

            Init();

            ShowMap();
        }

        public event EventHandler NewButtonClicked;
        public event EventHandler LoadButtonClicked;
        public event EventHandler ExitButtonClicked;
        public void ButtonClicked(object sender, RoutedEventArgs e)
        {
            Button button = e.OriginalSource as Button;
            if (button.Content.ToString().ToLower().Contains("new"))
                NewButtonClicked?.Invoke(sender, EventArgs.Empty);
            else if (button.Content.ToString().ToLower().Contains("load"))
                LoadButtonClicked?.Invoke(sender, EventArgs.Empty);
            else
                ExitButtonClicked?.Invoke(sender, EventArgs.Empty);

        }

        public void ShowMainMenu()
        {
            _renderer.ShowMenuObject(MainMenu);
            isMainMenuShown = true;
        }
        public void RemoveMainMenu()
        {
            _renderer.RemoveMenuObject(MainMenu);
            isMainMenuShown = false;
        }

        /// <summary>
        /// Prints the text into the Hud dialogbox. Returns false if it was not possible.
        /// </summary>
        /// <param name="dialog"></param>
        public void ShowDialog(string dialog)
        {
            CurrentHud.ShowDialog(dialog);
            IsDialogShown = true;
        }

        public void RemoveDialog()
        {
            CurrentHud.RemoveDialog();
            IsDialogShown = false;
        }

        /// <summary>
        /// Only for UI testing;
        /// </summary>
        public void ShowMap()
        {
            MapBase mapBase = new MapBase(new Vector2(0, 0));
            Chunk airchunkMissingTexturetest = new Chunk();
            Chunk airChunk = new Chunk();

            BitmapImage ground = TextureHandler.GetImageByName("GroundBase");
            BitmapImage air = TextureHandler.GetImageByName("BaseBackGroung");

            BitmapImage missing = TextureHandler.GetImageByName("kekekekek");

            //airchunkMissingTexturetest.FillChunkWith(air);

            for (int i = 0; i < 16; i++)
            {
                airchunkMissingTexturetest.SetTileAt(i, 0, missing);
            }

            //airChunk.FillChunkWith(air);

            mapBase.SetChunkAt(0, 0, airchunkMissingTexturetest);
            mapBase.SetChunkAt(1, 0, airChunk);

            Chunk groundChunk = new Chunk();
            //groundChunk.FillChunkWith(ground);

            mapBase.SetChunkAt(0, -1, groundChunk);

            //_renderer.CurrentMap = mapBase;

            ShowDialog("TEST DYNAMIC DIALOG THAT IS VERY VERY LONG TO TEST THE LENGHT OF THIS DAMN THING AND I HOPE IT WILL WORK CAUSE I AM SO DONE WITH THIS PROJECT AT FKING SATURDAY 6PM REEEEEEEEE. OK IT WORKS HF GUYS");

            // RemoveDialog();
        }

        /// <summary>
        /// Initialise Ui HUD.
        /// </summary>
        private void Init()
        {

            _renderer.Hud = CurrentHud;

        }
    }
}


