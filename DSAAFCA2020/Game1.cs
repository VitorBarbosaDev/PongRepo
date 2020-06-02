
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using DSAAFCA2020.Inputs;
using DSAAFCA2020.Sprites;
using DSAAFCA2020.States;
using Microsoft.Xna.Framework.Audio;

namespace DSAAFCA2020
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private SpriteFont font;

        string Message = "Game Fully Done";

        public static SoundEffect soundEffect;
        public static int ScreenWidth;
        public static int ScreenHeight;
        public static Random Random;
      


        //SCENESTUFF
        private State _currentState;
        private State _nextState;

     public void ChangeState(State state)
        {
            _nextState = state;
        }


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

    
        protected override void Initialize()
        {
          
         
            //setting screen width
            ScreenWidth = graphics.PreferredBackBufferWidth;
            ScreenHeight = graphics.PreferredBackBufferHeight;
            Random = new Random();

            IsMouseVisible = true;


            base.Initialize();
        }

        
        protected override void LoadContent()
        {
           
            spriteBatch = new SpriteBatch(GraphicsDevice);

            _currentState = new MenuState(this, graphics.GraphicsDevice, Content);

            font = Content.Load<SpriteFont>("TextFont");

            soundEffect = Content.Load<SoundEffect>("Point");

        }

      
        protected override void UnloadContent()
        {
           
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if(_nextState != null)
            {
                _currentState = _nextState;
                _nextState = null;
            }

            _currentState.Update(gameTime);

            _currentState.PostUpdate(gameTime);











      














            base.Update(gameTime);


           
        }

      
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _currentState.Draw(gameTime, spriteBatch);


            spriteBatch.Begin();

           

           

        //    spriteBatch.DrawString(font, Message + " " + ID + " " + Name, new Vector2(10, 10), Color.White);

           

            
            spriteBatch.End();
           

            base.Draw(gameTime);
        }
    }
}
