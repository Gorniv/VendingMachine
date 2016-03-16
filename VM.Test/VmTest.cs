using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Interface;
using VendingMachine.Models;
using Xunit;

namespace VM.Test
{
    
    public class VmTest
    {
        [Fact]
        public  void OddTest()
        {
            IOddMoneyCoreVm vm=new SimpleOddMoneyCoreVm();

            var odd=vm.GetMonies(100, _getMoney(10,10,10,10));
            Assert.True(odd[10].Pieces==10);

            odd = vm.GetMonies(52, _getMoney(10, 10, 10, 10));
            Assert.True(odd[10].Pieces == 5);
            Assert.True(odd[2].Pieces == 1);

            odd = vm.GetMonies(1, _getMoney(10, 10, 10, 10));
            Assert.True(odd[1].Pieces == 1);

            odd = vm.GetMonies(1+2+5+10, _getMoney(10, 10, 10, 10));
            Assert.True(odd[1].Pieces == 1);
            Assert.True(odd[2].Pieces == 1);
            Assert.True(odd[5].Pieces == 1);
            Assert.True(odd[10].Pieces == 1);
        }

        private List<KeyValuePair<int, Money>> _getMoney(int count1,int count2,int count5, int count10)
        {
            var moneys = new List<KeyValuePair<int, Money>>()
            {
                new KeyValuePair<int, Money>(1,new Money(1,count1)),
                new KeyValuePair<int, Money>(2,new Money(2,count2)),
                new KeyValuePair<int, Money>(5,new Money(5,count5)),
                new KeyValuePair<int, Money>(10,new Money(10,count10))
            };
            return moneys;
        }
    }
}
