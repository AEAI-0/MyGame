using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TextureAtlas
{
    public class LinkMovement
    {
        public Texture2D Texture { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        private int currentFrame;
        private int totalFrames;
        private float timer;

        public LinkMovement(Texture2D texture, int rows, int columns)
        {
            Texture = texture;
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            totalFrames = Rows * Columns;
        }

        public void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timer > 0.5f)
            {
                timer = 0f;
                currentFrame = (currentFrame + 1) % totalFrames; // 循环播放
            }
        }
        public void MoveDown(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timer > 0.5f)
            {
                timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (timer > 0.5f)
                {
                    timer = 0f;
                    if (currentFrame >= 0 && currentFrame < 1)
                    {
                        currentFrame++;
                    }
                    else
                    {
                        currentFrame = 0;
                    }
                }
            }
        }
        public void MoveUp(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timer > 0.5f)
            {
                timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (timer > 0.5f)
                {
                    timer = 0f;
                    if (currentFrame >= 6 && currentFrame < 7)
                    {
                        currentFrame++;
                    }
                    else
                    {
                        currentFrame = 6;
                    }
                }
            }
        }
        public void MoveLeft(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timer > 0.5f)
            {
                timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (timer > 0.5f)
                {
                    timer = 0f;
                    if (currentFrame >= 2 && currentFrame < 3)
                    {
                        currentFrame++;
                    }
                    else
                    {
                        currentFrame = 2;
                    }
                }
            }
        }
        public void MoveRight(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timer > 0.5f)
            {
                timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (timer > 0.5f)
                {
                    timer = 0f;
                    if (currentFrame >= 4 && currentFrame < 5)
                    {
                        currentFrame++;
                    }
                    else
                    {
                        currentFrame = 4;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int row = currentFrame / Columns;
            int column = currentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);

            spriteBatch.Begin();
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
            spriteBatch.End();
        }
    }
}