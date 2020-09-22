using System;
using Microsoft.Xna.Framework;

namespace Infrastructure.ObjectModel.Animators.ConcreteAnimators
{
    public class RotateAnimator : SpriteAnimator
    {
        private TimeSpan m_RotateLength;
        private TimeSpan m_TimeLeftForRotate;
        private double m_LastRotationTime;

        public TimeSpan RotateLength
        {
            get { return m_RotateLength; }
            set { m_RotateLength = value; }
        }

        // CTORs
        public RotateAnimator(string i_Name, TimeSpan i_RotateLength, TimeSpan i_AnimationLength)
            : base(i_Name, i_AnimationLength)
        {
            this.m_RotateLength = i_RotateLength;
            this.m_TimeLeftForRotate = i_RotateLength;
            this.m_LastRotationTime = 0;
        }

        public RotateAnimator(TimeSpan i_RotateLength, TimeSpan i_AnimationLength)
            : this("Rotate", i_RotateLength, i_AnimationLength)
        {
            this.m_RotateLength = i_RotateLength;
            this.m_TimeLeftForRotate = i_RotateLength;
        }

        protected override void DoFrame(GameTime i_GameTime)
        {
            m_TimeLeftForRotate -= i_GameTime.ElapsedGameTime;

            if (i_GameTime.TotalGameTime.TotalSeconds - m_LastRotationTime >= m_RotateLength.TotalSeconds)
            {


                m_LastRotationTime = i_GameTime.TotalGameTime.TotalSeconds;
            }

            if (m_TimeLeftForRotate.TotalSeconds < 0)
            {
                // we have elapsed, so blink
                this.BoundSprite.Visible = !this.BoundSprite.Visible;
                m_TimeLeftForRotate = m_RotateLength;
            }

        }

        protected override void RevertToOriginal()
        {
            this.BoundSprite.Visible = m_OriginalSpriteInfo.Visible;
        }
    }
}
