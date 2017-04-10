using System;
using System.Drawing;
using System.Windows.Forms;

namespace Snake
{
    public partial class Form1 : Form
    {
        private GameLogic Gameplay = new GameLogic();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case (Keys.W):
                    if (Gameplay.CurrentDirection != GameLogic.Direction.Down) { Gameplay.CurrentDirection = GameLogic.Direction.Up; }
                    return;

                case (Keys.D):
                    if (Gameplay.CurrentDirection != GameLogic.Direction.Left) { Gameplay.CurrentDirection = GameLogic.Direction.Right; }
                    return;

                case (Keys.S):
                    if (Gameplay.CurrentDirection != GameLogic.Direction.Up) { Gameplay.CurrentDirection = GameLogic.Direction.Down; }
                    return;

                case (Keys.A):
                    if (Gameplay.CurrentDirection != GameLogic.Direction.Right) { Gameplay.CurrentDirection = GameLogic.Direction.Left; }
                    return;
            }
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            Gameplay.GameMove();
            Score.Text = "Score: " + Gameplay.Score;
            this.Invalidate();
            if (Gameplay.GameOver())
            {
                GameTimer.Enabled = false;
                PlayButton.Enabled = true;
                MessageBox.Show("Game Over!");
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            PlayButton.Enabled = false;
            GameTimer.Enabled = true;
            GameTimer.Interval = 50;
            Gameplay.InitaliseGame();
            Gameplay.PlaceFruit();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            // clear snake form game board
            for (int Rows = 0; Rows < 50; Rows++)
            {
                for (int Columns = 0; Columns < 50; Columns++)
                {
                    if (Gameplay.GameWorld[Rows][Columns] == 1) e.Graphics.FillRectangle(Brushes.DarkGreen, Columns * 300 / 50, Rows * 300 / 50, 300 / 50, 300 / 50);
                    else if (Gameplay.GameWorld[Rows][Columns] == 3) e.Graphics.FillEllipse(Brushes.Red, Columns * 300 / 50, Rows * 300 / 50, 300 / 50, 300 / 50);
                    else if (Gameplay.GameWorld[Rows][Columns] == 4) e.Graphics.FillRectangle(Brushes.Black, Columns * 300 / 50, Rows * 300 / 50, 300 / 50, 300 / 50);
                }
            }
        }
    }
}