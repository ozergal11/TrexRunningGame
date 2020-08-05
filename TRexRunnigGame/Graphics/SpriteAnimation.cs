using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Linq;
using Microsoft.Xna.Framework.Graphics;

namespace TRexRunnigGame.Graphics
{
    public class SpriteAnimation
    {
        private List<SpriteAnimationFraem> _frames = new List<SpriteAnimationFraem>();

        public SpriteAnimationFraem this[int index]
        {
            get
            {
                return GetFrame(index);
            }
        }

        public SpriteAnimationFraem CurrentFrame
        {
            get
            {
                return _frames
                    .Where(f => f.TimeStamp <= PlayBackPorgress)
                    .OrderBy(f => f.TimeStamp)
                    .LastOrDefault();

            }
        }

        public float Duration
        {
            get
            {


                if (!_frames.Any())
                    return 0;
                return _frames.Max(f => f.TimeStamp);
            }
        }

        public bool IsPlaying { get; private set; }

        public float PlayBackPorgress { get; private set; }

        public bool ShouldLoop { get; set; } = true;

        public void AddFrame(Sprite sprite, float timeStamp)
        {
            SpriteAnimationFraem frame = new SpriteAnimationFraem(sprite, timeStamp);

            _frames.Add(frame);

        }



        public void Update(GameTime gametime)
        {
            if(IsPlaying)
            {
                PlayBackPorgress += (float)gametime.ElapsedGameTime.TotalSeconds;
                if (PlayBackPorgress > Duration)
                {
                    if(ShouldLoop)
                    PlayBackPorgress -= Duration;
                    else
                    {
                        Stop();
                    }
                }
            }
        }  

        public void Drew(SpriteBatch spriteBatch, Vector2 position)
        {
            SpriteAnimationFraem frame = CurrentFrame;

            if(frame!= null)
            frame.Sprite.Drew(spriteBatch, position);
            
            
        }

        public void Play()
        {
            IsPlaying = true;
        }

        public void Stop()
        {
            IsPlaying = false;
            PlayBackPorgress = 0;
        }


        public SpriteAnimationFraem GetFrame(int index)
        {
            if (index < 0 || index >= _frames.Count)
                throw new ArgumentOutOfRangeException(nameof(index), "this Frame does not exist");
            return _frames[index];
            
        } 

        public void Clear()
        {

            Stop();
            _frames.Clear();
        }
    }

}
