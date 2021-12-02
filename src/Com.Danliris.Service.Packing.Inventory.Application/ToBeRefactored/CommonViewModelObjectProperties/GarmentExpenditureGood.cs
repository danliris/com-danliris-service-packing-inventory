using System;

namespace Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties
{
    public class GarmentExpenditureGood 
    {
        public string Id { get; set; }
        public string RONo { get; set; }
        public string Invoice { get; set; }
        public string ExpenditureGoodNo { get; set; }
        public string Article { get; set; }
        public string ContractNo { get; set; }
        public GarmentComodity Comodity { get; set; }
        public UnitDepartment Unit { get; set; }
        public Buyer2 Buyer { get; set; }
        public double TotalQuantity { get; set; }     
    }

    public class GarmentComodity
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class UnitDepartment
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }
    }

    public class Buyer2
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }

    //public class GarmentExpenditureGoodMutation
    //{
    //    public string ExpenditureGoodId { get; set; }
    //    public string RO { get; set; }
    //    public string Article { get; set; }
    //    public string UnitCode { get; set; }
    //    public string BuyerContract { get; set; }
    //    public string ExpenditureTypeName { get; set; }
    //    public string Description { get; set; }
    //    public string ComodityName { get; set; }
    //    public string ComodityCode { get; set; }
    //    public string SizeNumber { get; set; }
    //    public string Descriptionb { get; set; }
    //    public double Qty { get; set; }

    //}
}
