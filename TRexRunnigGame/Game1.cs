using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
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
        private const float FadeSpeed = 500f;
        private GraphicsDeviceManager graphics;
        private SpriteBatch _spriteBatch;

        private SoundEffect _sfxHit;
        private SoundEffect _sfxButtonPress;
        private SoundEffect _sfxScoreReached;

        private Texture2D _spriteSheetTexture;
        private Texture2D _fadeInAnimation;

        private float _FadeInTexturePosX;

        private TRex  _TRex;

        private ImputController _InputController;

        private GroundManager _groundmanager;

        private EntityManager _entityManager;

        private KeyboardState _previousKeyboradState;
        public GameState State { get; set; }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _entityManager = new EntityManager();
            State = GameState.Initial;
            _FadeInTexturePosX = TRex.TrexWidth;
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


            _fadeInAnimation = new Texture2D(GraphicsDevice, 1, 1);
            _fadeInAnimation.SetData(new Color[] { Color.White });


            _TRex = new TRex(_spriteSheetTexture, new Vector2(Trexpos, TrexYpos - TRex.TrexHeiht), _sfxButtonPress);
            _TRex.DrewOrder = 10;


            _InputController = new ImputController(_TRex);

            _groundmanager = new GroundManager(_spriteSheetTexture, _entityManager,  _TRex);

            _entityManager.AddEntity(_TRex);
            _entityManager.AddEntity(_groundmanager);

            _groundmanager.Initialize();
        }

        
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }


        protected override void Update(GameTime gameTime)
        {

            // TODO: Add your update logic here

            base.Update(gameTime);

            KeyboardState KeyboardState = Keyboard.GetState();

            if (State == GameState.Playing)
                _InputController.ProcessControl(gameTime);

            else if (State == GameState.Transition)
                _FadeInTexturePosX += (float)gameTime.ElapsedGameTime.TotalSeconds * FadeSpeed;
            else if (State == GameState.Initial)
            {
                bool IsStartKey = KeyboardState.IsKeyDown(Keys.Up) || KeyboardState.IsKeyDown(Keys.Space);
                bool WasKey = KeyboardState.IsKeyDown(Keys.Up) || KeyboardState.IsKeyDown(Keys.Up);

                if (IsStartKey && WasKey)
                {
                    StartGame();
                }
            }

            _entityManager.Update(gameTime);

            _previousKeyboradState = KeyboardState;

        }

        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            _spriteBatch.Begin();
            _entityManager.Drew(_spriteBatch, gameTime);
            if(State == GameState.Initial || State == GameState.Transition)
            {
                _spriteBatch.Draw(_fadeInAnimation, new Rectangle((int)Math.Round(_FadeInTexturePosX), 0, Window_Width, Window_Height), Color.White); 
            }


            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private bool StartGame()
        {
            if(State != GameState.Initial )
                return false;

            State = GameState.Transition;
            _TRex.StartJumping();
            return true;
        }
    }
} 
