using Terraria;
using Terraria.ID;
using Terraria.GameInput;
using Terraria.ModLoader;

using Microsoft.Xna.Framework;

namespace MitaNPC.Common
{
    public class MitaNPCPlayer : ModPlayer
    {
        public bool travelRing = false;

        public override void ResetEffects()
        {
            travelRing = false;
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (Player.dead)
                return;

            if (MitaNPCKeybinds.TravelRingHotKey.JustPressed && travelRing && Main.myPlayer == Player.whoAmI && !Player.HasBuff(BuffID.ChaosState))
            {
                Vector2 teleportLocation;
                teleportLocation.X = (float)Main.mouseX + Main.screenPosition.X;
                if (Player.gravDir == 1f)
                    teleportLocation.Y = (float)Main.mouseY + Main.screenPosition.Y - (float)Player.height;
                else
                    teleportLocation.Y = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY;
                teleportLocation.X -= (float)(Player.width / 2);
                if (teleportLocation.X > 50f && teleportLocation.X < (float)(Main.maxTilesX * 16 - 50) && teleportLocation.Y > 50f && teleportLocation.Y < (float)(Main.maxTilesY * 16 - 50))
                {
                    if (!Collision.SolidCollision(teleportLocation, Player.width, Player.height))
                    {
                        Player.Teleport(teleportLocation);
                        NetMessage.SendData(MessageID.TeleportEntity, -1, -1, null, 0, (float)Player.whoAmI, teleportLocation.X, teleportLocation.Y, 1, 0, 0);
                        Player.AddBuff(BuffID.ChaosState, 60 * 20);
                    }
                }

            }
        }
    }
}
