using Com.Moonlay.Models;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.MaterialDeliveryNoteWeaving
{
    public class ItemsMaterialDeliveryNoteWeavingModel : StandardEntity
    {

        public ItemsMaterialDeliveryNoteWeavingModel()
        {

        }

        public ItemsMaterialDeliveryNoteWeavingModel(
            string itemnosop,
            string itemmaterialName,
            string itemgrade,
            string itemtype,
            string itemcode,
            decimal inputbale,
            decimal inputpiece,
            decimal inputmeter,
            decimal inputkg
            )
        {
            ItemNoSOP = itemnosop;
            ItemMaterialName = itemmaterialName;
            ItemGrade = itemgrade;
            ItemType = itemtype;
            ItemCode = itemcode;
            InputBale = inputbale;
            InputPiece = inputpiece;
            InputMeter = inputmeter;
            InputKg = inputkg;

        }

        public string ItemNoSOP { get; set; }
        public string ItemMaterialName { get; set; }
        public string ItemGrade { get; set; }
        public string ItemType { get; set; }
        public string ItemCode { get; set; }
        public decimal InputBale { get; set; }
        public decimal InputPiece { get; set; }
        public decimal InputMeter { get; set; }
        public decimal InputKg { get; set; }

        public void SetItemNoSOP(string newItemNoSPP)
        {
            if (newItemNoSPP != ItemNoSOP)
            {
                ItemNoSOP = newItemNoSPP;
            }
        }

        public void SetItemMaterialName(string newItemMaterialName)
        {
            if (newItemMaterialName != ItemMaterialName)
            {
                ItemMaterialName = newItemMaterialName;
            }
        }

        public void SetitemGrade(string newitemGrade)
        {
            if (newitemGrade != ItemGrade)
            {
                ItemGrade = newitemGrade;
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

        public void SetinputBale(decimal newinputBale)
        {
            if (newinputBale != InputBale)
            {
                InputBale = newinputBale;
            }
        }

        public void SetinputPiece(decimal newinputPiece)
        {
            if (newinputPiece != InputPiece)
            {
                InputPiece = newinputPiece;
            }
        }

        public void SetinputMeter(decimal newinputMeter)
        {
            if (newinputMeter != InputMeter)
            {
                InputMeter = newinputMeter;
            }
        }

        public void SetinputKg(decimal newinputKg)
        {
            if (newinputKg != InputKg)
            {
                InputKg = newinputKg;
            }
        }
    }
}