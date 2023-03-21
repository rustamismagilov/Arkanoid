using System;

namespace ConsoleGame.Objects
{
    // class, which will be used for actions with the paddle
    public class Board : IObject
    {
        // the character used to represent the paddle on the screen
        private char symbol = '-';

        // the size of the paddle (maximum offset from the center point)
        public int Size { get; private set; }

        // the position of the center point of the paddle
        public Vector2D Center { get; private set; }

        // paddle initialization
        // this method sets the initial position and size of the paddle
        public Board()
        {
            Size = 4;
            Renderer renderer = Renderer.Instance;
            ResetPosition();

            // binds the left and right arrow keys to the Left() and Right() methods respectively
            renderer.Bindings.Add(ConsoleKey.LeftArrow, Left);
            renderer.Bindings.Add(ConsoleKey.RightArrow, Right);
            Render();
        }

        // this method resets the position of the paddle to the center of the screen
        public void ResetPosition()
        {
            Center = new Vector2D(Renderer.Instance.Width / 2, 2);
        }

        // this method clears the screen of the paddle's previous position
        public void Clear()
        {
            Renderer renderer = Renderer.Instance;
            Vector2D a = Center + new Vector2D(-Size, 0);
            Vector2D b = Center + new Vector2D(Size, 0);

            renderer.FillRect(' ', a, b);
        }

        // this method renders the paddle on the screen
        public void Render()
        {
            Renderer renderer = Renderer.Instance;

            Vector2D a = Center + new Vector2D(-Size, 0);
            Vector2D b = Center + new Vector2D(Size, 0);

            renderer.FillRect(symbol, a, b);
        }

        // this method handles the left arrow key press, moving the paddle to the left
        private void Left()
        {
            symbol = ' ';
            Render();

            if (Center.X > Size + 1)
                Center += new Vector2D(-3, 0);
            symbol = '-';

            Render();
        }

        // this method handles the right arrow key press, moving the paddle to the right
        private void Right()
        {
            symbol = ' ';

            Render();

            Renderer renderer = Renderer.Instance;

            if (Center.X < renderer.Width - 2 - Size)
                Center += new Vector2D(3, 0);
            symbol = '-';

            Render();
        }
    }
}
