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

namespace DSAAFCA2020.States
{
    class GameState : State
    {
        //GraphicsDeviceManager graphics;
        public static Score _score;
        private List<Sprite> _sprites;
        public static int ScreenWidth;
        public static int ScreenHeight;
        //  public static Random Random;
        public int GameNum = 0;
      
        //scoremanage
        
        private SpriteFont _font;
        private ScoreManager _scoreManager;


        Song song;

        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            _scoreManager = ScoreManager.Load();//loading file for scores

            _font = _content.Load<SpriteFont>("Fonts/HighScores");


            ScreenWidth = Game1.ScreenWidth;
            ScreenHeight = Game1.ScreenHeight;

            //   var batTexture = _content.Load<Texture2D>("Bat");
            var Playertx = _content.Load<Texture2D>("GreenTennis");
            var Playertx1 = _content.Load<Texture2D>("BlueTennis");
            var ballTexture = _content.Load<Texture2D>("Ball");
           
            song = _content.Load<Song>("GameBg");
            _score = new Score(_content.Load<SpriteFont>("Font"));

            _sprites = new List<Sprite>()//List of sprites
                {
        new Sprite(_content.Load<Texture2D>("Background")),
        new Bat(Playertx)//Player 1
        {
          Position = new Vector2(20, (ScreenHeight / 2) - (Playertx.Height / 2)),
          batId =0,
          Input = new Input()
          {
            Up = Keys.W,
            Down = Keys.S,
            Left = Keys.A,
            Right = Keys.D,
            Action = Keys.Space
          }
        },
        new Bat(Playertx1)//Player 2
        {
          Position = new Vector2(ScreenWidth - 20 - Playertx1.Width, (ScreenHeight / 2) - (Playertx1.Height / 2)),
          batId = 1,
          Input = new Input()
          {
            Up = Keys.Up,
            Down = Keys.Down,
            Left = Keys.Left,
            Right = Keys.Right,
            Action = Keys.RightShift
          }
        },
        new Ball(ballTexture)
        {
          Position = new Vector2((ScreenWidth / 2) - (ballTexture.Width / 2), (ScreenHeight / 2) - (ballTexture.Height / 2)),
          Score = _score,
        }
      };
            MediaPlayer.Play(song);
            MediaPlayer.IsRepeating = true;

            _score.Score1 = 0;
            _score.Score2 = 0;
        }

       



        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            foreach (var sprite in _sprites) //Drawing list of sprites
                sprite.Draw(spriteBatch);

            
            _score.Draw(spriteBatch);


        //    spriteBatch.DrawString(_font, "Highscores:\n" + string.Join("\n" + "GameNum ", _scoreManager.Highscores.Select(c => c.Games + ": " + c.GameScore).ToArray()), new Vector2(480, 10), Color.Red);
            spriteBatch.End();



        }



        public override void PostUpdate(GameTime gameTime)
        {

        }

        public override void Update(GameTime gameTime)
        {
            foreach (var sprite in _sprites)
            {
                sprite.Update(gameTime, _sprites);
            }

           




            if(_score.Score1 > 11 || _score.Score2 > 11)
            {
                _scoreManager.Add(new GameScores()
                {
                    PlayerName = ($"Game{GameNum}"),
                    GameScore = ($"Player 1 : {_score.Score1}  Player 2: {_score.Score2}"),
                    Games = GameNum
                }
                );
                ScoreManager.Save(_scoreManager);


               
                GameNum = GameNum + 1;

                _game.ChangeState(new EndGameState(_game, _graphicsDevice, _content));

            }

            //base.Update(gameTime);
        }
    }
}
