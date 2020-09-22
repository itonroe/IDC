//*** Guy Ronen © 2008-2011 ***//
using System;
using System.Drawing;
using Microsoft.Xna.Framework;

namespace Infrastructure.ObjectModel.Animators.ConcreteAnimators
{
    public class ShrinkAnimator : SpriteAnimator
    {
        private TimeSpan m_ShrinkLength;
        private TimeSpan m_TimeLeftForNextShrink;

        public TimeSpan ShrinkLength
        {
            get { return m_ShrinkLength; }
            set { m_ShrinkLength = value; }
        }

        // CTORs
        public ShrinkAnimator(string i_Name, TimeSpan i_ShrinkLength, TimeSpan i_AnimationLength)
            : base(i_Name, i_AnimationLength)
        {
            this.m_ShrinkLength = i_ShrinkLength;
            this.m_TimeLeftForNextShrink = i_ShrinkLength;
        }

        public ShrinkAnimator(TimeSpan i_ShrinkLength, TimeSpan i_AnimationLength)
            : this("Shrink", i_ShrinkLength, i_AnimationLength)
        {
            this.m_ShrinkLength = i_ShrinkLength;
            this.m_TimeLeftForNextShrink = i_ShrinkLength;
        }

        protected override void DoFrame(GameTime i_GameTime)
        {
            m_TimeLeftForNextShrink -= i_GameTime.ElapsedGameTime;

            if (m_TimeLeftForNextShrink.TotalSeconds < 0)
            {
                // we have elapsed, so shrink
                BoundSprite.Scales *= new Vector2(0.5f, 0.5f);
                m_TimeLeftForNextShrink = m_ShrinkLength;
            }
        }

        protected override void RevertToOriginal()
        {
            this.BoundSprite.Visible = m_OriginalSpriteInfo.Visible;
        }
    }
}
