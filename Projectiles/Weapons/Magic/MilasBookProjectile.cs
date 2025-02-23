using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using Microsoft.Xna.Framework;

namespace MitaNPC.Projectiles.Weapons.Magic
{
    public class MilasBookProjectile : ModProjectile
    {
        public float startSpeed;

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 15;
        }

        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 13;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.extraUpdates = 1;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 60 * 15;
            Projectile.friendly = true;
            Projectile.alpha = 255;
        }

        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, 1f, 1f, 1f);

            ++Projectile.frameCounter;
            if (Projectile.frameCounter >= 20)
            {
                Projectile.frameCounter = 0;
                Projectile.frame = Main.rand.Next(0, 15);
            }

            if (Projectile.alpha > 0)
                Projectile.alpha -= 15;
            if (Projectile.alpha < 0)
                Projectile.alpha = 0;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = Main.player[Projectile.owner];
            float numberOfDusts = 4f;
            float rotFactor = 360f / numberOfDusts;
            for (int i = 0; i < numberOfDusts; i++)
            {
                float rot = MathHelper.ToRadians(i * rotFactor);
                Vector2 offset = new Vector2(18f, 0).RotatedBy(rot * Main.rand.NextFloat(1f, 1.2f));
                Vector2 velOffset = new Vector2(12f, 0).RotatedBy(rot * Main.rand.NextFloat(1f, 1.2f));
                Dust dust = Dust.NewDustPerfect(Projectile.Center + offset, DustID.WhiteTorch, new Vector2(velOffset.X, velOffset.Y));
                dust.noGravity = true;
                dust.velocity = velOffset;
                dust.scale = Main.rand.NextFloat(0.5f, 1.3f);
            }
            Projectile.Kill();
        }
    }
}
