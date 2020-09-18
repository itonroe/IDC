using System;
using System.Collections.Generic;
using System.Text;
using Invaders.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace C20_Ex01_Roe_313510489_Omer_206126138.Classes
{
    public class Player : Ship
    {
        // Score
        private GameScore m_Score;
        private int m_PlayerNumber;
        private Color m_Color;

        public GameScore Score {

            get
            {
                return m_Score;
            }
            set
            {
                m_Score = value;
            }
        }

        public Player(int io_PlayerNumber, Microsoft.Xna.Framework.Content.ContentManager i_contentManager) : base()
        {

            if(io_PlayerNumber == (int)ePlayerNumber.Blue || io_PlayerNumber == (int)ePlayerNumber.Green)
            {
                m_PlayerNumber = io_PlayerNumber;
                SetPlayerNumber(io_PlayerNumber);
            }

            else
            {
                throw new Exception("Bad input");
            }

            m_Score = new GameScore(i_contentManager);

        }

        public void SetPlayerNumber(int i_PlayerNumber)
        {
            switch (i_PlayerNumber)
            {
                case 1:
                    base.m_TexturePath = @"Sprites\Ship01_32x32";
                    this.m_Color = Color.Blue;
                    break;
                case 2:
                    base.m_TexturePath = @"Sprites\Ship02_32x32"; // need to be the other Ship2 image
                    this.m_Color = Color.Green;
                    break;
                default:
                    base.m_TexturePath = @"Sprites\Ship01_32x32";
                    this.m_Color = Color.Red;
                    break;
            }
        }

        enum ePlayerNumber
        {
            Blue = 1,
            Green =2 ,
        }

        public void Draw(SpriteBatch i_spriteBatch)
        {
            if(base.Lifes > 0)
                base.Draw(i_spriteBatch);

            DrawLives(i_spriteBatch);
            DrawScore(i_spriteBatch);
            
        }

        private void DrawLives(SpriteBatch i_spriteBatch)
        {
            for( int i=1; i<= base.Lifes; i++)
            {
                Point positionForLifes = new Point(i_spriteBatch.GraphicsDevice.Viewport.Width - i * base.m_ShipTexture.Width/2, base.m_ShipTexture.Height/2 *(m_PlayerNumber - 1));
                Point scale = new Point(base.m_ShipTexture.Width/2, base.m_ShipTexture.Height / 2);
                Rectangle rectangle = new Rectangle(positionForLifes, scale);
                 i_spriteBatch.Draw(base.m_ShipTexture,rectangle, Color.White * 0.5f);
            }
        }

        private void DrawScore(SpriteBatch i_SpriteBatch)
        {
            i_SpriteBatch.DrawString(m_Score.m_ConsolasFont, $"P{m_PlayerNumber} Score:{m_Score.Score}", new Vector2(0,(m_PlayerNumber-1)* 20), m_Color);
        }
    }
}
