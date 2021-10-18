using BikeDistributor.Infrastructure.interfaces;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace BikeDistributor.Domain.Models
{
    [BsonDiscriminator("Bike")]
    public class Bike : IBike
    {
        public const int OneThousand = 1000;
        public const int TwoThousand = 2000;
        public const int FiveThousand = 5000;

        private int _price;
        public string Brand { get; set; }
        public string Model { get; set; }
        public virtual int Price { get => _price; }
        public virtual int BasePrice { get; set; } = 0;
        public virtual string Description { get; set; }

        public virtual bool isStandard { get => true; }

        public Bike(string brand, string model, int price)
        {
            BasePrice = price;
            if (string.IsNullOrEmpty(brand) || string.IsNullOrEmpty(model))
            {
                throw new Exception("cannot create an item without brand or model");
            }
            Brand = brand;
            Model = model;
            //Price = price;
            _price = price;
        }

        public Bike()
        {

        }
        public virtual void RecalculatePrice()
        {
            _price = BasePrice;
        }
    }
}
