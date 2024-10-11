using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessProgramManagementSystem_V2
{
    internal class FitnessProgram
    {
        public string FitnessProgramId { get; set; }
        public string title { get; set; }
        public string duration { get; set; }
        public decimal price { get; set; }

        public FitnessProgram(string fitnessProgramId, string title, string duration, decimal price)
        {
            this.FitnessProgramId = fitnessProgramId;
            this.title = title;
            this.duration = duration;
            this.price = price;
        }

        public override string ToString()
        {
            return $"fitnessProgramId: {FitnessProgramId}, Title: {title}, Duration: {duration}, Price: {price}";
        }

    }

}
