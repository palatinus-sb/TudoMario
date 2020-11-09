using System;

namespace TudoMario
{
    public sealed class MovementModifier
    {
        public Direction Direction { get; }
        public Mode Mode { get; }
        public float Value { get; }

        private MovementModifier(Direction direction, Mode mode, float value)
        {
            Direction = direction;
            Mode = mode;
            Value = value;
        }

        public static MovementModifier IceWalk = new MovementModifier(Direction.Left | Direction.Right, Mode.Multiplicative, 1.5f);
        public static MovementModifier SwampWalk = new MovementModifier(Direction.Left | Direction.Right | Direction.Up | Direction.Down, Mode.Multiplicative, 0.5f);
        public static MovementModifier SlowFall = new MovementModifier(Direction.Down, Mode.Multiplicative, 0.5f);
        public static MovementModifier JumpBoost = new MovementModifier(Direction.Up, Mode.Multiplicative, 2f);

        public static MovementModifier BlockUp = new MovementModifier(Direction.Up, Mode.Absolute, 0f);
        public static MovementModifier BlockDown = new MovementModifier(Direction.Down, Mode.Absolute, 0f);
        public static MovementModifier BlockLeft = new MovementModifier(Direction.Left, Mode.Absolute, 0f);
        public static MovementModifier BlockRight = new MovementModifier(Direction.Right, Mode.Absolute, 0f);
    }

    [Flags]
    public enum Direction { Up = 1, Down = 2, Left = 4, Right = 8 }
    public enum Mode { Absolute, Additive, Multiplicative }
}
