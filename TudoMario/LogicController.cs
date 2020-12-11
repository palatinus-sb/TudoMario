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
using Windows.ApplicationModel.Core;
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

            //timer.Tick += OnTimerTick;
            renderer.MapFinishedLoading += OnMapFinishedLoading;
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
            LoadMap.SwitchMap += OnSwitchMap;
        }

        private Timer timer;
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
            LoadPickedMap(LoadMap.CurrentLevel);

            uiController.ShowMainMenu();
            //watch.Start();
            gameStarted = true;

        }

        public void OnTimerTick(object sender, EventArgs e)
        {
            //Debug.WriteLine(watch.ElapsedMilliseconds);
            watch.Restart();
            CheckGameState().Wait();
            ActorsPerformBeahviour();
            RenderGameState().Wait();

        }

        public void OnMapFinishedLoading(object sender, EventArgs e)
        {
            timer = new Timer(16);
            timer.Start();

            timer.Tick += OnTimerTick;
        }

        public async void NewButtonClicked(object sender, EventArgs e)
        {
            Debug.WriteLine(map.MainPlayer.Position.X + " : " + map.MainPlayer.Position.Y);
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                CoreDispatcherPriority.Normal,
                () => LoadPickedMap(0));
        }
        public async void LoadButtonClicked(object sender, EventArgs e)
        {
            await TmpOnSwitchMap();
        }
        public void ExitButtonClicked(object sender, EventArgs e)
        {
            CoreApplication.Exit();
        }
        public void CoreWindow_KeyDown(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs e)
        {
            VirtualKey pressedKey = e.VirtualKey;
            if (pressedKey == VirtualKey.Escape || pressedKey == VirtualKey.GamepadB)
            {
                if (uiController.IsMainMenuShown)
                    uiController.RemoveMainMenu();
                else if (uiController.IsDialogShown)
                    uiController.RemoveDialog();
                else if (!uiController.IsMainMenuShown)
                    uiController.ShowMainMenu();
            }
        }

        public void OnSwitchMap(object sender, EventArgs e)
        {
            TmpOnSwitchMap().Wait();
            //ColliderBase.ClearAllColliders();
        }
        private async Task TmpOnSwitchMap()
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                CoreDispatcherPriority.Normal,
                () => LoadPickedMap(LoadMap.CurrentLevel));
        }

        private async Task CheckGameState()
        {
            if (!gameStarted)
                return;
            if (gameEnded)
            {
                timer.Stop();
            }
            else if (!map.MainPlayer.IsAlive)
            {
                await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                CoreDispatcherPriority.Normal,
                () => LoadPickedMap(LoadMap.CurrentLevel));
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

        private async Task RenderGameState()
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                 CoreDispatcherPriority.Normal,
                 () => renderer.Render());
        }

        private void LoadPickedMap(int level)
        {
            if (timer != null)
                timer.Stop();
            ColliderBase.ClearAllColliders();
            LoadMap.PreLoad(level);
            LoadMap.PostLoad();

            var tempMap = LoadMap.map;
            map = tempMap;

            map.MainPlayer.MovementSpeed.Y = 0;
            map.MainPlayer.MovementSpeed.X = 0;

            renderer.CurrentMap = tempMap;
        }
    }
}
