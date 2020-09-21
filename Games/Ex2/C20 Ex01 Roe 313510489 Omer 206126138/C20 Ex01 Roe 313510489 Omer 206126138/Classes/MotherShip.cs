using System;
using System.Collections.Generic;
using System.Text;
using Invaders.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace C20_Ex01_Roe_313510489_Omer_206126138.Classes
{
    public class MotherShip : Enemy
    {
        private const int k_positionY = 32; 
        private const string k_AssetName = @"Sprites\MotherShip_32x120";

        public MotherShip(Game i_Game) : base (k_AssetName, i_Game)
        {
            k_EnemyVelocityPerSecond = 95;
            TintColor = Color.Red;
            Visible = false;

            initPositions();
        }

        private void initPositions()
        {
            Position = new Vector2(0, 32);
        }
        protected override void InitSourceRectangle()
        {
            base.InitSourceRectangle();

            this.SourceRectangle = new Rectangle(
                    0,
                    0,
                    (int)Texture.Width,
                    (int)Texture.Height);
        }

        public void GetReadyToPop()
        {
            initPositions();
            Visible = true;
        }

        public void MoveRight(GameTime i_GameTime)
        {
            MoveRight(i_GameTime, (int)(k_EnemyVelocityPerSecond * (float)i_GameTime.ElapsedGameTime.TotalSeconds));

            if (Position.X >= GraphicsDevice.Viewport.Width)
            {
                Visible = false;
            }
        }

        public bool IntersectionWithShipBullets(Ship i_Ship)
        {
            bool hit = false;

            Bullet bullet1 = i_Ship.Bullet1;
            Bullet bullet2 = i_Ship.Bullet2;

            Rectangle bulletRectangle1 = new Rectangle((int)bullet1.Position.X, (int)bullet1.Position.Y, bullet1.Texture.Width, bullet1.Texture.Height);
            Rectangle bulletRectangle2 = new Rectangle((int)bullet2.Position.X, (int)bullet2.Position.Y, bullet2.Texture.Width, bullet2.Texture.Height);
            Rectangle MotherShipRectangle = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);

            if (bulletRectangle1.Intersects(MotherShipRectangle) && bullet1.IsActive)
            {
                hit = true;

                Visible = false;
                bullet1.IsActive = false;

                initPositions();
            }
            else if (bulletRectangle2.Intersects(MotherShipRectangle) && bullet2.IsActive)
            {
                hit = true;

                Visible = false;
                bullet2.IsActive = false;

                initPositions();
            }

            return hit;
        }
    }
}
