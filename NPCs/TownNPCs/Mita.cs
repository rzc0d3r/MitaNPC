using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;
using Terraria.GameContent;
using Terraria.GameContent.Personalities;

using ReLogic.Content;

using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using MitaNPC.Items.Armor.Vanity;
using MitaNPC.Items.Accessories;
using MitaNPC.Items.Potions;
using MitaNPC.Items.Consumables;
using Terraria.ModLoader.IO;

namespace MitaNPC.NPCs.TownNPCs
{
    [AutoloadHead]
    public class Mita : ModNPC
    {
        public const string Shop1 = "Shop 1";
        public int MitaSkin = 1;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 23; // The total amount of frames the NPC has. You may need to change this based on how many frames your sprite sheet has.

            NPCID.Sets.ExtraFramesCount[Type] = 9; // These are the frames for raising their arm, sitting, talking, blinking, and attack. This is the remaining number of frames after the walking frames.
            NPCID.Sets.AttackFrameCount[Type] = 4; // The amount of frames in the attacking animation.
            NPCID.Sets.DangerDetectRange[Type] = 60; // The amount of pixels away from the center of the NPC that it tries to attack enemies. There are 16 pixels in 1 tile so a range of 700 is almost 44 tiles.
            NPCID.Sets.AttackType[Type] = 3; // The type of attack the Town NPC performs. 0 = throwing, 1 = shooting, 2 = magic, 3 = melee
            NPCID.Sets.AttackTime[Type] = 10; // The amount of time it takes for the NPC's attack animation to be over once it starts. Measured in ticks. There are 60 ticks per second, so an amount of 90 will take 1.5 seconds.
            NPCID.Sets.AttackAverageChance[Type] = 1; // The denominator for the chance for a Town NPC to attack. Lower numbers make the Town NPC appear more aggressive.
            //NPCID.Sets.HatOffsetY[Type] = 4; // For when a party is active, the party hat spawns at a Y offset. Adjust this number to change where on your NPC's head the party hat sits.
            NPC.Happiness
                .SetBiomeAffection<UndergroundBiome>(AffectionLevel.Love)
                .SetBiomeAffection<ForestBiome>(AffectionLevel.Like)
                .SetBiomeAffection<HallowBiome>(AffectionLevel.Dislike)
                .SetBiomeAffection<JungleBiome>(AffectionLevel.Dislike)

                .SetNPCAffection(NPCID.Angler, AffectionLevel.Love)
                .SetNPCAffection(NPCID.ArmsDealer, AffectionLevel.Like)
                .SetNPCAffection(NPCID.Painter, AffectionLevel.Like)
                .SetNPCAffection(NPCID.Demolitionist, AffectionLevel.Like)
                .SetNPCAffection(NPCID.Truffle, AffectionLevel.Dislike)

                // Hates all girls (NPC)
                .SetNPCAffection(NPCID.Nurse, AffectionLevel.Hate)
                .SetNPCAffection(NPCID.BestiaryGirl, AffectionLevel.Hate)
                .SetNPCAffection(NPCID.Dryad, AffectionLevel.Hate)
                .SetNPCAffection(NPCID.Stylist, AffectionLevel.Hate)
                .SetNPCAffection(NPCID.Mechanic, AffectionLevel.Hate)
                .SetNPCAffection(NPCID.PartyGirl, AffectionLevel.Hate)
                .SetNPCAffection(NPCID.Steampunker, AffectionLevel.Hate)
                .SetNPCAffection(NPCID.Princess, AffectionLevel.Hate)
            ;
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
            AnimationType = NPCID.Mechanic; // Sets the animation style to follow the animation of your chosen vanilla Town NPC.
        }

        public override bool CanTownNPCSpawn(int numTownNPCs)
        {
            return true;
        }

        public override string GetChat()
        {
            WeightedRandom<string> chat = new();

            string greeting1 = Language.GetTextValue("Mods.MitaNPC.NPCs.Mita.Dialogue.Greeting1", Main.LocalPlayer.name);
            string greeting2 = Language.GetTextValue("Mods.MitaNPC.NPCs.Mita.Dialogue.Greeting2");
            string nightPhrase1 = Language.GetTextValue("Mods.MitaNPC.NPCs.Mita.Dialogue.NightPhrase1");
            string nightPhrase2 = Language.GetTextValue("Mods.MitaNPC.NPCs.Mita.Dialogue.NightPhrase2");
            string nightPhrase3 = Language.GetTextValue("Mods.MitaNPC.NPCs.Mita.Dialogue.NightPhrase3");
            string nightPhrase4 = Language.GetTextValue("Mods.MitaNPC.NPCs.Mita.Dialogue.NightPhrase4");
            string nightPhrase5 = Language.GetTextValue("Mods.MitaNPC.NPCs.Mita.Dialogue.NightPhrase5");
            string nightPhrase6 = Language.GetTextValue("Mods.MitaNPC.NPCs.Mita.Dialogue.NightPhrase6");
            string nightPhrase7 = Language.GetTextValue("Mods.MitaNPC.NPCs.Mita.Dialogue.NightPhrase7");
            string youUsingDigitsInName = Language.GetTextValue("Mods.MitaNPC.NPCs.Mita.Dialogue.YouUsingDigitsInName");

            if (Main.IsItDay()) // Daytime chat
            {
                chat.Add(greeting1);
                chat.Add(greeting2);
                foreach (char ch in Main.LocalPlayer.name)
                {
                    if (Char.IsDigit(ch))
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

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = Language.GetTextValue("LegacyInterface.28"); // Shop
        }

        public override void OnChatButtonClicked(bool firstButton, ref string shopName)
        {
            if (firstButton)
                shopName = Shop1;
        }

        public override void AddShops()
        {
            NPCShop npcShop = new NPCShop(Type, Shop1);
            npcShop.Add(ModContent.ItemType<MitasCap>());
            npcShop.Add(ModContent.ItemType<TravelRing>(), [Condition.Hardmode, Condition.DownedMechBossAll]);
            npcShop.Add(ModContent.ItemType<Carrot>());
            npcShop.Add(ModContent.ItemType<MitaDefaultActivator>());
            npcShop.Add(ModContent.ItemType<MitaCappieActivator>());
            npcShop.Add(ModContent.ItemType<MitaMilaActivator>());
            npcShop.Add(new Item(ItemID.PsychoKnife) { shopCustomPrice = Item.buyPrice(2, 50, 0, 0) }, [Condition.Hardmode, Condition.BloodMoon]);
            npcShop.Register();
        }

        public override void TownNPCAttackSwing(ref int itemWidth, ref int itemHeight)
        {
            itemWidth = 30;
            itemHeight = 32;
        }

        public override void DrawTownAttackSwing(ref Texture2D item, ref Rectangle itemFrame, ref int itemSize, ref float scale, ref Vector2 offset)
        {
            // This hook takes a Texture2D instead of an int for the item. That means the weapon our Town NPC uses doesn't need to be an existing item.
            // But, that also means we need to load the texture ourselves. Luckily, GetItemDrawFrame() can do the work for us.
            // The first parameter is what you set as the item.
            // Then, there are two "out" parameters. We can use those out parameters.
            Main.GetItemDrawFrame(ItemID.PsychoKnife, out Texture2D itemTexture, out Rectangle itemRectangle);

            // Set the item texture to the item texture.
            item = itemTexture;

            // This is the source rectangle for the texture that will be drawn.
            // In this case, it is just the entire bounds of the texture because it has only one frame.
            // You could change this if your texture has multiple frames to be animated.
            itemFrame = itemRectangle;

            // Set the size of the item to the size of one of the dimensions. This will always be a square, but it doesn't matter that much.
            // itemSize is only used to determine how far into the swing animation it should be.
            itemSize = itemRectangle.Width;

            // The scale affects how far the arc of the swing is from the Town NPC.
            // This is not how large the item will be drawn on the screen.
            // A scale of 0 will draw the swing directly on the Town NPC.
            // We set it to 0.15f so it the arc is slightly in front of the Town NPC.
            scale = 0.25f;

            // offset will change the position of the item.
            // Change this to match with the location of the Town NPC's hand.
            // Remember, positive Y values go down.
            offset = new Vector2(2, 5f);
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
            knockback = 4f;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (Main.dedServ)
                return false;

            SpriteEffects sprite_effects = NPC.direction == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            Texture2D NPCTexture = TextureAssets.Npc[NPC.type].Value;

            if (MitaSkin == 2)
                NPCTexture = ModContent.Request<Texture2D>(Texture + "_Cappie", AssetRequestMode.AsyncLoad).Value;
            else if (MitaSkin == 3)
                NPCTexture = ModContent.Request<Texture2D>(Texture + "_Mila", AssetRequestMode.AsyncLoad).Value;

            spriteBatch.Draw(NPCTexture, NPC.Center - screenPos + new Vector2(0, NPC.gfxOffY) - new Vector2(0f, 6f), NPC.frame, drawColor, NPC.rotation, NPC.frame.Size() / 2, NPC.scale, sprite_effects, 0);
            return false;
        }

        public override void LoadData(TagCompound tag)
        {
            if (tag.ContainsKey("MitaSkin"))
                MitaSkin = tag.GetInt("MitaSkin");
        }

        public override void SaveData(TagCompound tag)
        {
            tag["MitaSkin"] = MitaSkin;
        }
    }
}