using System;
using System.Collections.Generic;
using System.Text;
using GameScreens.Sprites;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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

            m_SoundOn = (Game as GameWithScreens).SoundsOn;
            m_BackgroundMusicVolume = (int)((Game as GameWithScreens).BackgroundSound.Volume * 100f);
            m_SoundEffectsVolume = (int)((Game as GameWithScreens).EffectsSounds[0].Volume * 100f);
            Game.Window.ClientSizeChanged += Window_ClientSizeChanged;
            initMenuItems();
        }

        public SoundSettings(Game i_Game, int i_BackgroundMusicVolume, int i_SoundEffectsVolume, bool i_SoundOn) : base(i_Game)
        {
            m_Background = new Background(i_Game, @"Sprites\BG_Space01_1024x768", 1);

            this.Add(m_Background);

            m_SoundOn = i_SoundOn;
            m_BackgroundMusicVolume = i_BackgroundMusicVolume;
            m_SoundEffectsVolume = i_SoundEffectsVolume;
            Game.Window.ClientSizeChanged += Window_ClientSizeChanged;
            initMenuItems();
        }

        private void initMenuItems()
        {
            m_Background.Width = Game.Window.ClientBounds.Width;
            m_Background.Height = Game.Window.ClientBounds.Height;

            m_MenuItems = new List<string>();

            string mode = (Game as GameWithScreens).SoundsOn ? "On" : "Off";
            m_MenuItems.Add($"Toggle Sound: {mode}");
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
                (Game as GameWithScreens).EffectsSounds[(int)GameWithScreens.eEffectsSounds.MenuMove].Play();

                if (m_CurrentMenuItemIndex >= m_MenuItems.Count)
                {
                    m_CurrentMenuItemIndex = 0;
                }
            }

            if (InputManager.KeyPressed(Keys.Up))
            {
                m_CurrentMenuItemIndex--;
                (Game as GameWithScreens).EffectsSounds[(int)GameWithScreens.eEffectsSounds.MenuMove].Play();

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

            UpdateSettings();

            if (InputManager.KeyPressed(Keys.Enter) && m_CurrentMenuItemIndex == 3)
            {
                ExitScreen();
            }
        }

        private void UpdateSettings()
        {
            (Game as GameWithScreens).BackgroundSound.Volume = m_BackgroundMusicVolume / 100f;
            foreach(SoundEffectInstance soundEffectInstance in (Game as GameWithScreens).EffectsSounds)
            {
                soundEffectInstance.Volume = m_SoundEffectsVolume / 100f;
            }

            (Game as GameWithScreens).PrevBGSoundVolume = m_BackgroundMusicVolume / 100f;
            (Game as GameWithScreens).PrevEfeectsSoundsVolume = m_SoundEffectsVolume / 100f;
        }

        private void SwitchSound()
        {
            (Game as GameWithScreens).ToogleMuteAllSounds();

            string mode = (Game as GameWithScreens).SoundsOn ? "On" : "Off";

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
            Color ActiveColor = Color.Yellow;
            Color InActiveColor = Color.White;

            SpriteFont consolasFont = ContentManager.Load<SpriteFont>(@"Fonts\Consolas");

            int startY = GraphicsDevice.Viewport.Height / 2 - 30;
            int offset = 20;
            int i = 0;

            foreach (string menuItem in m_MenuItems)
            {
                SpriteBatch.DrawString(consolasFont, $"{m_MenuItems[i]}", new Vector2(GraphicsDevice.Viewport.Width / 2 - 180, startY + (offset * i)), i == m_CurrentMenuItemIndex ? ActiveColor : InActiveColor);

                i++;
            }
        }

        private void Window_ClientSizeChanged(object sender, EventArgs e)
        {
            m_Background.Scales = new Vector2(Game.Window.ClientBounds.Width / m_Background.WidthBeforeScale, Game.Window.ClientBounds.Height / m_Background.HeightBeforeScale);
        }
    }
}
