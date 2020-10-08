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
    class SoundSettings : GameScreen
    {
        private Background m_Background;
        private List<string> m_MenuItems;

        private bool m_SoundOn;
        private int m_BackgroundMusicVolume;
        private int m_SoundEffectsVolume;

        private int m_CurrentMenuItemIndex;

        public SoundSettings(Game i_Game) : base(i_Game)
        {
            m_Background = new Background(i_Game, @"Sprites\BG_Space01_1024x768", 1);
            this.Add(m_Background);

            m_SoundOn = true;
            m_BackgroundMusicVolume = 100;
            m_SoundEffectsVolume = 100;

            initMenuItems();
        }

        private void initMenuItems()
        {
            m_MenuItems = new List<string>();

            m_MenuItems.Add("Toggle Sound: On");
            m_MenuItems.Add($"Background Music Volume: {m_BackgroundMusicVolume}");
            m_MenuItems.Add($"Sounds Effects Volume: {m_SoundEffectsVolume}");
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

            if (InputManager.KeyPressed(Keys.PageUp))
            {
                switch (m_CurrentMenuItemIndex)
                {
                    case 0:
                        SwitchSound();
                        break;
                    case 1:
                        if (m_BackgroundMusicVolume < 100)
                            m_BackgroundMusicVolume += 10;

                        m_MenuItems[1] = $"Background Music Volume: {m_BackgroundMusicVolume}";
                        break;
                    case 2:
                        if (m_SoundEffectsVolume < 100)
                            m_SoundEffectsVolume += 10;

                        m_MenuItems[2] = $"Sounds Effects Volume: {m_SoundEffectsVolume}";
                        break;
                }
            }

            if (InputManager.KeyPressed(Keys.PageDown))
            {
                switch (m_CurrentMenuItemIndex)
                {
                    case 0:
                        SwitchSound();
                        break;
                    case 1:
                        if (m_BackgroundMusicVolume > 0)
                            m_BackgroundMusicVolume -= 10;

                        m_MenuItems[1] = $"Background Music Volume: {m_BackgroundMusicVolume}";
                        break;
                    case 2:
                        if (m_SoundEffectsVolume > 0)
                            m_SoundEffectsVolume -= 10;
                        m_MenuItems[2] = $"Sounds Effects Volume: {m_SoundEffectsVolume}";
                        break;
                }
            }

            if (InputManager.KeyPressed(Keys.Enter) && m_CurrentMenuItemIndex == 3)
            {
                ExitScreen();
            }
        }

        private void SwitchSound()
        {
            m_SoundOn = !m_SoundOn;
            string mode = m_SoundOn ? "On" : "Off";

            m_MenuItems[0] = $"Toggle Sound: {mode}";
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
