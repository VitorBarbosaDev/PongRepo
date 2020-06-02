using ActivityTracker;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using DSAAFCA2020.Inputs;
using DSAAFCA2020.Sprites;
using DSAAFCA2020.States;
using Microsoft.Xna.Framework.Content;
using DSAAFCA2020.Managers;
using System.Linq;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using DSAAFCA2020.Controls;

namespace DSAAFCA2020.States
{
    class ScoreBoardState : State
    {

        public static int ScreenWidth;
        public static int ScreenHeight;

        //  public static Random Random;
        private SpriteFont _font;
        private ScoreManager _scoreManager;
        private List<Component> _components;

        Song song;

    public ScoreBoardState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
      : base(game, graphicsDevice, content)
    {
            var buttonTexture = _content.Load<Texture2D>("Controls/Button");
            var buttonFont = _content.Load<SpriteFont>("Fonts/Font");

            var GoBackButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(300, 300),
                Text = "Go Back",
            };

            GoBackButton.Click += GoBackButton_Click;

            _scoreManager = ScoreManager.Load();//loading file for scores

        _font = _content.Load<SpriteFont>("Fonts/HighScores");


        ScreenWidth = Game1.ScreenWidth;
        ScreenHeight = Game1.ScreenHeight;

     


            _components = new List<Component>()
      {
                    GoBackButton
      };

        }





    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);
            spriteBatch.DrawString(_font, "Highscores:\n" + string.Join("\n" + "GameNum ", _scoreManager.Highscores.Select(c => c.Games + ": " + c.GameScore).ToArray()), new Vector2(300, 100), Color.White);

            
            spriteBatch.End();



    }

        private void GoBackButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));
        }

        public override void PostUpdate(GameTime gameTime)
    {

    }

    public override void Update(GameTime gameTime)
    {
            foreach (var component in _components)
                component.Update(gameTime);

            //base.Update(gameTime);
        }
}
}
