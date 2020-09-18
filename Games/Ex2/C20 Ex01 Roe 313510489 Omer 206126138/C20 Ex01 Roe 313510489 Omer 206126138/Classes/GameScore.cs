using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace C20_Ex01_Roe_313510489_Omer_206126138.Classes
{
    public class GameScore
    {
        private int m_Score;
        public SpriteFont m_ConsolasFont;

        public GameScore(Microsoft.Xna.Framework.Content.ContentManager i_contentManager)
        {
            m_Score = 0;
            LoadContent(i_contentManager);
        }

        public int Score 
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

        protected void LoadContent(Microsoft.Xna.Framework.Content.ContentManager i_contentManager)
        {
            m_ConsolasFont = i_contentManager.Load<SpriteFont>(@"Fonts\Consolas");
        }

        public void AddScore(int i_ScoreToAdd)
        {
            m_Score += i_ScoreToAdd;
            if (m_Score < 0)
            {
                m_Score = 0;
            }
        }
    }
}
