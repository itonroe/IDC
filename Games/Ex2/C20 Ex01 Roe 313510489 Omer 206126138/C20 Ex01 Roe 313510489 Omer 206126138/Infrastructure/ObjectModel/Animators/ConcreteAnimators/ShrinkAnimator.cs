//*** Guy Ronen © 2008-2011 ***//
using System;
using System.Drawing;
using Microsoft.Xna.Framework;

namespace Infrastructure.ObjectModel.Animators.ConcreteAnimators
{
    public class ShrinkAnimator : SpriteAnimator
    {
        private TimeSpan m_ShrinkLength;

        public TimeSpan ShrinkLength
        {
            get { return m_ShrinkLength; }
            set { m_ShrinkLength = value; }
        }

        // CTORs
        public ShrinkAnimator(string i_Name, TimeSpan i_AnimationLength)
            : base(i_Name, i_AnimationLength)
        {
            this.m_ShrinkLength = i_AnimationLength;
        }

        protected override void DoFrame(GameTime i_GameTime)
        {
            this.BoundSprite.RotationOrigin = new Vector2(16, 16);
            m_ShrinkLength -= i_GameTime.ElapsedGameTime;

            float proportion = (float)(m_ShrinkLength.TotalSeconds / this.AnimationLength.TotalSeconds);
            this.BoundSprite.Scales *= new Vector2(proportion, proportion);
        }

        protected override void RevertToOriginal()
        {
            this.BoundSprite.Visible = m_OriginalSpriteInfo.Visible;
        }
    }
}
