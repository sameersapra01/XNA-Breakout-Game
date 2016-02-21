
//
// FILE         : Input.cs
// PROJECT      : FirstGame1
// DATE         : 10/8/2015
// AUTHOR       : SAMEER SAPRA
// DESCRIPTION  : This file needs no instantiation because its declared static.


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace FirstGame1
{
    public static class Input
    {
        //used in paddle speed boost
        public static float speed = 1.0f;
        static Input()
        {

        }

        /// <summary>
        /// Function name   : public static Vector2 GetKeyboardInputDirection
        /// Description     : it gets the keyboard input whwtehr left or right
        /// </summary>
        /// <param name="playerIndex"></param>
        /// <returns></returns>
        public static Vector2 GetKeyboardInputDirection(PlayerIndex playerIndex)
        {
            Vector2 direction = Vector2.Zero;
            KeyboardState keyboardState = Keyboard.GetState(playerIndex);

            if (playerIndex == PlayerIndex.One)
            {
                //moive to the left side
                if (keyboardState.IsKeyDown(Keys.Left))
                    direction.X += -speed;
                //move to the right
                if (keyboardState.IsKeyDown(Keys.Right))
                    direction.X += 1;
            }
            return direction;
        }
    }
}
