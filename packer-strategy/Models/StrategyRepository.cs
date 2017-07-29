//
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
//
using System.Collections.Generic;
using System.Linq;

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
            return _context.Strategies.FirstOrDefault(t => t.Id == key);
        }

        public void Remove(string key)
        {
            var entity = _context.Strategies.First(t => t.Id == key);
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
