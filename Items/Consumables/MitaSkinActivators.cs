using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using MitaNPC.NPCs.TownNPCs;

namespace MitaNPC.Items.Consumables
{
    public abstract class MitaActivatorBase : ModItem
    {
        protected int MitaSkin { get; set; } = 1;

        protected MitaActivatorBase(int MitaSkin)
        {
            this.MitaSkin = MitaSkin;
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 12;
            Item.useTurn = true;
            Item.maxStack = Item.CommonMaxStack;
            Item.useAnimation = 50;
            Item.useTime = 50;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.UseSound = SoundID.Item92;
            Item.consumable = true;

            Item.value = Item.buyPrice(0, 0, 10, 0);
            Item.rare = ItemRarityID.Blue;

            Item.buffType = BuffID.Honey; // Without it, Item will be infinite and don't works
        }

        public override void OnConsumeItem(Player player)
        {
            foreach (NPC npc in Main.npc)
            {
                if (npc.ModNPC is Mita mita)
                {
                    mita.MitaSkin = MitaSkin;
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        ModPacket packet = Mod.GetPacket();
                        packet.Write((byte)MitaNPC.MitaNPCMessageType.MitaSkinManager);
                        packet.Write(true);     // true - edit Mita skin on Server
                        packet.Write(MitaSkin); // second argument is true -> it argument is readed by Server)
                        packet.Write(true);
                        packet.Send();
                    }
                    break;
                }
            }
        }

        public override bool CanUseItem(Player player)
        {
            foreach (NPC npc in Main.npc)
            {
                if (npc.ModNPC is Mita mita)
                    return true;
            }
            return false;
        }
    }

    public class MitaDefaultActivator : MitaActivatorBase
    {
        MitaDefaultActivator() : base(1) { }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 24;
            Item.height = 16;
        }
    }

    public class MitaCappieActivator : MitaActivatorBase
    {
        MitaCappieActivator() : base(2) { }
    }

    public class MitaMilaActivator : MitaActivatorBase
    {
        MitaMilaActivator() : base(3) { }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 24;
            Item.height = 16;
        }
    }
}