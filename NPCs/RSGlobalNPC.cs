using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using OldSchoolRuneScape.Items;
using OldSchoolRuneScape.Projectiles;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace OldSchoolRuneScape.NPCs
{
    public class RSGlobalNPC : GlobalNPC
    {
        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }

        public bool snared = false;
        public bool ZGSfreeze = false;

        public override void ResetEffects(NPC npc)
        {
            snared = false;
            ZGSfreeze = false;
        }

        public override void PostAI(NPC npc)
        {
            if (snared)
            {
                if (npc.velocity.Y < 0)
                {
                    npc.velocity.Y *= 0.8f;
                }
                npc.velocity.X *= 0.8f;
            }
            if (ZGSfreeze)
            {
                npc.position -= npc.velocity;
                for (int j = 0; j <= npc.height; j++) 
                {
                    if (j == 0 || j == npc.height)
                    {
                        for (int i = 0; i < npc.width; i += 2)
                        {
                            if (Main.rand.NextFloat() < 1f)
                            {
                                Dust dust;
                                Vector2 position = npc.position + new Vector2(i, j);
                                dust = Terraria.Dust.NewDustPerfect(position, 160, new Vector2(0f, 0f), 0, new Color(255, 255, 255), 1.2f);
                            }
                        }
                    }
                    else if (j % 2 == 0)
                    {
                        if (Main.rand.NextFloat() < 1f)
                        {
                            Dust dust;
                            Vector2 position = npc.position + new Vector2(0, j);
                            dust = Terraria.Dust.NewDustPerfect(position, 160, new Vector2(0f, 0f), 0, new Color(255, 255, 255), 1.2f);
                            dust = Terraria.Dust.NewDustPerfect(position + new Vector2(npc.width, 0), 160, new Vector2(0f, 0f), 0, new Color(255, 255, 255), 1.2f);
                        }
                    }
                }
            }
        }

        public override bool PreDraw(NPC npc, SpriteBatch spriteBatch, Color drawColor)
        {
            if (snared)
            {
                Texture2D draw = mod.GetTexture("Items/Magic/Entangle");
                Vector2 drawPos = npc.position - Main.screenPosition + new Vector2((npc.width / 2) - (draw.Width / 2), -draw.Height - 10);
                spriteBatch.Draw(draw, drawPos, null, new Color(180, 180, 180, 0), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
            }
            return true;
        }

        public override void NPCLoot(NPC npc)
        {
            if (!Terraria.GameContent.Events.DD2Event.Ongoing)
            {
                if (npc.lifeMax > 5 && !npc.SpawnedFromStatue)
                {
                    if (Main.rand.Next(14) == 0)
                    {
                        int drop = ModContent.ItemType<Items.Magic.aaRuneessence>();
                        if (Main.hardMode)
                        {
                            drop = ModContent.ItemType<Items.Magic.aDarkessence>();
                        }
                        if (NPC.downedBoss3)
                        {
                            drop = ModContent.ItemType<Items.Magic.abPureessence>();
                        }
                        Item.NewItem(npc.Hitbox, drop, Main.rand.Next(5, 11));
                    }
                    if (Main.rand.Next(300) == 0 && Main.player[npc.target].ZoneUnderworldHeight)
                    {
                        Item.NewItem(npc.Hitbox, ModContent.ItemType<Onyx>());
                    }
                    if (Main.rand.Next(2) == 0)
                    {
                        RuneDrops(npc);
                    }
                    if (Main.rand.Next(100) == 0 || (Main.player[npc.target].GetModPlayer<OSRSplayer>().RingofWealth && Main.rand.Next(50) == 0))
                    {
                        RareDropTable(npc);
                    }
                    if (Main.rand.Next(128) == 0 && OSRSworld.downedOlm)
                    {
                        Item.NewItem(npc.Hitbox, ModContent.ItemType<Items.ClueScroll.MasterClue>());
                    }
                    else if (Main.rand.Next(128) == 0 && NPC.downedPlantBoss)
                    {
                        Item.NewItem(npc.Hitbox, ModContent.ItemType<Items.ClueScroll.EliteClue>());
                    }
                    else if (Main.rand.Next(128) == 0 && Main.hardMode)
                    {
                        Item.NewItem(npc.Hitbox, ModContent.ItemType<Items.ClueScroll.HardClue>());
                    }
                    else if (Main.rand.Next(128) == 0 && NPC.downedBoss3)
                    {
                        Item.NewItem(npc.Hitbox, ModContent.ItemType<Items.ClueScroll.MediumClue>());
                    }
                    else if (Main.rand.Next(128) == 0)
                    {
                        Item.NewItem(npc.Hitbox, ModContent.ItemType<Items.ClueScroll.EasyClue>());
                    }
                }
                if (npc.type == NPCID.MoonLordCore)
                {
                    for (int i = 0; i < Main.ActivePlayersCount; i++)
                    {
                        ClueStep(5, 2, Main.player[i]);
                    }
                }
                if (npc.type == NPCID.WallofFlesh)
                {
                    for (int i = 0; i < Main.ActivePlayersCount; i++)
                    {
                        ClueStep(3, 5, Main.player[i]);
                    }
                }
                if (npc.type == NPCID.Golem)
                {
                    for (int i = 0; i < Main.ActivePlayersCount; i++)
                    {
                        ClueStep(4, 6, Main.player[i]);
                    }
                }
            }
        }
        private void RuneDrops(NPC npc)
        {
            if ((NPCID.Sets.Skeletons.Contains(npc.type) || Main.player[npc.target].ZoneCorrupt || Main.player[npc.target].ZoneCrimson))
            {
                int item = ModContent.ItemType<Items.Magic.Mindrune>();
                if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
                {
                    item = ModContent.ItemType<Items.Magic.Bloodrune>();
                }
                else if (Main.hardMode)
                {
                    item = ModContent.ItemType<Items.Magic.Deathrune>();
                }
                else if (NPC.downedBoss3)
                {
                    item = ModContent.ItemType<Items.Magic.Chaosrune>();
                }
                Item.NewItem(npc.Hitbox, item, Main.rand.Next(1, 6));
            }
            if (npc.type == NPCID.BabySlime || npc.type == NPCID.BlackSlime || npc.type == NPCID.BlueSlime || npc.type == NPCID.CorruptSlime || npc.type == NPCID.DungeonSlime
                || npc.type == NPCID.GreenSlime || npc.type == NPCID.IceSlime || npc.type == NPCID.RedSlime || npc.type == NPCID.YellowSlime || npc.type == NPCID.UmbrellaSlime
                || npc.type == NPCID.AnglerFish || npc.type == NPCID.FlyingFish || npc.type == NPCID.FungoFish || npc.type == NPCID.Piranha || Main.player[npc.target].ZoneBeach)
            {
                Item.NewItem(npc.Hitbox, ModContent.ItemType<Items.Magic.Waterrune>(), Main.rand.Next(1, 6));
            }
            if (Main.player[npc.target].ZoneJungle && NPC.downedBoss3)
            {
                Item.NewItem(npc.Hitbox, ModContent.ItemType<Items.Magic.Naturerune>(), Main.rand.Next(1, 6));
            }
            if (Main.player[npc.target].ZoneHoly)
            {
                if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
                {
                    Item.NewItem(npc.Hitbox, ModContent.ItemType<Items.Magic.Astralrune>(), Main.rand.Next(1, 6));
                }
                else
                {
                    Item.NewItem(npc.Hitbox, ModContent.ItemType<Items.Magic.Cosmicrune>(), Main.rand.Next(1, 6));
                }
            }
            if (Main.player[npc.target].ZoneSnow)
            {
                if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
                {
                    Item.NewItem(npc.Hitbox, ModContent.ItemType<Items.Magic.Soulrune>(), Main.rand.Next(1, 6));
                }
                else if (NPC.downedBoss3)
                {
                    Item.NewItem(npc.Hitbox, ModContent.ItemType<Items.Magic.Lawrune>(), Main.rand.Next(1, 6));
                }
                else
                {
                    Item.NewItem(npc.Hitbox, ModContent.ItemType<Items.Magic.Bodyrune>(), Main.rand.Next(1, 6));
                }
            }
            if (Main.player[npc.target].ZoneDesert)
            {
                Item.NewItem(npc.Hitbox, ModContent.ItemType<Items.Magic.Earthrune>(), Main.rand.Next(1, 6));
            }
            if (Main.player[npc.target].ZoneSkyHeight)
            {
                Item.NewItem(npc.Hitbox, ModContent.ItemType<Items.Magic.Airrune>(), Main.rand.Next(1, 6));
            }
            if (Main.player[npc.target].ZoneUnderworldHeight)
            {
                Item.NewItem(npc.Hitbox, ModContent.ItemType<Items.Magic.Firerune>(), Main.rand.Next(1, 6));
            }
        }
        private void RareDropTable(NPC npc)
        {
            int table = Main.rand.Next(4);
            int drop = 0;
            int stack = 1;
            if (table == 0) //gems
            {
                drop = Main.rand.Next(8);
                stack = 5;
                switch (drop)
                {
                    case 0:
                        drop = ItemID.Sapphire;
                        break;
                    case 1:
                        drop = ItemID.Emerald;
                        break;
                    case 2:
                        drop = ItemID.Diamond;
                        break;
                    case 3:
                        drop = ItemID.Ruby;
                        break;
                    case 4:
                        drop = ItemID.Amethyst;
                        break;
                    case 5:
                        drop = ItemID.Amber;
                        break;
                    case 6:
                        drop = ItemID.Topaz;
                        break;
                    default:
                        drop = ModContent.ItemType<Dragonstone>();
                        stack = 1;
                        break;
                }
            }
            if (table == 1) //runes
            {
                drop = Main.rand.Next(7);
                stack = Main.rand.Next(25, 31);
                switch (drop)
                {
                    case 0:
                        drop = ModContent.ItemType<Items.Magic.Airrune>();
                        break;
                    case 1:
                        drop = ModContent.ItemType<Items.Magic.Waterrune>();
                        break;
                    case 2:
                        drop = ModContent.ItemType<Items.Magic.Earthrune>();
                        break;
                    case 3:
                        drop = ModContent.ItemType<Items.Magic.Firerune>();
                        break;
                    case 4:
                        drop = ModContent.ItemType<Items.Magic.Bodyrune>();
                        break;
                    case 5:
                        drop = ModContent.ItemType<Items.Magic.Cosmicrune>();
                        break;
                    default:
                        drop = ModContent.ItemType<Items.Magic.Mindrune>();
                        break;
                }
            }
            if (table == 2) //ammo
            {
                drop = Main.rand.Next(11);
                stack = Main.rand.Next(10, 26);
                switch (drop)
                {
                    case 0:
                        drop = ItemID.WoodenArrow;
                        break;
                    case 1:
                        drop = ItemID.FlamingArrow;
                        break;
                    case 2:
                        drop = ItemID.FrostburnArrow;
                        break;
                    case 3:
                        drop = ItemID.BoneArrow;
                        break;
                    case 4:
                        drop = ItemID.HellfireArrow;
                        break;
                    case 5:
                        drop = ItemID.UnholyArrow;
                        break;
                    case 6:
                        drop = ModContent.ItemType<Items.Ammo.Diamondbolt>();
                        break;
                    case 7:
                        drop = ModContent.ItemType<Items.Ammo.Emeraldbolt>();
                        break;
                    case 8:
                        drop = ModContent.ItemType<Items.Ammo.Rubybolt>();
                        break;
                    case 9:
                        drop = ModContent.ItemType<Items.Ammo.Sapphirebolt>();
                        break;
                    default:
                        drop = ModContent.ItemType<Items.Ammo.Runebolt>();
                        break;
                }
            }
            if (table == 3) //metals
            {
                drop = Main.rand.Next(13);
                stack = Main.rand.Next(1, 8);
                switch (drop)
                {
                    case 0:
                        drop = ItemID.CopperBar;
                        break;
                    case 1:
                        drop = ItemID.CopperBar;
                        break;
                    case 2:
                        drop = ItemID.DemoniteBar;
                        break;
                    case 3:
                        drop = ItemID.CrimtaneBar;
                        break;
                    case 4:
                        drop = ItemID.HellstoneBar;
                        break;
                    case 5:
                        drop = ItemID.LeadBar;
                        break;
                    case 6:
                        drop = ItemID.IronBar;
                        break;
                    case 7:
                        drop = ItemID.PlatinumBar;
                        break;
                    case 8:
                        drop = ItemID.GoldBar;
                        break;
                    case 9:
                        drop = ItemID.SilverBar;
                        break;
                    case 10:
                        drop = ItemID.TungstenBar;
                        break;
                    case 11:
                        drop = ItemID.TinBar;
                        break;
                    default:
                        drop = ItemID.MeteoriteBar;
                        break;
                }
            }
            Item.NewItem(npc.Hitbox, drop, stack);
            for (int i = 0; i < 35; i++)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, 159, 0, 0, 0, default(Color), 1.5f);
            }
        }
        public override void GetChat(NPC npc, ref string chat)
        {
            Player player = Main.player[npc.FindClosestPlayer()];
            OSRSplayer p = player.GetModPlayer<OSRSplayer>();
            if (npc.type == NPCID.Guide && p.easyClue == 4)
            {
                player.GetModPlayer<OSRSplayer>().cluestep = 1;
            }
            if (npc.type == NPCID.Merchant && p.easyClue == 5)
            {
                player.GetModPlayer<OSRSplayer>().cluestep = 1;
            }
            if (npc.type == NPCID.Nurse && p.easyClue == 6)
            {
                player.GetModPlayer<OSRSplayer>().cluestep = 1;
            }
            if (npc.type == NPCID.Demolitionist && p.easyClue == 7)
            {
                player.GetModPlayer<OSRSplayer>().cluestep = 1;
            }
            if (npc.type == NPCID.Angler && p.easyClue == 8)
            {
                player.GetModPlayer<OSRSplayer>().cluestep = 1;
            }
            if (npc.type == NPCID.Dryad && p.mediumClue == 1)
            {
                player.GetModPlayer<OSRSplayer>().cluestep = 2;
            }
            if (npc.type == NPCID.DyeTrader && p.mediumClue == 2)
            {
                player.GetModPlayer<OSRSplayer>().cluestep = 2;
            }
            if (npc.type == NPCID.ArmsDealer && p.mediumClue == 3)
            {
                player.GetModPlayer<OSRSplayer>().cluestep = 2;
            }
            if (npc.type == NPCID.DD2Bartender && p.mediumClue == 4)
            {
                player.GetModPlayer<OSRSplayer>().cluestep = 2;
            }
            if (npc.type == NPCID.Painter && p.mediumClue == 5)
            {
                player.GetModPlayer<OSRSplayer>().cluestep = 2;
            }
            if (npc.type == NPCID.Clothier && p.mediumClue == 6)
            {
                player.GetModPlayer<OSRSplayer>().cluestep = 2;
            }
            if (npc.type == NPCID.Mechanic && p.mediumClue == 7)
            {
                player.GetModPlayer<OSRSplayer>().cluestep = 2;
            }
            if (npc.type == NPCID.GoblinTinkerer && p.mediumClue == 8)
            {
                player.GetModPlayer<OSRSplayer>().cluestep = 2;
            }
            if (npc.type == NPCID.Stylist && p.mediumClue == 9)
            {
                player.GetModPlayer<OSRSplayer>().cluestep = 2;
            }
            if (npc.type == NPCID.TravellingMerchant && p.mediumClue == 10)
            {
                player.GetModPlayer<OSRSplayer>().cluestep = 2;
            }
            if (npc.type == NPCID.WitchDoctor && p.mediumClue == 11)
            {
                player.GetModPlayer<OSRSplayer>().cluestep = 2;
            }
            if (npc.type == NPCID.Wizard && p.hardClue == 1)
            {
                player.GetModPlayer<OSRSplayer>().cluestep = 3;
            }
            if (npc.type == NPCID.PartyGirl && p.hardClue == 2)
            {
                player.GetModPlayer<OSRSplayer>().cluestep = 3;
            }
            if (npc.type == NPCID.TaxCollector && p.hardClue == 3)
            {
                player.GetModPlayer<OSRSplayer>().cluestep = 3;
            }
            if (npc.type == NPCID.Steampunker && p.hardClue == 4)
            {
                player.GetModPlayer<OSRSplayer>().cluestep = 3;
            }
            if (npc.type == NPCID.SkeletonMerchant && p.eliteClue == 1)
            {
                player.GetModPlayer<OSRSplayer>().cluestep = 4;
            }
            if (npc.type == NPCID.Cyborg && p.eliteClue == 2)
            {
                player.GetModPlayer<OSRSplayer>().cluestep = 4;
            }
            if (npc.type == NPCID.Truffle && p.eliteClue == 3)
            {
                player.GetModPlayer<OSRSplayer>().cluestep = 4;
            }
            if (npc.type == NPCID.Pirate && p.eliteClue == 4)
            {
                player.GetModPlayer<OSRSplayer>().cluestep = 4;
            }
        }

        public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
        {
            int i = Main.player[Main.myPlayer].GetModPlayer<OSRSplayer>().slayerMob;
            if (pool.ContainsKey(i) && i != 0)
            {
                pool[i] = pool[i] * 5f;
            }
        }

        private bool VariantTesting(int num, int npctype)
        {
            if (num == npctype) { return true; }
            if (num == NPCID.Zombie)
            {
                string s = Lang.GetNPCName(npctype).ToString();
                if (s.Contains("Zombie") || s.Contains("Doctor") || s.Contains("Groom") || s.Contains("Bride")) { return true; }
            }
            if (num == NPCID.DemonEye)
            {
                string s = Lang.GetNPCName(npctype).ToString();
                if (s.Contains("Demon Eye")) { return true; }
            }
            if (num == NPCID.Crimera)
            {
                string s = Lang.GetNPCName(npctype).ToString();
                if (s.Contains("Crimera")) { return true; }
            }
            if (num == NPCID.EaterofSouls)
            {
                string s = Lang.GetNPCName(npctype).ToString();
                if (s.Contains("Eater of Souls")) { return true; }
            }
            if (num == NPCID.WallCreeper)
            {
                string s = Lang.GetNPCName(npctype).ToString();
                if (s.Contains("Wall Creeper")) { return true; }
            }
            if (num == NPCID.BoneSerpentHead)
            {
                string s = Lang.GetNPCName(npctype).ToString();
                if (s.Contains("Bone Serpent")) { return true; }
            }
            if (num == NPCID.Demon)
            {
                string s = Lang.GetNPCName(npctype).ToString();
                if (s.Contains("Demon")) { return true; }
            }
            if (num == NPCID.Bee)
            {
                string s = Lang.GetNPCName(npctype).ToString();
                if (s.Contains("Bee")) { return true; }
            }
            if (num == NPCID.AngryBones)
            {
                string s = Lang.GetNPCName(npctype).ToString();
                if (s.Contains("Angry Bones")) { return true; }
            }
            if (num == NPCID.Hornet)
            {
                string s = Lang.GetNPCName(npctype).ToString();
                if (s.Contains("Hornet")) { return true; }
            }
            if (num == NPCID.TombCrawlerBody)
            {
                string s = Lang.GetNPCName(npctype).ToString();
                if (s.Contains("Tomb Crawler")) { return true; }
            }
            if (num == NPCID.Mummy)
            {
                string s = Lang.GetNPCName(npctype).ToString();
                if (s.Contains("Mummy")) { return true; }
            }
            if (num == NPCID.DesertGhoul)
            {
                string s = Lang.GetNPCName(npctype).ToString();
                if (s.Contains("Ghoul")) { return true; }
            }
            if (num == NPCID.MossHornet)
            {
                string s = Lang.GetNPCName(npctype).ToString();
                if (s.Contains("Moss Hornet")) { return true; }
            }
            if (num == NPCID.WyvernBody)
            {
                string s = Lang.GetNPCName(npctype).ToString();
                if (s.Contains("Wyvern")) { return true; }
            }
            if (num == NPCID.DuneSplicerBody)
            {
                string s = Lang.GetNPCName(npctype).ToString();
                if (s.Contains("Dune Splicer")) { return true; }
            }
            return false;
        }

        public override void OnHitByItem(NPC npc, Player player, Item item, int damage, float knockback, bool crit)
        {
            if (npc.life <= 0 && VariantTesting(player.GetModPlayer<OSRSplayer>().slayerMob, npc.type))
            {
                if (player.GetModPlayer<OSRSplayer>().slayerLeft > 0)
                {
                    player.GetModPlayer<OSRSplayer>().slayerLeft--;
                    player.GetModPlayer<OSRSplayer>().SlayerTextUpdate();
                    player.GetModPlayer<OSRSplayer>().SlayMessage();
                }
            }
            if (npc.life <= 0 && (player.GetModPlayer<OSRSplayer>().easyClue != 0 || player.GetModPlayer<OSRSplayer>().mediumClue != 0 || player.GetModPlayer<OSRSplayer>().hardClue != 0 || player.GetModPlayer<OSRSplayer>().eliteClue != 0 || player.GetModPlayer<OSRSplayer>().masterClue != 0))
            {
                if (npc.type == NPCID.BlueSlime && item.type == ItemID.WoodenSword) { ClueStep(1, 8, player); }
                if (npc.type == NPCID.Psycho && item.type == ItemID.PsychoKnife) { ClueStep(4, 25, player); }
                if (npc.type == NPCID.ManEater && item.melee) { ClueStep(1, 11, player); }
                if (npc.boss && item.type == ItemID.CopperShortsword) { ClueStep(5, 5, player); }
                if (npc.type == ModContent.NPCType<Gargoyle>() && item.type == ModContent.ItemType<Granitemaul>()) { ClueStep(2, 34, player); }
                //copies
                if (npc.type == NPCID.KingSlime) { ClueStep(1, 17, player); }
                if (npc.type == NPCID.EyeofCthulhu) { ClueStep(1, 18, player); }
                if (npc.type == NPCID.GiantWormHead || npc.type == NPCID.GiantWormBody || npc.type == NPCID.GiantWormTail) { ClueStep(1, 10, player); }
                if (npc.type == NPCID.DemonEye || npc.type == 193 || npc.type == 191 || npc.type == 192 || npc.type == 190 || npc.type == 194) { ClueStep(1, 9, player); }
                if (npc.type == NPCID.EaterofSouls || npc.type == NPCID.BigEater || npc.type == NPCID.LittleEater || npc.type == NPCID.Crimera || npc.type == NPCID.BigCrimera || npc.type == NPCID.LittleCrimera) { ClueStep(1, 14, player); }
                if (npc.type == NPCID.FlyingFish) { ClueStep(1, 15, player); }
                if (npc.type == NPCID.Vulture) { ClueStep(1, 16, player); }
                if (npc.type == NPCID.QueenBee) { ClueStep(2, 12, player); }
                if (npc.type == NPCID.Spazmatism || npc.type == NPCID.Retinazer || npc.type == NPCID.TheDestroyer || npc.type == NPCID.TheDestroyerBody || npc.type == NPCID.TheDestroyerTail || npc.type == NPCID.SkeletronPrime) { ClueStep(3, 6, player); }
                if (npc.type == NPCID.Plantera) { ClueStep(4, 5, player); }
                if (npc.type == NPCID.DukeFishron) { ClueStep(4, 7, player); }
                if (npc.type == NPCID.Pumpking) { ClueStep(4, 8, player); }
                if (npc.type == NPCID.IceQueen) { ClueStep(4, 9, player); }
                if (npc.type == NPCID.MartianSaucerCore) { ClueStep(4, 10, player); }
                if (npc.type == NPCID.DD2Betsy) { ClueStep(5, 1, player); }
                if (npc.type == NPCID.DarkCaster) { ClueStep(2, 13, player); }
                if (npc.type == NPCID.Harpy) { ClueStep(2, 14, player); }
                if (npc.type == NPCID.Demon || npc.type == NPCID.RedDevil) { ClueStep(2, 15, player); }
                if (npc.type == NPCID.Shark) { ClueStep(2, 16, player); }
                if (npc.type == NPCID.GoldfishWalker) { ClueStep(2, 17, player); }
                if (npc.type >= 87 && npc.type <= 92 && player.ZoneBeach && player.wet) { ClueStep(5, 4, player); }
                if (npc.type == NPCID.BigMimicHallow) { ClueStep(3, 7, player); }
                if (npc.type == NPCID.KingSlime && player.armor[10].type == ItemID.KingSlimeMask) { ClueStep(2, 28, player); }
                if (npc.type == NPCID.IceGolem) { ClueStep(3, 11, player); }
                if (npc.type == NPCID.SandElemental) { ClueStep(3, 12, player); }
                if (npc.type == NPCID.Mimic) { ClueStep(3, 13, player); }
                if (npc.type == NPCID.Moth) { ClueStep(3, 14, player); }
                if (npc.type == NPCID.BoneLee) { ClueStep(4, 16, player); }
                if (npc.type == NPCID.RuneWizard) { ClueStep(4, 27, player); }
                if (npc.type == NPCID.GoblinSummoner) { ClueStep(3, 20, player); }
                if (npc.type == NPCID.Butcher && player.inventory[player.selectedItem].type == ItemID.ButchersChainsaw) { ClueStep(5, 28, player); }
                if (npc.type == NPCID.DukeFishron && player.armor[10].type == ItemID.DukeFishronMask) { ClueStep(5, 29, player); }
                if (npc.type == ModContent.NPCType<Goblin>()) { ClueStep(1, 31, player); }
                if (npc.type == ModContent.NPCType<Greendragon>()) { ClueStep(2, 35, player); }
                if (npc.type == ModContent.NPCType<Bluedragon>()) { ClueStep(3, 31, player); }
                if (npc.type == ModContent.NPCType<Reddragon>()) { ClueStep(3, 32, player); }
                if (npc.type == ModContent.NPCType<Blackdragon>()) { ClueStep(3, 33, player); }
                if (npc.type == ModContent.NPCType<Abyssaldemon>()) { ClueStep(3, 34, player); }
                if (npc.type == ModContent.NPCType<Lavadragon>()) { ClueStep(4, 31, player); }
                if (npc.type == ModContent.NPCType<Smokedevil>()) { ClueStep(4, 32, player); }
                if (npc.type == ModContent.NPCType<Demonicgorilla>()) { ClueStep(4, 33, player); }
                if (npc.type == ModContent.NPCType<Olm.Olm>()) { ClueStep(5, 33, player); }
                if (npc.type == ModContent.NPCType<Elvarg.Elvarg>()) { ClueStep(2, 36, player); }
                if (npc.type == ModContent.NPCType<Chaoselemental.Chaoselemental>()) { ClueStep(3, 37, player); }
                if (npc.type == ModContent.NPCType<Barrows.Barrowsspirit>()) { ClueStep(4, 37, player); }
            }
        }
        public override void OnHitByProjectile(NPC npc, Projectile projectile, int damage, float knockback, bool crit)
        {
            Player player = Main.player[projectile.owner];
            if (npc.life <= 0 && VariantTesting(player.GetModPlayer<OSRSplayer>().slayerMob, npc.type))
            {
                if (player.GetModPlayer<OSRSplayer>().slayerLeft > 0)
                {
                    player.GetModPlayer<OSRSplayer>().slayerLeft--;
                    player.GetModPlayer<OSRSplayer>().SlayerTextUpdate();
                    player.GetModPlayer<OSRSplayer>().SlayMessage();
                }
            }
            if (npc.life <= 0 && (player.GetModPlayer<OSRSplayer>().easyClue != 0 || player.GetModPlayer<OSRSplayer>().mediumClue != 0 || player.GetModPlayer<OSRSplayer>().hardClue != 0 || player.GetModPlayer<OSRSplayer>().eliteClue != 0 || player.GetModPlayer<OSRSplayer>().masterClue != 0))
            {
                if (npc.type == NPCID.CaveBat && projectile.arrow) { ClueStep(1, 12, player); }
                if ((npc.type == NPCID.Bunny || npc.type == NPCID.PartyBunny) && projectile.magic) { ClueStep(1, 13, player); }
                if (npc.type == NPCID.Guide && projectile.arrow) { ClueStep(2, 18, player); }
                if (npc.type == NPCID.Clothier && projectile.arrow) { ClueStep(3, 9, player); }
                if (npc.type == NPCID.Clothier && projectile.type == ProjectileID.MagicDagger) { ClueStep(3, 29, player); }
                if (npc.type == NPCID.Paladin && projectile.type == ProjectileID.PaladinsHammerFriendly) { ClueStep(4, 26, player); }
                if ((npc.type >= 402 && npc.type <= 404) && (projectile.type >= 625 && projectile.type <= 628)) { ClueStep(5, 20, player); }
                if (npc.type == NPCID.VortexRifleman && player.inventory[player.selectedItem].type == ItemID.VortexBeater) { ClueStep(5, 21, player); }
                if ((npc.type >= 412 && npc.type <= 414) && projectile.type == ProjectileID.Daybreak) { ClueStep(5, 22, player); }
                if (npc.type == NPCID.NebulaBrain && (projectile.type == ProjectileID.NebulaBlaze1 || projectile.type == ProjectileID.NebulaBlaze2)) { ClueStep(5, 23, player); }
                if (npc.type == NPCID.SnowmanGangsta && projectile.type == ProjectileID.SnowBallFriendly) { ClueStep(4, 30, player); }
                if (npc.type == ModContent.NPCType<Saradominwizard>() && projectile.type == ModContent.ProjectileType<Sarastrike>()) { ClueStep(3, 35, player); }
                if (npc.type == ModContent.NPCType<Zamorakwizard>() && projectile.type == ModContent.ProjectileType<Zamorakflame>()) { ClueStep(3, 35, player); }
                if (npc.type == ModContent.NPCType<Guthixwizard>() && projectile.type == ModContent.ProjectileType<Guthixclaw>()) { ClueStep(3, 35, player); }
                //copies
                if (npc.type == NPCID.KingSlime) { ClueStep(1, 17, player); }
                if (npc.type == NPCID.EyeofCthulhu) { ClueStep(1, 18, player); }
                if (npc.type == NPCID.GiantWormHead || npc.type == NPCID.GiantWormBody || npc.type == NPCID.GiantWormTail) { ClueStep(1, 10, player); }
                if (npc.type == NPCID.DemonEye || npc.type == 193 || npc.type == 191 || npc.type == 192 || npc.type == 190 || npc.type == 194) { ClueStep(1, 9, player); }
                if (npc.type == NPCID.EaterofSouls || npc.type == NPCID.BigEater || npc.type == NPCID.LittleEater || npc.type == NPCID.Crimera || npc.type == NPCID.BigCrimera || npc.type == NPCID.LittleCrimera) { ClueStep(1, 14, player); }
                if (npc.type == NPCID.FlyingFish) { ClueStep(1, 15, player); }
                if (npc.type == NPCID.Vulture) { ClueStep(1, 16, player); }
                if (npc.type == NPCID.QueenBee) { ClueStep(2, 12, player); }
                if (npc.type == NPCID.Spazmatism || npc.type == NPCID.Retinazer || npc.type == NPCID.TheDestroyer || npc.type == NPCID.TheDestroyerBody || npc.type == NPCID.TheDestroyerTail || npc.type == NPCID.SkeletronPrime) { ClueStep(3, 6, player); }
                if (npc.type == NPCID.Plantera) { ClueStep(4, 5, player); }
                if (npc.type == NPCID.DukeFishron) { ClueStep(4, 7, player); }
                if (npc.type == NPCID.Pumpking) { ClueStep(4, 8, player); }
                if (npc.type == NPCID.IceQueen) { ClueStep(4, 9, player); }
                if (npc.type == NPCID.MartianSaucerCore) { ClueStep(4, 10, player); }
                if (npc.type == NPCID.DD2Betsy) { ClueStep(5, 1, player); }
                if (npc.type == NPCID.DarkCaster) { ClueStep(2, 13, player); }
                if (npc.type == NPCID.Harpy) { ClueStep(2, 14, player); }
                if (npc.type == NPCID.Demon || npc.type == NPCID.RedDevil) { ClueStep(2, 15, player); }
                if (npc.type == NPCID.Shark) { ClueStep(2, 16, player); }
                if (npc.type == NPCID.GoldfishWalker) { ClueStep(2, 17, player); }
                if (npc.type >= 87 && npc.type <= 92 && player.ZoneBeach && player.wet) { ClueStep(5, 4, player); }
                if (npc.type == NPCID.BigMimicHallow) { ClueStep(3, 7, player); }
                if (npc.type == NPCID.KingSlime && player.armor[10].type == ItemID.KingSlimeMask) { ClueStep(2, 28, player); }
                if (npc.type == NPCID.IceGolem) { ClueStep(3, 11, player); }
                if (npc.type == NPCID.SandElemental) { ClueStep(3, 12, player); }
                if (npc.type == NPCID.Mimic) { ClueStep(3, 13, player); }
                if (npc.type == NPCID.Moth) { ClueStep(3, 14, player); }
                if (npc.type == NPCID.BoneLee) { ClueStep(4, 16, player); }
                if (npc.type == NPCID.RuneWizard) { ClueStep(4, 27, player); }
                if (npc.type == NPCID.GoblinSummoner) { ClueStep(3, 20, player); }
                if (npc.type == NPCID.Butcher && player.inventory[player.selectedItem].type == ItemID.ButchersChainsaw) { ClueStep(5, 28, player); }
                if (npc.type == NPCID.DukeFishron && player.armor[10].type == ItemID.DukeFishronMask) { ClueStep(5, 29, player); }
                if (npc.type == ModContent.NPCType<Goblin>()) { ClueStep(1, 31, player); }
                if (npc.type == ModContent.NPCType<Greendragon>()) { ClueStep(2, 35, player); }
                if (npc.type == ModContent.NPCType<Bluedragon>()) { ClueStep(3, 31, player); }
                if (npc.type == ModContent.NPCType<Reddragon>()) { ClueStep(3, 32, player); }
                if (npc.type == ModContent.NPCType<Blackdragon>()) { ClueStep(3, 33, player); }
                if (npc.type == ModContent.NPCType<Abyssaldemon>()) { ClueStep(3, 34, player); }
                if (npc.type == ModContent.NPCType<Lavadragon>()) { ClueStep(4, 31, player); }
                if (npc.type == ModContent.NPCType<Smokedevil>()) { ClueStep(4, 32, player); }
                if (npc.type == ModContent.NPCType<Demonicgorilla>()) { ClueStep(4, 33, player); }
                if (npc.type == ModContent.NPCType<Olm.Olm>()) { ClueStep(5, 33, player); }
                if (npc.type == ModContent.NPCType<Elvarg.Elvarg>()) { ClueStep(2, 36, player); }
                if (npc.type == ModContent.NPCType<Chaoselemental.Chaoselemental>()) { ClueStep(3, 37, player); }
                if (npc.type == ModContent.NPCType<Barrows.Barrowsspirit>()) { ClueStep(4, 37, player); }
            }
        }
        private void ClueStep(int Diff, int Step, Player player)
        {
            if (Diff == 1)
            {
                if (player.GetModPlayer<OSRSplayer>().easyClue == Step)
                {
                    player.GetModPlayer<OSRSplayer>().cluestep = Diff;
                }
            }
            if (Diff == 2)
            {
                if (player.GetModPlayer<OSRSplayer>().mediumClue == Step)
                {
                    player.GetModPlayer<OSRSplayer>().cluestep = Diff;
                }
            }
            if (Diff == 3)
            {
                if (player.GetModPlayer<OSRSplayer>().hardClue == Step)
                {
                    player.GetModPlayer<OSRSplayer>().cluestep = Diff;
                }
            }
            if (Diff == 4)
            {
                if (player.GetModPlayer<OSRSplayer>().eliteClue == Step)
                {
                    player.GetModPlayer<OSRSplayer>().cluestep = Diff;
                }
            }
            if (Diff == 5)
            {
                if (player.GetModPlayer<OSRSplayer>().masterClue == Step)
                {
                    player.GetModPlayer<OSRSplayer>().cluestep = Diff;
                }
            }
        }
    }
}