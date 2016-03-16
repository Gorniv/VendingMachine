using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using VendingMachine.Interface;
using VendingMachine.Models;

namespace VendingMachine.ViewModel
{
    /// <summary>
    ///     This class contains properties that the main View can data bind to.
    ///     <para>
    ///         Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    ///     </para>
    ///     <para>
    ///         You can also use Blend to data bind with the tool's support.
    ///     </para>
    ///     <para>
    ///         See http://www.galasoft.ch/mvvm
    ///     </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private readonly IOddMoneyCoreVm _oddMoneyCore;
        private int _deposit;

        /// <summary>
        ///     Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IOddMoneyCoreVm oddMoneyCore)
        {
            _oddMoneyCore = oddMoneyCore;
            CustomerWalletList = new ObservableCollection<Money>
            {
                new Money(1, 10),
                new Money(2, 30),
                new Money(5, 20),
                new Money(10, 15)
            };
            CustomerWalletDict = CustomerWalletList.ToDictionary(c => c.Value);

            VmWalletList = new ObservableCollection<Money>
            {
                new Money(1, 100),
                new Money(2, 100),
                new Money(5, 100),
                new Money(10, 100)
            };
            VmWalletDict = VmWalletList.ToDictionary(c => c.Value);

            ProductsList = new ObservableCollection<Product>
            {
                new Product(@"Чай", 13, 10),
                new Product(@"Кофе", 18, 20),
                new Product(@"Кофе с молоком", 21, 20),
                new Product(@"Сок", 35, 15)
            };
        }

        public RelayCommand<Product> BuyCommand => new RelayCommand<Product>(_onBuyCommand, _canBuyCommand);

        public Dictionary<int, Money> CustomerWalletDict { get; set; }

        public ObservableCollection<Money> CustomerWalletList { get; set; }

        public int Deposit
        {
            get { return _deposit; }
            set
            {
                _deposit = value;
                RaisePropertyChanged(() => Deposit);
            }
        }

        public RelayCommand<Money> InsertCommand => new RelayCommand<Money>(_onInsertCommand, _canInsertCommand);

        public RelayCommand OddMoneyCommand => new RelayCommand(_onOddMoneyCommand);

        public ObservableCollection<Product> ProductsList { get; set; }

        public Dictionary<int, Money> VmWalletDict { get; set; }

        public ObservableCollection<Money> VmWalletList { get; set; }

        private bool _canBuyCommand(Product product)
        {
            return product?.Pieces > 0;
        }

        private bool _canInsertCommand(Money arg)
        {
            if (arg == null)
                return false;

            if (!CustomerWalletDict.ContainsKey(arg.Value))
                return false;

            var result = CustomerWalletDict[arg.Value].Pieces > 0;
            return result;
        }

        private void _onBuyCommand(Product product)
        {
            if (Deposit < product.Cost)
            {
                MessageBox.Show("Недостаточно средств", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            product.Pieces--;
            Deposit -= product.Cost;
            MessageBox.Show("Спасибо", "Успешная покупка", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void _onInsertCommand(Money obj)
        {
            CustomerWalletDict[obj.Value].Pieces--;
            if (VmWalletDict.ContainsKey(obj.Value))
                VmWalletDict[obj.Value].Pieces++;
            else
            {
                var money = new Money(obj.Value, obj.Pieces);
                VmWalletList.Add(money);
                VmWalletDict.Add(money.Value, money);
            }
            Deposit += obj.Value;
        }

        private void _onOddMoneyCommand()
        {
            var odd = _oddMoneyCore.GetMonies(Deposit, VmWalletDict.ToList());
            foreach (var money in odd)
            {
                //key always contains!
                CustomerWalletDict[money.Key].Pieces += money.Value.Pieces;
                Deposit -= money.Value.Pieces * money.Value.Value;
            }
        }
    }
}