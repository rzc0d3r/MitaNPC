using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using MitaNPC.Projectiles.Weapons.Magic;

namespace MitaNPC.Items.Weapons.Magic
{
    public class MilasBook : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 44;
            Item.damage = 30;
            Item.noMelee = true;
            Item.useAnimation = 18;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTime = 18;
            Item.knockBack = 2f;
            Item.UseSound = SoundID.Item8;
            Item.autoReuse = true;
            Item.maxStack = 1;
            Item.value = Item.buyPrice(0, 8, 0, 0);
            Item.rare = ItemRarityID.Blue;
            Item.shoot = ModContent.ProjectileType<MilasBookProjectile>();
            Item.shootSpeed = 8f;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 8;
        }
    }
}