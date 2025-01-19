using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;

using MitaNPC.Buffs;
using MitaNPC.Projectiles.Pets;

namespace MitaNPC.Items.Pets
{
    public class GameConsole : ModItem
	{
		public override void SetDefaults() {
			Item.CloneDefaults(ItemID.ZephyrFish);
			Item.shoot = ModContent.ProjectileType<FlyPetProjectile>();
			Item.buffType = ModContent.BuffType<GameConsoleBuff>();
			Item.UseSound = new SoundStyle("MitaNPC/Sounds/MarioCoinGrab") { Volume = 0.35f };
			Item.value = Item.buyPrice(gold: 12);
		}

        public override bool? UseItem(Player player)
        {
			if (player.whoAmI == Main.myPlayer)
				player.AddBuff(Item.buffType, 3600);
   			return true;
		}
	}
}