﻿using Terraria;
using Terraria.ModLoader;

using MitaNPC.Projectiles.Pets;

namespace MitaNPC.Buffs
{
    public class AlarmClockBuff : ModBuff
    {

        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
            Main.vanityPet[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            bool unused = false;
            player.BuffHandle_SpawnPetIfNeededAndSetTime(buffIndex, ref unused, ModContent.ProjectileType<BrokenMitaPetProjectile>());
        }
    }
}
