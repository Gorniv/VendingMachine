using System.Collections.Generic;

namespace VendingMachine.Models
{
    public class SimpleOddMoneyCoreVm: IOddMoneyCoreVm
    {
        public List<Money> GetMonies(int value, List<Money> monies)
        {
            return null;
        } 
    }

    public interface IOddMoneyCoreVm
    {
        List<Money> GetMonies(int value, List<Money> monies);
    }
}