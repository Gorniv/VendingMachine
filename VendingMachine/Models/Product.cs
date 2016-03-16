using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace VendingMachine.Models
{
    public class Product : ViewModelBase
    {
        private int _pieces;

        public Product(string name, int cost, int pieces)
        {
            Name = name;
            Cost = cost;
            Pieces = pieces;
        }

        public string Name { get; set; }
        public int Cost { get; set; }

        public int Pieces
        {
            get { return _pieces; }
            set
            {
                _pieces = value;
                RaisePropertyChanged(() => Pieces);
            }
        }
    }
}
