﻿using System.Linq;
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
    public class SlayerMasterMazchna : ModNPC
    {
        public override string Texture
        {
            get
            {
                return "OldSchoolRuneScape/NPCs/Slayer_Mazchna";
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
            npc.damage = 20;
            npc.defense = 20;
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
                Gore.NewGore(npc.position, npc.velocity * Main.rand.NextFloat(0.9f, 1.1f), mod.GetGoreSlot("Gores/Mazchna1"), npc.scale);
                Gore.NewGore(npc.position, npc.velocity * Main.rand.NextFloat(0.9f, 1.1f), mod.GetGoreSlot("Gores/Mazchna2"), npc.scale);
                Gore.NewGore(npc.position, npc.velocity * Main.rand.NextFloat(0.9f, 1.1f), mod.GetGoreSlot("Gores/Mazchna2"), npc.scale);
                Gore.NewGore(npc.position, npc.velocity * Main.rand.NextFloat(0.9f, 1.1f), mod.GetGoreSlot("Gores/Mazchna3"), npc.scale);
            }
        }
        public override bool CheckConditions(int left, int right, int top, int bottom)
        {
            return true;
        }
        public override string TownNPCName()
        {
            return "Mazchna";
        }
        public override string GetChat()
        {
            Player player = Main.player[Main.myPlayer];
            player.GetModPlayer<OSRSplayer>().SlayerTextUpdate();
            if (player.GetModPlayer<OSRSplayer>().slayTasksComplete >= 20 && OSRSworld.slayBossProgress < 2)
            {
                return "Great progress on those tasks! " +
                    "If you are up for it, I might have something more challenging for you. " +
                    "Screams have been reported echoing from the deeps, and a mysterious creature has been sighted in the night. " +
                    "I have added an item to my shop that will lure the creature to you. " +
                    "This is the toughest challenge I can provide, so if you manage to slay it, a new master will come and take my place in this world.";
            }
            if (player.GetModPlayer<OSRSplayer>().slayerMob != 0)
            {
                if (player.GetModPlayer<OSRSplayer>().slayerLeft == 0)
                {
                    player.GetModPlayer<OSRSplayer>().SlayerReward("Mazchna");
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
            damage = 40;
            knockback = 4f;
        }
        public override void DrawTownAttackSwing(ref Texture2D item, ref int itemSize, ref float scale, ref Vector2 offset)
        {
            item = mod.GetTexture("NPCs/MazchnaSword");
            scale = 1f;
            itemSize = 54;
            offset = new Vector2(0);
        }
        public override bool CanTownNPCSpawn(int numTownNPCs, int money)
        {
            return OSRSworld.slayBossProgress == 1;
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
                    mp.SlayerTask("Mazchna");
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