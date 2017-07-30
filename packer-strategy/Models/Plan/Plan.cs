//
// Copyright (c) Simply Code Ltd. All rights reserved.
// Licensed under the MIT License.
// See LICENSE file in the project root for full license information.
//
using System;
using System.Collections.Generic;

namespace packer_strategy.Models.Plan
{
    /*!
     * \class   Plan
     *
     * \brief   A plan.
     */
    public class Plan
    {
        /*!
         * \property    public string Id
         *
         * \brief   Gets or sets the identifier.
         *
         * \return  The identifier.
         */
        public string Id { get; set; }

        /*!
         * \property    public string Name
         *
         * \brief   Gets or sets the name.
         *
         * \return  The name.
         */
        public string Name { get; set; }

        /*!
         * \property    public string Notes
         *
         * \brief   Gets or sets the notes.
         *
         * \return  The notes.
         */
        public string Notes { get; set; }

        /*!
         * \property    public DateTime Time
         *
         * \brief   Gets or sets the Date/Time of the time.
         *
         * \return  The time.
         */
        public DateTime Time { get; set; }

        /*!
         * \property    public List<Stage> Stages
         *
         * \brief   Gets or sets the stages.
         *
         * \return  The stages.
         */
        public List<Stage> Stages { get; set; }
    }
}
