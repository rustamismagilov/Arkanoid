using System;

namespace ConsoleGame
{
    // A two-dimensional vector
    public class Vector2D
    {
        // The coordinate on the X-axis
        public int X { get; set; }

        // The coordinate on the Y-axis
        public int Y { get; set; }

        // Initialization of a vector by its coordinates
        public Vector2D(int x = 0, int y = 0)
        {
            Set(x, y);
        }

        // Cloning a vector
        public Vector2D(Vector2D a)
        {
            Set(a.X, a.Y);
        }

        // Check for vector equality
        // true, if the vectors are equal, false otherwise
        public bool Equals(int x, int y) => (x == X && y == Y);
        public bool Equals(Vector2D v) => Equals(v.X, v.Y);

        // Set the vector values
        public void Set(int x, int y)
        {
            X = x;
            Y = y;
        }

        // Vector addition
        public static Vector2D Sum(Vector2D a, Vector2D b)
        {
            Vector2D res = new Vector2D(a);
            res.X += b.X;
            res.Y += b.Y;
            return res;
        }

        // Vector multiplication
        public static Vector2D Mult(Vector2D a, Vector2D b)
        {
            Vector2D res = new Vector2D(a);
            res.X *= b.X;
            res.Y *= b.Y;
            return res;
        }

        // Vector multiplication by a number
        public static Vector2D Mult(Vector2D a, int num)
        {
            Vector2D res = new Vector2D(a);
            res.X *= num;
            res.Y *= num;
            return res;
        }

        // Operator overload for vector addition
        public static Vector2D operator +(Vector2D a, Vector2D b) => Sum(a, b);

        // Operator overload for vector multiplication
        public static Vector2D operator *(Vector2D a, Vector2D b) => Mult(a, b);

        // Operator overload for vector multiplication by a number
        public static Vector2D operator *(Vector2D a, int num) => Mult(a, num);
    }
}
