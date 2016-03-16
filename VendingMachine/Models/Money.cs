using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace VendingMachine.Models
{
    public class Money : ViewModelBase
    {
        private int _pieces;

        public Money(int value, int pieces)
        {
            Value = value;
            Pieces = pieces;
        }

        public int Value { get; set; }

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
