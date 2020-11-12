using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TudoMario
{
    public class Vector2 : IEquatable<Vector2>
    {
        private System.Numerics.Vector2 vector;
        public float X { get => vector.X; set => vector.X = value; }
        public float Y { get => vector.Y; set => vector.Y = value; }

        public Vector2()
        {
            vector = new System.Numerics.Vector2();
        }

        public Vector2(float x, float y)
        {
            vector = new System.Numerics.Vector2(x, y);
        }

        public float Length() => vector.Length();

        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            var v = a.vector + b.vector;
            return new Vector2(v.X, v.Y);
        }

        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            var v = a.vector - b.vector;
            return new Vector2(v.X, v.Y);
        }

        public override int GetHashCode() => vector.GetHashCode();

        public override bool Equals(object obj)
        {
            if (obj == null || !this.GetType().Equals(obj.GetType()))
                return false;
            else
            {
                Vector2 other = (Vector2)obj;
                return vector.Equals(other.vector);
            }
        }

        public bool Equals(Vector2 other) => vector.Equals(other.vector);

        public override string ToString() => $" {vector.X} | {vector.Y} ";
    }
}
