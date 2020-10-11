using System;
using System.Collections.Generic;
using System.Text;
using C20_Ex03_Roe_313510489_Omer_206126138.Screens;
using Infrastructure.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace C20_Ex03_Roe_313510489_Omer_206126138
{
    class GameWithScreens : Game
    {
        private GraphicsDeviceManager m_GraphicManager;
        private SoundEffectInstance m_BgSound;

        public SoundEffectInstance BackgroundSound{
            set
            {
                m_BgSound = value;
            }
            get
            {
                return m_BgSound;
            }
        }

        public GraphicsDeviceManager GraphicManager
        {
            set
            {
                m_GraphicManager = value;
            }
            get
            {
                return m_GraphicManager;
            }
        }

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
            i_ScreenManager.Push(new PlayScreen(this, 1));
            i_ScreenManager.Push(new LevelTransitionScreen(this, 1)); 
            i_ScreenManager.SetCurrentScreen(new WelcomeScreen(this));
        }

        protected override void Initialize()
        {
            base.Initialize();
            m_BgSound = this.Content.Load<SoundEffect>("Sounds/BGMusic").CreateInstance();
            m_BgSound.IsLooped = true;
            m_BgSound.Play();
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
