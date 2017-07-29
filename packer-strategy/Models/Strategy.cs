//
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
//
using System;
using System.Collections.Generic;

namespace packer_strategy.Models
{
    public class Strategy
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public DateTime Time { get; set; }

        public List<Stage> Stages { get; set; }
    }
}
