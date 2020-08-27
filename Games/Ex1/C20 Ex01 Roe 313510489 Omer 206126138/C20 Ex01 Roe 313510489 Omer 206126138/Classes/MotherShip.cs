using Invaders.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace C20_Ex01_Roe_313510489_Omer_206126138.Classes
{
    class MotherShip : Enemy
    {
        public MotherShip()
        {
            base.IsAlive = false;
        }

        public void initPositions()
        {
            k_EnemyVelocityPerSecond = 95;
            base.Position = new Vector2(0, 0);
        }

        public override void LoadContent(ContentManager i_ContentManager, string i_Model)
        {
            base.LoadContent(i_ContentManager, i_Model);
        }

        public void GetReadyToPop()
        {
            base.IsAlive = true;
        }

        public void MoveRight(GameTime i_GameTime, GraphicsDevice i_GraphicDevice)
        {
            base.MoveRight(i_GameTime);
            if (base.Position.X >= i_GraphicDevice.Viewport.Width)
            {
                base.IsAlive = false;
                base.Position = Vector2.Zero;
            }
        }

        public bool IntersectionWithShipBullets(Ship i_Ship)
        {
            bool hit = false;

            Bullet bullet1 = i_Ship.Bullet1;
            Bullet bullet2 = i_Ship.Bullet2;

            Rectangle bulletRectangle1 = new Rectangle((int)bullet1.Position.X, (int)bullet1.Position.Y, bullet1.Texture.Width, bullet1.Texture.Height);
            Rectangle bulletRectangle2 = new Rectangle((int)bullet2.Position.X, (int)bullet2.Position.Y, bullet2.Texture.Width, bullet2.Texture.Height);
            Rectangle MotherShipRectangle = new Rectangle((int)this.Position.X, (int)this.Position.Y, this.Texture.Width, this.Texture.Height);

            if (bulletRectangle1.Intersects(MotherShipRectangle) && bullet1.IsActive)
            {
                hit = true;

                base.IsAlive = false;
                bullet1.IsActive = false;

                initPositions();
            }
            else if (bulletRectangle2.Intersects(MotherShipRectangle) && bullet2.IsActive)
            {
                hit = true;

                base.IsAlive = false;
                bullet2.IsActive = false;

                initPositions();
            }

            return hit;
        }

    }
}
