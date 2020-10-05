using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Infrastructure.ObjectModel.Animators.ConcreteAnimators
{
    public class FaderAnimator : SpriteAnimator
    {
        public FaderAnimator(string i_Name, TimeSpan i_AnimationLength) 
            : base(i_Name, i_AnimationLength)
        {
        }

        protected override void DoFrame(GameTime i_GameTime)
        {
        }

        protected override void RevertToOriginal()
        {
            this.BoundSprite.Opacity = m_OriginalSpriteInfo.Opacity;
        }
    }
}
