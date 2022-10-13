using Hotel.View;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Hotel.Models.BusinessLogicLayer
{
    public class RoomBLL
    {
        HotelEntities context = new HotelEntities();

        public List<Room> GetAllRooms(DateTime checkIn, DateTime checkOut)
        {


            var list = context.SeeRoomsAvailable(checkIn.Date.ToString("yyyy-MM-dd"), checkOut.Date.ToString("yyyy-MM-dd"));
            List<Room> rooms = new List<Room>();

            foreach (var roomAvailable in list.ToList())
            {
                Room room = new Room();
                room.number = roomAvailable.number;
                room.price = roomAvailable.price;
                room.type = roomAvailable.type;
                
                string[] features = roomAvailable.features.Split(',');

                foreach (var feature in features)
                {
                    Feature roomFeature = new Feature();
                    roomFeature.name = feature;
                    room.Features.Add(roomFeature);
                }

                if (roomAvailable.pictures != null)
                {
                    if (roomAvailable.pictures.Length > 0)
                    {
                        string[] split = roomAvailable.pictures.Split(new Char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        room.image = split[0];

                        foreach (var image in split)
                        {
                            Picture roomPicture = new Picture();
                            roomPicture.url = image;
                            room.Pictures.Add(roomPicture);
                        }


                    }
                }
                rooms.Add(room);

            }
            return rooms;
        }

        public List<Service> GetAllServices()
        {
            var list = context.GetAllServices();
            List<Service> services = new List<Service>();

            foreach (var service in list)
            {
                Service serviceRoom = new Service();
                serviceRoom.price = service.price;
                serviceRoom.name = service.name;

                services.Add(serviceRoom);
            }

            return services;
        }

        public void CreateRoom(List<string> services, string type, int number, int price, string[] images)
        {

            foreach (var image in images)
            {
                Picture picture = new Picture();
                picture.url = image;
                context.Pictures.Add(picture);
            }

            Room room = new Room();
            room.price = price;
            room.number = number;
            room.type = type;

            foreach (var service in services)
            {
                var feature = context.Features.Find(service);
                room.Features.Add(feature);
            }


            foreach (var image in images)
            {
                var imageDb = context.Pictures.Find(image);
                room.Pictures.Add(imageDb);
            }


            context.Rooms.Add(room);

            try
            {
                context.SaveChanges();
                MessageBox.Show("The room was created succesfully!");
                ClientWindow clientWindow = new ClientWindow();
                App.Current.MainWindow.Close();
                App.Current.MainWindow = clientWindow;
                clientWindow.Show();
            } 
            catch(Exception) 
            {
                MessageBox.Show("Something went wrong!");
            }
        }

        public User RegisterMethod(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();

            return user;
        }

        public void Delete(Room room)
        {
            var roomDb = context.Rooms.Find(room.number);
            context.Rooms.Remove(roomDb);

            context.SaveChanges();

            MessageBox.Show("The room was deleted succesfully!");
            ClientWindow clientWindow = new ClientWindow();
            App.Current.MainWindow.Close();
            App.Current.MainWindow = clientWindow;
            clientWindow.Show();

        }

        public void Edit(double editPrice, int editNumber, string editType, string images, bool editAir, bool editBalcony, bool editShower, bool editTv, bool editWifi)
        {


            List<string> features = new List<string>();
            if (editAir)
            {

                features.Add("air_conditioner");
            }
            if (editBalcony)
            {

                features.Add("balcony");
            }
            if (editShower)
            {
                features.Add("shower");
            }
            if (editTv)
            {
                features.Add("tv");
            }
            if (editWifi)
            {
                features.Add("wifi");
            }


            List<Feature> featuresList = new List<Feature>();

            foreach (var feature in features)
            {
                var dbFeature = context.Features.Find(feature);
                featuresList.Add(dbFeature);
            }


            List<Picture> pictureList = new List<Picture>();

            if (images != null)
            {
                if (images.Contains(','))
                {
                    //splitImages = images.Split(',');
                    string[] split = images.Split(new Char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var image in split)
                    {
                        var picture = context.Pictures.Find(image);
                        pictureList.Add(picture);
                    }
                }
                else
                {
                    string[] split = images.Split(new Char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    var picture = context.Pictures.Find(split);

                    pictureList.Add(picture);
                }
            }
            


            var dbRoom = context.Rooms.Find(editNumber);

            dbRoom.price = editPrice;
            dbRoom.number = editNumber;
            dbRoom.type = editType;

            dbRoom.Pictures.Clear();
            foreach (var picture in pictureList)
            {
                dbRoom.Pictures.Add(picture);
            }

            dbRoom.Features.Clear();
            foreach (var feature in featuresList)
            {

                dbRoom.Features.Add(feature);
            }


            context.Rooms.AddOrUpdate(dbRoom);
            //context.Entry(dbRoom).CurrentValues.SetValues(dbRoom);
            context.SaveChanges();

            MessageBox.Show("The room was edited succesfully!");
            ClientWindow clientWindow = new ClientWindow();
            App.Current.MainWindow.Close();
            App.Current.MainWindow = clientWindow;
            clientWindow.Show();
        }

    }
}