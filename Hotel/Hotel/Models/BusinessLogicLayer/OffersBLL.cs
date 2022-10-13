using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Models.BusinessLogicLayer
{
    public class OffersBLL
    {
        private HotelEntities context = new HotelEntities();

        public List<Offer> GetAllOffers()
        {
            var list  = context.GetAllOffers();
            List<Offer> offers = new List<Offer>();

            foreach(var dbOffer in list)
            {
                Offer offer = new Offer();
                offer.id = dbOffer.id;
                offer.price = dbOffer.price;
                offer.name = dbOffer.name;
                offer.description = dbOffer.description;
                offer.start_date = dbOffer.start_date;
                offer.end_date = dbOffer.end_date;
                var service = context.Offers.Find(dbOffer.id).Services;
                
                offer.Services = service;
                offers.Add(offer);
            }
            return offers;
        }

        public void ReserveOffer()
        {

        }
    }
}
