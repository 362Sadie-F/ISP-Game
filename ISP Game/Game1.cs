using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ISP_Game
{
    public class Game1 : Game //3rd person, look down on map, keyboard to move sprite to explore rooms, bump into things reveals them
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Rectangle mainWindow;
        Texture2D menuScreen;
        Texture2D tutorial;
        Texture2D level1;


        enum Screen
        {
            Menu,
            Level0,
            Level1,
            GameOver,
            GameWin
        }
        Screen screen;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            screen = Screen.Level1;
            mainWindow = new Rectangle(0, 0, 800, 600);
            _graphics.PreferredBackBufferHeight = mainWindow.Height;
            _graphics.PreferredBackBufferWidth = mainWindow.Width;
            _graphics.ApplyChanges();


            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            //menuScreen = Content.Load<Texture2D>(" ");
            tutorial = Content.Load<Texture2D>("0");
            level1 = Content.Load<Texture2D>("1");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            if (screen == Screen.Menu)
            {
                _spriteBatch.Draw(menuScreen, new Rectangle(0, 0, 800, 600), Color.White);

            }
            else if (screen == Screen.Level0)
            {
                _spriteBatch.Draw(tutorial, new Rectangle(0, 0, 800, 600), Color.White);
            }
            else if (screen == Screen.Level1)
            {
                _spriteBatch.Draw(level1, new Rectangle(0, 0, 800, 600), Color.White);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
