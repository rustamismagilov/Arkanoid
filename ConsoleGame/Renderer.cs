using System;
using System.Collections.Generic;
using System.Text;
using ConsoleGame.Objects;

namespace ConsoleGame
{
    // this method is responsible for rendering the game objects in a console application
    public sealed class Renderer
    {
        private static Renderer _instance;

        public static Renderer Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Renderer();
                return _instance;
            }
        }

        // representing the width of the console window
        public int Width { get; private set; }

        // representing the height of the console window
        public int Height { get; private set; }

        // animation continuation flag. if true - animation continues, false - stops
        private bool is_animation_start = false;

        // Object for binding keypress handlers
        public KeyBindings Bindings = new KeyBindings();

        // Objects in the scene
        public List<IObject> Scene = new List<IObject>();

        private Renderer()
        {
            // sets the Width and Height properties of the Renderer object to the width and height of the console window respectively
            Width = Console.WindowWidth;
            Height = Console.WindowHeight;

            // attaches key handlers to the Renderer object based on some bindings
            KeyHandlers.Attach(Bindings);

            // sets the visibility of the cursor in the console window to false,
            // so it is not visible when rendering output to the console
            Console.CursorVisible = false;
        }

        // A method that starts the rendering process
        public void Start()
        {
            is_animation_start = true;

            // An infinite loop to continuously render and handle user input
            while (true)
            {
                // If the flag to start the animation is false, break out of the loop and exit rendering
                if (!is_animation_start)
                {
                    Clear();
                    break;
                }

                // Loop through all objects in the scene and call their Render method to draw them on the screen
                for (int i = 0; i < Scene.Count; i++)
                {
                    ((IObject)Scene[i]).Render();
                }

                // Check if a key was pressed, and if so, execute the corresponding command
                while (Console.KeyAvailable)
                {
                    ConsoleKey key = Console.ReadKey(true).Key;
                    Bindings.Exec(key);
                }
            }
        }

        // Method to stop the rendering process by setting the "is_animation_start" flag to false
        public void Stop()
        {
            is_animation_start = false;
        }

        // Method to clear the console by overwriting all characters on the console with empty 
        public void DrawCanvas()
        {
            for (int i = 0; i < Height; i++)
                for (int j = 0; j < Width; j++)
                    Console.Write(' ');
        }

        // Method to clear the console, set the cursor position to (0,0) and hide the cursor
        public void Clear()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.CursorVisible = false;
        }

        // This method fills a rectangular area with a specified symbol
        public void FillRect(char symbol, Vector2D a, Vector2D b = null)
        {
            // If only one Vector2D point (a) is given, it fills a single point in the console with the symbol
            if (b == null || a.Equals(b))
            {
                Console.SetCursorPosition(a.X, this.Height - 1 - a.Y);
                Console.Write(symbol);
            }

            // Otherwise, it iterates through all points between the two Vector2D points (a and b), and sets the cursor position to each point, filling it with the specified symbol.
            else
            {
                for (int y = a.Y; y <= b.Y; y++)
                {
                    for (int x = a.X; x <= b.X; x++)
                    {
                        // If the cursor position goes outside of the console window, it is repositioned to the nearest valid position
                        if (x < 0) Console.SetCursorPosition(0, this.Height - 1 - y);
                        else if (x + 1 > Width) Console.SetCursorPosition(Width - 1, this.Height - 1 - y);
                        else
                            Console.SetCursorPosition(x, this.Height - 1 - y);
                        Console.Write(symbol);
                    }
                }
            }
        }

        // print text with an indent and the ability to split lines if they are longer than the console width
        public void PrintLineWithMargin(String line, int margin_left, bool no_br = true) // The method takes three parameters:
                                                                                         // line which is the text to be printed,
                                                                                         // margin_left which is the number of spaces to indent the text from the left edge of the console,
                                                                                         // and no_br which is a boolean value that determines whether or not to add a line break after printing the text
        {
            // checks if the margin_left value is negative and sets it to 0 if it is
            if (margin_left < 0)
                margin_left = 0;


            // If the line parameter is an empty string, the method prints a blank line with the width of the console
            if (line.Length == 0)
            {
                for (int i = 0; i < Width; i++)
                    Console.Write(' ');
                Console.WriteLine();
            }

            // If the length of the line parameter is greater than the console width minus twice the margin_left value,
            // it means that the text needs to be split into multiple lines
            else if (line.Length > Width - margin_left * 2)
            {
                List<String> words = new List<string>(line.Split(' '));

                StringBuilder str = new StringBuilder();
                int str_length = 0;

                while (words.Count > 0)
                {
                    while (str_length < Width - margin_left * 2 && words.Count > 0)
                    {
                        String word = words[0];

                        if (word.Length > Width - margin_left * 2 - str_length)
                            break;

                        words.RemoveAt(0);
                        str_length += word.Length + 1;
                        str.Append(word + ' ');
                    }

                    for (int i = 0; i < margin_left; i++)
                        Console.Write(' ');

                    Console.Write(str);

                    for (int i = 0; i < Width - margin_left - str_length; i++)
                        Console.Write(' ');

                    Console.WriteLine();
                    str.Clear();
                    str_length = 0;
                }
            }
            else
            {
                for (int i = 0; i < margin_left; i++)
                    Console.Write(' ');

                Console.Write(line);

                for (int i = 0; i < Width - margin_left - line.Length; i++)
                    Console.Write(' ');

                if (!no_br)
                    Console.WriteLine();
            }

        }

        // prints the given line string at the center of the console window
        public void PrintLineOnCenter(String line)
        {
            PrintLineWithMargin(line, (this.Width - line.Length) / 2);
        }

        // sets the console cursor position to the coordinates specified in the Vector2D
        public void SetCursorPosition(Vector2D a)
        {
            Console.SetCursorPosition(a.X, Height - 1 - a.Y);
        }

        // writes the given string line to the console output
        public void Write(string line)
        {
            Console.Write(line);
        }

        // writes the given integer line to the console output
        public void Write(int line)
        {
            Console.Write(line);
        }
    }
}
