using System;
using System.Collections.Generic;

namespace Cards.DBContext
{
    public partial class Cards
    {
        public int Id { get; set; }
        public string CorrelationId { get; set; }
        public string CompanyName { get; set; }
        public string CardHolderName { get; set; }
        public string CardId { get; set; }
    }
}
