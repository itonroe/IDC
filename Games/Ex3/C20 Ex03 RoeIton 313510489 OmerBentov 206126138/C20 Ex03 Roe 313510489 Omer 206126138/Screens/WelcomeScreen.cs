﻿using System;
using System.Collections.Generic;
using System.Text;
using GameScreens.Sprites;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace C20_Ex03_Roe_313510489_Omer_206126138.Screens
{
    class WelcomeScreen : GameScreen
    {
        Background m_Background;

        public WelcomeScreen(Game i_Game) : base(i_Game)
        {
            m_Background = new Background(i_Game, @"Sprites\BG_Space01_1024x768", 1);
            this.Add(m_Background);
        }

        public override void Initialize()
        {
            base.Initialize();
        }
        
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (InputManager.KeyPressed(Keys.Enter))
            {
                //Start Game with Level 1
                ExitScreen();
            }

            if (InputManager.KeyPressed(Keys.Escape))
            {
                //Close Game
            }

            if (InputManager.KeyPressed(Keys.M))
            {
                //Open Menu
            }
        }
    }
}