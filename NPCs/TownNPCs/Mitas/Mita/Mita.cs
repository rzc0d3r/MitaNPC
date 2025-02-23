using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.Utilities;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.Personalities;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MitaNPC.Items.Potions;
using MitaNPC.Items.Accessories;
using MitaNPC.Items.Weapons.Melee;

namespace MitaNPC.NPCs.TownNPCs.Mitas.Mita
{
    public abstract class MitaBase : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 23; // The total amount of frames the NPC has. You may need to change this based on how many frames your sprite sheet has.

            NPCID.Sets.ExtraFramesCount[Type] = 9; // These are the frames for raising their arm, sitting, talking, blinking, and attack. This is the remaining number of frames after the walking frames.
            NPCID.Sets.AttackFrameCount[Type] = 4; // The amount of frames in the attacking animation.
            NPCID.Sets.DangerDetectRange[Type] = 90; // The amount of pixels away from the center of the NPC that it tries to attack enemies. There are 16 pixels in 1 tile so a range of 700 is almost 44 tiles.
            NPCID.Sets.AttackType[Type] = 3; // The type of attack the Town NPC performs. 0 = throwing, 1 = shooting, 2 = magic, 3 = melee
            NPCID.Sets.AttackTime[Type] = 15; // The amount of time it takes for the NPC's attack animation to be over once it starts. Measured in ticks. There are 60 ticks per second, so an amount of 90 will take 1.5 seconds.
            NPCID.Sets.AttackAverageChance[Type] = 1; // The denominator for the chance for a Town NPC to attack. Lower numbers make the Town NPC appear more aggressive.
                                                      //NPCID.Sets.HatOffsetY[Type] = 4; // For when a party is active, the party hat spawns at a Y offset. Adjust this number to change where on your NPC's head the party hat sits.

            NPC.Happiness
                .SetBiomeAffection<ForestBiome>(AffectionLevel.Love)
                .SetBiomeAffection<UndergroundBiome>(AffectionLevel.Like)
                .SetBiomeAffection<HallowBiome>(AffectionLevel.Dislike)
                .SetBiomeAffection<JungleBiome>(AffectionLevel.Dislike)

                .SetNPCAffection(ModContent.NPCType<MiSidePlayer>(), AffectionLevel.Love)
                //.SetNPCAffection(NPCID.ArmsDealer, AffectionLevel.Like)
                //.SetNPCAffection(NPCID.Painter, AffectionLevel.Like)
                //.SetNPCAffection(NPCID.Demolitionist, AffectionLevel.Like)
                //.SetNPCAffection(NPCID.Truffle, AffectionLevel.Dislike)

                // Hates all girls (NPC)
                .SetNPCAffection(NPCID.Nurse, AffectionLevel.Hate)
                .SetNPCAffection(NPCID.BestiaryGirl, AffectionLevel.Hate)
                .SetNPCAffection(NPCID.Dryad, AffectionLevel.Hate)
                .SetNPCAffection(NPCID.Stylist, AffectionLevel.Hate)
                .SetNPCAffection(NPCID.Mechanic, AffectionLevel.Hate)
                .SetNPCAffection(NPCID.PartyGirl, AffectionLevel.Hate)
                .SetNPCAffection(NPCID.Steampunker, AffectionLevel.Hate)
                .SetNPCAffection(NPCID.Princess, AffectionLevel.Hate);

            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Velocity = 0.55f,
                Direction = -1 // left
            };

            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
        }

        public override void SetDefaults()
        {
            NPC.townNPC = true; // Sets NPC to be a Town NPC
            NPC.friendly = true; // The NPC will not attack player
            NPC.width = 18; // The width of the hitbox (hurtbox)
            NPC.height = 40; // The height of the hitbox (hurtbox)
            NPC.aiStyle = NPCAIStyleID.Passive;
            NPC.damage = 10; // This is the amount of damage the NPC will deal as contact damage. This is NOT the damage dealt by the Town NPC's attack.
            NPC.defense = 15; // All vanilla Town NPCs have a base defense of 15. This will increases as more bosses are defeated.
            NPC.lifeMax = 250; // All vanilla Town NPCs have 250 HP.
            NPC.HitSound = SoundID.NPCHit1; // The sound that is played when the NPC takes damage.
            NPC.DeathSound = SoundID.NPCDeath1; // The sound that is played with the NPC dies.
            NPC.knockBackResist = 0.5f; // All vanilla Town NPCs have 50% knockback resistance.
            AnimationType = NPCID.Mechanic; // Sets the animation style to follow the animation of your chosen vanilla Town NPC.
        }

        public override bool CanTownNPCSpawn(int numTownNPCs) => true;

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = Language.GetTextValue("LegacyInterface.28"); // Shop
        }

        public override void TownNPCAttackSwing(ref int itemWidth, ref int itemHeight)
        {
            itemWidth = 30;
            itemHeight = 32;
        }

        public override void DrawTownAttackSwing(ref Texture2D item, ref Rectangle itemFrame, ref int itemSize, ref float scale, ref Vector2 offset)
        {
            Main.GetItemDrawFrame(ItemID.PsychoKnife, out Texture2D itemTexture, out Rectangle itemRectangle);

            item = itemTexture;
            itemFrame = itemRectangle;
            itemSize = itemRectangle.Width;
            scale = 0.15f;

            // Remember, positive Y values go down.
            if (NPC.spriteDirection == -1) // left
                offset = new Vector2(-3f, 11f);
            else
                offset = new Vector2(3f, 11f);
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            // How long, in ticks, the Town NPC must wait before they can attack again.
            // The actual length will be: cooldown <= length < (cooldown + randExtraCooldown)
            cooldown = 24;
            randExtraCooldown = 6;
        }

        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = 30;
            knockback = 5f;
        }

        public override void OnChatButtonClicked(bool firstButton, ref string shopName)
        {
            if (firstButton)
                shopName = "Shop";
        }
    }

    [AutoloadHead]
    public class Mita : MitaBase
    {
        public override string GetChat()
        {
            WeightedRandom<string> chat = new();

            string greeting1 = Language.GetTextValue("Mods.MitaNPC.NPCs.Mita.StandartMitaDialogue.Greeting1", Main.LocalPlayer.name);
            string greeting2 = Language.GetTextValue("Mods.MitaNPC.NPCs.Mita.StandartMitaDialogue.Greeting2");
            string nightPhrase1 = Language.GetTextValue("Mods.MitaNPC.NPCs.Mita.StandartMitaDialogue.NightPhrase1");
            string nightPhrase2 = Language.GetTextValue("Mods.MitaNPC.NPCs.Mita.StandartMitaDialogue.NightPhrase2");
            string nightPhrase3 = Language.GetTextValue("Mods.MitaNPC.NPCs.Mita.StandartMitaDialogue.NightPhrase3");
            string nightPhrase4 = Language.GetTextValue("Mods.MitaNPC.NPCs.Mita.StandartMitaDialogue.NightPhrase4");
            string nightPhrase5 = Language.GetTextValue("Mods.MitaNPC.NPCs.Mita.StandartMitaDialogue.NightPhrase5");
            string nightPhrase6 = Language.GetTextValue("Mods.MitaNPC.NPCs.Mita.StandartMitaDialogue.NightPhrase6");
            string nightPhrase7 = Language.GetTextValue("Mods.MitaNPC.NPCs.Mita.StandartMitaDialogue.NightPhrase7");
            string youUsingDigitsInName = Language.GetTextValue("Mods.MitaNPC.NPCs.Mita.StandartMitaDialogue.YouUsingDigitsInName");

            if (Main.IsItDay()) // Daytime chat
            {
                chat.Add(greeting1);
                chat.Add(greeting2);
                foreach (char ch in Main.LocalPlayer.name)
                {
                    if (char.IsDigit(ch))
                    {
                        chat.Add(youUsingDigitsInName);
                        break;
                    }
                }
            }
            else // Night-time chat
            {
                chat.Add(nightPhrase1);
                chat.Add(nightPhrase2);
                chat.Add(nightPhrase3);
                chat.Add(nightPhrase4);
                chat.Add(nightPhrase5);
                chat.Add(nightPhrase6);
                chat.Add(nightPhrase7);
            }

            string pathToSound = "MitaNPC/Sounds/Mita/";
            if (Language.ActiveCulture.Name == "ru-RU")
                pathToSound += "Russian";
            else
                pathToSound += "Japanese";

            string phrase = chat.Get();
            if (phrase == greeting1)
                SoundEngine.PlaySound(new SoundStyle(pathToSound + "/Location1/12"));
            else if (phrase == greeting2)
                SoundEngine.PlaySound(new SoundStyle(pathToSound + "/Location1/10"));
            else if (phrase == youUsingDigitsInName)
                SoundEngine.PlaySound(new SoundStyle(pathToSound + "/Location1/117"));
            else if (phrase == nightPhrase1)
                SoundEngine.PlaySound(new SoundStyle(pathToSound + "/Location17/18"));
            else if (phrase == nightPhrase2)
                SoundEngine.PlaySound(new SoundStyle(pathToSound + "/Location17/23"));
            else if (phrase == nightPhrase3)
                SoundEngine.PlaySound(new SoundStyle(pathToSound + "/Location17/74"));
            else if (phrase == nightPhrase4)
                SoundEngine.PlaySound(new SoundStyle(pathToSound + "/Location17/109"));
            else if (phrase == nightPhrase5)
                SoundEngine.PlaySound(new SoundStyle(pathToSound + "/Location17/127"));
            else if (phrase == nightPhrase6)
                SoundEngine.PlaySound(new SoundStyle(pathToSound + "/Location17/154"));
            else if (phrase == nightPhrase7)
                SoundEngine.PlaySound(new SoundStyle(pathToSound + "/Location17/157"));
            return phrase;
        }

        public override void AddShops()
        {
            NPCShop shop = new NPCShop(Type, "Shop");
            shop.Add(ModContent.ItemType<GameCartridge>())
            .Add(ModContent.ItemType<Carrot>())
            .Add(ModContent.ItemType<TravelRing>(), Condition.Hardmode, Condition.DownedMechBossAll)
            .Add(new Item(ItemID.PsychoKnife) { shopCustomPrice = Item.buyPrice(platinum: 2, gold: 50) }, Condition.Hardmode, Condition.BloodMoon)
            .Register();
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                new FlavorTextBestiaryInfoElement("Mods.MitaNPC.NPCs.Mita.BestiaryDescription")
            });
        }
    }
}