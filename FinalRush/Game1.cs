using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FinalRush
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Player player;
        Board board;
        Texture2D Rectangle;
        float timer = 0;
        private SpriteFont font;

        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            player = new Player();
            board = new Board();

            this.graphics.PreferredBackBufferWidth = 1050;
            this.graphics.PreferredBackBufferHeight = 600;
            this.Window.Position = new Point(190, 90);
            this.graphics.ApplyChanges();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            Vector2 playerPosition = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X, GraphicsDevice.Viewport.TitleSafeArea.Y + GraphicsDevice.Viewport.TitleSafeArea.Height / 2);
            player.Initialize(Content.Load<Texture2D>("Graphics\\player"), playerPosition);

            Rectangle = new Texture2D(GraphicsDevice, 1, 1);
            Rectangle.SetData(new[] { Color.White });

            font = Content.Load<SpriteFont>("font");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            UpdatePlayer(gameTime);

            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            base.Update(gameTime);
        }

        private void UpdatePlayer(GameTime gameTime)
        {
            if (currentKeyboardState.IsKeyDown(Keys.Left))
            {
                player.VelocityX -= 0.1f;
            }

            if (currentKeyboardState.IsKeyDown(Keys.Right))
            {
                player.VelocityX += 0.1f;
            }

            if (currentKeyboardState.IsKeyDown(Keys.Up))
            {
                player.VelocityY -= 0.1f;
            }

            if (currentKeyboardState.IsKeyDown(Keys.Down))
            {
                player.VelocityY += 0.1f;
            }
            player.Position.X += player.VelocityX;
            player.Position.Y += player.VelocityY;
            player.VelocityY = MathHelper.Clamp(player.VelocityY, -5, 5);
            player.VelocityX = MathHelper.Clamp(player.VelocityX, -5, 5);

            float outX = MathHelper.Clamp(player.Position.X, 0, GraphicsDevice.Viewport.Width - player.Width);
            float outY = MathHelper.Clamp(player.Position.Y, 0, GraphicsDevice.Viewport.Height - player.Height);

            if (outX == 0 || outX == GraphicsDevice.Viewport.Width - player.Width)
            {
                player.Position.X = outX;
                player.VelocityX = 0;
            }
            if (outY == 0 || outY == GraphicsDevice.Viewport.Height - player.Height)
            {
                player.Position.Y = outY;
                player.VelocityY = 0;
            }
            if (currentKeyboardState.IsKeyDown(Keys.Down | Keys.Up | Keys.Left | Keys.Right) == false)
            {
                if (player.VelocityX > 0)
                    player.VelocityX -= 0.02f;
                else if (player.VelocityX < 0)
                    player.VelocityX += 0.02f;

                if (player.VelocityY > 0)
                    player.VelocityY -= 0.02f;
                else if (player.VelocityY < 0)
                {
                    player.VelocityY += 0.02f;
                }
            }
        }
        
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            board.Draw(spriteBatch, Rectangle);
            player.Draw(spriteBatch);
            spriteBatch.DrawString(font, "Player1: " + timer.ToString("0.00"), new Vector2(5, 580), Color.Black);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
