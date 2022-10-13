using Hotel.Help;
using Hotel.Models;
using Hotel.Models.BusinessLogicLayer;
using Hotel.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hotel.ViewModel
{
    public class AdminViewModel : ObservableObject
    {
        public RoomBLL roomBLL = new RoomBLL();
        public ICommand CreateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand EditCommand { get; set; }

        private ObservableCollection<Service> services;
        public Room SelectedRoom;

        public AdminViewModel(Room room)
        {

            services = new ObservableCollection<Service>(roomBLL.GetAllServices());
            CreateCommand = new RelayCommands(Create);
            DeleteCommand = new RelayCommands(Delete);
            EditCommand = new RelayCommands(Edit);
            SelectedRoom = room;

            InitializeEditRoom();
            


        }
        public ObservableCollection<Service> Services
        {
            get { return services; }
            set { OnPropertyChanged(ref services, value); }
        }

        public List<string> features = new List<string>();

        private bool air;
        public bool Air
        {
            get { return air; } 
            set { OnPropertyChanged(ref air, value); }
        }

        private bool balcony;
        public bool Balcony
        {
            get { return balcony; }
            set { OnPropertyChanged(ref balcony, value); }
        }
        private bool shower;
        public bool Shower
        {
            get { return shower; }
            set { OnPropertyChanged(ref shower, value); }
        }

        private bool tv;
        public bool Tv
        {
            get { return tv; }
            set { OnPropertyChanged(ref tv, value); }
        }

        private bool wifi;
        public bool Wifi
        {
            get { return wifi; }
            set { OnPropertyChanged(ref wifi, value); }
        }

        private int number;
        public int Number
        {
            get { return number; }
            set { OnPropertyChanged(ref number, value); }
        }

        private string type;
        public string Type
        {
            get { return type; }
            set { OnPropertyChanged(ref type, value); }
        }

        private int price;
        public int Price
        {
            get { return price; }
            set { OnPropertyChanged(ref price, value); }
        }

        private string images;
        public string Images
        {
            get { return images; }
            set { OnPropertyChanged(ref images, value); }
        }

        private int tabIndex;
        public int TabIndex
        {
            get { return tabIndex; }
            set { OnPropertyChanged(ref tabIndex, value); }
        }
        

        public void Create()
        {
            List<string> services = new List<string>();

            if (Wifi)
            {
                services.Add("wifi");
            }
            if (Air)
            {
                services.Add("air_conditioner");
            }
            if (Tv)
            {
                services.Add("tv");
            }
            if (Shower)
            {
                services.Add("shower");
            }
            if (Balcony)
            {
                services.Add("balcony");
            }

            //List<string> images = new List<string>();

            string[] images = new string[0];
            if (Images != null)
                images = Images.Split(new Char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

            roomBLL.CreateRoom(services, type, number, price, images);
        }

        public void InitializeEditRoom()
        {

            foreach(var feature in SelectedRoom.Features)
            {
                
                
                if (feature.name == " air_conditioner")
                {
                    editAir = true;
                   
                }
                else if (feature.name == " shower")
                {
                    editShower = true;
                }
                else if (feature.name == " tv")
                {
                    editTv =true;
                }
                else if (feature.name == " wifi")
                {
                    editWifi = true;
                }
                else if (feature.name == " balcony")
                {
                    editBalcony = true;
                }
            }
            


            editPrice = SelectedRoom.price;
            editNumber = SelectedRoom.number;
            editType = SelectedRoom.type;
            string images = "";

            foreach (var image in SelectedRoom.Pictures)
            {
                images += image.url + ',';
            }
            if(images != "")
                editImages = images.Remove(images.Length - 1);

        }


        public void Delete()
        {
            roomBLL.Delete(SelectedRoom);
        }

        public void Edit()
        {
            roomBLL.Edit(editPrice, editNumber, editType, editImages,  editAir,  editBalcony,  editShower, editTv, editWifi);
        }

        //proprieties for editedRoom
        private bool editAir;
        public bool EditAir
        {
            get { return editAir; }
            set { OnPropertyChanged(ref editAir, value); }
        }

        private bool editBalcony;
        public bool EditBalcony
        {
            get { return editBalcony; }
            set { OnPropertyChanged(ref editBalcony, value); }
        }
        private bool editShower;
        public bool EditShower
        {
            get { return editShower; }
            set { OnPropertyChanged(ref editShower, value); }
        }

        private bool editTv;
        public bool EditTv
        {
            get { return editTv; }
            set { OnPropertyChanged(ref editTv, value); }
        }

        private bool editWifi;
        public bool EditWifi
        {
            get { return editWifi; }
            set { OnPropertyChanged(ref editWifi, value); }
        }

        private int editNumber;
        public int EditNumber
        {
            get { return editNumber; }
            set { OnPropertyChanged(ref editNumber, value); }
        }

        private string editType;
        public string EditType
        {
            get { return editType; }
            set { OnPropertyChanged(ref editType, value); }
        }

        private double editPrice;
        public double EditPrice
        {
            get { return editPrice; }
            set { OnPropertyChanged(ref editPrice, value); }
        }

        private string editImages;
        public string EditImages
        {
            get { return editImages; }
            set { OnPropertyChanged(ref editImages, value); }
        }

    }
}
