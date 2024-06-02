using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace FinCodeUnd
{
    public partial class Game : Form
    {
        Image player;
        List<string> playerMovements = new List<string>();
        int steps = 0;
        int slowDownFrameRate = 0;
        bool goLeft, goRight, goUp, goDown;
        int playerX = 500;
        int playerY = 500;
        int playerHeight = 160;
        int playerWidth = 100;
        int playerSpeed = 20;

        public Game()
        {
            InitializeComponent();
            SetUp();
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
            }
            if (e.KeyCode == Keys.Up)
            {
                goUp = true;
            }
            if (e.KeyCode == Keys.Down)
            {
                goDown = true;
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
            if (e.KeyCode == Keys.Up)
            {
                goUp = false;
            }
            if (e.KeyCode == Keys.Down)
            {
                goDown = false;
            }
        }

        private void FormPaintEvent(object sender, PaintEventArgs e)
        {
            Graphics Canvas = e.Graphics;
            int offsetX = -(playerX - this.ClientSize.Width / 2);
            int offsetY = -(playerY - this.ClientSize.Height / 2);
            Canvas.DrawImage(this.BackgroundImage, offsetX, offsetY, this.BackgroundImage.Width, this.BackgroundImage.Height);
            Canvas.DrawImage(player, this.ClientSize.Width / 2 - playerWidth / 2, this.ClientSize.Height / 2 - playerHeight / 2, playerWidth, playerHeight);
        }

        private void TimerEvent(object sender, EventArgs e)
        {
            if (goLeft && playerX > 0)
            {
                playerX -= playerSpeed;
                AnimatePlayer(4, 7);
            }
            else if (goRight && playerX + playerWidth < this.BackgroundImage.Width)
            {
                playerX += playerSpeed;
                AnimatePlayer(8, 11);
            }
            else if (goUp && playerY > 0)
            {
                playerY -= playerSpeed;
                AnimatePlayer(12, 15);
            }
            else if (goDown && playerY + playerHeight < this.BackgroundImage.Height)
            {
                playerY += playerSpeed;
                AnimatePlayer(0, 3);
            }
            else
            {
                AnimatePlayer(0, 0);
            }
            this.Invalidate();
        }

        private void SetUp()
        {
            this.BackgroundImage = Image.FromFile("bg_big.jpg");
            this.BackgroundImageLayout = ImageLayout.Center;
            this.DoubleBuffered = true;
            playerMovements = Directory.GetFiles("player", "*.png").ToList();
            player = Image.FromFile(playerMovements[0]);
        }

        private void AnimatePlayer(int start, int end)
        {
            slowDownFrameRate += 1;
            if (slowDownFrameRate == 4)
            {
                steps++;
                slowDownFrameRate = 0;
            }
            if (steps > end || steps < start)
            {
                steps = start; 
            }
            player = Image.FromFile(playerMovements[steps]);
        }
    }
}