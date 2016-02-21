
//
// FILE         : Bricks.cs
// PROJECT      : FirstGame1
// DATE         : 10/8/2015
// AUTHOR       : SAMEER SAPRA
// DESCRIPTION  : This file is for all 36 bricks. It has its own draw function to draw the sprite.
//


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FirstGame1
{
    public class Bricks
    {
        //variables, constants and objects
        public Vector2 Position;
        public Texture2D Texture;
        public int Points;
        public Color color;


        /// <summary>
        /// Function name   :   public void Draw
        /// Desription      : It draws the the sprite
        /// </summary>
        /// <param name="spriteBatch">Enable the sproite to be drawn</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, color);
        }

        /// <summary>
        /// Function name   : public static bool CheckBallBrickCollision
        /// Desription      : It checks the bound of the brick with the ball. to determine whether it hit the brick or not
        /// </summary>
        /// <param name="brick">Which brick to test agaoinst the ball</param>
        /// <param name="ball">Ball which is about to hit the brick</param>
        /// <returns></returns>

        public static bool CheckBallBrickCollision(Bricks brick, Ball ball)
        {
            if (brick.Bounds.Intersects(ball.Bounds))
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// Description : Return the telemetry of a brick
        /// </summary>
        public Rectangle Bounds
        {
            get { return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height); }
        }
    }
}
