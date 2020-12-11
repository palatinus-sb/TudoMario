using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using TudoMario.Map;
using TudoMario.Rendering;
using TudoMario.Ui;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;


namespace TudoMario
{
    internal class LogicController
    {
        public LogicController(Renderer renderer, MapBase map, UiController uiController)
        {
            this.renderer = renderer;
            this.map = map;
            this.uiController = uiController;

            uiController.NewButtonClicked += NewButtonClicked;
            uiController.LoadButtonClicked += LoadButtonClicked;
            uiController.ExitButtonClicked += ExitButtonClicked;

            LoadMap.UiControl = uiController;

            timer.Tick += OnTimerTick;
            renderer.MapFinishedLoading += OnMapFinishedLoading;
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
        }

        private Timer timer = new Timer(16);
        private Renderer renderer;
        private MapBase map;
        private UiController uiController;
        private bool gameStarted = false;
        private bool gameEnded = false;
        private Stopwatch watch = new Stopwatch();
        private int currentLevel = 0;

        public void AddActorToGame(ActorBase actorBase)
        {
            map.AddActor(actorBase);
        }

        public void StartGame()
        {

            /// 3 = 6 HUH
            LoadMap.CurrentLevel = currentLevel;
            LoadPickedMap(LoadMap.CurrentLevel).Wait();

            uiController.ShowMainMenu();
            watch.Start();
            gameStarted = true;
        }

        public void OnTimerTick(object sender, EventArgs e)
        {
            //Debug.WriteLine(watch.ElapsedMilliseconds);
            watch.Restart();
            CheckGameState();
            ActorsPerformBeahviour();
            RenderGameState();

        }

        public void OnMapFinishedLoading(object sender, EventArgs e)
        {
            timer.Start();
        }

        public void NewButtonClicked(object sender, EventArgs e)
        {
            Debug.WriteLine("Player x: " + renderer.CurrentMap.MainPlayer.Position.X);
            Debug.WriteLine("Player y: " + renderer.CurrentMap.MainPlayer.Position.Y);
        }
        public void LoadButtonClicked(object sender, EventArgs e)
        {
            LoadMap.SaveCurrentLevel();
        }
        public void ExitButtonClicked(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
        public void CoreWindow_KeyDown(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs e)
        {
            VirtualKey pressedKey = e.VirtualKey;
            if (pressedKey == VirtualKey.Escape)
            {
                if (uiController.IsMainMenuShown)
                    uiController.RemoveMainMenu();
                else if (uiController.IsDialogShown)
                    uiController.RemoveDialog();
                else if (!uiController.IsMainMenuShown)
                    uiController.ShowMainMenu();
            }
        }

        private void CheckGameState()
        {
            if (!gameStarted)
                return;
            if (gameEnded)
            {
                timer.Stop();
            }
            else if (!map.MainPlayer.IsAlive)
            {
                timer.Stop();
                LoadPickedMap(LoadMap.CurrentLevel).Wait();

            }
            /*if (currentLevel != LoadMap.CurrentLevel)
            {
                currentLevel = LoadMap.CurrentLevel;
                LoadPickedMap(LoadMap.CurrentLevel).Wait();
            }*/
        }

        private void ActorsPerformBeahviour()
        {
            if (map?.MapActorList is null)
                return;

            foreach (var actor in map.MapActorList)
            {
                actor.Tick();
            }
        }

        private void RenderGameState()
        {
            //Rendering can happen async without waiting.
#pragma warning disable CS4014
            Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                CoreDispatcherPriority.Normal,
                () => renderer.Render());
#pragma warning restore CS4014
        }

        /* private async Task LoadPickedMap(int level)
         {

             await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                 CoreDispatcherPriority.Normal,
                 () =>
                 {
                     LoadMap.PreLoad(level);
                     LoadMap.PostLoad();

                     this.map = LoadMap.map;
                     renderer.CurrentMap = LoadMap.map;
                 });
             /*LoadMap.PreLoad(level);
             LoadMap.PostLoad();

             this.map = LoadMap.map;
             renderer.CurrentMap = LoadMap.map;
         }*/

#pragma warning disable CS1998
#pragma warning disable CS4014
        private async Task LoadPickedMap(int level)
        {
            LoadMap.PreLoad(level);
            LoadMap.PostLoad();

            this.map = LoadMap.map;
            var tempMap = LoadMap.map;
            Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                CoreDispatcherPriority.Normal,
                () => renderer.CurrentMap = tempMap);
        }
    }
}
