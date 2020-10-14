using Bindings;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Myra;
using Myra.Graphics2D;
using Myra.Graphics2D.UI;

namespace Client
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        public static SpriteBatch _spriteBatch;

        ClientTCP ctcp;
        ClientHandleData clientDataHandle;

        InterfaceGUI IGUI = new InterfaceGUI();
        public static Desktop _desktop;

        float WalkTimer;
        public static new int Tick;
        public static int ElapsedTime;
        public static int FrameTime;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _graphics.PreferredBackBufferWidth = 1200;
            _graphics.PreferredBackBufferHeight = 1000;
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            ctcp = new ClientTCP();
            clientDataHandle = new ClientHandleData();
            clientDataHandle.InitializeMessages();
            ctcp.ConnectToServer();

    

            Graphics.InitializeGraphics(Content);

            base.Initialize();

            IGUI.InitializeGUI(this, _desktop);
            MenuManager.ChangeMenu(MenuManager.Menu.Login, _desktop);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _desktop = new Desktop();
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightSkyBlue);
            if (Globals.InGame)
            {
                Tick = (int)gameTime.TotalGameTime.TotalMilliseconds;
                ElapsedTime = (Tick - FrameTime);
                FrameTime = Tick;

                if (WalkTimer < Tick)
                {
                    GameLogic.ProcessMovement(Globals.playerIndex);
                    WalkTimer = Tick + 30;
                }

                CheckKeys();
                GameLogic.CheckMovement();
                Graphics.RenderGraphics();
            }

            _desktop.Render();

            base.Draw(gameTime);
        }

        private void CheckKeys()
        {
            Globals.DirUp = Keyboard.GetState().IsKeyDown(Keys.Up);
            Globals.DirDown = Keyboard.GetState().IsKeyDown(Keys.Down);
            Globals.DirRight = Keyboard.GetState().IsKeyDown(Keys.Right);
            Globals.DirLeft = Keyboard.GetState().IsKeyDown(Keys.Left);
        }
    }
}
