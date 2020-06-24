namespace Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties
{
    public class BankAccount
    {
        public int id { get; set; }
        public string accountName { get; set; }
        public string bankAddress { get; set; }
        public string swiftCode { get; set; }
        public string AccountNumber { get; set; }
        public Currency Currency { get; set; }
    }
}
