using TudoMario;

namespace TudoMarioTests
{
    class DummyActor : ActorBase
    {
        public DummyActor(string id = "") : base(id) { }

        public DummyActor(Vector2 position, Vector2 size, string id = "") : base(position, size, id) { }

        protected override void PerformBehaviour()
        {
            System.Diagnostics.Debug.WriteLine("I'm alive! (" + id + ")");
        }
    }
}
