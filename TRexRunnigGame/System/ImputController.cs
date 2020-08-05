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

            if (!_previouskeyBoradState.IsKeyDown(Keys.Up) && KeyboardState.IsKeyDown(Keys.Up))
            {
                if (_Trex.State != TrexState.jumping)
                {
                    _Trex.StartJumping();

                }

            }

            else if (_Trex.State == TrexState.jumping && !KeyboardState.IsKeyDown(Keys.Up))
            {
                _Trex.CancleJump();
            }
            _previouskeyBoradState = KeyboardState;


        }
    }
}
