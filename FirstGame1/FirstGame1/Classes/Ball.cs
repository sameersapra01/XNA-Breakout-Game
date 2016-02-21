

//
// FILE         : Ball.cs
// PROJECT      : FirstGame1
// DATE         : 10/8/2015
// AUTHOR       : SAMEER SAPRA
// DESCRIPTION  : This file is for a ball object inherits the abstract Gameobjct class. It overrides one function form the base class i.e Move()
//


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.IO;

namespace FirstGame1
{
    public class Ball:GameObject
    {
        //variables, constants and objects
        public Vector2 Velocity;
        public Random random;
        public bool playerLost;
        public int turns;
        public bool didHitTop = false;
        public float ballSpeed = 0.6f;

        public Ball()
        {
            random = new Random();
            playerLost = false;
            turns = 3;
        }

        /// <summary>
        /// Function name : public void Launch
        /// Description   : This function is used to positoin the ball initially. Is called whenever ball touches the bottom of the screen or to start a new game. 
        /// </summary>
        /// <param name="speed">initial speed of the ball</param>
        public void Launch(float speed)
        {
            Position = new Vector2(Game1.ScreenWidth / 2 - (Texture.Width) / 2, Game1.ScreenHeight - (Texture.Height * 4));

            Velocity.X = ballSpeed;
            Velocity.Y = ballSpeed;

            // 50% chance whether it launches left or right
            if (random.Next(2) == 1)
            {
                Velocity.X *= -1; //launch to the left
            }
            Velocity *= speed;
        }

        /// <summary>
        ///  Function name :  public void CheckWallCollision
        ///  Description   : It checks the collision of the ball and wall
        /// </summary>
        public void CheckWallCollision()
        {
            if (Position.Y < 0)
            {
                Position.Y = 0;
                Velocity.Y *= -1;
                didHitTop = true;
            }
            //player loses
            if (Position.Y + Texture.Height > Game1.ScreenHeight)
            {
                playerLost = true;
                turns--;
            }
            if(Position.X<0)
            {
                Position.X = 0;
                Velocity.X *= -1;
            }
            if(Position.X+Texture.Width > Game1.ScreenWidth)
            {
                Position.X = Game1.ScreenWidth - Texture.Width;
                Velocity.X *= -1;
            }           
        }

      /// <summary>
        /// Function name : public override void Move
        /// Description   : It changes the position of the ball by calling the base methodd 
      /// </summary>
      /// <param name="amount"></param>
        public override void Move(Vector2 amount)
        {      
            base.Move(amount);        
            CheckWallCollision();
        }
    }
}
