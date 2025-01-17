using Terraria;
using Terraria.ID;
using Terraria.GameInput;
using Terraria.ModLoader;

using Microsoft.Xna.Framework;
using static MitaNPC.MitaNPC;
using Terraria.ModLoader.IO;

namespace MitaNPC.Common
{
    public class MitaNPCPlayer : ModPlayer
    {
        public bool travelRing = false;
        public bool martialArtsManualUsed = false;
        public bool shootersManualUsed = false;
        public bool pokemonManualUsed = false;
        public bool magiciansManualUsed = false;

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
                        Player.AddBuff(BuffID.ChaosState, 60 * 20); // 20s
                    }
                }
            }
        }

        public override void OnEnterWorld()
        {
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                ModPacket packet = Mod.GetPacket();
                packet.Write((byte)MitaNPCMessageType.MitaSkinManager);
                packet.Write(false); // get the Mita skin from Server, rather than modifying the Mita skin on Server
                packet.Write(-1);    // just filling in with something (if second argument is false -> it argument is ignored by Server)
                packet.Write(true);  // true - receive reply packet
                packet.Send();
            }
        }

        public override void ModifyWeaponDamage(Item item, ref StatModifier damage)
        {
            if (martialArtsManualUsed)
            {
                Player.autoReuseGlove = true;
                if (item.CountsAsClass<MeleeDamageClass>())
                {
                    damage *= 1.1f; // +10% damage
                }
            }
            if (pokemonManualUsed)
            {
                Player.autoReuseGlove = true;
                if (item.CountsAsClass<SummonDamageClass>())
                {
                    damage *= 1.1f; // +10% damage
                }
            }
            if (shootersManualUsed)
            {
                if (item.CountsAsClass<RangedDamageClass>())
                {
                    damage *= 1.1f; // +10% damage
                }
            }
            if (magiciansManualUsed)
            {
                if (item.CountsAsClass<MagicDamageClass>())
                {
                    damage *= 1.1f; // +10% damage
                }
            }
        }

        public override void SaveData(TagCompound tag)
        {
            tag["martialArtsManualUsed"] = martialArtsManualUsed;
            tag["shootersManualUsed"] = shootersManualUsed;
            tag["pokemonManualUsed"] = pokemonManualUsed;
            tag["magiciansManualUsed"] = magiciansManualUsed;
        }

        public override void LoadData(TagCompound tag)
        {
            if (tag.ContainsKey("martialArtsManualUsed"))
                martialArtsManualUsed = tag.GetBool("martialArtsManualUsed");
            if (tag.ContainsKey("shootersManualUsed"))
                martialArtsManualUsed = tag.GetBool("shootersManualUsed");
            if (tag.ContainsKey("pokemonManualUsed"))
                martialArtsManualUsed = tag.GetBool("pokemonManualUsed");
            if (tag.ContainsKey("magiciansManualUsed"))
                martialArtsManualUsed = tag.GetBool("magiciansManualUsed");
        }
    }
}