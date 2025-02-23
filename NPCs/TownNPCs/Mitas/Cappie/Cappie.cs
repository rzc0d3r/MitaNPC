using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.Utilities;
using Terraria.ModLoader;
using Terraria.Localization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MitaNPC.NPCs.TownNPCs.Mitas.Mita;
using MitaNPC.Items.Armor.Vanity;
using Terraria.GameContent.Bestiary;

namespace MitaNPC.NPCs.TownNPCs.Mitas.Cappie
{
    [AutoloadHead]
    public class Cappie : MitaBase
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            NPCID.Sets.DangerDetectRange[Type] = 30;
        }

        public override string GetChat()
        {
            WeightedRandom<string> chat = new();

            string mitaCappie_greeting1 = Language.GetTextValue("Mods.MitaNPC.NPCs.Mita.MitaCappieDialogue.Greeting1");
            string mitaCappie_greeting2 = Language.GetTextValue("Mods.MitaNPC.NPCs.Mita.MitaCappieDialogue.Greeting2");
            string mitaCappie_phrase1 = Language.GetTextValue("Mods.MitaNPC.NPCs.Mita.MitaCappieDialogue.Phrase1");
            string mitaCappie_phrase2 = Language.GetTextValue("Mods.MitaNPC.NPCs.Mita.MitaCappieDialogue.Phrase2");
            string mitaCappie_phrase3 = Language.GetTextValue("Mods.MitaNPC.NPCs.Mita.MitaCappieDialogue.Phrase3");
            string mitaCappie_phrase4 = Language.GetTextValue("Mods.MitaNPC.NPCs.Mita.MitaCappieDialogue.Phrase4");
            string mitaCappie_phrase5 = Language.GetTextValue("Mods.MitaNPC.NPCs.Mita.MitaCappieDialogue.Phrase5");

            chat.Add(mitaCappie_greeting1);
            chat.Add(mitaCappie_greeting2);
            chat.Add(mitaCappie_phrase1);
            chat.Add(mitaCappie_phrase2);
            chat.Add(mitaCappie_phrase3);
            chat.Add(mitaCappie_phrase4);

            string pathToSound = "MitaNPC/Sounds/Mita/";
            if (Language.ActiveCulture.Name == "ru-RU")
                pathToSound += "Russian";
            else
                pathToSound += "Japanese";

            string phrase = chat.Get();
            if (phrase == mitaCappie_greeting1)
                SoundEngine.PlaySound(new SoundStyle(pathToSound + "/Location7/140"));
            else if (phrase == mitaCappie_greeting2)
                SoundEngine.PlaySound(new SoundStyle(pathToSound + "/Location7/317"));
            else if (phrase == mitaCappie_phrase1)
                SoundEngine.PlaySound(new SoundStyle(pathToSound + "/Location7/368"));
            else if (phrase == mitaCappie_phrase2)
                SoundEngine.PlaySound(new SoundStyle(pathToSound + "/Location7/427"));
            else if (phrase == mitaCappie_phrase3)
                SoundEngine.PlaySound(new SoundStyle(pathToSound + "/Location7/459"));
            else if (phrase == mitaCappie_phrase4)
                SoundEngine.PlaySound(new SoundStyle(pathToSound + "/Location7/482"));
            else if (phrase == mitaCappie_phrase5)
                SoundEngine.PlaySound(new SoundStyle(pathToSound + "/Location7/507"));
            return phrase;
        }

        public override void AddShops()
        {
            NPCShop shop = new NPCShop(Type, "Shop");
            shop.Add(ModContent.ItemType<MitasCap>())
            .Register();
        }

        public override void DrawTownAttackSwing(ref Texture2D item, ref Rectangle itemFrame, ref int itemSize, ref float scale, ref Vector2 offset)
        {
            Main.GetItemDrawFrame(ItemID.None, out Texture2D itemTexture, out Rectangle itemRectangle);

            item = itemTexture;
            itemFrame = itemRectangle;
            itemSize = itemRectangle.Width;
            scale = 0.15f;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                new FlavorTextBestiaryInfoElement("Mods.MitaNPC.NPCs.Cappie.BestiaryDescription")
            });
        }
    }
}