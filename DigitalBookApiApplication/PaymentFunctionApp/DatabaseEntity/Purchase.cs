﻿using System;
using System.Collections.Generic;

namespace PaymentFunctionApp.DatabaseEntity
{
    public partial class Purchase
    {
        public int PurchaseId { get; set; }
        public string EmailId { get; set; }
        public int? BookId { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public string PaymentMode { get; set; }

        public virtual Book Book { get; set; }
    }
}
