using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace packer_strategy.Models
{
    public interface IStrategyRepository
    {
        void Add(Strategy item);
        IEnumerable<Strategy> GetAll();
        Strategy Find(long key);
        void Remove(long key);
        void Update(Strategy item);
    }
}
