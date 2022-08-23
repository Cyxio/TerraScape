using System.Collections.Generic;
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
    public class SlayerMasterVannaka : ModNPC
    {
        public override string Texture => "OldSchoolRuneScape/NPCs/Slayer_Vannaka";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Slayer Master");
            Main.npcFrameCount[NPC.type] = 25;
            NPCID.Sets.ExtraFramesCount[NPC.type] = 9;
            NPCID.Sets.AttackFrameCount[NPC.type] = 4;
            NPCID.Sets.DangerDetectRange[NPC.type] = 100;
            NPCID.Sets.AttackType[NPC.type] = 3;
            NPCID.Sets.AttackTime[NPC.type] = 20;
            NPCID.Sets.AttackAverageChance[NPC.type] = 30;
            NPCID.Sets.HatOffsetY[NPC.type] = 0;
        }
        public override void SetDefaults()
        {
            NPC.townNPC = true;
            NPC.friendly = true;
            NPC.width = 18;
            NPC.height = 40;
            NPC.aiStyle = 7;
            NPC.damage = 30;
            NPC.defense = 30;
            NPC.lifeMax = 250;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.5f;
            AnimationType = NPCID.DyeTrader;
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                return;
            }
            if (NPC.life <= 0)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * Main.rand.NextFloat(0.9f, 1.1f), Mod.Find<ModGore>("Vannaka1").Type, NPC.scale);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * Main.rand.NextFloat(0.9f, 1.1f), Mod.Find<ModGore>("Vannaka2").Type, NPC.scale);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * Main.rand.NextFloat(0.9f, 1.1f), Mod.Find<ModGore>("Vannaka2").Type, NPC.scale);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * Main.rand.NextFloat(0.9f, 1.1f), Mod.Find<ModGore>("Vannaka3").Type, NPC.scale);
            }
        }
        public override bool CheckConditions(int left, int right, int top, int bottom)
        {
            return true;
        }
        public override List<string> SetNPCNameList()/* tModPorter Suggestion: Return a list of names */
        {
            return new List<string> { "Vannaka" };
        }
        public override string GetChat()
        {
            Player player = Main.player[Main.myPlayer];
            player.GetModPlayer<OSRSplayer>().SlayerTextUpdate();
            if (player.GetModPlayer<OSRSplayer>().slayTasksComplete >= 30 && OSRSworld.slayBossProgress < 3)
            {
                return "You seem to have cleared a lot of my tasks already, well done. " +
                    "I suppose you are up for a greater challenge? " +
                    "A rotten stench is oozing from somewhere deep in this world, the cause is still unknown. " +
                    "I have added an item to my shop that might attract the source of the smell. " +
                    "This is the toughest challenge I can provide, so if you manage to slay it, a new master will come and take my place in this world.";
            }
            if (player.GetModPlayer<OSRSplayer>().slayerMob != 0)
            {
                if (player.GetModPlayer<OSRSplayer>().slayerLeft == 0)
                {
                    player.GetModPlayer<OSRSplayer>().SlayerReward("Vannaka");
                    return "Well done! Here's your reward.";
                }
                else if (Main.rand.NextBool(2))
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
            damage = 60;
            knockback = 4f;
        }
        public override void DrawTownAttackSwing(ref Texture2D item, ref int itemSize, ref float scale, ref Vector2 offset)
        {
            item = ModContent.Request<Texture2D>("NPCs/VannakaSword").Value;
            scale = 1f;
            itemSize = 54;
            offset = new Vector2(0);
        }
        public override bool CanTownNPCSpawn(int numTownNPCs, int money)
        {
            return OSRSworld.slayBossProgress == 2;
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
                    mp.SlayerTask("Vannaka");
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
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.HealthPack>());
            shop.item[nextSlot].shopCustomPrice = new int?(5);
            shop.item[nextSlot].shopSpecialCurrency = OldSchoolRuneScape.SlayerTokenCurrencyInt;
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.BaconPack>());
            shop.item[nextSlot].shopCustomPrice = new int?(10);
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
    }
}