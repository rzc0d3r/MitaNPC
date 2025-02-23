using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MitaNPC.Items.Armor.Vanity
{
    [AutoloadEquip(EquipType.Head)]
    public class MitasCap: ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 16;
            Item.rare = ItemRarityID.Blue;
            Item.vanity = true;
            Item.value = Item.buyPrice(0, 2, 0, 0);
        }
    }
}
