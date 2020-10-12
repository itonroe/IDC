using System;
using System.Collections.Generic;
using System.Text;
using C20_Ex03_Roe_313510489_Omer_206126138.Screens;
using Infrastructure.Managers;
using Microsoft.Xna.Framework;

namespace C20_Ex03_Roe_313510489_Omer_206126138
{
    class GameWithScreens : Game
    {
        GraphicsDeviceManager m_GraphicManager;

        public GameWithScreens()
        {
            m_GraphicManager = new GraphicsDeviceManager(this);


            m_GraphicManager.PreferredBackBufferWidth = 750;
            m_GraphicManager.PreferredBackBufferHeight = 600;
            m_GraphicManager.ApplyChanges();

            this.Content.RootDirectory = "Content";

            InputManager inputManager = new InputManager(this);

            ScreensMananger screensManager = new ScreensMananger(this);
            setScreens(screensManager);
        }

        private void setScreens(ScreensMananger i_ScreenManager)
        {
            //i_ScreenManager.Push(new GameOverScreen(this));
            i_ScreenManager.SetCurrentScreen(new PlayScreen(this));
        }

        /*protected void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }*/
    }
}
