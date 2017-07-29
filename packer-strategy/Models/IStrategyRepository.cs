//
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
//
using System.Collections.Generic;

namespace packer_strategy.Models
{
    public interface IStrategyRepository
    {
        void Add(Strategy item);
        IEnumerable<Strategy> GetAll();
        Strategy Find(string key);
        void Remove(string key);
        void Update(Strategy item);
    }
}
