using Com.Moonlay.Models;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.MaterialDeliveryNoteWeaving
{
    public class ItemsMaterialDeliveryNoteWeavingModel : StandardEntity
    {

        public ItemsMaterialDeliveryNoteWeavingModel(
            string itemnospp,
            string itemmaterialName,
            string itemdesign,
            string itemtype,
            string itemcode,
            decimal inputpacking,
            decimal length,
            decimal inputconversion
            )
        {
            ItemNoSPP = itemnospp;
            ItemMaterialName = itemmaterialName;
            ItemDesign = itemdesign;
            ItemType = itemtype;
            ItemCode = itemcode;
            InputPacking = inputpacking;
            Length = length;
            InputConversion = inputconversion;

        }

        public string ItemNoSPP { get; set; }
        public string ItemMaterialName { get; set; }
        public string ItemDesign { get; set; }
        public string ItemType { get; set; }
        public string ItemCode { get; set; }
        public decimal InputPacking { get; set; }
        public decimal Length { get; set; }
        public decimal InputConversion { get; set; }

        public void SetItemNoSPP(string newItemNoSPP)
        {
            if (newItemNoSPP != ItemNoSPP)
            {
                ItemNoSPP = newItemNoSPP;
            }
        }

        public void SetItemMaterialName(string newItemMaterialName)
        {
            if (newItemMaterialName != ItemMaterialName)
            {
                ItemMaterialName = newItemMaterialName;
            }
        }

        public void SetItemDesign(string newItemDesign)
        {
            if (newItemDesign != ItemDesign)
            {
                ItemDesign = newItemDesign;
            }
        }

        public void SetItemType(string newItemType)
        {
            if (newItemType != ItemType)
            {
                ItemType = newItemType;
            }
        }

        public void SetItemCode(string newItemCode)
        {
            if (newItemCode != ItemCode)
            {
                ItemCode = newItemCode;
            }
        }

        public void SetInputPacking(decimal newInputPacking)
        {
            if (newInputPacking != InputPacking)
            {
                InputPacking = newInputPacking;
            }
        }

        public void SetLength(decimal newLength)
        {
            if (newLength != Length)
            {
                Length = newLength;
            }
        }

        public void SetInputConversion(decimal newInputConversion)
        {
            if (newInputConversion != InputConversion)
            {
                InputConversion = newInputConversion;
            }
        }
    }
}