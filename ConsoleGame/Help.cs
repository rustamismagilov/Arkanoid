using System;
using System.Text;
using System.Threading;

namespace ConsoleGame
{
    // Help screen class that displays game instructions
    public class Help
    {
        // Array of help text lines
        private String[] _content;

        // Constructor that reads the help text from a file
        public Help()
        {
            ReadFile();
        }

        // Displays the help screen
        public void Show()
        {
            Renderer renderer = Renderer.Instance;
            renderer.Stop();

            // Calculates the top margin of the help text
            int margin_top = (renderer.Height - _content.Length) / 2;

            if (margin_top < 0)
                margin_top = 0;

            renderer.DrawCanvas();

            // Prints the first line of the help text at the center of the screen
            renderer.SetCursorPosition(new Vector2D(0, renderer.Height - margin_top));
            renderer.PrintLineOnCenter(_content[0]);

            // Prints the rest of the help text with a margin of 3
            for (int i = 1; i < _content.Length; i++)
            {
                Thread.Sleep(0);
                renderer.PrintLineWithMargin(_content[i], 3 );
            }

            // Prints an empty line with a margin of 3
            renderer.PrintLineWithMargin("", 3);

            bool waitingForStart = true;

            // Waits for the user to press a key
            while (waitingForStart)
            {
                switch (Console.ReadKey(true).Key)
                {
                    // If the user presses spacebar, clears the console screen and starts the game
                    case ConsoleKey.Spacebar:
                        Console.Clear();
                        waitingForStart = false;
                        Start.StartGame();
                        break;

                    // If the user presses escape, clears the console screen and exits the game
                    case ConsoleKey.Escape:
                        waitingForStart = false;
                        Console.Clear();
                        break;
                }
            }
        }

        // Reads the help text from a string literal
        private void ReadFile()
        {
            String file = "ARKANOID"+
            "\n\n\n"+
            "The player controls a small platform that can be moved horizontally  " +
            "from one wall to another, preventing the ball from falling down. " +
            "Hitting a block with the ball causes the block to break." +
            "\n\n\n" +
            "Left arrow key/right arrow key - moves the platform" +
            "\n\n" +
            "Esc, Q - quit the game"+
            "\n\n\n\n" +
            "Press Space to start the game\n";
            _content = file.Split('\n');
            Console.Clear();
        }
    }
}
