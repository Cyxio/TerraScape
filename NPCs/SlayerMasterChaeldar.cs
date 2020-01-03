using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace OldSchoolRuneScape.NPCs
{
    [AutoloadHead]
    public class SlayerMasterChaeldar : ModNPC
    {
        public override string Texture
        {
            get
            {
                return "OldSchoolRuneScape/NPCs/Slayer_Chaeldar";
            }
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Slayer Master");
            Main.npcFrameCount[npc.type] = 25;
            NPCID.Sets.ExtraFramesCount[npc.type] = 9;
            NPCID.Sets.AttackFrameCount[npc.type] = 4;
            NPCID.Sets.DangerDetectRange[npc.type] = 100;
            NPCID.Sets.AttackType[npc.type] = 3;
            NPCID.Sets.AttackTime[npc.type] = 20;
            NPCID.Sets.AttackAverageChance[npc.type] = 30;
            NPCID.Sets.HatOffsetY[npc.type] = 0;
        }
        public override void SetDefaults()
        {
            npc.townNPC = true;
            npc.friendly = true;
            npc.width = 18;
            npc.height = 40;
            npc.aiStyle = 7;
            npc.damage = 45;
            npc.defense = 40;
            npc.lifeMax = 250;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.knockBackResist = 0.5f;
            animationType = NPCID.DyeTrader;
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                Gore.NewGore(npc.position, npc.velocity * Main.rand.NextFloat(0.9f, 1.1f), mod.GetGoreSlot("Gores/Chaeldar1"), npc.scale);
                Gore.NewGore(npc.position, npc.velocity * Main.rand.NextFloat(0.9f, 1.1f), mod.GetGoreSlot("Gores/Chaeldar2"), npc.scale);
                Gore.NewGore(npc.position, npc.velocity * Main.rand.NextFloat(0.9f, 1.1f), mod.GetGoreSlot("Gores/Chaeldar2"), npc.scale);
                Gore.NewGore(npc.position, npc.velocity * Main.rand.NextFloat(0.9f, 1.1f), mod.GetGoreSlot("Gores/Chaeldar3"), npc.scale);
            }
        }
        public override bool CheckConditions(int left, int right, int top, int bottom)
        {
            return true;
        }
        public override string TownNPCName()
        {
            return "Chaeldar";
        }
        public override string GetChat()
        {
            Player player = Main.player[Main.myPlayer];
            player.GetModPlayer<OSRSplayer>().SlayerTextUpdate();
            if (player.GetModPlayer<OSRSplayer>().slayTasksComplete >= 40 && OSRSworld.slayBossProgress < 4)
            {
                return "Ah, you have completed a fair amount of my tasks already. " +
                    "I suppose you are up for a greater challenge? " +
                    "Once stationary pieces of stalactite in the marble caves have been moving on their own and gathering together to form a mass of some sort. " +
                    "I have added an item to my shop that will most likely attract the being to you. " +
                    "This is the toughest challenge I can provide, so if you manage to slay it, a new master will come and take my place in this world.";
            }
            if (player.GetModPlayer<OSRSplayer>().slayerMob != 0)
            {
                if (player.GetModPlayer<OSRSplayer>().slayerLeft == 0)
                {
                    player.GetModPlayer<OSRSplayer>().SlayerReward("Chaeldar");
                    return "Well done! Here's your reward.";
                }
                else if (Main.rand.Next(2) == 0)
                {
                    int a = player.GetModPlayer<OSRSplayer>().slayerMob;
                    int am = player.GetModPlayer<OSRSplayer>().slayerLeft;
                    string name = Lang.GetNPCName(a).ToString();
                    name = OSRSplayer.FixEndings(name, am);
                    return "You're currently assigned to kill " + name + "; only " + am + " more to go.";
                }
            }
            switch (Main.rand.Next(4))
            {
                case 0:
                    return "'Ello, and what are you after then?";
                case 1:
                    return "I have a wide selection of Slayer equipment; take a look!";
                case 2:
                    return "You have currently completed " + player.GetModPlayer<OSRSplayer>().slayTasksComplete + " slayer tasks.";
                default:
                    return "I'm a Slayer Master. I train adventurers to learn the weaknesses of seemingly invulnerable monsters. To learn how, you need to kill specific monsters. I'll identify suitable targets and assign you a quota.";
            }
        }
        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = 90;
            knockback = 4f;
        }
        public override void DrawTownAttackSwing(ref Texture2D item, ref int itemSize, ref float scale, ref Vector2 offset)
        {
            item = mod.GetTexture("NPCs/ChaeldarStaff");
            scale = 1f;
            itemSize = 54;
            offset = new Vector2(0);
        }
        public override bool CanTownNPCSpawn(int numTownNPCs, int money)
        {
            return OSRSworld.slayBossProgress == 3;
        }
        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = Language.GetTextValue("LegacyInterface.28");
            button2 = "Slayer Task";
        }
        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {
            if (firstButton)
            {
                shop = true;
            }
            if (!firstButton)
            {
                OSRSplayer mp = Main.player[Main.myPlayer].GetModPlayer<OSRSplayer>();
                if (mp.slayerMob == 0)
                {
                    mp.SlayerTask("Chaeldar");
                }
                else
                {
                    string name = Lang.GetNPCName(mp.slayerMob).ToString();
                    name = OSRSplayer.FixEndings(name, mp.slayerLeft);
                    Main.npcChatText = "You're still on an assignment. You need to finish that one first. You need to kill " + mp.slayerLeft + " " + name + ".";
                }
            }
        }
        public override void SetupShop(Chest shop, ref int nextSlot)
        {
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.SlayerGem>());
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.SkipToken>());
            shop.item[nextSlot].shopCustomPrice = new int?(10);
            shop.item[nextSlot].shopSpecialCurrency = OldSchoolRuneScape.SlayerTokenCurrencyInt;
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.PotionPack>());
            shop.item[nextSlot].shopCustomPrice = new int?(5);
            shop.item[nextSlot].shopSpecialCurrency = OldSchoolRuneScape.SlayerTokenCurrencyInt;
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.BaconPack>());
            shop.item[nextSlot].shopCustomPrice = new int?(10);
            shop.item[nextSlot].shopSpecialCurrency = OldSchoolRuneScape.SlayerTokenCurrencyInt;
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.GHealthPack>());
            shop.item[nextSlot].shopCustomPrice = new int?(25);
            shop.item[nextSlot].shopSpecialCurrency = OldSchoolRuneScape.SlayerTokenCurrencyInt;
            nextSlot++;
            for (int i = 0; i < 255; i++)
            {
                if (Main.player[i].active && Main.player[i].GetModPlayer<OSRSplayer>().slayTasksComplete >= 10)
                {
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<Slayer.CrawlingSummon>());
                    nextSlot++;
                    if (Main.player[i].GetModPlayer<OSRSplayer>().slayTasksComplete >= 20)
                    {
                        shop.item[nextSlot].SetDefaults(ModContent.ItemType<Slayer.BansheeSummon>());
                        nextSlot++;
                    }
                    if (Main.player[i].GetModPlayer<OSRSplayer>().slayTasksComplete >= 30)
                    {
                        shop.item[nextSlot].SetDefaults(ModContent.ItemType<Slayer.SpectreSummon>());
                        nextSlot++;
                    }
                    if (Main.player[i].GetModPlayer<OSRSplayer>().slayTasksComplete >= 40)
                    {
                        shop.item[nextSlot].SetDefaults(ModContent.ItemType<Slayer.MarbleSummon>());
                        nextSlot++;
                    }
                    break;
                }
            }
        }
        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 15;
            randExtraCooldown = 30;
        }
        public override void TownNPCAttackSwing(ref int itemWidth, ref int itemHeight)
        {
            itemWidth = 54;
            itemHeight = 54;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Rectangle sourceRect = new Rectangle(0, 0, 18, 28);
            if (Main.GameUpdateCount % 16 < 4)
            {
                sourceRect.Y = 28;
            }
            else if (Main.GameUpdateCount % 16 < 8)
            {
                sourceRect.Y = 56;
            }
            else if (Main.GameUpdateCount % 16 < 12)
            {
                sourceRect.Y = 28;
            }
            else
            {
                sourceRect.Y = 0;
            }
            SpriteEffects s = SpriteEffects.FlipHorizontally;
            if (npc.direction == -1)
            {
                s = SpriteEffects.None;
            }
            spriteBatch.Draw(mod.GetTexture("NPCs/ChaeldarWings"), npc.position + new Vector2(-12 * npc.direction, 0) - Main.screenPosition, sourceRect, Color.White, 0f, Vector2.Zero, 1f, s, 0f);
            return base.PreDraw(spriteBatch, drawColor);
        }
    }
}