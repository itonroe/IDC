using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using GameScreens.Sprites;
using Infrastructure.ObjectModel;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace C20_Ex03_Roe_313510489_Omer_206126138.Menues
{
    class MainMenu : GameScreen
    {
        private Background m_Background;
        private List<string> m_MenuItems;

        private int m_CurrentMenuItemIndex;

        public MainMenu(Game i_Game) : base(i_Game)
        {
            m_Background = new Background(i_Game, @"Sprites\BG_Space01_1024x768", 1);
            this.Add(m_Background);

            initMenuItems();
        }

        private void initMenuItems()
        {
            m_MenuItems = new List<string>();

            m_MenuItems.Add("Screen Settings");
            string numOfPlayers = (Game as GameWithScreens).NumOfPlayers == 1 ? "One" : "Two";
            m_MenuItems.Add($"Players: {numOfPlayers}");
            m_MenuItems.Add("Sound Settings");
            m_MenuItems.Add("Play");
            m_MenuItems.Add("Quit");

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

            if (InputManager.KeyPressed(Keys.Enter))
            {
                switch (m_CurrentMenuItemIndex)
                {
                    case 0:
                        //Screen Settings
                        ScreensManager.SetCurrentScreen(new ScreenSettings(Game));
                        break;
                    case 2:
                        //Sound Settings
                        ScreensManager.SetCurrentScreen(new SoundSettings(Game));
                        break;
                    case 3:
                        //Play
                        ExitScreen();
                        break;
                    case 4:
                        //Quit
                        Game.Exit();
                        break;
                    default:
                        break;
                }
            }

            if ((InputManager.KeyPressed(Keys.PageUp) || InputManager.KeyPressed(Keys.PageDown)) && m_CurrentMenuItemIndex == 1)
            {
                ReplaceNumOfPlayers();
            }
        }

        private void ReplaceNumOfPlayers()
        {
            if ((Game as GameWithScreens).NumOfPlayers == 1)
            {
                (Game as GameWithScreens).NumOfPlayers = 2;
                m_MenuItems[1] = "Players: Two";
            }
            else
            {
                (Game as GameWithScreens).NumOfPlayers = 1;
                m_MenuItems[1] = "Players: One";
            }
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
    }
}
