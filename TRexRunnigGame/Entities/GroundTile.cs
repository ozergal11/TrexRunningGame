using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TRexRunnigGame.Graphics;

namespace TRexRunnigGame.Entities
{
    public class GroundTile : IGameEntity
    {
        private float _PositionY;
        private float groundTileYPOS;

        public float PositionX { get; set; }
        public Sprite Sprite { get; }

        public int DrewOrder { get; set; }

        public GroundTile( float PositionX, float positiony, Sprite sprite)
        {
            this.PositionX = PositionX;
            this._PositionY = positiony;
            this.Sprite = sprite;
        }

        public GroundTile(float positionX, float groundTileYPOS)
        {
            PositionX = positionX;
            this.groundTileYPOS = groundTileYPOS;
        }

        public void Drew(SpriteBatch spriteBatch, GameTime gametime)
        { 
            Sprite.Drew(spriteBatch, new Vector2(PositionX, _PositionY ));
        }

        public void Update(GameTime gametime)
        {
           
        }
    }
}
