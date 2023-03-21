using System;

namespace ConsoleGame.Objects
{
    // class, which will be used for actions with the ball
    public class Ball : IObject
    {
        // this enum represents the possible states of the ball during gameplay
        enum State
        {
            StandOnBoard,   // the StandOnBoard state means that the ball is stationary on the paddle
            Moving,         // the Moving state means that the ball is currently in motion
            Stop            // the Stop state means that the ball has stopped moving
        }

        // this private field represents the current state of the ball
        private State _state = State.StandOnBoard;

        // this private field represents the current frame number for ball movement,
        // which is used to calculate the speed and direction of the ball
        private int _movingStep = 0;

        // this private constant field represents the character symbol used to represent the ball in the game
        private readonly char _symbol = 'o';

        // this public property represents the direction of the ball's movement in the game
        public Vector2D Direction { get; private set; }

        // this private field represents the paddle object in the game,
        // which is used to calculate collisions and update the ball's movement direction
        private readonly Board _board;

        // this private field represents panel with (in the left lower corner in-game),
        // which is used to update the player's score during gameplay
        private readonly Points _score;

        // this property represents the current location of the ball on the game board,
        // which is updated during gameplay as the ball moves and collides with game objects
        public Vector2D Center { get; private set; }

        // this method initializes a new instance of the Ball class, which represents the game ball
        public Ball(Board board, Points score) // the method takes a Board object and a Points object as parameters, which will be used to keep track of the paddle and the player's score, respectively
        {
            _board = board;
            _score = score;
            Direction = new Vector2D(1, 1); // the Direction property is set to a new Vector2D object with coordinates (1,1), which represents the initial direction of the ball
            ResetPosition(); // the ResetPosition method is called to set the ball's initial position on the board
            Renderer.Instance.Bindings.Add(ConsoleKey.Spacebar, StartMoving); // key binding is added to the Renderer.Instance.Bindings dictionary to allow the player to start moving the ball using the spacebar
        }

        // this method sets the ball's center position to the center of the board plus a Vector2D with coordinates (1, 0)
        public void ResetPosition()
        { Center = _board.Center + new Vector2D(1, 0); }

        // this method clears the screen from the ball object by filling it with the space character (' ')
        // at its current center position using the Renderer.Instance.FillRect() method
        public void Clear() { Renderer.Instance.FillRect(' ', Center); }

        // this method handles the rendering of the ball object
        public void Render()
        {
            Vector2D vector_2 = new Vector2D(Center); // create a new Vector2D object based on the Center property of the Ball, which represents its position on the game board

            switch (_state) // checks the current state of the Ball
            {
                case State.StandOnBoard:
                    vector_2 = _board.Center + new Vector2D(0, 1); // if the ball is currently standing on the board (State.StandOnBoard), its position is set to the center of the board plus an offset of (0, 1)
                    break;
                case State.Moving: // if the ball is currently moving (State.Moving), the method checks if the ball has collided with any of the walls or with the paddle
                    if (_movingStep > 250)
                    {
                        // if the ball has collided with the left or right walls, the X component of its direction vector is inverted
                        if (vector_2.X >= Renderer.Instance.Width - 1 || vector_2.X <= 0)
                            Direction *= new Vector2D(-1, 1); // invert the coordinate of the direction of movement along X axis

                        // if the ball has collided with the top wall, the Y component of its direction vector is inverted
                        else if (vector_2.Y >= Renderer.Instance.Height - 1)
                            Direction *= new Vector2D(1, -1); // invert the coordinate of the direction of movement along Y axis

                        // If the ball has collided with the paddle, its direction vector is adjusted based on which part of the board the ball hit
                        else if (
                            vector_2.Y <= 4 &&
                            vector_2.Y == _board.Center.Y + 1 &&
                            vector_2.X >= _board.Center.X - _board.Size &&
                            vector_2.X <= _board.Center.X + _board.Size
                             )
                        {
                            if (Direction.Equals(new Vector2D(1, -1)))
                                Direction = new Vector2D(1, 1);
                            else if (Direction.Equals(new Vector2D(-1, -1)))
                                Direction = new Vector2D(-1, 1);
                        }
                        else if (vector_2.Y <= 3) // if the ball has reached the bottom of the screen without colliding with the paddle, the game is over and the FinalScreen is displayed
                        {
                            _state = State.Stop;

                            FinalScreen screen = new FinalScreen(_score);
                            screen.Show();
                        }
                        vector_2 += Direction; //the ball's position is updated based on its current direction vector and the _movingStep counter is incremented

                        _movingStep = 0;
                    }
                    else
                        _movingStep++;

                    break;
            }
            // if the ball has moved to a new position, the Renderer.Instance.FillRect method is used to redraw the ball on the screen and update the Center property of the Ball object
            if (!vector_2.Equals(Center))
            {
                Renderer.Instance.FillRect(' ', Center);
                Renderer.Instance.FillRect(_symbol, vector_2);
                Center = vector_2;
            }
        }

        // change the direction of the ball by reflecting it across the X or Y axis
        // this method is called when the ball hits the paddle
        public void ChangeDirection()
        {
            // create a new Vector2D based on the current direction of the ball,
            // and then changes that direction based on which quadrant it was in before the collision
            Vector2D v = new Vector2D(Direction);

            // for example, if the ball was moving up and to the right (Direction = (1,1)), it will be reflected down and to the right (Direction = (1,-1))
            if (v.X == 1 && v.Y == 1)
                v = new Vector2D(1, -1);
            else if (v.X == 1 && v.Y == -1)
                v = new Vector2D(-1, -1);
            else if (v.X == -1 && v.Y == -1)
                v = new Vector2D(-1, 1);
            else if (v.X == -1 && v.Y == 1)
                v = new Vector2D(1, -1);

            Direction = v;
        }

        // this method is called when the player presses the Spacebar
        private void StartMoving()
        {
            // it changes the state of the Ball object to State.Moving,
            // which allows the ball to start moving on the game board
            _state = State.Moving;
        }
    }
}
