using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement
{
    public class DyeingPrintingAreaReferenceModel : StandardEntity
    {
        public DyeingPrintingAreaReferenceModel(string type, int transactionId, int previousTransactionId)
        {
            Type = type;
            TransactionId = transactionId;
            PreviousTransactionId = previousTransactionId;
        }

        public string Type { get; private set; }
        public int TransactionId { get; private set; }
        public int PreviousTransactionId { get; private set; }
    }
}
