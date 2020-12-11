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

            timer.Tick += OnTimerTick;
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
        }

        private Timer timer = new Timer(16);
        private Renderer renderer;
        private MapBase map;
        private UiController uiController;
        //private bool gameStarted = false;
        private bool gameEnded = false;
        private Stopwatch watch = new Stopwatch();

        public void AddActorToGame(ActorBase actorBase)
        {
            renderer.CurrentMap.AddActor(actorBase);
        }

        public void StartGame()
        {
            timer.Start();
            LoadPickedMap(LoadMap.currentLevel);
            uiController.ShowMainMenu();
            watch.Start();
        }

        public void OnTimerTick(object sender, EventArgs e)
        {
            Debug.WriteLine(watch.ElapsedMilliseconds);
            watch.Restart();
            CheckGameState();
            ActorsPerformBeahviour();
            RenderGameState();
        }

        public void NewButtonClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
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
                else
                    uiController.ShowMainMenu();
            }
        }

        private void CheckGameState()
        {
            if (gameEnded)
            {
                timer.Stop();
            }
        }

        private void ActorsPerformBeahviour()
        {
            if (renderer?.CurrentMap?.MapActorList is null)
                return;

            foreach (var actor in renderer.CurrentMap.MapActorList)
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

        private void LoadPickedMap(int level)
        {
            renderer.CurrentMap = LoadMap.PreLoad(level);
        }
    }
}
