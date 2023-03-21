using System;

namespace ConsoleGame
{
    // Handles key presses
    public class KeyHandlers
    {
        // Exit the game when Esc or Q is pressed
        private static void Quit()
        {
            Renderer.Instance.Stop();
        }

        // Initializes key bindings
        public static void Attach(KeyBindings bindings)
        {
            // Add key bindings to quit the game
            bindings.Add(ConsoleKey.Escape, Quit);
            bindings.Add(ConsoleKey.Q, Quit);
        }
    }
}
