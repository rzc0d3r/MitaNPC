using System;
using System.IO;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using MitaNPC.NPCs.TownNPCs;


namespace MitaNPC
{
	// Please read https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-Guide#mod-skeleton-contents for more information about the various files in a mod.
	public class MitaNPC : Mod
	{
        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            MitaNPCMessageType msgType = (MitaNPCMessageType)reader.ReadByte();
            switch (msgType)
            {
                case MitaNPCMessageType.MitaSkinManager:
                    foreach (NPC npc in Main.npc)
                    {
                        if (npc.ModNPC is Mita mita)
                        {
                            int mitaSkin;
                            if (reader.ReadBoolean()) // true - change Mita Skin on server
                                mitaSkin = reader.ReadInt32();
                            else // false - copy current Mita Skin on server
                            {
                                mitaSkin = mita.MitaSkin;
                                reader.ReadInt32(); // ignoring second argument from packet
                            }
                            mita.MitaSkin = mitaSkin;
                            if (reader.ReadBoolean()) // true - send reply packet + server synchronizes all clients
                            {
                                ModPacket packet = this.GetPacket();
                                packet.Write((byte)MitaNPCMessageType.MitaSkinManager);
                                packet.Write(true);
                                packet.Write(mitaSkin);
                                packet.Write(false);
                                packet.Send();
                            }
                            break;
                        }
                    }
                    break;
            }
        }

        public enum MitaNPCMessageType : byte
        {
            MitaSkinManager
        }
    }
}
