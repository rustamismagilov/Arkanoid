using System;
using ConsoleGame.Objects;

namespace ConsoleGame
{
    internal class Start
    {
        // Starts the game
        public static void StartGame()
        {
            // Creates a renderer instance
            Renderer renderer = Renderer.Instance;

            // Draws the game canvas
            renderer.DrawCanvas();

            // Starts the game
            renderer.Start();
        }

        // Entry point of the program
        public static void Main(string[] args)
        {
            // Clears the console
            Console.Clear();

            // Creates a renderer instance
            Renderer renderer = Renderer.Instance;

            // Initializes the game objects
            Points score = new Points();
            Board board = new Board();
            Ball ball = new Ball(board, score);
            Blocks blocks = new Blocks(ball, score);

            // Adds the game objects to the renderer's scene
            renderer.Scene.Add(score);
            renderer.Scene.Add(ball);
            renderer.Scene.Add(blocks);
            renderer.Scene.Add(board);

            // Shows the help screen and then clears the console
            Help help = new Help();
            help.Show();
            Console.Clear();
        }
    }
}
