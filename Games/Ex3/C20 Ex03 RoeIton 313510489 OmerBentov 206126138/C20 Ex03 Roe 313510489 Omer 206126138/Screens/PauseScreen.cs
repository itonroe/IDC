using System;
using System.Collections.Generic;
using System.Text;
using C20_Ex03_Roe_313510489_Omer_206126138.Menues;
using GameScreens.Sprites;
using Infrastructure.ObjectModel;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace C20_Ex03_Roe_313510489_Omer_206126138.Screens
{
    class PauseScreen : GameScreen
    {
        public PauseScreen(Game i_Game) : base(i_Game)
        {
            
            this.IsModal = true;
            this.IsOverlayed = true;
            this.BlackTintAlpha = 0.40f;
            this.UseGradientBackground = false;
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Initialize()
        {
            base.Initialize();
        }
        
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (InputManager.KeyPressed(Keys.R))
            {
                //Return
                ExitScreen();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            SpriteBatch.Begin();

            drawInstructions();

            SpriteBatch.End();
        }

        private void drawInstructions()
        {
            SpriteFont consolasFont = ContentManager.Load<SpriteFont>(@"Fonts\Consolas");

            SpriteBatch.DrawString(consolasFont, $"Pause Game\n\n" +
                                                 $"R - Return", new Vector2(GraphicsDevice.Viewport.Width / 2 - 180, GraphicsDevice.Viewport.Height / 2 - 30), Color.White);
        }
    }
}
