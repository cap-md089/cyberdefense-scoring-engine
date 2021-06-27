﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Protocol.CompetitionMessages
{
    public class CompetitionMessage : IJsonProtocolMessage
    {
        public Guid ResponseGuid { get; init; } = Guid.NewGuid();
    }
}