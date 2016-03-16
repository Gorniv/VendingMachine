using System;
using System.Collections.Generic;
using System.Linq;
using VendingMachine.Interface;

namespace VendingMachine.Models
{
    public class SimpleOddMoneyCoreVm : IOddMoneyCoreVm
    {

        public Dictionary<int, Money> GetMonies(int deposit, List<KeyValuePair<int, Money>> moneies)
        {
            var result = new Dictionary<int, Money>();
            foreach (var item in moneies.OrderByDescending(c => c.Key).ToList())
            {
                while (deposit >= item.Key && item.Value.Pieces > 0)
                {
                    deposit -= item.Key;
                    if (!result.ContainsKey(item.Key))
                        result.Add(item.Key, new Money(item.Key, 1));
                    else
                        result[item.Key].Pieces++;
                    item.Value.Pieces--;
                }
            }
            //Не достижимая ситуация,так как деньги сначала вносятся.
            if (deposit != 0)
                throw new Exception("Не нашлось монет нужного достоинства");

            return result;
        }
    }
}