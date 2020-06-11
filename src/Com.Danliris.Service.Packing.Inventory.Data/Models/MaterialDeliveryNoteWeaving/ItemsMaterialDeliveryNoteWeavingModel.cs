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
            decimal? inputbale,
            decimal? inputpiece,
            decimal? inputmeter,
            decimal? inputkg
            )
        {
            itemNoSOP = itemnosop;
            itemMaterialName = itemmaterialName;
            itemGrade = itemgrade;
            itemType = itemtype;
            itemCode = itemcode;
            inputBale = inputbale;
            inputPiece = inputpiece;
            inputMeter = inputmeter;
            inputKg = inputkg;

        }

        public string itemNoSOP { get; set; }
        public string itemMaterialName { get; set; }
        public string itemGrade { get; set; }
        public string itemType { get; set; }
        public string itemCode { get; set; }
        public decimal? inputBale { get; set; }
        public decimal? inputPiece { get; set; }
        public decimal? inputMeter { get; set; }
        public decimal? inputKg { get; set; }

        public void SetItemNoSOP(string newItemNoSPP)
        {
            if (newItemNoSPP != itemNoSOP)
            {
                itemNoSOP = newItemNoSPP;
            }
        }

        public void SetItemMaterialName(string newItemMaterialName)
        {
            if (newItemMaterialName != itemMaterialName)
            {
                itemMaterialName = newItemMaterialName;
            }
        }

        public void SetitemGrade(string newitemGrade)
        {
            if (newitemGrade != itemGrade)
            {
                itemGrade = newitemGrade;
            }
        }

        public void SetItemType(string newItemType)
        {
            if (newItemType != itemType)
            {
                itemType = newItemType;
            }
        }

        public void SetItemCode(string newItemCode)
        {
            if (newItemCode != itemCode)
            {
                itemCode = newItemCode;
            }
        }

        public void SetinputBale(decimal? newinputBale)
        {
            if (newinputBale != inputBale)
            {
                inputBale = newinputBale;
            }
        }

        public void SetinputPiece(decimal? newinputPiece)
        {
            if (newinputPiece != inputPiece)
            {
                inputPiece = newinputPiece;
            }
        }

        public void SetinputMeter(decimal? newinputMeter)
        {
            if (newinputMeter != inputMeter)
            {
                inputMeter = newinputMeter;
            }
        }

        public void SetinputKg(decimal? newinputKg)
        {
            if (newinputKg != inputKg)
            {
                inputKg = newinputKg;
            }
        }
    }
}