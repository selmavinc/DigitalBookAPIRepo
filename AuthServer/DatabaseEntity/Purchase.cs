﻿using System;
using System.Collections.Generic;

namespace AuthServer.DatabaseEntity
{
    public partial class Purchase
    {
        public int PurchaseId { get; set; }
        public string? EmailId { get; set; }
        public int? BookId { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public string? PaymentMode { get; set; }
    }
}
