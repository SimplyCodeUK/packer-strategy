//
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
//
using System;
using System.Collections.Generic;

namespace packer_strategy.Models.Plan
{
    /*! A plan. */
    public class Plan
    {
        /*!
         * Gets or sets the identifier.
         *
         * @return  The identifier.
         */
        public string ID { get; set; }

        /*!
         * Gets or sets the name.
         *
         * @return  The name.
         */
        public string Name { get; set; }

        /*!
         * Gets or sets the notes.
         *
         * @return  The notes.
         */
        public string Notes { get; set; }

        /*!
         * Gets or sets the Date/Time of the time.
         *
         * @return  The time.
         */
        public DateTime Time { get; set; }

        /*!
         * Gets or sets the stages.
         *
         * @return  The stages.
         */
        public List<Stage> Stages { get; set; }
    }
}
