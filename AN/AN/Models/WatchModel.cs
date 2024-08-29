using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AN.Models
{
    public class WatchModel : ICloneable
    {
        public WatchModel(List<WatchModelDetails> details ,decimal totalPrice, string name)
        {
            Details = details;
            Name = name;
            TotalPrice = totalPrice;
            Fill();
        }

        public WatchModel(List<WatchModelDetails> details, decimal totalPrice, string name, int n)
        {
            Details = details;
            Name = name;
            TotalPrice = totalPrice;
        }
        public WatchModel()
        {
            Details = new();
            Fill();
        }
        public string Name { get; set; }
        public decimal TotalPrice { get; set; }
        public List<WatchModelDetails> Details { get; set; }

        private void Fill()
        {
            Details.Add(new WatchModelDetails("1", "стекло",0, 0));
            Details.Add(new WatchModelDetails("2", "ремень+бляшка",0, 0));
            Details.Add(new WatchModelDetails("3", "стрелки",0, 0));
            Details.Add(new WatchModelDetails("4", "заводник",0, 0));
            Details.Add(new WatchModelDetails("5", "кнопки",0, 0));
            Details.Add(new WatchModelDetails("6", "механизм",0, 0));
            Details.Add(new WatchModelDetails("7", "цыферблат",0, 0));
            Details.Add(new WatchModelDetails("8", "корпус",0, 0));
            Details.Add(new WatchModelDetails("9", "мантировка",0, 0));
            Details.Add(new WatchModelDetails("10", "сборка часов",0, 0));
            Details.Add(new WatchModelDetails("11", "закрепка",0, 0));
            Details.Add(new WatchModelDetails("12", "карат. брилл",0, 0));
            Details.Add(new WatchModelDetails("13", "штук. брилл",0, 0));
            Details.Add(new WatchModelDetails("14", "размер. брилл",0, 0));
            Details.Add(new WatchModelDetails("15", "коробка",0, 0));
            Details.Add(new WatchModelDetails("16", "золото",0, 0));
            Details.Add(new WatchModelDetails("17", "угар",0, 0));
            Details.Add(new WatchModelDetails("18", "продовец",0, 0));
            Details.Add(new WatchModelDetails("19", "офис",0, 0));
            Details.Add(new WatchModelDetails("20", "цэх",0, 0));
            Details.Add(new WatchModelDetails("21", "карго",0, 0));
            Details.Add(new WatchModelDetails("22", "перевод",0, 0));
            Details.Add(new WatchModelDetails("23", "штамп",0, 0));
            Details.Add(new WatchModelDetails("24", "литио",0, 0));
            Details.Add(new WatchModelDetails("25", "расход",0, 0));
        }

        public object Clone()
        {
            return new WatchModel(new(this.Details), this.TotalPrice, this.Name, 1);
        }
    }
}
