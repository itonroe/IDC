﻿using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.ObjectModel.Screens;
using Invaders.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace C20_Ex03_Roe_313510489_Omer_206126138.Classes
{
    public class Player : Ship
    {
        // Score
        private GameScore m_Score;
        private int m_PlayerNumber;
        private Color m_TextColor;

        private GameScreen m_GameScreen;

        public GameScore Score 
        {
            get
            {
                return m_Score;
            }

            set
            {
                m_Score = value;
            }
        }

        public Player(int io_PlayerNumber, GameScreen i_GameScreen) : base(@"Sprites\Ship01_32x32", i_GameScreen)
        {
            m_GameScreen = i_GameScreen;

            if(io_PlayerNumber == (int)ePlayerNumber.Blue || io_PlayerNumber == (int)ePlayerNumber.Green)
            {
                m_PlayerNumber = io_PlayerNumber;
                SetPlayerNumber(io_PlayerNumber);
            }
            else
            {
                throw new Exception("Bad input");
            }

            m_Score = new GameScore(ContentManager);
        }

        public void SetPlayerNumber(int i_PlayerNumber)
        {
            switch (i_PlayerNumber)
            {
                case 1:
                    AssetName = @"Sprites\Ship01_32x32";
                    m_TextColor = Color.Blue;
                    break;
                case 2:
                    AssetName = @"Sprites\Ship02_32x32";
                    m_TextColor = Color.Green;
                    break;
                default:
                    m_TexturePath = @"Sprites\Ship01_32x32";
                    //// this.TintColor = Color.Red
                    break;
            }
        }

        public enum ePlayerNumber
        {
            Blue = 1,
            Green = 2,
        }

        public override void Draw(GameTime gameTime)
        {
            try
            {
                base.Draw(gameTime);
            }
            catch
            {
                Console.WriteLine("Try");
            }
        }

        public void DrawLives(SpriteBatch i_SpriteBatch)
        {
            for(int i = 1; i <= Lifes; i++)
            {
                Point positionForLifes = new Point(i_SpriteBatch.GraphicsDevice.Viewport.Width - (i * Texture.Width / 2), Texture.Height / 2 * (m_PlayerNumber - 1));
                Point scale = new Point(Texture.Width / 2, Texture.Height / 2);
                Rectangle rectangle = new Rectangle(positionForLifes, scale);
                i_SpriteBatch.Draw(Texture, rectangle, Color.White * 0.5f);
            }
        }

        public void DrawScore(SpriteBatch i_SpriteBatch)
        {
            i_SpriteBatch.DrawString(m_Score.m_ConsolasFont, $"P{m_PlayerNumber} Score:{m_Score.Score}", new Vector2(0, (m_PlayerNumber - 1) * 20), m_TextColor);
        }
    }
}
