using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using Microsoft.Xna.Framework;

using System;

using MitaNPC.Buffs;

namespace MitaNPC.Projectiles.Pets
{
    public class BrokenMitaPetProjectile : ModProjectile
    {
        private bool fly = false;
        private int playerStill;

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 5;
            Main.projPet[Projectile.type] = true;
            ProjectileID.Sets.CharacterPreviewAnimations[Projectile.type] = ProjectileID.Sets.SimpleLoop(0, Main.projFrames[Projectile.type] - 1, 22)
                .WithOffset(-6, -5f)
                .WithSpriteDirection(-1)
                .WithCode(CustomFloatAnimation);
        }

        public override void SetDefaults()
        {
            Projectile.netImportant = true;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft *= 5;
            Projectile.tileCollide = true;
            Projectile.width = 30;
            Projectile.height = 42;
            Projectile.scale = 0.85f;
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            Player player = Main.player[Projectile.owner];
            Vector2 projCenter = Projectile.Center;
            Vector2 playerDirection = player.Center - projCenter;
            float playerDistance = playerDirection.Length();
            fallThrough = playerDistance > 200f;
            return true;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (!player.dead && player.HasBuff(ModContent.BuffType<AlarmClockBuff>()))
                Projectile.timeLeft = 2;
            if (!player.active)
            {
                Projectile.active = false;
                return;
            }

            Vector2 vector46 = Projectile.position;
            if (!fly)
            {
                Projectile.rotation = 0;
                Vector2 projCenter = Projectile.Center;
                Vector2 playerDirection = player.Center - projCenter;
                float playerDistance = playerDirection.Length();
                if (Projectile.velocity.Y == 0 && (HoleBelow() || (playerDistance > 110f && Projectile.position.X == Projectile.oldPosition.X)))
                    Projectile.velocity.Y = -5f;
                Projectile.velocity.Y += 0.20f;
                if (Projectile.velocity.Y > 7f)
                    Projectile.velocity.Y = 7f;
                if (playerDistance > 600f)
                {
                    fly = true;
                    Projectile.velocity.X = 0f;
                    Projectile.velocity.Y = 0f;
                    Projectile.tileCollide = false;
                }
                if (playerDistance > 100f)
                {
                    if (player.position.X - Projectile.position.X > 0f)
                    {
                        Projectile.velocity.X += 0.10f;
                        if (Projectile.velocity.X > 7f)
                            Projectile.velocity.X = 7f;
                    }
                    else
                    {
                        Projectile.velocity.X -= 0.10f;
                        if (Projectile.velocity.X < -7f)
                            Projectile.velocity.X = -7f;
                    }
                }
                if (playerDistance < 100f)
                {
                    if (Projectile.velocity.X != 0f)
                    {
                        if (Projectile.velocity.X > 0.4f)
                            Projectile.velocity.X -= 0.15f;
                        else if (Projectile.velocity.X < -0.4f)
                            Projectile.velocity.X += 0.15f;
                        else if (Projectile.velocity.X < 0.4f && Projectile.velocity.X > -0.4f)
                            Projectile.velocity.X = 0f;
                    }
                }
                if (Projectile.position.X == Projectile.oldPosition.X && Projectile.position.Y == Projectile.oldPosition.Y && Projectile.velocity.X == 0)
                    Projectile.frame = 0;
                else if (Projectile.velocity.Y > 0.3f && Projectile.position.Y != Projectile.oldPosition.Y)
                {
                    Projectile.frame = 1;
                    Projectile.frameCounter = 0;
                }
                else
                {
                    Projectile.frameCounter++;
                    if (Projectile.frameCounter > 8)
                    {
                        Projectile.frame++;
                        Projectile.frameCounter = 0;
                    }
                    if (Projectile.frame > 3)
                        Projectile.frame = 0;
                }
            }
            else if (fly)
            {
                float flySpeed = 0.3f;
                Projectile.tileCollide = false;
                Vector2 flyDirection = Projectile.Center;
                float horiPos = Main.player[Projectile.owner].position.X + (float)(Main.player[Projectile.owner].width / 2) - flyDirection.X;
                float vertiPos = Main.player[Projectile.owner].position.Y + (float)(Main.player[Projectile.owner].height / 2) - flyDirection.Y;
                vertiPos += (float)Main.rand.Next(-10, 21);
                horiPos += (float)Main.rand.Next(-10, 21);
                horiPos += (float)(60 * -(float)Main.player[Projectile.owner].direction);
                vertiPos -= 60f;
                float playerDistance = (float)Math.Sqrt((double)(horiPos * horiPos + vertiPos * vertiPos));
                if (playerDistance > 1200f)
                {
                    Projectile.position.X = Main.player[Projectile.owner].Center.X - (float)(Projectile.width / 2);
                    Projectile.position.Y = Main.player[Projectile.owner].Center.Y - (float)(Projectile.height / 2);
                    Projectile.netUpdate = true;
                }
                if (playerDistance < 100f)
                {
                    flySpeed = 0.1f;
                    if (player.velocity.Y == 0f)
                        ++playerStill;
                    else
                        playerStill = 0;
                    if (playerStill > 60 && !Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
                    {
                        fly = false;
                        Projectile.tileCollide = true;
                    }
                }
                if (playerDistance < 50f)
                {
                    if (Math.Abs(Projectile.velocity.X) > 2f || Math.Abs(Projectile.velocity.Y) > 2f)
                        Projectile.velocity *= 0.90f;
                    flySpeed = 0.01f;
                }
                else
                {
                    if (playerDistance < 100f)
                        flySpeed = 0.1f;
                    if (playerDistance > 300f)
                        flySpeed = 1f;
                    playerDistance = 18f / playerDistance;
                    horiPos *= playerDistance;
                    vertiPos *= playerDistance;
                }
                if (Projectile.velocity.X <= horiPos)
                {
                    Projectile.velocity.X = Projectile.velocity.X + flySpeed;
                    if (flySpeed > 0.05f && Projectile.velocity.X < 0f)
                        Projectile.velocity.X = Projectile.velocity.X + flySpeed;
                }
                if (Projectile.velocity.X > horiPos)
                {
                    Projectile.velocity.X = Projectile.velocity.X - flySpeed;
                    if (flySpeed > 0.05f && Projectile.velocity.X > 0f)
                        Projectile.velocity.X = Projectile.velocity.X - flySpeed;
                }
                if (Projectile.velocity.Y <= vertiPos)
                {
                    Projectile.velocity.Y = Projectile.velocity.Y + flySpeed;
                    if (flySpeed > 0.05f && Projectile.velocity.Y < 0f)
                        Projectile.velocity.Y = Projectile.velocity.Y + flySpeed * 2f;
                }
                if (Projectile.velocity.Y > vertiPos)
                {
                    Projectile.velocity.Y = Projectile.velocity.Y - flySpeed;
                    if (flySpeed > 0.05f && Projectile.velocity.Y > 0f)
                        Projectile.velocity.Y = Projectile.velocity.Y - flySpeed * 2f;
                }
                Projectile.rotation = Projectile.velocity.X * 0.03f;
                Projectile.frame = 4;
            }
            if (Projectile.velocity.X > 0.25f)
                Projectile.spriteDirection = -1;
            else if (Projectile.velocity.X < -0.25f)
                Projectile.spriteDirection = 1;
        }

        private bool HoleBelow()
        {
            int tileWidth = 4;
            int tileX = (int)(Projectile.Center.X / 16f) - tileWidth;
            if (Projectile.velocity.X > 0)
                tileX += tileWidth;
            int tileY = (int)((Projectile.position.Y + Projectile.height) / 16f);
            for (int y = tileY; y < tileY + 2; y++)
            {
                for (int x = tileX; x < tileX + tileWidth; x++)
                {
                    if (Main.tile[x, y].HasTile)
                        return false;
                }
            }
            return true;
        }

        private void CustomFloatAnimation(Projectile proj, bool walking)
        {
            float num = 0.5f;
            float num2 = (float)Main.timeForVisualEffects % 60f / 60f;
            proj.position.Y += 0f - num + (float)(0.5 * (Math.Cos(num2 * ((float)Math.PI * 2f) * 2f) * (double)(num * 2f)));
        }
    }
}