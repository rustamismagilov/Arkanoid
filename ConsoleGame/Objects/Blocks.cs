using System;
using System.Collections.Generic;

namespace ConsoleGame.Objects
{
    // class, which will be used for actions with the blocks
    public class Blocks : IObject
    {
        // ball object
        private Ball _ball;

        // points
        private Points _score;

        // array of blocks
        public List<Vector2D> Block { get; private set; }

        // rendered blocks
        private List<Vector2D> _renderedDots;

        // bottom line position
        private readonly int _bottomLine = Renderer.Instance.Height * 3 / 5;

        // center of the array of blocks
        public Vector2D BossCenter { get; private set; }

        // Boss rotation vector
        private Vector2D _bossRotate;

        // initializes the blocks and score
        public Blocks(Ball ball, Points score)
        {
            _ball = ball;
            _score = score;
            Block = new List<Vector2D>();
            Renderer renderer = Renderer.Instance;

            // calculates the center of the block array
            BossCenter = new Vector2D(renderer.Width / 2, renderer.Height - 1);

            // calculates the step of rotation for the boss block
            int rotate_step = renderer.Width / 10;
            if (rotate_step == 0)
                rotate_step = 1;

            // initializes the boss block rotation vector
            _bossRotate = new Vector2D(rotate_step);

            // generates the blocks
            GenerateBlocks();
        }

        // this method is responsible for rendering the blocks and checking for collision with the ball
        public void Render()
        {
            Renderer renderer = Renderer.Instance;

            // _renderedDots list is used to store the blocks that have already been rendered,
            // so that unnecessary rendering calls can be avoided
            if (_renderedDots != Block)
            {
                foreach (Vector2D Dot in Block)
                    renderer.FillRect('#', Dot);
                _renderedDots = Block;
            }

            // if the ball's Center.Y position is greater than or equal to _bottomLine - 1,
            // which means the ball is close to the bottom line
            if (_ball.Center.Y >= _bottomLine - 1)
            {
                // the method checks if there is a block at the current ball position by iterating through the Block list
                for (int i = Block.Count - 1; i >= 0; i--)
                {
                    // if a block is found at the current ball position then
                    if (_ball.Center.X == Block[i].X && _ball.Center.Y + 1 == Block[i].Y)
                    {
                        _ball.ChangeDirection();                    // the ball's direction is changed 
                        Renderer.Instance.FillRect(' ', Block[i]);  // the block is erased from the screen
                        _score.Point++;                             // the score is incremented
                        Block.RemoveAt(i);                          // and the block is removed from the Block list
                        break;
                    }
                }
            }
        }

        // this method generates a rectangular field by iterating through the x and y coordinates
        // of the renderer's width and height respectively and adding new Vector2D objects to the Block list
        public void GenerateField()
        {
            for (int x = 0; x < Renderer.Instance.Width; x++)
                for (int y = Renderer.Instance.Height - 1; y >= _bottomLine; y--)
                    Block.Add(new Vector2D(x, y));
        }

        public void GenerateTree()
        {
            Renderer renderer = Renderer.Instance;

            int[] linesLength = new int[renderer.Height - 1 - _bottomLine];

            linesLength[0] = 1;

            for (int i = 1; i < linesLength.Length - 1; i++)
                linesLength[i] = linesLength[i - 1] + (renderer.Width - 1) / linesLength.Length * i;

            for (int i = 0; i < linesLength.Length - 1; i++)
            {
                int y = renderer.Height - 1 - i;
                int left = (renderer.Width - 1) / 2 - linesLength[i] / 2;
                int right = (renderer.Width - 1) / 2 + linesLength[i] / 2;

                if (left < 0)
                    left = 0;
                if (right > renderer.Width - 1)
                    right = renderer.Width - 1;

                for (int x = left; x <= right; x++)
                    Block.Add(new Vector2D(x, y));
            }
        }

        private void BossLine(int x_start, int x_end, int y)
        {
            for (int x = x_start; x <= x_end; x++)
                Block.Add(new Vector2D(x, y));
        }

        public void GenerateBlocks()
        {
            Block.Add(BossCenter);
            int y = BossCenter.Y;

            Renderer renderer = Renderer.Instance;

            // creating arrays of blocks
            BossLine(BossCenter.X - 20, BossCenter.X + 20, y--);
            BossLine(BossCenter.X - 20, BossCenter.X + 20, y--);
            BossLine(BossCenter.X - 20, BossCenter.X + 20, y--);
            BossLine(BossCenter.X - 20, BossCenter.X + 20, y--);
            BossLine(BossCenter.X - 20, BossCenter.X + 20, y--);
        }
    }
}
