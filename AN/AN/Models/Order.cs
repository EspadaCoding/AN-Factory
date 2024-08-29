using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace AN.Models
{
    public enum Karat { Karat14, Karat18 }

    public class Order : IEquatable<Order> , ICloneable
    {
        public Order()
        {
        }
        public Order(string batch_number, string model, int number, Karat karat, decimal gold_cost, decimal bronzeWeight, decimal goldWeight)
        {
            this.Batch_number = batch_number;
            this.SendingDate = DateTime.Now;
            this.Model = model;
            this.Number = number;
            this.Karat = karat;
            this.Gold_cost = gold_cost;
            this.BronzeWeight = bronzeWeight;
            this.GoldWeight = goldWeight;
            this.IsDone = false;
        }

        public Order(string batch_number, string model, int number, Karat karat, decimal gold_cost, decimal bronzeWeight, decimal goldWeight, decimal totalWeight, decimal waste)
        {
            this.Batch_number = batch_number;
            this.SendingDate = DateTime.Now;
            this.Model = model;
            this.Number = number;
            this.Karat = karat;
            this.Gold_cost = gold_cost;
            this.BronzeWeight = bronzeWeight;
            this.GoldWeight = goldWeight;
            this.TotalWeight = totalWeight;
            this.Waste = waste;
            this.IsDone = false;
        }

        public Order(string batch_number, string model, int number, Karat karat, decimal bronzeWeight, decimal goldWeight)
        {
            this.Batch_number = batch_number;
            this.SendingDate = DateTime.Now;
            this.Model = model;
            this.Number = number;
            this.Karat = karat;
            this.BronzeWeight = bronzeWeight;
            this.GoldWeight = goldWeight;
            this.GoldHistory = $"{goldWeight}";
            this.BronzeHistory = $"{bronzeWeight}";
            this.IsDone = false;
        }

        //номер партии
        public string Batch_number { get; set; }

        //время опубликования и сдачи заказа
        public DateTime SendingDate { get; set; }
        public DateTime AcceptingDate { get; set; }

        //модель
        public string Model { get; set; }

        //количество изделей
        public int Number { get; set; }
        
        //карат 
        public Karat Karat { get; set; }

        //цена золота в момент занесения
        public decimal Gold_cost { get; set; }

        //Весс бронзы
        public decimal BronzeWeight { get; set; }

        //Вес золота
        public decimal GoldWeight { get; set; }

        //Общий вес корпусов 
        public decimal TotalWeight { get; set; }

        //Угар (Потери) 
        public decimal Waste { get; set; }

        //сделанно ли данный заказ
        public bool IsDone { get; set; }

        //изменение золота
        public string GoldHistory { get; set; }

        //изменение бронза
        public string BronzeHistory { get; set; }


        public override bool Equals(object obj)
        {
            return obj is Order order &&
                   Batch_number == order.Batch_number &&
                   SendingDate == order.SendingDate &&
                   Model == order.Model &&
                   Number == order.Number &&
                   Karat == order.Karat &&
                   Gold_cost == order.Gold_cost &&
                   BronzeWeight == order.BronzeWeight &&
                   GoldWeight == order.GoldWeight &&
                   TotalWeight == order.TotalWeight &&
                   Waste == order.Waste &&
                   IsDone == order.IsDone;
        }

        public bool Equals(Order other)
        {
            return Batch_number == other.Batch_number &&
                  Model == other.Model &&
                  Number == other.Number &&
                  Karat == other.Karat &&
                  IsDone == other.IsDone;
        }

        public override int GetHashCode()
        {
            HashCode hash = new();
            hash.Add(Batch_number);
            hash.Add(SendingDate);
            hash.Add(Model);
            hash.Add(Number);
            hash.Add(Karat);
            hash.Add(Gold_cost);
            hash.Add(IsDone);
            hash.Add(BronzeWeight);
            hash.Add(GoldWeight);
            hash.Add(TotalWeight);
            hash.Add(Waste);
            hash.Add(GoldHistory);
            hash.Add(BronzeHistory);
            return hash.ToHashCode();
        }

        //задание сделано
        public void OrderDone()
        {
            this.IsDone = true;
        }

        public object Clone()
        {
            return new Order(this.Batch_number, this.Model, this.Number, this.Karat, this.BronzeWeight, this.GoldWeight);
        }
    }
}
