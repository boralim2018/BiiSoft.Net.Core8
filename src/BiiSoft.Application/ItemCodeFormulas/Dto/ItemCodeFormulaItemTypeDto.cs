using BiiSoft.Enums;
using BiiSoft.Extensions;

namespace BiiSoft.ItemCodeFormulas.Dto
{
    public class ItemCodeFormulaItemTypeDto
    {
        public ItemType ItemType { get; set; }
        public string ItemTypeName => ItemType.GetName();
    }

}
