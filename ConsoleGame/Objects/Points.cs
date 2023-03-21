namespace ConsoleGame.Objects
{
    // points panel 
    public class Points : IObject
    {
        // points score
        public int Point { get; set; }

        // boolean flag which is used to check if the points panel has been initialized
        private bool _isInitialized = false;

        // stores the position of the cursor for rendering the points
        private Vector2D _countCursorPosition;

        // property that holds the current score
        public Points()
        {
            Point = 0;
        }

        // this method displays the score at the specified cursor position
        public void Render()
        {
            Renderer renderer = Renderer.Instance;

            if (!_isInitialized)
            {
                renderer.FillRect(
                    '-',
                    new Vector2D(0, 0),
                    new Vector2D(renderer.Width - 2, 0)
                );

                int margin = 5;

                string points_line = "Your points: ";
                _countCursorPosition = new Vector2D(points_line.Length + margin, -1);

                
                renderer.PrintLineWithMargin(points_line, margin, true);
                _isInitialized = true;
            }

            RenderPoint();

        }

        // this method sets the cursor position using the Renderer instance,
        // and then writes the number using the Write() method
        private void RenderNumber(Vector2D cursor_position, int number)
        {
            Renderer renderer = Renderer.Instance;
            renderer.SetCursorPosition(cursor_position);
            renderer.Write(number);
        }

        // displaying the amount of points
        private void RenderPoint()
        {
            RenderNumber(_countCursorPosition, Point);
        }
        
    }
}
