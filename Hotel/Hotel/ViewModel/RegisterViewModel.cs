using Hotel.Help;
using Hotel.Model;
using Hotel.Models;
using Hotel.Models.BusinessLogicLayer;
using Hotel.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Hotel.Utilities;
namespace Hotel.ViewModel
{
    internal class RegisterViewModel : ObservableObject
    {
        public ICommand BackCommand { get; }
        public ICommand RegisterCommand { get; }
        UserBLL userBLL = new UserBLL();
        User user = new User();
        public RegisterViewModel()
        {
            BackCommand = new RelayCommands(Back);
            RegisterCommand = new RelayCommands(Register);
        }

        public void Back()
        {
            MainWindow mainWindow = new MainWindow();
            App.Current.MainWindow = mainWindow;
            mainWindow.Show();
        }

        public void Register()
        {


            user.username = Username;
            user.first_name = FirstName;
            user.last_name = LastName;
            user.role = Role;

            switch (Role)
            {
                case "0":
                    user.role = "admin";
                    break;
                case "1":
                    user.role = "employee";
                    break;
                case "2":
                    user.role = "client";
                    break;
            }
            userBLL.RegisterMethod(user);


            StaticResources.LoggedUser = user;
            ClientWindow clientWindow = new ClientWindow();
            App.Current.MainWindow.Close();
            App.Current.MainWindow = clientWindow;
            clientWindow.Show();
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
        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set { OnPropertyChanged(ref firstName, value); }
        }
        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set { OnPropertyChanged(ref lastName, value); }
        }
        private string role;
        public string Role
        {
            get { return role; }
            set { OnPropertyChanged(ref role, value); }
        }
    }
}