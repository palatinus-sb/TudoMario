using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TudoMario
{
    public class Vector2
    {
        private System.Numerics.Vector2 vector;
        public float X { get => vector.X; set => vector = new System.Numerics.Vector2(value, vector.Y); }
        public float Y { get => vector.Y; set => vector = new System.Numerics.Vector2(vector.X, value); }

        public Vector2()
        {
            vector = new System.Numerics.Vector2();
        }

        public Vector2(float x, float y)
        {
            vector = new System.Numerics.Vector2(x, y);
        }

        public float Length() => vector.Length();

        public override string ToString()
        {
            return $" {vector.X} | {vector.Y} ";
        }
    }
}
