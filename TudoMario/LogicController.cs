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
using Windows.UI.Core;
using Windows.UI.Xaml;


namespace TudoMario
{
    class LogicController
    {
        public LogicController(Renderer renderer, MapBase map, UiController uiController)
        {
            this.renderer = renderer;
            this.map = map;
            this.uiController = uiController;
            timer.Tick += OnTimerTick;
        }

        private Timer timer = new Timer(16);
        private Renderer renderer;
        private MapBase map;
        private UiController uiController;
        private bool gameStarted = false;
        private bool gameEnded = false;

        public void AddActorToGame(ActorBase actorBase)
        {
            renderer.CurrentMap.AddActor(actorBase);
        }

        public void StartGame()
        {
            timer.Start();
            gameStarted = true;
            LoadPickedMap("TestMapv2.0.csv");
        }

        public void OnTimerTick(object sender, EventArgs e)
        {
            CheckGameState();
            CheckCollisions(); // is this needed?
            ActorsPerformBeahviour();
            RenderGameState();
        }

        private void CheckGameState() { }

        private void CheckCollisions() { }

        private void ActorsPerformBeahviour()
        {
            foreach (var actor in map.MapActorList)
                actor.Tick();
        }

        private void RenderGameState()
        {
            Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
            () =>
            {
                renderer.Render();
            });
        }

        private void LoadPickedMap(string fileName)
        {
            renderer.CurrentMap = LoadMap.Load(fileName);
        }
    }
}
