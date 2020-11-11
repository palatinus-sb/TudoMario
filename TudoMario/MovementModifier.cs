using System;

namespace TudoMario
{
    public sealed class MovementModifier
    {
        private static Func<float, float, float> add = (x, y) => x + y;
        private static Func<float, float, float> multiply = (x, y) => x * y;
        private static Func<float, float, float> setvalue = (x, y) => y;

        public Direction Direction { get; }
        public float Value { get; }
        public Mode Mode { get; }
        public Func<float, float, float> Function { get; }

        private MovementModifier(Direction direction, float value, Mode mode)
        {
            Direction = direction;
            Value = value;
            Mode = mode;
            Function = Mode switch
            {
                Mode.Additive => add,
                Mode.Multiplicative => multiply,
                Mode.Absolute => setvalue,
                _ => throw new Exception($"Invalid Mode '{Mode}'."),
            };
        }

        public static MovementModifier IceWalk = new MovementModifier(Direction.Left | Direction.Right, 1.5f, Mode.Multiplicative);
        public static MovementModifier SwampWalk = new MovementModifier(Direction.Left | Direction.Right | Direction.Up | Direction.Down, 0.5f, Mode.Multiplicative);
        public static MovementModifier SlowFall = new MovementModifier(Direction.Down, 0.5f, Mode.Multiplicative);
        public static MovementModifier JumpBoost = new MovementModifier(Direction.Up, 2f, Mode.Multiplicative);

        public static MovementModifier BlockUp = new MovementModifier(Direction.Up, 0f, Mode.Absolute);
        public static MovementModifier BlockDown = new MovementModifier(Direction.Down, 0f, Mode.Absolute);
        public static MovementModifier BlockLeft = new MovementModifier(Direction.Left, 0f, Mode.Absolute);
        public static MovementModifier BlockRight = new MovementModifier(Direction.Right, 0f, Mode.Absolute);
    }

    [Flags]
    public enum Direction { Up = 1, Down = 2, Left = 4, Right = 8 }
    public enum Mode { Absolute, Additive, Multiplicative }
}
