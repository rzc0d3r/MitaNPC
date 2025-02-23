using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.Utilities;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Personalities;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria.GameContent.Bestiary;
using MitaNPC.Items.Pets;
using MitaNPC.Items.Potions;

using MitaNPC.NPCs.TownNPCs.Mitas.Mita;

namespace MitaNPC.NPCs.TownNPCs
{
    [AutoloadHead]
    public class MiSidePlayer : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 25; // The total amount of frames the NPC has. You may need to change this based on how many frames your sprite sheet has.

            NPCID.Sets.ExtraFramesCount[Type] = 9; // These are the frames for raising their arm, sitting, talking, blinking, and attack. This is the remaining number of frames after the walking frames.
            NPCID.Sets.AttackFrameCount[Type] = 4; // The amount of frames in the attacking animation.
            NPCID.Sets.DangerDetectRange[Type] = 60; // The amount of pixels away from the center of the NPC that it tries to attack enemies. There are 16 pixels in 1 tile so a range of 700 is almost 44 tiles.
            NPCID.Sets.AttackType[Type] = 3; // The type of attack the Town NPC performs. 0 = throwing, 1 = shooting, 2 = magic, 3 = melee
            NPCID.Sets.AttackTime[Type] = 10; // The amount of time it takes for the NPC's attack animation to be over once it starts. Measured in ticks. There are 60 ticks per second, so an amount of 90 will take 1.5 seconds.
            NPCID.Sets.AttackAverageChance[Type] = 1; // The denominator for the chance for a Town NPC to attack. Lower numbers make the Town NPC appear more aggressive.
                                                      //NPCID.Sets.HatOffsetY[Type] = 4; // For when a party is active, the party hat spawns at a Y offset. Adjust this number to change where on your NPC's head the party hat sits.

            NPC.Happiness
                .SetBiomeAffection<ForestBiome>(AffectionLevel.Love)
                .SetBiomeAffection<HallowBiome>(AffectionLevel.Like)
                .SetBiomeAffection<DesertBiome>(AffectionLevel.Dislike)
                .SetBiomeAffection<UndergroundBiome>(AffectionLevel.Dislike)

                .SetNPCAffection(ModContent.NPCType<Mita>(), AffectionLevel.Love)

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
            NPC.aiStyle = NPCAIStyleID.Passive; // Copies the AI of passive NPCs. This is AI Style 7.
            NPC.damage = 10; // This is the amount of damage the NPC will deal as contact damage. This is NOT the damage dealt by the Town NPC's attack.
            NPC.defense = 15; // All vanilla Town NPCs have a base defense of 15. This will increases as more bosses are defeated.
            NPC.lifeMax = 250; // All vanilla Town NPCs have 250 HP.
            NPC.HitSound = SoundID.NPCHit1; // The sound that is played when the NPC takes damage.
            NPC.DeathSound = SoundID.NPCDeath1; // The sound that is played with the NPC dies.
            NPC.knockBackResist = 0.5f; // All vanilla Town NPCs have 50% knockback resistance. Think of this more as knockback susceptibility. 1f = 100% knockback taken, 0f = 0% knockback taken.
            AnimationType = NPCID.Guide; // Sets the animation style to follow the animation of your chosen vanilla Town NPC.
        }

        public override bool CanTownNPCSpawn(int numTownNPCs)
        {
            return true;
        }

        public override string GetChat()
        {
            WeightedRandom<string> chat = new();

            string phrase1 = Language.GetTextValue("Mods.MitaNPC.NPCs.MiSidePlayer.Phrase1");
            string phrase2 = Language.GetTextValue("Mods.MitaNPC.NPCs.MiSidePlayer.Phrase2");
            string phrase3 = Language.GetTextValue("Mods.MitaNPC.NPCs.MiSidePlayer.Phrase3");
            string phrase4 = Language.GetTextValue("Mods.MitaNPC.NPCs.MiSidePlayer.Phrase4");
            string phrase5 = Language.GetTextValue("Mods.MitaNPC.NPCs.MiSidePlayer.Phrase5");
            string phrase6 = Language.GetTextValue("Mods.MitaNPC.NPCs.MiSidePlayer.Phrase6");
            string phrase7 = Language.GetTextValue("Mods.MitaNPC.NPCs.MiSidePlayer.Phrase7");
            string phrase8 = Language.GetTextValue("Mods.MitaNPC.NPCs.MiSidePlayer.Phrase8");
            string phrase9 = Language.GetTextValue("Mods.MitaNPC.NPCs.MiSidePlayer.Phrase9");
            string phrase10 = Language.GetTextValue("Mods.MitaNPC.NPCs.MiSidePlayer.Phrase10");
            string phrase11 = Language.GetTextValue("Mods.MitaNPC.NPCs.MiSidePlayer.Phrase11");
            string phrase12 = Language.GetTextValue("Mods.MitaNPC.NPCs.MiSidePlayer.Phrase12");
            string phrase13 = Language.GetTextValue("Mods.MitaNPC.NPCs.MiSidePlayer.Phrase13");
            string phrase14 = Language.GetTextValue("Mods.MitaNPC.NPCs.MiSidePlayer.Phrase14");

            chat.Add(phrase1);
            chat.Add(phrase2);
            chat.Add(phrase3);
            chat.Add(phrase4);
            chat.Add(phrase5);
            chat.Add(phrase6);
            chat.Add(phrase7);
            chat.Add(phrase8);
            chat.Add(phrase9);
            chat.Add(phrase10);
            chat.Add(phrase11);
            chat.Add(phrase12);
            chat.Add(phrase13);
            chat.Add(phrase14);

            string pathToSound = "";
            if (Language.ActiveCulture.Name == "ru-RU")
                pathToSound = "MitaNPC/Sounds/MiSidePlayer/Russian";

            string phrase = chat.Get();

            if (Language.ActiveCulture.Name == "ru-RU")
            {
                if (phrase == phrase1)
                    SoundEngine.PlaySound(new SoundStyle(pathToSound + "/Location1/84"));
                else if (phrase == phrase2)
                    SoundEngine.PlaySound(new SoundStyle(pathToSound + "/Location2/2"));
                else if (phrase == phrase3)
                    SoundEngine.PlaySound(new SoundStyle(pathToSound + "/Location3/70+71"));
                else if (phrase == phrase4)
                    SoundEngine.PlaySound(new SoundStyle(pathToSound + "/Location3/191+192"));
                else if (phrase == phrase5)
                    SoundEngine.PlaySound(new SoundStyle(pathToSound + "/Location3/364+365+366"));
                else if (phrase == phrase6)
                    SoundEngine.PlaySound(new SoundStyle(pathToSound + "/Location4/18"));
                else if (phrase == phrase7)
                    SoundEngine.PlaySound(new SoundStyle(pathToSound + "/Location6/26"));
                else if (phrase == phrase8)
                    SoundEngine.PlaySound(new SoundStyle(pathToSound + "/Location7/4"));
                else if (phrase == phrase9)
                    SoundEngine.PlaySound(new SoundStyle(pathToSound + "/Location7/70"));
                else if (phrase == phrase10)
                    SoundEngine.PlaySound(new SoundStyle(pathToSound + "/Location7/90"));
                else if (phrase == phrase11)
                    SoundEngine.PlaySound(new SoundStyle(pathToSound + "/Location7/296"));
                else if (phrase == phrase12)
                    SoundEngine.PlaySound(new SoundStyle(pathToSound + "/Location7/438"));
                else if (phrase == phrase13)
                    SoundEngine.PlaySound(new SoundStyle(pathToSound + "/Location7/616"));
                else if (phrase == phrase14)
                    SoundEngine.PlaySound(new SoundStyle(pathToSound + "/Location7/773"));
            }

            return phrase;
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = Language.GetTextValue("LegacyInterface.28"); // Shop
        }

        public override void OnChatButtonClicked(bool firstButton, ref string shopName)
        {
            if (firstButton)
                shopName = "Shop";
        }

        public override void AddShops()
        {
            NPCShop shop = new NPCShop(Type, "Shop");
            shop.Add(ModContent.ItemType<GameConsole>())
            .Add(ModContent.ItemType<Ramen>())
            .Add(ModContent.ItemType<AlarmClock>())
            .Register();
        }

        public override void TownNPCAttackSwing(ref int itemWidth, ref int itemHeight)
        {
            itemWidth = 12;
            itemHeight = 28;
        }

        public override void DrawTownAttackSwing(ref Texture2D item, ref Rectangle itemFrame, ref int itemSize, ref float scale, ref Vector2 offset)
        {
            Main.GetItemDrawFrame(ItemID.Ruler, out Texture2D itemTexture, out Rectangle itemRectangle);

            item = itemTexture;
            itemFrame = itemRectangle;
            itemSize = itemRectangle.Width;
            scale = 1f;

            // Remember, positive Y values go down.
            if (NPC.spriteDirection == -1)
                offset = new Vector2(-2f, 0);
            else
                offset = new Vector2(2f, 0);
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
            knockback = 2f;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                new FlavorTextBestiaryInfoElement("Mods.MitaNPC.NPCs.MiSidePlayer.BestiaryDescription")
            });
        }
    }
}