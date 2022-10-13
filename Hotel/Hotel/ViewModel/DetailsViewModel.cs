using Hotel.Help;
using Hotel.Models;
using Hotel.Models.BusinessLogicLayer;
using Hotel.Utilities;
using Hotel.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Hotel.ViewModel
{
    internal class DetailsViewModel : ObservableObject
    {

        public HotelEntities context = new HotelEntities();
        public ICommand BackCommand { get; }
        public ICommand NextCommand { get; }
        public ICommand PreviousCommand { get; }
        public ICommand TestCommand { get; }
        static RoomBLL room = new RoomBLL();
        static ServiceBLL serviceBLL = new ServiceBLL();

        private ObservableCollection<Feature> features;
        public ObservableCollection<Feature> Features { get { return features; } set { OnPropertyChanged(ref features, value); } }

        public ObservableCollection<Service> service;
        public ObservableCollection<Service> Service { get { return service; } set { OnPropertyChanged(ref service, value); } }


        List<Hotel.Models.Room> roomsList1;
        int indexOfRoomInList;
        int imageIndex = 0;
        public DetailsViewModel(int index, Room roomNumber, DateTime check, DateTime chech)
        {
            roomCopy = roomNumber;
            BackCommand = new RelayCommands(Back);
            NextCommand = new RelayCommands(Next);
            PreviousCommand = new RelayCommands(Previous);
            TestCommand = new RelayCommands(Testbutton);

            List<Hotel.Models.Room> roomsList = room.GetAllRooms(check, chech);

            indexOfRoomInList = index;
            if (roomsList[index].Pictures.Count > 0)
                ImageSource = roomsList[index].Pictures.ElementAt(imageIndex).url;
            roomsList1 = roomsList;

            Features = new ObservableCollection<Feature>(roomNumber.Features);

            List<Service> servicesList = serviceBLL.GetAllServicesForRoom();

            Service = new ObservableCollection<Service>(servicesList);

            //checkIn=
            //checkIn = check.Date.ToString("yyyy-MM-dd");
            //checkOut = chech.Date.ToString("yyyy-MM-dd");

            checkIn = check;
            checkOut = chech;

            AllInclusivePrice = (int)Service.ElementAt(0).price;
            BarbequePrice = (int)Service.ElementAt(1).price;
            BreakfastPrice = (int)Service.ElementAt(2).price;
            transportPrice = (int)Service.ElementAt(3).price;

            AllInclusiveLabel = Service.ElementAt(0).name + " -";
            BarbequeLabel = Service.ElementAt(1).name + " -";
            BreakfastLabel = Service.ElementAt(2).name + " -";
            transportLabel = Service.ElementAt(3).name + " -";

        }

        private string imageSource;
        public string ImageSource
        {
            get
            {
                return imageSource;
            }
            set
            {
                OnPropertyChanged(ref imageSource, value);
            }
        }

        public void Back()
        {
            ClientWindow clientWindow = new ClientWindow();
            App.Current.MainWindow.Close();
            App.Current.MainWindow = clientWindow;
            clientWindow.Show();
        }

        public void Next()
        {
            if (imageIndex < roomsList1[indexOfRoomInList].Pictures.Count - 1)
                imageIndex++;
        }
        public void Previous()
        {
            if (imageIndex < roomsList1[indexOfRoomInList].Pictures.Count - 1 && imageIndex > 0)
                imageIndex--;
        }

        public void Testbutton()
        {

            //roomCopy
            //
            List<string> services = new List<string>();
            if (AllInclusiveBool)
                services.Add("all_inclusive");
            if (BarbequeBool)
                services.Add("barbeque");
            if (BreakfastBool)
                services.Add("breakfast");
            if (TransportBool)
                services.Add("transport");

            if (StaticResources.LoggedUser.role == "client")
            {
                var newReceipt = new Receipt();
                newReceipt.price = 0;

                var receipt = context.Receipts.Add(newReceipt);
                foreach (var service in services)
                    receipt.Services.Add(context.Services.Find(service));


                var newBooking = new Booking();
                newBooking.start_date = checkIn;
                newBooking.end_date = checkOut;

                double final_price = 0;
                foreach (var service in receipt.Services)
                    final_price += service.price;

                int days = checkOut.Day - checkIn.Day;
                final_price += roomCopy.price;


                receipt.price = final_price;
                newBooking.username = StaticResources.LoggedUser.username;
                newBooking.room_number = roomCopy.number;
                //newBooking.id_receipt = context.Receipts.Last().id;
                newBooking.id_receipt = context.Receipts.OrderByDescending(x => x.id).FirstOrDefault().id + 1;
                newBooking.state = "active";

                context.Bookings.Add(newBooking);

                try
                {
                    context.SaveChanges();
                    FinalPrice = "The booking was succesfull, the total price is " + final_price.ToString();
                }
                catch (Exception)
                {
                }

            }
        }

        private bool allInclusiveBool;
        public bool AllInclusiveBool
        {
            get { return allInclusiveBool; }
            set
            {
                OnPropertyChanged(ref allInclusiveBool, value);
            }
        }

        private bool barbequeBool;
        public bool BarbequeBool
        {
            get
            {
                return barbequeBool;
            }
            set
            {
                OnPropertyChanged(ref barbequeBool, value);
            }
        }

        private bool breakfastBool;
        public bool BreakfastBool
        {
            get
            {
                return breakfastBool;
            }
            set
            {

                OnPropertyChanged(ref breakfastBool, value);
            }
        }


        private bool transportBool;
        public bool TransportBool
        {
            get
            {
                return transportBool;
            }
            set
            {

                OnPropertyChanged(ref transportBool, value);
            }
        }

        private int allInclusivePrice;
        public int AllInclusivePrice
        {
            get
            {
                return allInclusivePrice;
            }
            set
            {
                OnPropertyChanged(ref allInclusivePrice, value);
            }
        }

        private int barbequePrice;
        public int BarbequePrice
        {
            get
            {
                return barbequePrice;
            }
            set
            {
                OnPropertyChanged(ref barbequePrice, value);
            }
        }

        private int breakfastPrice;
        public int BreakfastPrice
        {
            get
            {
                return breakfastPrice;
            }
            set
            {
                OnPropertyChanged(ref breakfastPrice, value);
            }
        }


        private int transportPrice;
        public int TransportPrice
        {
            get
            {
                return transportPrice;
            }
            set
            {
                OnPropertyChanged(ref transportPrice, value);
            }
        }


        private string allInclusiveLabel;
        public string AllInclusiveLabel
        {
            get
            {
                return allInclusiveLabel;
            }
            set
            {
                OnPropertyChanged(ref allInclusiveLabel, value);
            }
        }

        private string barbequeLabel;
        public string BarbequeLabel
        {
            get
            {
                return barbequeLabel;
            }
            set
            {
                OnPropertyChanged(ref barbequeLabel, value);
            }
        }

        private string breakfastLabel;
        public string BreakfastLabel
        {
            get
            {
                return breakfastLabel;
            }
            set
            {
                OnPropertyChanged(ref breakfastLabel, value);
            }
        }


        private string transportLabel;
        public string TransportLabel
        {
            get
            {
                return transportLabel;
            }
            set
            {
                OnPropertyChanged(ref transportLabel, value);
            }

        }

        private Room roomCopy;
        public Room RoomCopy
        {
            get
            {
                return roomCopy;
            }
            set
            {
                OnPropertyChanged(ref roomCopy, value);
            }
        }


        private DateTime checkIn;
        public DateTime CheckIn
        {
            get
            {
                return checkIn;
            }
            set
            {
                OnPropertyChanged(ref checkIn, value);
            }
        }

        private DateTime checkOut;
        public DateTime CheckOut
        {
            get
            {
                return checkOut;
            }
            set
            {
                OnPropertyChanged(ref checkOut, value);
            }
        }

        public string finalPrice;
        public string FinalPrice
        {
            get
            {
                return finalPrice;
            }
            set
            {
                OnPropertyChanged(ref finalPrice, value);
            }
        }
    }
}