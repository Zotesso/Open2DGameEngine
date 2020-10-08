using Microsoft.Xna.Framework;
using Myra;
using Myra.Graphics2D;
using Myra.Graphics2D.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Client
{
    class InterfaceGUI
    {
        public static List<Panel> Windows = new List<Panel>();

        public void InitializeGUI(Game1 game, Desktop desktop)
        {
            MyraEnvironment.Game = game;

            CreateWindow_Login(desktop);
            CreateWindow_Register(desktop);
        }

        public void CreateWindow(Panel panel)
        {
            Windows.Add(panel);
        }

        public void CreateWindow_Login(Desktop desktop)
        {

            Panel panel = new Panel();

            Label lblLogin = new Label
            {
                Id = "lblLogin",
                Text = "Login:",
                TextColor = Color.Black,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            panel.Widgets.Add(lblLogin);

            TextBox txtLogin = new TextBox
            {
                Margin = new Thickness(0, 40, 0, 0),
                Width = 200,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            panel.Widgets.Add(txtLogin);

            Label lblPassword = new Label
            {
                Margin = new Thickness(0, 80, 0, 0),
                Id = "lblPassword",
                Text = "Password:",
                TextColor = Color.Black,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            panel.Widgets.Add(lblPassword);

            TextBox txtPassword = new TextBox
            {
                Margin = new Thickness(0, 120, 0, 0),
                Width = 200,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            panel.Widgets.Add(txtPassword);

            // Button
            TextButton sendLogin = new TextButton
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Bottom,
                Margin = new Thickness(0, 0, 0, 50),
                Text = "LOGAR"
            };

            panel.Widgets.Add(sendLogin);

            TextButton register = new TextButton
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Bottom,
                Margin = new Thickness(0, 0, 0, 20),
                Text = "CRIAR CONTA"
            };

            register.Click += (s, a) =>
            {
                MenuManager.ChangeMenu(MenuManager.Menu.Register, desktop);
            };

            panel.Widgets.Add(register);

            CreateWindow(panel);
        }

        public void CreateWindow_Register(Desktop desktop)
        {

            Panel panel = new Panel();

            Label lblRegister = new Label
            {
                Id = "lblRegister",
                Text = "Username:",
                TextColor = Color.Black,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            panel.Widgets.Add(lblRegister);

            TextBox txtUsername = new TextBox
            {
                Margin = new Thickness(0, 40, 0, 0),
                Width = 200,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            panel.Widgets.Add(txtUsername);

            Label lblPassword = new Label
            {
                Margin = new Thickness(0, 80, 0, 0),
                Id = "lblPassword",
                Text = "Senha:",
                TextColor = Color.Black,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            panel.Widgets.Add(lblPassword);

            TextBox txtPassword = new TextBox
            {
                Margin = new Thickness(0, 120, 0, 0),
                Width = 200,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            panel.Widgets.Add(txtPassword);

            Label lblPasswordRepeat = new Label
            {
                Margin = new Thickness(0, 160, 0, 0),
                Id = "lblPasswordRepeat",
                Text = "Repita a Senha:",
                TextColor = Color.Black,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            panel.Widgets.Add(lblPasswordRepeat);

            TextBox txtPasswordRepeat = new TextBox
            {
                Margin = new Thickness(0, 200, 0, 0),
                Width = 200,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            panel.Widgets.Add(txtPasswordRepeat);
            // Button
            TextButton sendRegister = new TextButton
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Bottom,
                Margin = new Thickness(0, 0, 0, 50),
                Text = "CADASTRAR"
            };

            panel.Widgets.Add(sendRegister);


            TextButton back = new TextButton
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Bottom,
                Margin = new Thickness(0, 0, 0, 20),
                Text = "Voltar"
            };

            back.Click += (s, a) =>
            {
                MenuManager.ChangeMenu(MenuManager.Menu.Login, desktop);
            };

            panel.Widgets.Add(back);
            CreateWindow(panel);
        }
    }
}
