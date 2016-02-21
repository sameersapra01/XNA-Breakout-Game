
//
// FILE         : Game1.cs
// PROJECT      : FirstGame1
// DATE         : 10/8/2015
// AUTHOR       : SAMEER SAPRA
// DESCRIPTION  : This project is a simple version of breakout. It has some boosts like paddle speed, paddle tunnel and multi ball. It has 36 bricks to hit with a single ball. Whenever
//                ball hits a brick, that brick is disappeared adding points according to the brick hit by the ball. User gets 3 chance to clear 2 screens of 36 bricks. 
//



using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace FirstGame1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {

        //variables, constants and objects
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;
        List<Bricks> isDrawn = new List<Bricks>();
        Paddle paddle;
        Ball ball;
        Ball ball2;
        Ball ball3;
        Bricks[] brick = new Bricks[36];


        public static int ScreenWidth;
        public static int ScreenHeight;

        const float BALL_START_SPEED = 7f;

        float KEYBOARD_PADDLE_SPEED = 10f;

        int horDistanceBetweenBricks = 40;
        int verDistanceBetweenBricks = 25;
        int totalPoints = 0;
        int hitsWithTheBrick = 0;
        int count = 0;

        bool startGame = false;
        bool gameisPaused = false;
        bool hitTopRow = false;
        bool multiBall = false;

        string pressedZ = "Z: Paddle Tunnel";
        string pressedX = "X: PaddleSpeed";
        string pressedC = "C: Multi:Ball";
        string diffLevel = "DifficultyLevel : 1";

        //Random rnd = new Random();

        public Game1():base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here           
            ScreenWidth = GraphicsDevice.Viewport.Width;
            ScreenHeight = GraphicsDevice.Viewport.Height;

            //to clear the screen if user selects a new game
            isDrawn.Clear();

            //creating instances of paddle and 3 balls.
            paddle = new Paddle();
            ball = new Ball();
            ball2 = new Ball();
            ball3 = new Ball();

            //loop tp instantiate the bricks with their points and color and adding it to the list(isDrawn)
            for (int i = 0; i < 36; i++)            //total 36 bricks
            {              
                //instantiating a brick
                brick[i] = new Bricks();
                
                //adding a brick to the list
                isDrawn.Add(brick[i]);

                if (i < 12)
                {
                    //bottom rows have point 1
                    brick[i].Points = 1;

                    //this row is red in color
                    if(i<6)
                    brick[i].color = Color.Red;
                    else
                    brick[i].color = Color.Yellow;  //this row is yellow in color
                }
                if(i>=12 && i<24)
                {
                    //bricks in middle rows have 3 points each 
                    brick[i].Points = 3;
                    if(i>=12 && i<18)
                    brick[i].color = Color.Blue;
                    else
                    brick[i].color = Color.Gray;
                }
                if(i>=24 && i<36)
                {
                    brick[i].Points = 5;
                    if (i >=24 && i < 30)
                        brick[i].color = Color.Green;   //this row is yellow in green
                    else
                        brick[i].color = Color.IndianRed;   //this row is yellow in indianred
                }
            }
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {         
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("SpriteFont1");
            // TODO: use this.Content to load your game content here

            //assigning the texture and position to the paddle
            paddle.Texture = Content.Load<Texture2D>("Paddle");
            paddle.Position = new Vector2(ScreenWidth/2-(paddle.Texture.Width)/2, ScreenHeight - (paddle.Texture.Height*2));
           
            //assigning the texture and position to the ball
            ball.Texture = Content.Load<Texture2D>("Ball");
            ball.Position = new Vector2(ScreenWidth / 2 - (ball.Texture.Width) / 2, ScreenHeight - (ball.Texture.Height * 4));
        

            //assigning the texture and position for each brick
            foreach (Bricks bri in brick)
            {               
                bri.Texture = Content.Load<Texture2D>("Brick");
                if (count % 6 == 0)
                {
                    horDistanceBetweenBricks = 60;
                    verDistanceBetweenBricks += 25;
                            
                    bri.Position = new Vector2(horDistanceBetweenBricks, (ScreenHeight/2) - verDistanceBetweenBricks);               
                }
                else
                {
                    bri.Position = new Vector2(horDistanceBetweenBricks, (ScreenHeight/2) - verDistanceBetweenBricks);                 
                }
                horDistanceBetweenBricks+=120;
                count++;
            }
          
            //start the ball
            ball.Launch(BALL_START_SPEED);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            // TODO: Add your update logic here
            ScreenWidth = GraphicsDevice.Viewport.Width;
            ScreenHeight = GraphicsDevice.Viewport.Height;

            //game gets paused if user presses P 
            if ((Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.P)))
            {
                gameisPaused = true;
            }
            //game continues if user presses space
            if ((Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Space)))
            {
                gameisPaused = false;
            }

            //to start a new game
            if ((Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.NumPad1)))
            {
                //assign everything to their initial values
                horDistanceBetweenBricks = 40;
                verDistanceBetweenBricks = 25;
                ball.turns = 3;
                totalPoints = 0;
                gameisPaused = false;
                pressedZ = "Z: Paddle Tunnel";
                pressedX = "X: Paddle Speed";
                Input.speed = 1.0f;
                this.Initialize();
            }

            //to pause or continue the game   
            if ((Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.NumPad2)))
            {
                if (gameisPaused)
                    gameisPaused = false;
                else
                    gameisPaused = true;
            }

            //to exit the game
            if ((Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.NumPad3)))
            {
                this.Exit();
            }

            //to change the difficulty level, if changed it will resize the paddle
            if ((Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.NumPad6)))
            {
                if (diffLevel == "DifficultyLevel : 1")
                {
                    diffLevel = "DifficultyLevel : 2";
                }
                if (diffLevel == "DifficultyLevel : 2")
                {
                    diffLevel = "DifficultyLevel : 1";
                }
                gameisPaused = false;
            }

            //to start the paddle tunnel boost
            if ((Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Z)))
            {
                paddle.checkWallCollision = true;
                pressedZ = "";
            }

            //to start the paddle speed boost
            if ((Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.X)))
            {
                pressedX = "";
                Input.speed = 2.6f;
            }

            //to start the multiball boost
            if ((Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.C)))
            {
                pressedC = "";

                ball2.Texture = Content.Load<Texture2D>("Ball");
                ball2.Position = new Vector2(ScreenWidth / 2 - (ball.Texture.Width) / 2, ScreenHeight - (ball.Texture.Height * 4));
                ball2.Launch(BALL_START_SPEED);
                multiBall = true;

                //ball3.Texture = Content.Load<Texture2D>("Ball");
                //ball3.Position = new Vector2(ScreenWidth / 2 - (ball.Texture.Width) / 2, ScreenHeight - (ball.Texture.Height * 4));
                //ball3.Launch(BALL_START_SPEED);
            }

            //if game is paused
            if (!gameisPaused)
            {

                //if user is out of turns
                if (ball.turns != 0)
                {
                    //if user has hit all the bricks, then show all the bricks again
                    if (isDrawn.Count == 0)
                    {
                        startGame = false;
                        horDistanceBetweenBricks = 40;
                        verDistanceBetweenBricks = 25;
                        this.Initialize();
                    }

                    
                    if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Space))
                    {
                        startGame = true;
                        ball.playerLost = false;
                    }

                    if (startGame)
                    {
                           
                        ball.Move(ball.Velocity);
                        if (multiBall)
                        {
                            //this is the second ball
                            ball2.Move(ball2.Velocity);
                        }
                    }
                    else
                    {
                        paddle.Position = new Vector2(ScreenWidth / 2 - (paddle.Texture.Width) / 2, ScreenHeight - (paddle.Texture.Height * 2));
                        ball.Launch(BALL_START_SPEED);
                    }

                    //get the input form the keyboard to move the paddle
                    Vector2 paddleVelocity = Input.GetKeyboardInputDirection(PlayerIndex.One) * KEYBOARD_PADDLE_SPEED;
                    paddle.Move(paddleVelocity);

                    //check the ball and paddle collision 
                    if (GameObject.CheckPaddleBallCollision(paddle, ball))
                    {
                        ball.Velocity.Y = -Math.Abs(ball.Velocity.Y);
                    }
                    //if multiball boost is on
                    if (multiBall)
                    {

                        if (GameObject.CheckPaddleBallCollision(paddle, ball2))
                        {
                            ball2.Velocity.Y = -Math.Abs(ball2.Velocity.Y);
                        }
                    }
                    //check ball and brick collision
                    foreach (Bricks brick in isDrawn)
                    {
                        if (Bricks.CheckBallBrickCollision(brick, ball))
                        {

                            if (isDrawn.IndexOf(brick) >= 0 && isDrawn.IndexOf(brick) < 6)
                            {
                                hitTopRow = true;
                            }
                            //disappear the ball
                            isDrawn.Remove(brick);
                            ball.Velocity.Y = Math.Abs(ball.Velocity.Y);
                            //add the points 
                            totalPoints += brick.Points;

                            //to increase the speed after 4 hits and 12 hits
                            hitsWithTheBrick++;
                            break;
                        }
                    }

                    //check the collision between other ball and bricks
                    if (multiBall)
                    {
                        foreach (Bricks brick in isDrawn)
                        {
                            if (Bricks.CheckBallBrickCollision(brick, ball2))
                            {

                                if (isDrawn.IndexOf(brick) >= 0 && isDrawn.IndexOf(brick) < 6)
                                {
                                    hitTopRow = true;
                                }

                                isDrawn.Remove(brick);
                                ball.Velocity.Y = Math.Abs(ball.Velocity.Y);
                                totalPoints += brick.Points;
                                hitsWithTheBrick++;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    //player lost the game
                    ball.Launch(BALL_START_SPEED);
                    paddle.Position = new Vector2(ScreenWidth / 2 - (paddle.Texture.Width) / 2, ScreenHeight - (paddle.Texture.Height * 2));
                }
            }
            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            //shrink the paddle if hit the top and the top brick
            if(ball.didHitTop && hitTopRow)
            {
                paddle.Texture = Content.Load<Texture2D>("rsz_paddle");              
            }

            //change the paddle size
            if(diffLevel=="DifficultyLevel : 2")
            {
                paddle.Texture = Content.Load<Texture2D>("rsz_paddle");
            }
            else
            {
                paddle.Texture = Content.Load<Texture2D>("paddle");
            }
          
            //draw the paddle
            paddle.Draw(spriteBatch);
         
            //if ball touches the bottom
            if (!ball.playerLost)
            {
                ball.Draw(spriteBatch);                
            }
            else
            {
                startGame = false;
                ball.Draw(spriteBatch);
            }
            if (multiBall)
            {
                if (!ball2.playerLost)
                {
                    ball2.Draw(spriteBatch);
                }
                else
                {
                    //startGame = false;
                    ball2.Draw(spriteBatch);
                }
            }

            //draw the bricks which are not hit by the ball
            foreach(Bricks brick in isDrawn)
            {
                brick.Draw(spriteBatch);
            }
      
            //thses sprite fonts are used to show the scores, difficulty level, boosts, turns 
            spriteBatch.DrawString(font, "Turns Left : " + ball.turns.ToString() + " Points : " + totalPoints.ToString() + "\n" + diffLevel, new Vector2(10, 10), Color.White);          
            spriteBatch.DrawString(font, "P : Pause ", new Vector2(320, 10), Color.Yellow);
            spriteBatch.DrawString(font, pressedZ +  " " + pressedX + "\n" + pressedC, new Vector2(450, 10), Color.White);
            spriteBatch.DrawString(font, "SAMEER SAPRA \n Game Simulation and Development \n Assignment 1 ", new Vector2(460, 400), Color.Yellow);
            if(gameisPaused)
            {
                spriteBatch.DrawString(font, "1. New Game \n2. Pause/Continue \n3. Exit \n4. Speed of play \n5. Number of points to win a game \n6. Difficulty level", new Vector2(250, 250), Color.White);
            }         
            spriteBatch.End();
            base.Draw(gameTime);
   
        }
    }
}
