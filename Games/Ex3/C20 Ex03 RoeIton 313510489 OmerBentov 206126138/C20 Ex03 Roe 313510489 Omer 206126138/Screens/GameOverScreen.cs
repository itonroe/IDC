using System;
using System.Collections.Generic;
using System.Text;
using C20_Ex03_Roe_313510489_Omer_206126138.Menues;
using GameScreens.Sprites;
using Infrastructure.Managers;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace C20_Ex03_Roe_313510489_Omer_206126138.Screens
{
    class GameOverScreen : GameScreen
    {
        private string m_Score;
        Background m_Background;

        public GameOverScreen(Game i_Game, string i_Score) : base(i_Game)
        {
            m_Score = i_Score;
            m_Background = new Background(i_Game, @"Sprites\BG_Space01_1024x768", 1);
            this.Add(m_Background);
            Game.Window.ClientSizeChanged += Window_ClientSizeChanged;
        }

        public override void Initialize()
        {
            base.Initialize();
        }
        
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (InputManager.KeyPressed(Keys.Home))
            {
                //Start new Game
                (m_ScreensManager as ScreensMananger).Push(new PlayScreen(Game, 1));
                ExitScreen();
                this.ScreensManager.SetCurrentScreen(new LevelTransitionScreen(Game, 1));
            }
            else if (InputManager.KeyPressed(Keys.Escape))
            {
                //Close Game
                this.Game.Exit();
            }
            else if(InputManager.KeyPressed(Keys.M))
            {
                //Open Menu
                (m_ScreensManager as ScreensMananger).Push(new PlayScreen(Game, 1));
                (m_ScreensManager as ScreensMananger).Push(new LevelTransitionScreen(Game, 1));
                ExitScreen();
                this.ScreensManager.SetCurrentScreen(new MainMenu(this.Game));
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            SpriteBatch.Begin();

            drawInstructions();

            SpriteBatch.End();
        }

        private void drawInstructions()
        {
            SpriteFont consolasFont = ContentManager.Load<SpriteFont>(@"Fonts\Consolas");

            SpriteBatch.DrawString(consolasFont, $"Game Over, {m_Score}\n\n" +
                                                 $"     HOME - Start New Game\n" +
                                                 $"     M     - Main Menu\n" +
                                                 $"     Esc   - Exit", new Vector2(GraphicsDevice.Viewport.Width / 2 - 180, GraphicsDevice.Viewport.Height / 2 - 30), Color.White);
        }

        private void Window_ClientSizeChanged(object sender, EventArgs e)
        {
            m_Background.Scales = new Vector2(Game.Window.ClientBounds.Width / m_Background.WidthBeforeScale, Game.Window.ClientBounds.Height / m_Background.HeightBeforeScale);
        }
    }
}
