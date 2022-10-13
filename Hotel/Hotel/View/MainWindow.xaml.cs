
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Hotel.Models;
namespace Hotel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            /* HotelEntities context = new HotelEntities();
             User user = new User();
             user.username = "Leo31431323123123";
             user.first_name = "Bote3zatu32";
             user.last_name = "Emil";
             user.role = "employee";

             //user = context.Users.Add(new User() { username = "Emil", first_name = "Botezatu", last_name = "Emil", role = "employee", });
             context.Users.Add(user);
             context.SaveChanges();*/
            

            //Testing the Stored Procedure Functionality
            //HotelEntities context = new HotelEntities();
            //var res = context.GetRoomsFeatures();
            //var res2 = 3;
        }
    }
}
