namespace Com.Danliris.Service.Packing.Inventory.Application.Master.Fabric
{
    public class PackingAndSKUCode
    {
        public PackingAndSKUCode(string packingCode, string skuCode)
        {
            PackingCode = packingCode;
            SKUCode = skuCode;
        }

        public string PackingCode { get; private set; }
        public string SKUCode { get; private set; }
    }
}