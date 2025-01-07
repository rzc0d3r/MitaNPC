using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MitaNPC.Items.Armor.Vanity
{
    [AutoloadEquip(EquipType.Head)]
    public class MitasCap : ModItem
    {
        public override void SetStaticDefaults()
        {
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 10;
            Item.rare = ItemRarityID.Blue;
            Item.vanity = true;
            Item.value = Item.buyPrice(0, 2, 0, 0);
            
        }
    }
}
