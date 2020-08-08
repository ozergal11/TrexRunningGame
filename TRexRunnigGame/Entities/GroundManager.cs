using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TRexRunnigGame.Graphics;

namespace TRexRunnigGame.Entities
{

    public class GroundManager : IGameEntity
    {
        private const float GroundTileYPOS = 119;

        private const int GroundLength = 600;
        private const int Sprite_HEIGHT = 14;

        private const int Ground_pos_x = 2;
        private const int Ground_pos_y = 54;

        private Texture2D _sprithsheet;
        private readonly EntityManager _entityManagar;

        private readonly List<GroundTile> _groundTIles;

        private Sprite _regularSprite;
        private Sprite _bumpySprite;

        private TRex _trex;

        private Random _random;

        public int DrewOrder { get; set; }


    
        public GroundManager(Texture2D spritesheet, EntityManager entityManager, TRex trex)
        {
            this._sprithsheet = spritesheet;
            _groundTIles = new List<GroundTile>();


            _entityManagar = entityManager;
            _regularSprite = new Sprite(spritesheet, Ground_pos_x, Ground_pos_y ,GroundLength, Sprite_HEIGHT);
            _regularSprite = new Sprite(spritesheet, Ground_pos_x + GroundLength, Ground_pos_y ,GroundLength, Sprite_HEIGHT);

            _trex = trex;
            _random = new Random();
        }

        public void Drew(SpriteBatch spritecatch, GameTime gametime)
        {
            
        }

        public void Update(GameTime gametime)
        {
            if(_groundTIles.Any())
            {
                float maxPos = _groundTIles.Max(g => g.PositionX);

                if (maxPos < 0)
                    SpawnTile(maxPos);
            }

            List<GroundTile> tiletoRemove = new List<GroundTile>();

            foreach(GroundTile gt in _groundTIles)
            {
                gt.PositionX -= _trex.Speed * (float)gametime.ElapsedGameTime.TotalSeconds;
                if( gt.PositionX < -GroundLength)
                {
                    _entityManagar.RemoveEntity(gt);
                    tiletoRemove.Add(gt);
                }
            }

            foreach(GroundTile gt in tiletoRemove)
            {
                _groundTIles.Remove(gt);
            }
        }

        public void Initialize()
        {
            _groundTIles.Clear();

            GroundTile groundtile = CreateRegularTile(0);
            _groundTIles.Add(groundtile);

            _entityManagar.AddEntity(groundtile);

        }


        private GroundTile CreateRegularTile(float PositionX)
        {
            GroundTile groundTile = new GroundTile(PositionX, GroundTileYPOS, _regularSprite);
            return groundTile;
        }
        private GroundTile CreatebumpyTile(float PositionX)
        {
            GroundTile groundTile = new GroundTile(PositionX, GroundTileYPOS, _bumpySprite);
            return groundTile;
        }

        private void SpawnTile(float maxPos)
        {
            double randomNumber = _random.NextDouble();

            float posX = maxPos+ GroundLength;
            GroundTile groundtiles;

            if (randomNumber > 0.5)
                groundtiles = CreatebumpyTile(posX);
            else
                groundtiles = CreateRegularTile(posX);

            _entityManagar.AddEntity(groundtiles);
            _groundTIles.Add(groundtiles);
        }
    }

}
