using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Hotel.Help;
using Hotel.View;
namespace Hotel.ViewModel
{
    internal class MenuViewModel
    {
        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }
        public ICommand GuestCommand { get; }
        public ICommand ExitCommand { get; }


        public MenuViewModel()
        {
            LoginCommand = new RelayCommands(Login);
            RegisterCommand = new RelayCommands(SignUp);
            ExitCommand = new RelayCommands(Exit);
            GuestCommand = new RelayCommands(Guest);


        }

        public void Login()
        {
            LoginWindow loginWindow = new LoginWindow();
            App.Current.MainWindow.Close();
            App.Current.MainWindow = loginWindow;
            loginWindow.Show();
        }

        public void SignUp()
        {
            RegisterWindow registerWindow = new RegisterWindow();

            App.Current.MainWindow.Close();
            App.Current.MainWindow = registerWindow;
            registerWindow.Show();
        }

        public void Guest()
        {
            ClientWindow clientWindow = new ClientWindow();
            App.Current.MainWindow.Close();
            App.Current.MainWindow = clientWindow;
            clientWindow.Show();
        }


        public void Exit()
        {
            App.Current.MainWindow.Close();
            App.Current.Shutdown();
        }
    }
}
