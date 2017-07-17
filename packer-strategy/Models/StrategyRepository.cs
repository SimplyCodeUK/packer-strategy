using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace packer_strategy.Models
{
    public class StrategyRepository : IStrategyRepository
    {
        private readonly StrategyContext _context;

        public StrategyRepository(StrategyContext context)
        {
            _context = context;
            Add(new Strategy { Name = "Item1" });
        }

        public IEnumerable<Strategy> GetAll()
        {
            return _context.Strategies.ToList();
        }

        public void Add(Strategy item)
        {
            _context.Strategies.Add(item);
            _context.SaveChanges();
        }

        public Strategy Find(string key)
        {
            return _context.Strategies.FirstOrDefault(t => t.Key == key);
        }

        public void Remove(string key)
        {
            var entity = _context.Strategies.First(t => t.Key == key);
            _context.Strategies.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(Strategy item)
        {
            _context.Strategies.Update(item);
            _context.SaveChanges();
        }
    }
}
