﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Protocol.CompetitionMessages
{
    public class SetAppliedPenalties : CompetitionMessage
    {
        public IEnumerable<int> PenaltyIds { get; init; }
    }
}
