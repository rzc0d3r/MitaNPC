using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

using MitaNPC.Common;

namespace MitaNPC.Items.PermanentBoosters
{
    public class MagiciansManual : ModItem
    {
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs("10");
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 30;
            Item.useTurn = true;
            Item.maxStack = Item.CommonMaxStack;
            Item.useAnimation = 50;
            Item.useTime = 50;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.UseSound = SoundID.Item92;
            Item.consumable = true;

            Item.value = Item.buyPrice(0, 15, 0, 0);
            Item.rare = ItemRarityID.Blue;

            Item.buffType = BuffID.Honey; // Without it, Item will be infinite and don't works
        }
        public override void OnConsumeItem(Player player)
        {
            MitaNPCPlayer modplayer = player.GetModPlayer<MitaNPCPlayer>();
            modplayer.magiciansManualUsed = true;
        }

        public override bool CanUseItem(Player player)
        {
            return !player.GetModPlayer<MitaNPCPlayer>().magiciansManualUsed;
        }
    }
}