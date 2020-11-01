using System;
using System.Collections.Generic;
using System.Text;
using Myra.Graphics2D.UI;

namespace Client
{
    class MenuManager
    {
        public static Menu menu;
        public enum Menu
        {
            Login,
            Register,
            InGame,
            adminPanel
        }

        public static void ChangeMenu(Menu menu, Desktop desktop)
        {
            foreach(Panel window in InterfaceGUI.Windows)
            {
                window.Visible = false;
            }

            desktop.Root = InterfaceGUI.Windows[(int)menu];

            InterfaceGUI.Windows[(int)menu].Visible = true;
        }

        public static void ShowAdminPanel()
        {
            var messageBox = Dialog.CreateMessageBox("Muito Curto", "Sua senha e Username devem ter pelomenos 6 digitos.");
            messageBox.ShowModal(Game1._desktop);
        }
    }
}
