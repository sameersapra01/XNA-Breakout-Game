
//
// FILE         : GameObject.cs
// PROJECT      : FirstGame1
// DATE         : 10/8/2015
// AUTHOR       : SAMEER SAPRA
// DESCRIPTION  : This file is the parent class with one virtual function which is override in all subclasses.



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Threading.Tasks;

namespace FirstGame1
{
    public class GameObject
    {

        //variables, constants and objects
        public Vector2 Position;
        public Texture2D Texture;
  
        /// <summary>
        /// Fucntion name  : public void Draw
        /// Desription     : Draws the sprite
        /// </summary>
        /// <param name="spriteBatch">Enable the sprite to be drawn</param>
        public void Draw(SpriteBatch spriteBatch)
        {    
            spriteBatch.Draw(Texture, Position, Color.White);
        }

        /// <summary>
        /// Fucntion name  :  public virtual void Move
        /// Desription     : Updates the posiion of the sprite
        /// </summary>
        /// <param name="amount">Postition to be updated</param>

        public virtual void Move(Vector2 amount)
        {
            Position += amount;
        }

        /// <summary>
        /// Description : Return the telemetry of a paddle
        /// </summary>
        public Rectangle Bounds
        {
            get { return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height); }
        }

    
        /// <summary>
        ///  Fucntion name  : public static bool CheckPaddleBallCollision
        /// Description     : Check the collison of the ball and the paddle
        /// </summary>
        /// <param name="paddle">paddle object</param>
        /// <param name="ball">ball object</param>
        /// <returns></returns>
        public static bool CheckPaddleBallCollision(Paddle paddle, Ball ball)
        {
            if(paddle.Bounds.Intersects(ball.Bounds))
            {
                return true;
            }
            return false;
        }
    }
}
