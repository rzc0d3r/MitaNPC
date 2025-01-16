using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

using System.Collections.Generic;

using MitaNPC.Common;
using Terraria.Localization;

namespace MitaNPC.Items.Accessories
{
    public class TravelRing : ModItem
    {
        float moveSpeedBonus = 0.1f;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs((int)(moveSpeedBonus*100));
        
        public override void SetStaticDefaults()
        {
            ItemID.Sets.AnimatesAsSoul[Type] = true;
            Main.RegisterItemAnimation(Type, new DrawAnimationVertical(12, 2));
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 20;
            Item.rare = ItemRarityID.Blue;
            Item.accessory = true;
            Item.value = Item.buyPrice(1, 25, 0, 0);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            MitaNPCPlayer modPlayer = player.GetModPlayer<MitaNPCPlayer>();
            modPlayer.travelRing = true;
            player.moveSpeed += moveSpeedBonus;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (MitaNPCKeybinds.TravelRingHotKey.GetAssignedKeys().Count != 0)
            {
                foreach (TooltipLine line in tooltips)
                {
                    line.Text = line.Text.Replace("[KEY]", MitaNPCKeybinds.TravelRingHotKey.GetAssignedKeys()[0]);
                }
            }
        }
    }
}