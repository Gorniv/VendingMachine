using System.Collections.Generic;
using VendingMachine.Models;

namespace VendingMachine.Interface
{
    public interface IOddMoneyCoreVm
    {
        Dictionary<int, Money> GetMonies(int deposit, List<KeyValuePair<int, Money>> toList);
    }
}