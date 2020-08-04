using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using TRexRunnigGame.Entities;
using TRexRunnigGame.Graphics;
using TRexRunnigGame.System;

namespace TRexRunnigGame
{
    
    public class Game1 : Game
    {
        private const string ASSET_NAME_SPRITESHEET = "TRexSpritesheet";
        private const string ASSET_NAME_SFX_HIT = "die";
        private const string ASSET_NAME_SFX_SCORE_REACHED = "point";
        private const string ASSET_NAME_SFX_BUTTON_PRESS = "jump";

        public const int Window_Height = 150;
        public const int Window_Width = 600;

        public const int Trexpos = 1;
        public const int TrexYpos = Window_Height - 16 ;

        private GraphicsDeviceManager graphics;
        private SpriteBatch _spriteBatch;

        private SoundEffect _sfxHit;
        private SoundEffect _sfxButtonPress;
        private SoundEffect _sfxScoreReached;

        private Texture2D _spriteSheetTexture;

        private TRex  _TRex;
        private ImputController _InputController;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            graphics.PreferredBackBufferWidth = Window_Width;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = Window_Height;   // set this value to the desired height of your window
            graphics.ApplyChanges();
        }

        
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _sfxButtonPress = Content.Load<SoundEffect>(ASSET_NAME_SFX_BUTTON_PRESS);
            _sfxHit = Content.Load<SoundEffect>(ASSET_NAME_SFX_HIT);
            _sfxScoreReached = Content.Load<SoundEffect>(ASSET_NAME_SFX_SCORE_REACHED);
            _spriteSheetTexture = Content.Load<Texture2D>(ASSET_NAME_SPRITESHEET);

            _TRex = new TRex(_spriteSheetTexture, new Vector2(Trexpos, TrexYpos - TRex.TrexHeiht), _sfxButtonPress);

            _InputController = new ImputController(_TRex);
        }

        
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        
        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here

            base.Update(gameTime);
            _InputController.ProcessControl(gameTime);
        
            _TRex.Update(gameTime);
        }

        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            _spriteBatch.Begin();
            _TRex.Drew(_spriteBatch, gameTime);

            _spriteBatch.End();



            base.Draw(gameTime);
        }
    }
} 
