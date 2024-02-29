using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentSubcon.GarmentReceiptSubconPackingList.ViewModel
{
    public class ApprovalGarmentReceiptSubconPackingListViewModel : IValidatableObject
    {
        public int id { get; set; }
        public string type { get; set; }
        public double kurs { get; set; }
        public string approvedMDRemark { get; set; }
        public bool isReject { get; set; }
        public string rejectTo { get; set; }
        public string rejectReason { get; set; }

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (type == "MD" && kurs == 0)
            {
                yield return new ValidationResult("Kurs tidak boleh kosong", new List<string> { "kurs" });
            }
            if(isReject == true && string.IsNullOrWhiteSpace(rejectReason))
            {
                yield return new ValidationResult("Alasan reject harus Di isi", new List<string> { "rejectReason" });
            }
        }
    }   

}
