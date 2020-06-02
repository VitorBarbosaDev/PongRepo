using DSAAFCA2020.Controls;
using DSAAFCA2020.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSAAFCA2020.States
{
    class EndGameState : State
    {

        public static int ScreenWidth;
        public static int ScreenHeight;

        //  public static Random Random;
        private SpriteFont _font;
        private ScoreManager _scoreManager;
        private List<Component> _components;

        

        public EndGameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            var buttonTexture = _content.Load<Texture2D>("Controls/Button");
            var buttonFont = _content.Load<SpriteFont>("Fonts/Font");

            var GoBackButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(300,300),
                Text = "Main Menu",
            };

            var PlayAgain = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(300, 250),
                Text = "Play Again",
            };


            var Quit = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(300, 350),
                Text = "Quit",
            };


            PlayAgain.Click += PlayAgain_Click;
            GoBackButton.Click += GoBackButton_Click;
            Quit.Click += Quit_Click;
            _scoreManager = ScoreManager.Load();//loading file for scores

            _font = _content.Load<SpriteFont>("Fonts/HighScores");


            ScreenWidth = Game1.ScreenWidth;
            ScreenHeight = Game1.ScreenHeight;

            


            _components = new List<Component>()
      {
                    GoBackButton,
                    PlayAgain,
                    Quit
      }; 

        }





        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);
            //  spriteBatch.DrawString(_font, "Highscores:\n" + string.Join("\n" + "GameNum ", _scoreManager.Highscores.Select(c => c.Games + ": " + c.GameScore).ToArray()), new Vector2(300, 300), Color.White);
            if (GameState._score.Score1 >= GameState._score.Score2)
            {
                spriteBatch.DrawString(_font, "Contrats Player 1", new Vector2(300, 100), Color.White);
            }
            else if (GameState._score.Score2 >= GameState._score.Score1)
            {
                spriteBatch.DrawString(_font, "Contrats Player 2", new Vector2(300, 100), Color.White);
            }
            spriteBatch.End();
        }

        private void GoBackButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));
        }
        private void PlayAgain_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content));
        }
        private void Quit_Click(object sender, EventArgs e)
        {
            _game.Exit();
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
