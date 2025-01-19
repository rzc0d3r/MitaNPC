using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using MitaNPC.Buffs;

namespace MitaNPC.Projectiles.Pets
{
    public class FlyPetProjectile : ModProjectile
    {
        private int customFrameCounter = 0;

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 2;
            Main.projPet[Projectile.type] = true;
            ProjectileID.Sets.CharacterPreviewAnimations[Projectile.type] = ProjectileID.Sets.SimpleLoop(0, Main.projFrames[Projectile.type], 26)
                .WithOffset(-22, -32f)
                .WithSpriteDirection(-1)
                .WithCode(DelegateMethods.CharacterPreview.Float);
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.ZephyrFish);
            Projectile.scale = 0.6f;
            AIType = ProjectileID.ZephyrFish;
        }

        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];
            player.zephyrfish = false;
            return true;
        }

        public override void AI()
        {
            Projectile.frameCounter = 0; // Ignoring default frameCounter
            Lighting.AddLight(Projectile.Center, 0.369f, 0.6f, 0.067f); // Lighting with a color like a texture

            Player player = Main.player[Projectile.owner];
            if (!player.dead && player.HasBuff(ModContent.BuffType<GameConsoleBuff>()))
                Projectile.timeLeft = 2;

            customFrameCounter++;
            if (customFrameCounter >= 12)
            {
                customFrameCounter = 0;
                Projectile.frame++;
            }
            if (Projectile.frame > 1)
                Projectile.frame = 0;
        }
    }
}