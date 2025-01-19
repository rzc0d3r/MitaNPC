using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;

namespace MitaNPC.Items.Potions
{
    public class Ramen : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.useTurn = true;
            Item.maxStack = Item.CommonMaxStack;
            Item.useAnimation = 100;
            Item.useTime = 100;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.UseSound = new SoundStyle("MitaNPC/Sounds/RamenConsume");
            Item.consumable = true;
            
            Item.value = Item.buyPrice(gold: 12);
            Item.rare = ItemRarityID.Blue;

            Item.buffType = BuffID.WellFed3;
            Item.buffTime = 60 * 60 * 8; // 8m
        }
    }
}
