

//
// FILE         : Paddle.cs
// PROJECT      : FirstGame1
// DATE         : 10/8/2015
// AUTHOR       : SAMEER SAPRA
// DESCRIPTION  : This file is for paddle sprite and inherits the Gameobject class. it has one override function i.e Move

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace FirstGame1
{
    public class Paddle:GameObject
    {
        public bool checkWallCollision = false;
   
        /// <summary>
        /// Function Name : public override void Move
        /// Description   : It overrides the move method of the base class
        /// </summary>
        /// <param name="amount">position to be updated</param>
        public override void Move(Microsoft.Xna.Framework.Vector2 amount)
        {
            base.Move(amount);
            if (!checkWallCollision)
            {
               
                if (Position.X <= 0)
                    Position.X = 0;
                if (Position.X + Texture.Width >= Game1.ScreenWidth)
                    Position.X = Game1.ScreenWidth - Texture.Width;
            }
                //if paddle tunnel boost is enabled
            else
            {
                if (Position.X < -Texture.Width / 2)
                    Position.X = Position.X + Game1.ScreenWidth + Texture.Width;
                if (Position.X > (Game1.ScreenWidth + Texture.Width / 2))
                    Position.X = Position.X - Game1.ScreenWidth - Texture.Width;
            }
        }
    }
}
