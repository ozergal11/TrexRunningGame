using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRexRunnigGame.Entities;

namespace TRexRunnigGame.System
{
    public class ImputController
    {
        private TRex _Trex;

        private KeyboardState  _previouskeyBoradState;

        public ImputController(TRex trex)
        {
            this._Trex = trex;
        }

        public void ProcessControl(GameTime gametime)
        {
            KeyboardState KeyboardState = Keyboard.GetState();

            bool  IsJumpKeyPressed = KeyboardState.IsKeyDown(Keys.Up) || KeyboardState.IsKeyDown(Keys.Space);
            bool wasJumpKeyPressed = _previouskeyBoradState.IsKeyDown(Keys.Up) || _previouskeyBoradState.IsKeyDown(Keys.Space);

            if (!wasJumpKeyPressed && IsJumpKeyPressed)
            {
                if (_Trex.State != TrexState.jumping)
                {
                    _Trex.StartJumping();

                }

            }

            else if (_Trex.State == TrexState.jumping && !IsJumpKeyPressed )
            {
                _Trex.CancleJump();
            }

            else if (KeyboardState.IsKeyDown(Keys.Down))
            {
                if (_Trex.State == TrexState.jumping || _Trex.State == TrexState.falling)
                    _Trex.Drop();
                else
                    _Trex.Duck();

            }

            else if (_Trex.State == TrexState.ducking && !KeyboardState.IsKeyDown(Keys.Down))
            {
                _Trex.CancleDucking();
            }
            _previouskeyBoradState = KeyboardState;
        }
    }
}
