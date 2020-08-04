using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRexRunnigGame.Graphics
{
    public class SpriteAnimationFraem
    {
        private Sprite _sprite;

        public Sprite Sprite
        {
            get
            {
                return _sprite;
            }

            set
            {
                _sprite = value ?? throw new ArgumentNullException("value", "the sprite cennot be null");
                
            }

        }
    
        public float TimeStamp { get; }

        public SpriteAnimationFraem(Sprite sprite, float TimeStamp)
        {
            Sprite = sprite;
            this.TimeStamp = TimeStamp;
        }
    }
}
