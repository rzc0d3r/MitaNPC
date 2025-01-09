using Terraria.ModLoader;

namespace MitaNPC
{
    public class MitaNPCKeybinds : ModSystem
    {
        public static ModKeybind TravelRingHotKey { get; private set; }

        public override void Load()
        {        
            TravelRingHotKey = KeybindLoader.RegisterKeybind(Mod, "TravelRingHotKey", "Q");
        }

        public override void Unload()
        {
            TravelRingHotKey = null;
        }
    }
}