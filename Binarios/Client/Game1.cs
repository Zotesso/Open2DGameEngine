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
        private SpriteBatch _spriteBatch;
        private Desktop _desktop;

        ClientTCP ctcp;
        ClientHandleData clientDataHandle;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            ctcp = new ClientTCP();
            clientDataHandle = new ClientHandleData();
            clientDataHandle.InitializeMessages();
            ctcp.ConnectToServer();
          //  ctcp.SendLogin();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            MyraEnvironment.Game = this;

            var panel = new Panel();

            var login = new Label
            {
                Id = "label",
                Text = "Login:",
                TextColor = Color.Black,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            panel.Widgets.Add(login);
            
            var textBox1 = new TextBox {
                Margin = new Thickness(0, 40, 0, 0),
                Width = 200,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            panel.Widgets.Add(textBox1);

            var password = new Label
            {
                Margin = new Thickness(0, 80, 0, 0),
                Id = "label",
                Text = "Password:",
                TextColor = Color.Black,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            panel.Widgets.Add(password);

            var textBox2 = new TextBox
            {
                Margin = new Thickness(0, 120, 0, 0),
                Width = 200,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            panel.Widgets.Add(textBox2);

            // Button
            var sendLogin = new TextButton
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Bottom,
                Margin = new Thickness(0, 0, 0, 50),
                Text = "LOGAR"
            };



            panel.Widgets.Add(sendLogin);


            // Add it to the desktop
            _desktop = new Desktop();
            _desktop.Root = panel;
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
            _desktop.Render();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
