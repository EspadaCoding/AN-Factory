using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AN.Models
{
    public class Worker : IEquatable<Worker>, ICloneable
    {
        public Worker(string firstName, string lastName, string phone, string secondPhone)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Phone = phone;
            this.SecondPhone = secondPhone;
            this.Orders = new List<Order>();
        }

        public Worker(string firstName, string lastName, string phone, string secondPhone, List<Order> orders) : this(firstName, lastName, phone, secondPhone)
        {
            this.Orders = orders;
        }

        public Worker()
        {
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string Phone { get; set; }
        public string SecondPhone { get; set; } 

        //Adress//
        public List<Order> Orders { get; set; }


        public override string ToString()
        {
            return $"Name - { this.FirstName }\nSurename - {this.LastName}\nPhone - {this.Phone}\nSecond Phone- {this.SecondPhone}";
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Worker);
        }

        public bool Equals(Worker other)
        {
            return other != null &&
                   FirstName == other.FirstName &&
                   LastName == other.LastName &&
                   Phone == other.Phone &&
                   SecondPhone == other.SecondPhone;
        }

        public override int GetHashCode()
        {
            HashCode hash = new();
            hash.Add(FirstName);
            hash.Add(LastName);
            hash.Add(Phone);
            hash.Add(SecondPhone);
            hash.Add(Orders);
            return hash.ToHashCode();
        }

        public object Clone()
        {
            return new Worker(this.FirstName,this.LastName,this.Phone,this.SecondPhone,this.Orders);
        }

        public int GetOrderID(Order order)
        {
            for (int i = 0; i < this.Orders.Count; i++)
            {
                if(order.Model == Orders[i].Model &&
                    order.Batch_number == Orders[i].Batch_number &&
                    order.Karat == Orders[i].Karat &&
                    order.Number == Orders[i].Number)
                {
                    return i;
                }
            }
            return -1;
        }
       
    }
}
