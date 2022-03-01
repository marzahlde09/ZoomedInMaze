using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ZoomedInMaze
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //global variables go here
        Environment maze;

        Texture2D qbert;
        Rectangle qbertRect;

        int speed;

        int[,] mazeMap;
        int[,] mazeCollisionMap;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            mazeMap = new int[,]{
                { 1, 0, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                { 1, 1, 1, 1, 1, 1, 1, 1, 0, 1 },
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                { 1, 0, 1, 1, 1, 0, 1, 1, 0, 1 },
                { 1, 0, 1, 1, 1, 1, 1, 1, 1, 1 }
            };

            mazeCollisionMap = new int[,]{
                { 1, 0, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                { 1, 1, 1, 1, 1, 1, 1, 1, 0, 1 },
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                { 1, 0, 1, 1, 1, 0, 1, 1, 1, 1 },
                { 1, 0, 1, 1, 1, 1, 1, 1, 1, 1 }
            };

            qbertRect = new Rectangle(325, 165, 150, 150);
            speed = 5;


            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            maze = new Environment(
                160, 160, 160,
                mazeMap,
                mazeCollisionMap,
                new Texture2D[] {
                    this.Content.Load<Texture2D>("minecraftGrass"),
                    this.Content.Load<Texture2D>("brickWall")
                });

            qbert = this.Content.Load<Texture2D>("qbert");

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            KeyboardState keys = Keyboard.GetState();

            if (keys.IsKeyDown(Keys.S))
            {
                maze.move(0, -1 * speed);
                while (maze.collides(qbertRect)[0] != -1)
                {
                    maze.move(0, 1);
                }
            }

            if (keys.IsKeyDown(Keys.W))
            {
                maze.move(0, speed);
                while (maze.collides(qbertRect)[0] != -1)
                {
                    maze.move(0, -1);
                }
            }

            if (keys.IsKeyDown(Keys.A))
            {
                maze.move(speed, 0);
                while (maze.collides(qbertRect)[0] != -1)
                {
                    maze.move(-1, 0);
                }
            }

            if (keys.IsKeyDown(Keys.D))
            {
                maze.move(-1 * speed, 0);
                while (maze.collides(qbertRect)[0] != -1)
                {
                    maze.move(1, 0);
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            maze.draw(_spriteBatch);
            _spriteBatch.Draw(qbert, qbertRect, Color.White);

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}