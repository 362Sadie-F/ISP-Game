using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace ISP_Game
{
    public class PlayerSprite
    {

    }

    public class Walls
    {
        Rectangle wall; //width is 9

    }


    public class Game1 : Game //3rd person, look down on map, keyboard to move sprite to explore rooms, bump into things reveals them
    {
       
        
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Rectangle mainWindow;
        Rectangle goIn;
        Rectangle escape;
        Rectangle button0;
        Rectangle level0Exit;
        Rectangle level1Exit;
         

        Texture2D menuScreen;
        Texture2D tutorial;
        Texture2D level1;
        Texture2D hallway1;
        Texture2D hallway2;
        Texture2D goInButton;
        Texture2D escapeButton;
        
        MouseState mouseState;
        KeyboardState keyboardState;

        Song fallenDown;
        Song lostWoods;
        Song carousel;
        

        enum Screen
        {
            Menu, //fallen down
            Level0, //lost woods, fade in and out. Replace audio
            Hall1, //door open and close
            Level1, //numbers
            Hall2, //door open and close
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
            screen = Screen.Menu;
            mainWindow = new Rectangle(0, 0, 800, 600);
            _graphics.PreferredBackBufferHeight = mainWindow.Height;
            _graphics.PreferredBackBufferWidth = mainWindow.Width;
            _graphics.ApplyChanges();
            if (screen == Screen.Menu)
            {
                goIn = new Rectangle(20, 205, 205, 255);
                button0 = new Rectangle(270, 20, 660, 95);
                escape = new Rectangle(20, 115, 210, 165);
            }

            else if (screen == Screen.Level0)
            {
                level0Exit = new Rectangle(615, 230, 625, 285);
            }
            // level1Exit = new Rectangle(20, 115, 210, 165);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            menuScreen = Content.Load<Texture2D>("Menu");
            tutorial = Content.Load<Texture2D>("0");
            level1 = Content.Load<Texture2D>("1");
            hallway1 = Content.Load<Texture2D>("DaycareHall");
            hallway2 = Content.Load<Texture2D>("SchoolHall");
            goInButton = Content.Load<Texture2D>("GoIn");
            escapeButton = Content.Load<Texture2D>("Escape");
            fallenDown = Content.Load<Song>("FallenDown");
            lostWoods = Content.Load<Song>("LostWoods");
            carousel = Content.Load<Song>("Carousel");

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            mouseState = Mouse.GetState();
            keyboardState = Keyboard.GetState();
            
            Window.Title = "Toy Box " + mouseState.Position.ToString();
            //collect 3 toys, move to door, have rectangle in front, code so it only advances when 3 toys are collected, display hallway photo for short time sound then move to next level.

            if (screen == Screen.Menu)
            {
               if (MediaPlayer.State == MediaState.Stopped)
               {
                   MediaPlayer.IsRepeating = true;
                    MediaPlayer.Volume = 0.5f;
                    MediaPlayer.Play(fallenDown);
               }

                if (goIn.Contains(mouseState.Position))
                {
                    
                    if (mouseState.RightButton == ButtonState.Pressed)
                    {
                        screen = Screen.Level0;
                        MediaPlayer.Stop();
                    }
                }
                if (escape.Contains(mouseState.Position))
                {
                    
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        Exit();
                    }
                }

            }

            else if (screen == Screen.Level0)
            {
               
                if (MediaPlayer.State == MediaState.Stopped)
                {
                    MediaPlayer.IsRepeating = true;
                    MediaPlayer.Volume = 0.5f;
                    MediaPlayer.Play(lostWoods);
                }

                if (keyboardState.IsKeyDown(Keys.E))
                {
                    screen = Screen.Hall1;
                    MediaPlayer.Stop();
                    //door open and close sound, then continue to level1
                }
                //if (level0Exit.Contains(mouseState.Position))
                //{
                //    // if player has all 3 objects, continue to next level. if not do nothing
                //    if (keyboardState.IsKeyDown(Keys.E))
                //    {
                //        screen = Screen.Hall1;
                //    }
                //}
            }

            else if (screen == Screen.Hall1)
            {
                if (keyboardState.IsKeyDown(Keys.O))
                {
                    screen = Screen.Level1;
                }
                
            }

            else if (screen == Screen.Level1)
            {
                if (MediaPlayer.State == MediaState.Stopped)
                {
                    MediaPlayer.IsRepeating = true;
                    MediaPlayer.Volume = 0.2f;
                    MediaPlayer.Play(carousel);
                }

                if (keyboardState.IsKeyDown(Keys.W))
                {
                    screen = Screen.Hall2;
                }

            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            if (screen == Screen.Menu) //menu
            {
                _spriteBatch.Draw(menuScreen, new Rectangle(0, 0, 800, 600), Color.White);

            }
            else if (screen == Screen.Level0) //Tutorial
            {
                _spriteBatch.Draw(tutorial, new Rectangle(0, 0, 800, 600), Color.White);
            }
            else if (screen == Screen.Hall1) //hallway to level 1
            {
                _spriteBatch.Draw(hallway1, new Rectangle(0, 0, 800, 600), Color.White);
            }
            else if (screen == Screen.Level1) //level 1
            {
                _spriteBatch.Draw(level1, new Rectangle(0, 0, 800, 600), Color.White);
            }
            else if (screen == Screen.Hall2) //hallway to level 2
            {
                _spriteBatch.Draw(hallway2, new Rectangle(0, 0, 800, 600), Color.White);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
