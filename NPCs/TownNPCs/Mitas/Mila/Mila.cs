using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.Utilities;
using Terraria.ModLoader;
using Terraria.Localization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MitaNPC.Items.Weapons.Magic;
using MitaNPC.Items.PermanentBoosters;
using MitaNPC.NPCs.TownNPCs.Mitas.Mita;
using Terraria.GameContent.Bestiary;


namespace MitaNPC.NPCs.TownNPCs.Mitas.Mila
{
    [AutoloadHead]
    public class Mila : MitaBase
    {
        public override string GetChat()
        {
            WeightedRandom<string> chat = new();

            string mila_greeting1 = Language.GetTextValue("Mods.MitaNPC.NPCs.Mita.MilaDialogue.Greeting1");
            string mila_greeting2 = Language.GetTextValue("Mods.MitaNPC.NPCs.Mita.MilaDialogue.Greeting2");
            string mila_greeting3 = Language.GetTextValue("Mods.MitaNPC.NPCs.Mita.MilaDialogue.Greeting3");
            string mila_greeting4 = Language.GetTextValue("Mods.MitaNPC.NPCs.Mita.MilaDialogue.Greeting4");
            string mila_phrase1 = Language.GetTextValue("Mods.MitaNPC.NPCs.Mita.MilaDialogue.Phrase1");
            string mila_phrase2 = Language.GetTextValue("Mods.MitaNPC.NPCs.Mita.MilaDialogue.Phrase2");
            string mila_phrase3 = Language.GetTextValue("Mods.MitaNPC.NPCs.Mita.MilaDialogue.Phrase3");
            string mila_phrase4 = Language.GetTextValue("Mods.MitaNPC.NPCs.Mita.MilaDialogue.Phrase4");
            string mila_phrase5 = Language.GetTextValue("Mods.MitaNPC.NPCs.Mita.MilaDialogue.Phrase5");
            string mila_phrase6 = Language.GetTextValue("Mods.MitaNPC.NPCs.Mita.MilaDialogue.Phrase6");
            string mila_phrase7 = Language.GetTextValue("Mods.MitaNPC.NPCs.Mita.MilaDialogue.Phrase7");
            string mila_phrase8 = Language.GetTextValue("Mods.MitaNPC.NPCs.Mita.MilaDialogue.Phrase8");
            string mila_phrase9 = Language.GetTextValue("Mods.MitaNPC.NPCs.Mita.MilaDialogue.Phrase9");
            string mila_phrase10 = Language.GetTextValue("Mods.MitaNPC.NPCs.Mita.MilaDialogue.Phrase10");
            string mila_phrase11 = Language.GetTextValue("Mods.MitaNPC.NPCs.Mita.MilaDialogue.Phrase11");
            string mila_phrase12 = Language.GetTextValue("Mods.MitaNPC.NPCs.Mita.MilaDialogue.Phrase12");
            string mila_phrase13 = Language.GetTextValue("Mods.MitaNPC.NPCs.Mita.MilaDialogue.Phrase13");
            string mila_phrase14 = Language.GetTextValue("Mods.MitaNPC.NPCs.Mita.MilaDialogue.Phrase14");
            string mila_phrase15 = Language.GetTextValue("Mods.MitaNPC.NPCs.Mita.MilaDialogue.Phrase15");
            string mila_phrase16 = Language.GetTextValue("Mods.MitaNPC.NPCs.Mita.MilaDialogue.Phrase16");
            string mila_phrase17 = Language.GetTextValue("Mods.MitaNPC.NPCs.Mita.MilaDialogue.Phrase17");
            string mila_phrase18 = Language.GetTextValue("Mods.MitaNPC.NPCs.Mita.MilaDialogue.Phrase18");
            string mila_phrase19 = Language.GetTextValue("Mods.MitaNPC.NPCs.Mita.MilaDialogue.Phrase19");
            string mila_phrase20 = Language.GetTextValue("Mods.MitaNPC.NPCs.Mita.MilaDialogue.Phrase20");

            chat.Add(mila_greeting1);
            chat.Add(mila_greeting2);
            chat.Add(mila_greeting3);
            chat.Add(mila_greeting4);
            chat.Add(mila_phrase1);
            chat.Add(mila_phrase2);
            chat.Add(mila_phrase3);
            chat.Add(mila_phrase4);
            chat.Add(mila_phrase5);
            chat.Add(mila_phrase6);
            chat.Add(mila_phrase7);
            chat.Add(mila_phrase8);
            chat.Add(mila_phrase9);
            chat.Add(mila_phrase10);
            chat.Add(mila_phrase11);
            chat.Add(mila_phrase12);
            chat.Add(mila_phrase13);
            chat.Add(mila_phrase14);
            chat.Add(mila_phrase15);
            chat.Add(mila_phrase16);
            chat.Add(mila_phrase17);
            chat.Add(mila_phrase18);
            chat.Add(mila_phrase19);
            chat.Add(mila_phrase20);


            string pathToSound = "MitaNPC/Sounds/Mita/";
            if (Language.ActiveCulture.Name == "ru-RU")
                pathToSound += "Russian";
            else
                pathToSound += "Japanese";

            string phrase = chat.Get();
            if (phrase == mila_greeting1)
                SoundEngine.PlaySound(new SoundStyle(pathToSound + "/Location19/43"));
            else if (phrase == mila_greeting2)
                SoundEngine.PlaySound(new SoundStyle(pathToSound + "/Location19/139"));
            else if (phrase == mila_greeting3)
                SoundEngine.PlaySound(new SoundStyle(pathToSound + "/Location19/318"));
            else if (phrase == mila_greeting4)
                SoundEngine.PlaySound(new SoundStyle(pathToSound + "/Location19/592"));
            else if (phrase == mila_phrase1)
                SoundEngine.PlaySound(new SoundStyle(pathToSound + "/Location19/92"));
            else if (phrase == mila_phrase2)
                SoundEngine.PlaySound(new SoundStyle(pathToSound + "/Location19/102"));
            else if (phrase == mila_phrase3)
                SoundEngine.PlaySound(new SoundStyle(pathToSound + "/Location19/122"));
            else if (phrase == mila_phrase4)
                SoundEngine.PlaySound(new SoundStyle(pathToSound + "/Location19/174"));
            else if (phrase == mila_phrase5)
                SoundEngine.PlaySound(new SoundStyle(pathToSound + "/Location19/233"));
            else if (phrase == mila_phrase6)
                SoundEngine.PlaySound(new SoundStyle(pathToSound + "/Location19/234"));
            else if (phrase == mila_phrase7)
                SoundEngine.PlaySound(new SoundStyle(pathToSound + "/Location19/252"));
            else if (phrase == mila_phrase8)
                SoundEngine.PlaySound(new SoundStyle(pathToSound + "/Location19/253"));
            else if (phrase == mila_phrase9)
                SoundEngine.PlaySound(new SoundStyle(pathToSound + "/Location19/352"));
            else if (phrase == mila_phrase10)
                SoundEngine.PlaySound(new SoundStyle(pathToSound + "/Location19/357"));
            else if (phrase == mila_phrase11)
                SoundEngine.PlaySound(new SoundStyle(pathToSound + "/Location19/358"));
            else if (phrase == mila_phrase12)
                SoundEngine.PlaySound(new SoundStyle(pathToSound + "/Location19/363"));
            else if (phrase == mila_phrase13)
                SoundEngine.PlaySound(new SoundStyle(pathToSound + "/Location19/366"));
            else if (phrase == mila_phrase14)
                SoundEngine.PlaySound(new SoundStyle(pathToSound + "/Location19/404"));
            else if (phrase == mila_phrase15)
                SoundEngine.PlaySound(new SoundStyle(pathToSound + "/Location19/405"));
            else if (phrase == mila_phrase16)
                SoundEngine.PlaySound(new SoundStyle(pathToSound + "/Location19/410"));
            else if (phrase == mila_phrase17)
                SoundEngine.PlaySound(new SoundStyle(pathToSound + "/Location19/423+424"));
            else if (phrase == mila_phrase18)
                SoundEngine.PlaySound(new SoundStyle(pathToSound + "/Location19/493"));
            else if (phrase == mila_phrase19)
                SoundEngine.PlaySound(new SoundStyle(pathToSound + "/Location19/534"));
            else if (phrase == mila_phrase20)
                SoundEngine.PlaySound(new SoundStyle(pathToSound + "/Location19/576"));
            return phrase;
        }

        public override void AddShops()
        {
            NPCShop shop = new NPCShop(Type, "Shop");
            shop.Add(ModContent.ItemType<MilasBook>())
            .Add(ModContent.ItemType<MartialArtsManual>())
            .Add(ModContent.ItemType<ShootersManual>())
            .Add(ModContent.ItemType<PokemonManual>())
            .Add(ModContent.ItemType<MagiciansManual>())
            .Register();
        }

        public override void DrawTownAttackSwing(ref Texture2D item, ref Rectangle itemFrame, ref int itemSize, ref float scale, ref Vector2 offset)
        {
            Main.GetItemDrawFrame(ItemID.Katana, out Texture2D itemTexture, out Rectangle itemRectangle);

            item = itemTexture;
            itemFrame = itemRectangle;
            itemSize = itemRectangle.Width;
            scale = 0.15f;

            // Remember, positive Y values go down.
            if (NPC.spriteDirection == -1) // left
                offset = new Vector2(2f, 12f);
            else
                offset = new Vector2(-2f, 12f);
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                new FlavorTextBestiaryInfoElement("Mods.MitaNPC.NPCs.Mila.BestiaryDescription")
            });
        }
    }
}