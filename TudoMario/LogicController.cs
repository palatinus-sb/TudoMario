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
        public LogicController(Renderer renderer,UiController uiController)
        {
            this.renderer = renderer;
            this.uiController = uiController;
            timer.Tick += OnTimerTick;
        }

        private Timer timer = new Timer(16);
        Renderer renderer;
        UiController uiController;
        bool gameStarted = false;
        bool gameEnded = false;

        public void AddActorToGame(ActorBase actorBase)
        {
            renderer.CurrentMap.AddActor(actorBase);
        }

        public void StartGame() {
            timer.Start();
            gameStarted = true;
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
            //foreach (var actor in mapbase.MapActorList)
            //    actor.Tick();
        }

        private void RenderGameState() 
        {
            Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
            () =>
            {
                renderer.Render();
            });
        }
    }
}
