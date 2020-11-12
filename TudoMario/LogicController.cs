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

namespace TudoMario
{
    class LogicController
    {
        public LogicController(Renderer renderer)
        {
            this.renderer = renderer;
            timer.Tick += OnTimerTick;
        }

        private Timer timer = new Timer(16);
        Renderer renderer;
        bool gameStarted = false;
        bool gameEnded = false;

        public void AddActorToGame(ActorBase actorBase)
        {
            renderer.CurrentMap.AddActor(actorBase);
        }

        public void StartGame() {
            timer.Start();
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
            renderer.Render();
        }
    }
}
