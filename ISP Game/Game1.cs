using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ISP_Game
{
    public class PlayerSprite
    {

    }

    public class Walls
    {
        List<Rectangle> mapWalls = new List<Rectangle>();
        // mapWalls.Add(new Rectangle(0,0,600,30)); 1
        // mapWalls.Add(new Rectangle(320, 0, 500, 230)) 2
        // mapWalls.Add(new Rectangle(320,290,500,60)) 3
        // mapWalls.Add(new Rectangle(625,225,800-625,125)) 4
        // mapWalls.Add(new Rectangle(750,0, 100, 600)) 5
        // mapWalls.Add(new Rectangle(0,560,800,100)) 6
        // mapWalls.Add(new Rectangle(0,173,95,9)) 7
        // mapWalls.Add(new Rectangle(185,123,95,9)) 8
        // mapWalls.Add(new Rectangle(0,330,95,9)) 9
        // mapWalls.Add(new Rectangle(84,340,9,72)) 10
        // mapWalls.Add(new Rectangle(195,330,95,9)) 11
        // mapWalls.Add(new Rectangle(196,340,9,72)) 12
        // mapWalls.Add(new Rectangle(320,348,9,72)) 13
        // mapWalls.Add(new Rectangle(320,476,9,72)) 14
        // mapWalls.Add(new Rectangle(430,425,22,125)) 15
        // mapWalls.Add(new Rectangle(516,350,22,125)) 16
        // mapWalls.Add(new Rectangle(6125,425,22,125)) 17

    }


    public class Game1 : Game //3rd person, look down on map, keyboard to move sprite to explore rooms, bump into things reveals them
    {
       
        
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Rectangle mainWindow;
        Rectangle goIn;
        Rectangle escape;
        Rectangle button0;
        Rectangle button1;
        Rectangle level0Exit;
        Rectangle level1Exit;
        Rectangle menuEscape;
        Rectangle toyLocation1;
        Rectangle toyLocation2;
        Rectangle toyLocation3;
        Rectangle spriteSize;
        Vector2 spriteSpeed;
        
        

        Texture2D menuScreen;
        Texture2D tutorial;
        Texture2D level1;
        Texture2D hallway1;
        Texture2D hallway2;
        Texture2D goInButton;
        Texture2D escapeButton;
        Texture2D gameEnd;
        Texture2D sprite;
        Texture2D toyBear;
        Texture2D toyCat;
        Texture2D toyDuck;
        
        MouseState mouseState;
        KeyboardState keyboardState;

        Song fallenDown;
        Song lostWoods;
        Song carousel;
        SoundEffect death;
        SoundEffectInstance deathEffect;
        SoundEffect emptyRoom;
        SoundEffectInstance emptyRoomEffect;
       
        

        enum Screen
        {
            Menu, //fallen down
            Level0, //lost woods, fade in and out. Replace audio
            Hall1, //door open and close
            Level1, //numbers
            Hall2, //door open and close
            GameOver, //"The END is just the BEGINing" click on end to esc, click on begin to restart
            GameWin //"You escaped"
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
            menuEscape = new Rectangle(685, 5, 112, 35);
            toyLocation1 = new Rectangle(714, 381, 30, 30);
            toyLocation2 = new Rectangle(25, 358, 30, 30);
            toyLocation3 = new Rectangle(289, 38, 30, 30);
            spriteSize = new Rectangle(15, 255, 40, 40);
            spriteSpeed = Vector2.Zero;

            if (screen == Screen.Menu)
            {
                goIn = new Rectangle(20, 205, 205, 255);
                button0 = new Rectangle(270, 20, 660, 95);
                button1 = new Rectangle(680, 20, 660, 95);
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
            escapeButton = Content.Load<Texture2D>("EscapeButton");
            gameEnd = Content.Load<Texture2D>("GameOver");
            toyBear = Content.Load<Texture2D>("bearToy");
            toyDuck = Content.Load<Texture2D>("duckToy");
            fallenDown = Content.Load<Song>("FallenDown");
            lostWoods = Content.Load<Song>("LostWoods");
            carousel = Content.Load<Song>("Carousel");
            emptyRoom = Content.Load<SoundEffect>("EmptyRoom");
            emptyRoomEffect = emptyRoom.CreateInstance();
            emptyRoomEffect.IsLooped = true;
            death = Content.Load<SoundEffect>("de@thEffect");
            deathEffect = death.CreateInstance();
            deathEffect.IsLooped = false;
            sprite = Content.Load<Texture2D>("SpriteIdle");

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            mouseState = Mouse.GetState();
            keyboardState = Keyboard.GetState();
            
            
            Window.Title = "Toy Box " + mouseState.Position.ToString();
            //collect 3 toys, move to door, have rectangle in front, code so it only advances when 3 toys are collected, display hallway photo for short time sound then move to next level.

           if (menuEscape.Contains(mouseState.Position))
           {
                if (mouseState.RightButton == ButtonState.Pressed)
                {
                    screen = Screen.Menu;
                    MediaPlayer.Stop();
                }
            }

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

                if (button0.Contains(mouseState.Position))
                {
                    if (mouseState.RightButton == ButtonState.Pressed)
                    {
                        screen = Screen.Level0;
                        MediaPlayer.Stop();
                    }
                }
                if (button1.Contains(mouseState.Position))
                {
                    if (mouseState.RightButton == ButtonState.Pressed)
                    {
                        screen = Screen.Level1;
                        MediaPlayer.Stop();
                    }
                }

            }

            else if (screen == Screen.Level0)
            {
                if (menuEscape.Contains(mouseState.Position))
                {
                    if (keyboardState.IsKeyDown(Keys.M))
                    {
                        screen = Screen.Menu;
                    }
                }

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
                
                 if (keyboardState.IsKeyDown(Keys.G))
                 {
                   screen = Screen.GameOver;
                   MediaPlayer.Stop();
                 }

                spriteSpeed = Vector2.Zero;
                if (keyboardState.IsKeyDown(Keys.W))
                {
                    spriteSpeed.Y -= 2;
                }
                if (keyboardState.IsKeyDown(Keys.S))
                {
                    spriteSpeed.Y += 2;
                }
                if (keyboardState.IsKeyDown(Keys.A))
                {
                    spriteSpeed.X -= 2;
                }
                if (keyboardState.IsKeyDown(Keys.D))
                {
                    spriteSpeed.X += 2;
                }
                spriteSize.Offset(spriteSpeed);

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

                if (keyboardState.IsKeyDown(Keys.P))
                {
                    screen = Screen.Hall2;
                }

                if (menuEscape.Contains(mouseState.Position))
                {
                    if (keyboardState.IsKeyDown(Keys.M))
                    {
                        screen = Screen.Menu;
                    }
                }

            }

            else if (screen == Screen.GameOver)
            {
                if (MediaPlayer.State == MediaState.Stopped)
                {
                    deathEffect.Play();
                    deathEffect.IsLooped = false;

                    if (deathEffect.IsLooped == false)
                    {
                        emptyRoomEffect.IsLooped = true;
                        emptyRoomEffect.Play();
                    }

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
                _spriteBatch.Draw(escapeButton, menuEscape, Color.White);
                _spriteBatch.Draw(toyBear, toyLocation1, Color.White);
                _spriteBatch.Draw(toyDuck, toyLocation2, Color.White);
               // _spriteBatch.Draw(toyCat, toyLocation3, Color.White);
                _spriteBatch.Draw(sprite, spriteSize, Color.White);

            }
            else if (screen == Screen.Hall1) //hallway to level 1
            {
                _spriteBatch.Draw(hallway1, new Rectangle(0, 0, 800, 600), Color.White);
            }
            else if (screen == Screen.Level1) //level 1
            {
                _spriteBatch.Draw(level1, new Rectangle(0, 0, 800, 600), Color.White);
                _spriteBatch.Draw(escapeButton, menuEscape, Color.White);
            }
            else if (screen == Screen.Hall2) //hallway to level 2
            {
                _spriteBatch.Draw(hallway2, new Rectangle(0, 0, 800, 600), Color.White);
            }
            else if (screen == Screen.GameOver) //game loose
            {
                _spriteBatch.Draw(gameEnd, new Rectangle(0, 0, 800, 600), Color.White);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
