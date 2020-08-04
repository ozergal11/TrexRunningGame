﻿
using Microsoft.Xna.Framework.Graphics;
using TRexRunnigGame.Graphics;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Audio;

namespace TRexRunnigGame.Entities
{
    public class TRex : IGameEntity
    {
        public const int Trexpos = 848;
        public const int TrexYpos = 0;
        public const int TrexWidth = 44;
        public const int TrexHeiht = 52;

        public const int _IdleBAckGround_posY = 0;
        public const int _IdleBAckGround_posX = 40;
        private const float Blink_Animation = 1f/4;

        private Sprite _IdleSprit;
        private Sprite _IdleBlinkSprite;

        private Random _random;

        public Vector2 Position { get; set; }

        public TrexState State { get; private set; }

        public bool IsAlive { get; private set; }

        public float Speed { get; private set; }

        public int DrewOrder {  get; set; }


        public Sprite _IdleBackGround;
        public SpriteAnimation _BlinkAnimation;

        private SoundEffect _JumpSound;

        public TRex(Texture2D spritesheet, Vector2 position, SoundEffect jumpSound)
        {
            this.Position = position;
            this._JumpSound = jumpSound;
            _IdleBackGround = new Sprite(spritesheet, _IdleBAckGround_posX, _IdleBAckGround_posY, TrexWidth, TrexHeiht);
            State = TrexState.idle;
            _random = new Random();

            _IdleSprit = new Sprite(spritesheet, Trexpos, TrexYpos, TrexWidth, TrexHeiht);
            _IdleBlinkSprite = new Sprite(spritesheet, Trexpos + TrexWidth, TrexYpos, TrexWidth, TrexHeiht);

            _BlinkAnimation = new SpriteAnimation();

            CreateBlinkAnimation();
            _BlinkAnimation.Play();
        }

        public void Drew(SpriteBatch spritebatch, GameTime gametime)
        {
            if (State == TrexState.idle)
            {
                _IdleBackGround.Drew(spritebatch, this.Position);
                _BlinkAnimation.Drew(spritebatch, this.Position);

            }


            

        }

        public void Update(GameTime gametime)
        {
            if(State == TrexState.idle)
            {
                

                if (!_BlinkAnimation.IsPlaying)
                {
                    CreateBlinkAnimation();
                    _BlinkAnimation.Play();
                }
                _BlinkAnimation.Update(gametime);
            }
        }

        private void CreateBlinkAnimation()
        {
            _BlinkAnimation.Clear();
            _BlinkAnimation.ShouldLoop = false;
       
            double BlinktimeStamp = 2f + _random.NextDouble() * (10f - 2f);

            _BlinkAnimation.AddFrame(_IdleSprit, 0);
            _BlinkAnimation.AddFrame(_IdleBlinkSprite, (float)BlinktimeStamp);
            _BlinkAnimation.AddFrame(_IdleSprit, (float)BlinktimeStamp + Blink_Animation);
        }

        public bool StartJumping()
        {
            if(State == TrexState.jumping || State == TrexState.falling)
            {
                return false;
            }
            _JumpSound.Play();
            return true;
        }


        public bool ContinueJumping()
        {

            return true;

        }




    }
}