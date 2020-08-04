using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRexRunnigGame.Entities
{
    public interface IGameEntity
    {
        int DrewOrder {get;}

        void Update(GameTime gametime);

        void Drew(SpriteBatch spritecatch, GameTime gametime);
    }
}
