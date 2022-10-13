using Hotel.Help;
using Hotel.Models;
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
    public class OfferViewModel : ObservableObject
    {
        public ICommand ReserveCommand { get; set; }
        HotelEntities context = new HotelEntities();
        public OfferViewModel(Offer selectedOfferPage)
        {
            selectedOffer = selectedOfferPage;
            ReserveCommand = new RelayCommands(Reserve);

            foreach (var service in selectedOfferPage.Services)
            {


                if (service.name == "all_inclusive")
                {
                    inclusive = true;

                }
                else if (service.name == "barbeque")
                {
                    barbeque = true;
                }
                else if (service.name == "breakfast")
                {
                    breakfast = true;
                }
                else if (service.name == "transport")
                {
                    transport = true;
                }

            }


            var list = context.GetAllServices().ToList();

            AllInclusivePrice = (int)list.ElementAt(0).price;
            BarbequePrice = (int)list.ElementAt(1).price;
            BreakfastPrice = (int)list.ElementAt(2).price;
            TransportPrice = (int)list.ElementAt(3).price;

            AllInclusiveLabel = list.ElementAt(0).name + " -";
            BarbequeLabel = list.ElementAt(1).name + " -";
            BreakfastLabel = list.ElementAt(2).name + " -";
            TransportLabel = list.ElementAt(3).name + " -";
        }

        public void Reserve()
        {
            List<string> services = new List<string>();
            if (Inclusive)
                services.Add("all_inclusive");
            if (Barbeque)
                services.Add("barbeque");
            if (Breakfast)
                services.Add("breakfast");
            if (Transport)
                services.Add("transport");

            List<Service> newServices = new List<Service>();
            foreach (var service in selectedOffer.Services)
                newServices.Add(service);


            if (StaticResources.LoggedUser.role == "client")
            {
                var newReceipt = new Receipt();
                newReceipt.price = 0;

                var receipt = context.Receipts.Add(newReceipt);

                foreach (var service in services)
                {
                    if (selectedOffer.Services.ToList().Find(serv => serv.name == service) == null)
                    {
                        receipt.Services.Add(context.Services.Find(service));
                    }
                }

                var newBooking = new Booking();
                newBooking.start_date = selectedOffer.start_date;
                newBooking.end_date = selectedOffer.end_date;

                double final_price = 0;
                foreach (var service in receipt.Services)
                    final_price += service.price;

                final_price += selectedOffer.price;

                FinalPrice = "The booking was succesfull, the total price is " + final_price.ToString();

                receipt.price = final_price;
                newBooking.username = StaticResources.LoggedUser.username;
                newBooking.room_number = Number;
                newBooking.id_receipt = context.Receipts.OrderByDescending(x => x.id).FirstOrDefault().id + 1;
                newBooking.state = "active";

                context.Bookings.Add(newBooking);

                try{
                    context.SaveChanges();
                    MessageBox.Show("The room is booked succesfully!");
                   

                }
                catch (Exception)
                {
                    MessageBox.Show("The room is already booked!");
                }

                ClientWindow clientWindow = new ClientWindow();
                App.Current.MainWindow.Close();
                App.Current.MainWindow = clientWindow;
                clientWindow.Show();

            }
        }
        private Offer selectedOffer;
        public Offer SelectedOffer
        {
            get { return selectedOffer; }
            set { OnPropertyChanged(ref selectedOffer, value); }
        }

        private bool inclusive;
        public bool Inclusive
        {
            get { return inclusive; }
            set { OnPropertyChanged(ref inclusive, value); }
        }

        private string finalPrice;
        public string FinalPrice
        {
            get { return finalPrice; }
            set { OnPropertyChanged(ref finalPrice, value); }
        }
        private bool barbeque;
        public bool Barbeque
        {
            get { return barbeque; }
            set { OnPropertyChanged(ref barbeque, value); }
        }

        private bool breakfast;
        public bool Breakfast
        {
            get { return breakfast; }
            set { OnPropertyChanged(ref breakfast, value); }
        }

        private bool transport;
        public bool Transport
        {
            get { return transport; }
            set { OnPropertyChanged(ref transport, value); }
        }

        private int number;
        public int Number
        {
            get { return number; }
            set { OnPropertyChanged(ref number, value); }
        }


        private int price;
        public int Price
        {
            get { return price; }
            set { OnPropertyChanged(ref price, value); }
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

    }
}
