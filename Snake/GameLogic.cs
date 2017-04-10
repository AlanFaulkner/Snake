using System;
using System.Collections.Generic;

namespace Snake
{
    public class GameLogic
    {
        public enum Direction { Up, Right, Down, Left }

        public Direction CurrentDirection = Direction.Up;
        public List<List<int>> GameWorld { get; set; } = new List<List<int>> { };
        public int Score = 0;

        private List<List<int>> Snake = new List<List<int>> { };
        private int SnakeSize;
        private int HeadLocation_X;
        private int HeadLocation_Y;
        private int Fruit_X;
        private int Fruit_Y;
        private Random rnd = new Random();

        public GameLogic()
        {
            InitaliseGame();
        }

        public void InitaliseGame()
        {
            SnakeSize = 4;
            HeadLocation_X = 24;
            HeadLocation_Y = 24;
            CurrentDirection = Direction.Up;

            // Build Gameboard
            GameWorld.Clear();
            for (int Rows = 0; Rows < 50; Rows++)
            {
                List<int> Row = new List<int> { };
                for (int Columns = 0; Columns < 50; Columns++)
                {
                    if (Columns == 0 || Columns == 49) { Row.Add(4); }
                    else if (Rows == 0 || Rows == 49) { Row.Add(4); }
                    else { Row.Add(0); }
                }

                GameWorld.Add(Row);
            }

            // initate snake
            Snake.Clear();
            for (int BodyPart = 0; BodyPart < SnakeSize; BodyPart++)
            {
                List<int> BodyPartLocation = new List<int> { HeadLocation_Y, HeadLocation_X };
                Snake.Add(BodyPartLocation);
            }
            GameWorld[HeadLocation_Y][HeadLocation_X] = 1;
        }

        public void PlaceFruit()
        {
            do
            {
                Fruit_X = rnd.Next(0, 50);
                Fruit_Y = rnd.Next(0, 50);
            }
            while (GameWorld[Fruit_X][Fruit_Y] != 0);

            GameWorld[Fruit_X][Fruit_Y] = 3;
        }

        public void GameMove()
        {
            MoveSnake();
        }

        public bool GameOver()
        {
            if (GameWorld[HeadLocation_Y][HeadLocation_X] == 1) { return true; }
            else if (GameWorld[HeadLocation_Y][HeadLocation_X] == 4) { return true; }
            else
            {
                if (GameWorld[HeadLocation_Y][HeadLocation_X] == GameWorld[Fruit_X][Fruit_Y])
                {
                    Score += 10;
                    SnakeSize += 2;
                    PlaceFruit();
                }
                UpdateSnake();
                UpdateGameWorld();
                return false;
            }
        }

        private void MoveSnake()
        {
            switch (CurrentDirection)
            {
                case (Direction.Up):
                    HeadLocation_Y--;
                    return;

                case (Direction.Right):
                    HeadLocation_X++;
                    return;

                case (Direction.Down):
                    HeadLocation_Y++;
                    return;

                case (Direction.Left):
                    HeadLocation_X--;
                    return;
            }
        }

        private void UpdateGameWorld()
        {
            // clear snake form game board
            for (int Rows = 0; Rows < 50; Rows++)
            {
                for (int Columns = 0; Columns < 50; Columns++)
                {
                    if (GameWorld[Rows][Columns] < 2) GameWorld[Rows][Columns] = 0;
                }
            }

            // redraw snake
            for (int BodyPart = 0; BodyPart < Snake.Count; BodyPart++)
            {
                GameWorld[Snake[BodyPart][1]][Snake[BodyPart][0]] = 1;
            }
        }

        private void UpdateSnake()
        {
            List<int> NewHeadLocation = new List<int> { HeadLocation_X, HeadLocation_Y };
            Snake.Add(NewHeadLocation);

            if (Snake.Count - 1 > SnakeSize) { Snake.RemoveAt(0); }
        }
    }
}