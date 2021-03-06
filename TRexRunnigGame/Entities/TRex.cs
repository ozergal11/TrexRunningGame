﻿using Microsoft.Xna.Framework.Graphics;
using TRexRunnigGame.Graphics;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Audio;

namespace TRexRunnigGame.Entities
{
    public class TRex : IGameEntity
    {

        private const float Run_Length = 0.1f;
        
        private const float MinJumpHeight = 40f;

        private float GRAVITY = 1600f;
        private const float JumpSpeed = -450f;

        private const float cancleJumpVelo = -200f;

        public const int Trexpos = 848;
        public const int TrexYpos = 0;
        public const int TrexWidth = 44;
        public const int TrexHeiht = 52;

        private const int TREX_DUCKING_WITDH = 59;
        private const int Trex_Ducking_X = Trexpos + TrexWidth * 6; 


        public const int _IdleBAckGround_posY = 0;
        public const int _IdleBAckGround_posX = 40;
        private const float Blink_Animation = 1f/4;

        private const float DropVelo = 600f;

        private Sprite _IdleSprit;
        private Sprite _IdleBlinkSprite;
        private SpriteAnimation _runAnimation;
        private SpriteAnimation _DuckingAnimation;

         

        private Random _random;


        public Vector2 Position { get; set; }

        public TrexState State { get; private set; }

        public bool IsAlive { get; private set; }

        public float Speed { get; private set; }  

        public int DrewOrder {  get; set; }


        public Sprite _IdleBackGround;
        public SpriteAnimation _BlinkAnimation;

        private SoundEffect _JumpSound;

        private float _Verveloc;
        private float _startposy;
        private float _DropVelo;


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

            _startposy = Position.Y;


            _runAnimation = new SpriteAnimation();

            _runAnimation.AddFrame(new Sprite(spritesheet, Trexpos + TrexWidth * 2, TrexYpos, TrexWidth, TrexHeiht), 0);
            _runAnimation.AddFrame(new Sprite(spritesheet, Trexpos + TrexWidth * 3, TrexYpos, TrexWidth, TrexHeiht), Run_Length);
            _runAnimation.AddFrame(_runAnimation[0].Sprite, Run_Length*2);

            _runAnimation.Play();
            _runAnimation.ShouldLoop = true;
             
            _DuckingAnimation = new SpriteAnimation();
            _DuckingAnimation.AddFrame(new Sprite(spritesheet, Trex_Ducking_X, TrexYpos, TREX_DUCKING_WITDH, TrexHeiht), 0);
            _DuckingAnimation.AddFrame(new Sprite(spritesheet, Trex_Ducking_X + TREX_DUCKING_WITDH, TrexYpos, TREX_DUCKING_WITDH, TrexHeiht), Run_Length);
            _DuckingAnimation.AddFrame(_DuckingAnimation[0].Sprite, Run_Length * 2);

            _DuckingAnimation.Play(); 
        }  

        public void Drew(SpriteBatch spritebatch, GameTime gametime)
        {
            if (State == TrexState.idle)
            {
                _IdleBackGround.Drew(spritebatch, this.Position);
                _BlinkAnimation.Drew(spritebatch, this.Position);

            }

            else if (State == TrexState.jumping || State == TrexState.falling)
            {

                _IdleSprit.Drew(spritebatch, Position);

            }

            else if (State == TrexState.runnig)
            {
                _runAnimation.Drew(spritebatch, Position);
            }
            
            else if (State == TrexState.ducking)
            {
                _DuckingAnimation.Drew(spritebatch, Position);
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

            else if (State == TrexState.jumping || State == TrexState.falling)
            {


                Position = new Vector2(Position.X, Position.Y + _Verveloc * (float)gametime.ElapsedGameTime.TotalSeconds + _DropVelo * (float)gametime.ElapsedGameTime.TotalSeconds);
                _Verveloc += GRAVITY * (float)gametime.ElapsedGameTime.TotalSeconds;

                if (_Verveloc >= 0)
                    State = TrexState.falling;

                if(Position.Y >= _startposy)
                {

                    Position = new Vector2(Position.X, Position.Y);
                    _Verveloc = 0;
                    State = TrexState.runnig;
                }


            }

            else if(State == TrexState.runnig)
            {
                _runAnimation.Update(gametime);
            }

            else if(State == TrexState.ducking)
            {
                _DuckingAnimation.Update(gametime);
            }

            _DropVelo = 0;

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
            State = TrexState.jumping;
            _Verveloc = JumpSpeed;
            return true;
        }


        public bool CancleJump()
        {
            if (State != TrexState.jumping || (_startposy -Position.Y) < MinJumpHeight)
                return false;

            
            _Verveloc = _Verveloc < cancleJumpVelo ? cancleJumpVelo : 0;
                
            return true;

        }

        public bool Duck()
        {
            if (State == TrexState.jumping || State == TrexState.falling)
                return false;


            State = TrexState.ducking;
            return true;
        }

        public bool CancleDucking()
        {
            if (State != TrexState.ducking)
                return false;

            State = TrexState.runnig;
            return true;
        }

        public bool Drop()
        {
            if (State != TrexState.falling && State !=TrexState.jumping)
                return false;


            State = TrexState.falling;

            _DropVelo = DropVelo;
            return true;
        }
    }
}
