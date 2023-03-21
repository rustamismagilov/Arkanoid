using System;
using ConsoleGame.Objects;

namespace ConsoleGame
{
    // this class represents the final screen that is displayed to the player when the game is over
    public class FinalScreen
    {
        // holds the score of the game
        private Points _score;

        // initialize the amount of points
        public FinalScreen(Points score)
        {
            _score = score;
        }

        // this method responsible for displaying the final screen
        public void Show()
        {
            Renderer renderer = Renderer.Instance;
            renderer.Stop(); // stops the renderer

            // clear the console screen and calculate the top margin of the screen to center the text
            int margin_top = (renderer.Height - 6) / 2;

            if (margin_top < 0)
                margin_top = 0;

            // displays the game over message along with the score of the game
            renderer.SetCursorPosition(new Vector2D(0, renderer.Height - margin_top));
            renderer.PrintLineOnCenter("YOU'RE LOST!");
            renderer.PrintLineOnCenter("Your scores: " + _score.Point);
            renderer.PrintLineOnCenter("Press ESCAPE to exit");

            // wait for the user to press the spacebar to exit the game
            while (Console.ReadKey(true).Key != ConsoleKey.Spacebar) ;
        }
    }
}
