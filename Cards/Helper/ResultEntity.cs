﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cards.Helper
{
    public class ResultEntity
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public ErrorDetailsEntity ErrorDetails { get; set; }
    }

}
