using System;
using System.Collections.Generic;
using System.Text;
using GameScreens.Sprites;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace C20_Ex03_Roe_313510489_Omer_206126138.Menues
{
    class ScreenSettings : GameScreen
    {
        private Background m_Background;
        private List<string> m_MenuItems;

        private bool m_FullScreenMode;
        private int m_CurrentMenuItemIndex;

        public ScreenSettings(Game i_Game) : base(i_Game)
        {
            m_Background = new Background(i_Game, @"Sprites\BG_Space01_1024x768", 1);
            this.Add(m_Background);

            m_FullScreenMode = false;

            initMenuItems();
        }

        private void initMenuItems()
        {
            m_MenuItems = new List<string>();

            m_MenuItems.Add("Allow Window Resizing: Off");
            m_MenuItems.Add("Full Screen Mode: Off");

            string modeMouse = this.Game.IsMouseVisible ? "Visible" : "Invisible";
            m_MenuItems.Add($"Mouse Visability: {modeMouse}");
            m_MenuItems.Add("Done");

            m_CurrentMenuItemIndex = 0;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (InputManager.KeyPressed(Keys.Down))
            {
                m_CurrentMenuItemIndex++;

                if (m_CurrentMenuItemIndex >= m_MenuItems.Count)
                {
                    m_CurrentMenuItemIndex = 0;
                }
            }

            if (InputManager.KeyPressed(Keys.Up))
            {
                m_CurrentMenuItemIndex--;

                if (m_CurrentMenuItemIndex <= -1)
                {
                    m_CurrentMenuItemIndex = m_MenuItems.Count - 1;
                }
            }

            if ((InputManager.KeyPressed(Keys.PageUp) || InputManager.KeyPressed(Keys.PageDown)))
            {
                switch (m_CurrentMenuItemIndex)
                {
                    case 0:
                        ReplaceResizing();
                        break;
                    case 1:
                        ReplaceFullScreenMode();
                        break;
                    case 2:
                        ReplaceMouseVisability();
                        break;
                }
            }

            if (InputManager.KeyPressed(Keys.Enter) && m_CurrentMenuItemIndex == 3)
            {
                ExitScreen();
            }
        }

        private void ReplaceResizing()
        {
            this.Game.Window.AllowUserResizing = !this.Game.Window.AllowUserResizing;
            string mode = this.Game.Window.AllowUserResizing ? "On" : "Off";

            m_MenuItems[0] = $"Allow Window Resizing: {mode}";
        }

        private void ReplaceFullScreenMode()
        {
            m_FullScreenMode = !m_FullScreenMode;
            string mode = m_FullScreenMode ? "On" : "Off";

            m_MenuItems[1] = $"Full Screen Mode: {mode}";
        }

        private void ReplaceMouseVisability()
        {
            this.Game.IsMouseVisible = !this.Game.IsMouseVisible;
            string mode = this.Game.IsMouseVisible ? "Visible" : "Invisible";

            m_MenuItems[2] = $"Mouse Visability: {mode}";
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            SpriteBatch.Begin();

            drawMenuItems();

            SpriteBatch.End();
        }

        private void drawMenuItems()
        {
            SpriteFont consolasFont = ContentManager.Load<SpriteFont>(@"Fonts\Consolas");

            StringBuilder stringBuilder = new StringBuilder();

            SpriteBatch.DrawString(consolasFont, $"{m_MenuItems[m_CurrentMenuItemIndex]}", new Vector2(GraphicsDevice.Viewport.Width / 2 - 180, GraphicsDevice.Viewport.Height / 2 - 30), Color.White);
        }
    }
}
