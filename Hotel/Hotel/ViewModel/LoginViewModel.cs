using Hotel.Help;
using Hotel.Models;
using Hotel.Models.BusinessLogicLayer;
using Hotel.Utilities;
using Hotel.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Hotel.ViewModel
{
    public class LoginViewModel : ObservableObject
    {
        public ICommand BackCommand { get; }
        public ICommand LoginCommand { get; }
        public LoginViewModel()
        {
            BackCommand = new RelayCommands(Back);
            LoginCommand = new RelayCommands(Login);
        }
        public void Back()
        {
            MainWindow mainWindow = new MainWindow();
            App.Current.MainWindow.Close();
            App.Current.MainWindow = mainWindow;
            mainWindow.Show();
        }

        public void Login()
        {
            UserBLL userBLL = new UserBLL();
            User user = new User();
            user.username = Username;
            if (userBLL.LoginMethod(user) == null)
            {
                MessageBox.Show("Username is not correct!");
            }
            else
            {
                if (userBLL.LoginMethod(user).role == "client" || userBLL.LoginMethod(user).role == "admin" || userBLL.LoginMethod(user).role == "employee")
                {
                    StaticResources.LoggedUser = userBLL.LoginMethod(user);
                    ClientWindow clientWindow = new ClientWindow();
                    App.Current.MainWindow.Close();
                    App.Current.MainWindow = clientWindow;
                    clientWindow.Show();
                }
            }
        }

        private string username;
        public string Username
        {
            get { return username; }
            set { OnPropertyChanged(ref username, value); }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set { OnPropertyChanged(ref password, value); }
        }

       
    }
}
