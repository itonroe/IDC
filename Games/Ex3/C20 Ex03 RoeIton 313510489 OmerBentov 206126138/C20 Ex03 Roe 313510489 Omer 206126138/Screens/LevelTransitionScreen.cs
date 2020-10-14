using System;
using System.Collections.Generic;
using System.Text;
using C20_Ex03_Roe_313510489_Omer_206126138.Menues;
using GameScreens.Sprites;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace C20_Ex03_Roe_313510489_Omer_206126138.Screens
{
    public class LevelTransitionScreen : GameScreen
    {
        private Background m_Background;
        private int m_Level;
        private string m_TimeToStart;

        private TimeSpan m_SecondsShow;

        public LevelTransitionScreen(Game i_Game, int i_Level) : base(i_Game)
        {
            m_Background = new Background(i_Game, @"Sprites\BG_Space01_1024x768", 1);
            m_Level = i_Level;
            m_SecondsShow = TimeSpan.FromSeconds(2.5);
            m_TimeToStart = "3";
            Game.Window.ClientSizeChanged += Window_ClientSizeChanged; 
            this.Add(m_Background); 
        }

        private void Window_ClientSizeChanged(object sender, EventArgs e)
        {
            m_Background.Scales = new Vector2(Game.Window.ClientBounds.Width / m_Background.WidthBeforeScale, Game.Window.ClientBounds.Height / m_Background.HeightBeforeScale);
        }

        public override void Initialize()
        {
            base.Initialize();
            m_Background.Scales = new Vector2(Game.Window.ClientBounds.Width / m_Background.WidthBeforeScale, Game.Window.ClientBounds.Height / m_Background.HeightBeforeScale);
        }
        
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            m_SecondsShow -= gameTime.ElapsedGameTime;

            if (m_SecondsShow.TotalSeconds >= 2)
            {
                m_TimeToStart = "3";
            } 
            else 
            if (m_SecondsShow.TotalSeconds >= 1 && m_SecondsShow.TotalSeconds < 2)
            {
                m_TimeToStart = "2";
            }
            else if (m_SecondsShow.TotalSeconds >= 0 && m_SecondsShow.TotalSeconds < 1)
            {
                m_TimeToStart = "1";
            }
            else
            {
                ExitScreen();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            SpriteBatch.Begin();

            SpriteFont consolasFont = ContentManager.Load<SpriteFont>(@"Fonts\Consolas");
            SpriteBatch.DrawString(consolasFont, $"Level {m_Level}\n\nTime To Start - '{m_TimeToStart}'", new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2), Color.White);

            SpriteBatch.End();
        }
    }
}
