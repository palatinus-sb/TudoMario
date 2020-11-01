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

        public UserControlHandler userControl = new UserControlHandler();
        private Timer timer = new Timer(16);
        Renderer renderer;
        bool gameStarted = false;
        bool gameEnded = false;

        public void AddActorToGame(ActorBase actorBase)
        {
            renderer.CurrentMap.AddActor(actorBase);
        }

        public void OnTimerTick(object sender, EventArgs e) 
        {
            CheckGameState();
            CheckCollisions();
            AiDecideMovement();
            //UserMovementBasedOnHandler(); 
            //ApplyPhysicsOnActiveActors();
            RenderGameState();        
        }

        private void CheckGameState() { }

        private void CheckCollisions() { }

        private void AiDecideMovement() { }

        private void RenderGameState() 
        {
            renderer.Render();
        }

        public void UserMovementBasedOnHandler(ActorBase actor)
        {
            //Vertical movement
            if (this.userControl.PressedKeys.Contains(KeyAction.UP)) {
                actor.MovementSpeed = new Vector2(actor.MovementSpeed.X, actor.MovementSpeed.Y + PhysicsController.JumpHeight);
            }

            //Horizontal movement
            if (this.userControl.PressedKeys.Contains(KeyAction.RIGHT)) {
                actor.MovementSpeed = new Vector2(actor.MovementSpeed.X + PhysicsController.Movement, actor.MovementSpeed.Y);
            } else if (this.userControl.PressedKeys.Contains(KeyAction.LEFT)) {
                actor.MovementSpeed = new Vector2(actor.MovementSpeed.X - PhysicsController.Movement, actor.MovementSpeed.Y);
            } else {
                actor.MovementSpeed = new Vector2(0, actor.MovementSpeed.Y);
            }
        }

        private void ApplyPhysicsOnActiveActors(ActorBase actor) 
        {
            PhysicsController.ApplyFrictionOnGround(actor);
            PhysicsController.ApplySpeedLimitOnGround(actor);
            PhysicsController.ApplyGravity(actor);
            actor.Position = new Vector2(actor.Position.X + actor.MovementSpeed.X, actor.Position.Y + actor.MovementSpeed.Y);
        }
    }
}
