using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.DataStructures;

using ReLogic.Content;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MitaNPC.Projectiles.Pets;

namespace MitaNPC.Buffs
{
    public class GameConsoleBuff : ModBuff
    {
        public static readonly int FrameCount = 2;
        public static readonly int AnimationSpeed = 14;
        private Asset<Texture2D> animatedTexture;
        
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
            Main.vanityPet[Type] = true;
            animatedTexture = ModContent.Request<Texture2D>("MitaNPC/Buffs/GameConsoleBuff");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            bool unused = false;
            player.BuffHandle_SpawnPetIfNeededAndSetTime(buffIndex, ref unused, ModContent.ProjectileType<FlyPetProjectile>());
        }

        public override bool RightClick(int buffIndex)
        {
            SoundEngine.PlaySound(new SoundStyle("MitaNPC/Sounds/MarioCoinGrabReversed") { Volume = 0.35f });
            return true;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, int buffIndex, ref BuffDrawParams drawParams)
        {
            Texture2D ourTexture = animatedTexture.Value;
            Rectangle ourSourceRectangle = ourTexture.Frame(verticalFrames: FrameCount, frameY: (int)Main.GameUpdateCount / AnimationSpeed % FrameCount);
            drawParams.Texture = ourTexture;
            drawParams.SourceRectangle = ourSourceRectangle;
            return true;
        }
    }
}
