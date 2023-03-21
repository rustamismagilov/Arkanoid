using System;
using System.Collections.Generic;

namespace ConsoleGame
{
    // Class for binding functions to key presses
    public class KeyBindings
    {
        // Dictionary: key - ConsoleKey (key pressed);
        // value - a function that takes a Renderer object and returns a boolean value, called when the key is pressed
        private readonly Dictionary<ConsoleKey, Action> _bindings = new Dictionary<ConsoleKey, Action>();

        // Method for calling the function when the key is pressed
        public bool Exec(ConsoleKey key)
        {
            Action callback;

            // Get function from dictionary
            _bindings.TryGetValue(key, out callback);

            try
            {
                // Call function
                callback.Invoke();
            }
            catch (NullReferenceException)
            {
                // If function does not exist, return false
                return false;
            }
            return true;
        }

        // Bind handler
        public void Add(ConsoleKey key, Action callback) { _bindings.Add(key, callback); }
    }
}
