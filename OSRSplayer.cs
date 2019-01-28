using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace OldSchoolRuneScape
{
    public class OSRSplayer : ModPlayer
    {
        public bool SpecCD = false;
        public bool Clawbuff = false;
        public bool Boltenchant = false;
        public bool Dharokset = false;
        public bool Guthanset = false;
        public bool Veracset = false;
        public bool Toragset = false;
        public bool Ahrimset = false;
        public bool Karilset = false;
        public bool Amuletdamned = false;
        public bool RingofWealth = false;
        public bool TomeFire = false;
        public bool snared = false;
        public bool GodCharge = false;
        public bool Vengeance = false;
        public bool Tormentedbracelet = false;
        public bool Amulettorture = false;
        public int TortureTime = 0;         
        public bool Necklaceanguish = false;
        public int AnguishTime = 0;
        public int MessageTime = 0;
        public ModDust MessageDust;        

        public override void UpdateLifeRegen()
        {
            if (player.bleed)
            {
                player.lifeRegen = 0;
            }
        }

        public override void ResetEffects()
        {
            SpecCD = false;
            Clawbuff = false;
            Boltenchant = false;
            Dharokset = false;
            Guthanset = false;
            Veracset = false;
            Toragset = false;
            Ahrimset = false;
            Karilset = false;
            Amuletdamned = false;
            RingofWealth = false;
            TomeFire = false;
            GodCharge = false;
            Vengeance = false;
            Tormentedbracelet = false;
            Amulettorture = false;           
            Necklaceanguish = false;
            RingCoins = false;
            RingNature = false;
            BloodHound = false;
        }

        public override void PostUpdate()
        {
            if (TortureTime > 0)
            {
                TortureTime--;
            }
            if (MessageTime > 0)
            {
                Dust.NewDust(new Vector2(player.Center.X - MessageDust.Texture.Width * 0.375f, player.position.Y - 40), 0, 0, MessageDust.Type);
                MessageTime--;
            }
        }

        public override void OnHitByNPC(NPC npc, int damage, bool crit)
        {
            if (AnguishTime > 0)
            {
                AnguishTime = 0;
            }
            if (Amulettorture && TortureTime == 0 && player.statLife < 150)
            {
                TortureTime = 18000;
                player.HealEffect(player.statLifeMax2 - player.statLife);
                player.statLife = player.statLifeMax2;
                player.AddBuff(BuffID.Wrath, 18000);
                player.AddBuff(BuffID.Rage, 18000);
            }
            if (Vengeance)
            {
                MessageTime = 90;
                MessageDust = mod.GetDust<Dusts.VengMessage>();
                Dust.NewDust(new Vector2(player.Center.X - 86, player.position.Y - 40), 0, 0, mod.DustType<Dusts.VengMessage>());
                player.ApplyDamageToNPC(npc, damage * 10, 0f, 0, false);
                npc.netUpdate = true;
                Vengeance = false;
                player.ClearBuff(mod.BuffType<Buffs.Vengeance>());
                for (int o = 0; o < 36; o++)
                {
                    Vector2 rotate = new Vector2(2).RotatedBy(MathHelper.ToRadians(10 * o));
                    int dust = Dust.NewDust(player.Center, 0, 0, 90);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].scale = 1f;
                    Main.dust[dust].velocity = rotate;
                }
            }
        }

        public override void OnHitPvp(Item item, Player target, int damage, bool crit)
        {
            if (target.GetModPlayer<OSRSplayer>().Vengeance)
            {
                player.Hurt(PlayerDeathReason.ByCustomReason(player.name + " got killed by vengeance lmao"), damage * 10, 0, true);
                target.GetModPlayer<OSRSplayer>().Vengeance = false;
                target.ClearBuff(mod.BuffType<Buffs.Vengeance>());
                target.GetModPlayer<OSRSplayer>().MessageDust = mod.GetDust<Dusts.VengMessage>();
                target.GetModPlayer<OSRSplayer>().MessageTime = 90;
                for (int o = 0; o < 36; o++)
                {
                    Vector2 rotate = new Vector2(2).RotatedBy(MathHelper.ToRadians(10 * o));
                    int dust = Dust.NewDust(player.Center, 0, 0, 90);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].scale = 1f;
                    Main.dust[dust].velocity = rotate;
                }
            }
        }

        public override void OnHitPvpWithProj(Projectile proj, Player target, int damage, bool crit)
        {
            if (target.GetModPlayer<OSRSplayer>().Vengeance)
            {
                player.Hurt(PlayerDeathReason.ByCustomReason(player.name + " got killed by vengeance lmao"), damage * 10, 0, true);
                target.GetModPlayer<OSRSplayer>().Vengeance = false;
                target.ClearBuff(mod.BuffType<Buffs.Vengeance>());
                target.GetModPlayer<OSRSplayer>().MessageDust = mod.GetDust<Dusts.VengMessage>();
                target.GetModPlayer<OSRSplayer>().MessageTime = 90;
                for (int o = 0; o < 36; o++)
                {
                    Vector2 rotate = new Vector2(2).RotatedBy(MathHelper.ToRadians(10 * o));
                    int dust = Dust.NewDust(player.Center, 0, 0, 90);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].scale = 1f;
                    Main.dust[dust].velocity = rotate;
                }
            }
        }

        public override bool Shoot(Item item, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (Karilset && item.ranged && Main.rand.Next(60) < item.useTime)
            {
                Projectile.NewProjectile(position + new Vector2(speedX, speedY) * 2, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI);
            }
            return true;
        }

        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (Guthanset && item.melee && (Main.rand.Next(10) == 0 || (Amuletdamned && Main.rand.Next(7) == 0)) && target.type != NPCID.TargetDummy)
            {
                int heal = damage / 10;
                if (heal > 10 && !Amuletdamned)
                {
                    heal = 10;
                }
                if (heal > 20 && Amuletdamned)
                {
                    heal = 20;
                }
                player.HealEffect(heal);
                player.statLife += heal;
                Dust.NewDust(target.Center, 0, 0, mod.DustType<Dusts.Guthanset>());
            }
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (Tormentedbracelet && Main.rand.Next(10) == 0 && proj.magic)
            {
                float amount = player.statLifeMax2 / (float)player.statLife;
                if (amount > 6)
                {
                    amount = 6;
                }
                for (int i = 0; i < amount; i++)
                {
                    Vector2 rotate = new Vector2(target.height / 2).RotateRandom(Math.PI);
                    Vector2 spd = rotate;
                    spd.Normalize();
                    Projectile.NewProjectile(target.Center + rotate, spd * 3, mod.ProjectileType<Projectiles.Tormentedsoul>(), 75, 0f, player.whoAmI, target.whoAmI);
                }
            }
            if (proj.ranged && Necklaceanguish && AnguishTime < 48)
            {
                AnguishTime++;
            }
            if (Guthanset && proj.melee && (Main.rand.Next(20) == 0 || (Amuletdamned && Main.rand.Next(10) == 0)) && target.type != NPCID.TargetDummy)
            {
                int heal = damage / 10;
                if (heal > 10 && !Amuletdamned)
                {
                    heal = 10;
                }
                if (heal > 20 && Amuletdamned)
                {
                    heal = 20;
                }
                player.HealEffect(heal);
                player.statLife += heal;
                Dust.NewDust(target.Center, 0, 0, mod.DustType<Dusts.Guthanset>());
            }
            if (Ahrimset && proj.magic)
            {
                target.AddBuff(BuffID.ShadowFlame, 360);
            }
            if (TomeFire && proj.magic)
            {
                target.AddBuff(BuffID.OnFire, 120);
                target.AddBuff(BuffID.Frostburn, 120);
                target.AddBuff(BuffID.CursedInferno, 120);
                target.AddBuff(BuffID.ShadowFlame, 120);
            }
        }

        //clueshit goes HERE
        public bool RingCoins = false;
        public bool RingNature = false;
        public bool BloodHound = false;
        public override void ModifyDrawLayers(List<PlayerLayer> layers)
        {
            if (RingCoins || RingNature)
            {
                foreach (PlayerLayer p in layers)
                {
                    p.visible = false;
                }
                if (RingCoins)
                {
                    Main.spriteBatch.Draw(mod.GetTexture("Items/ClueScroll/ClueRewards/Master/Coinstack"),
                    new Vector2(Main.screenWidth / 2 - 15, Main.screenHeight / 2 - 2), null, Color.White, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
                }
                else if (RingNature)
                {
                    Main.spriteBatch.Draw(mod.GetTexture("Items/ClueScroll/ClueRewards/Elite/NatureBush"),
                    new Vector2(Main.screenWidth / 2 - 24, Main.screenHeight / 2 - 9), null, Color.White, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
                }
            }
        }
        public int cluestep = 0;
        public int easyClue = 0;
        public int easySteps = 0;
        public int easyStage = 0;
        public int mediumClue = 0;
        public int mediumSteps = 0;
        public int mediumStage = 0;
        public int hardClue = 0;
        public int hardSteps = 0;
        public int hardStage = 0;
        public int eliteClue = 0;
        public int eliteSteps = 0;
        public int eliteStage = 0;
        public int masterClue = 0;
        public int masterSteps = 0;
        public int masterStage = 0;
        public int completedEasy = 0;
        public int completedMedium = 0;
        public int completedHard = 0;
        public int completedElite = 0;
        public int completedMaster = 0;
        public int challengeAns = 0;
        public int challengeDiff = 0;

        public override TagCompound Save()
        {
            TagCompound tag = new TagCompound();
            tag.Set("easyClue", easyClue);
            tag.Set("easySteps", easySteps);
            tag.Set("easyStage", easyStage);
            tag.Set("mediumClue", mediumClue);
            tag.Set("mediumSteps", mediumSteps);
            tag.Set("mediumStage", mediumStage);
            tag.Set("hardClue", hardClue);
            tag.Set("hardSteps", hardSteps);
            tag.Set("hardStage", hardStage);
            tag.Set("eliteClue", eliteClue);
            tag.Set("eliteSteps", eliteSteps);
            tag.Set("eliteStage", eliteStage);
            tag.Set("masterClue", masterClue);
            tag.Set("masterSteps", masterSteps);
            tag.Set("masterStage", masterStage);
            tag.Set("completedEasy", completedEasy);
            tag.Set("completedMedium", completedMedium);
            tag.Set("completedHard", completedHard);
            tag.Set("completedElite", completedElite);
            tag.Set("completedMaster", completedMaster);
            tag.Set("challengeAns", challengeAns);
            tag.Set("challengeDiff", challengeDiff);
            return tag;
        }
        public override void Load(TagCompound tag)
        {
            easyClue = tag.GetInt("easyClue");
            easySteps = tag.GetInt("easySteps");
            easyStage = tag.GetInt("easyStage");
            mediumClue = tag.GetInt("mediumClue");
            mediumSteps = tag.GetInt("mediumSteps");
            mediumStage = tag.GetInt("mediumStage");
            hardClue = tag.GetInt("hardClue");
            hardSteps = tag.GetInt("hardSteps");
            hardStage = tag.GetInt("hardStage");
            eliteClue = tag.GetInt("eliteClue");
            eliteSteps = tag.GetInt("eliteSteps");
            eliteStage = tag.GetInt("eliteStage");
            masterClue = tag.GetInt("masterClue");
            masterSteps = tag.GetInt("masterSteps");
            masterStage = tag.GetInt("masterStage");
            completedEasy = tag.GetInt("completedEasy");
            completedMedium = tag.GetInt("completedMedium");
            completedHard = tag.GetInt("completedHard");
            completedElite = tag.GetInt("completedElite");
            completedMaster = tag.GetInt("completedMaster");
            challengeAns = tag.GetInt("challengeAns");
            challengeDiff = tag.GetInt("challengeDiff");
        }

        public void ClueAnswer(Player player)
        {
            player.GetModPlayer<OSRSplayer>().cluestep = challengeDiff;
            player.GetModPlayer<OSRSplayer>().challengeDiff = 0;
            player.GetModPlayer<OSRSplayer>().challengeAns = 0;
        }
        public override void AnglerQuestReward(float rareMultiplier, List<Item> rewardItems)
        {
            if (player.GetModPlayer<OSRSplayer>().mediumClue == 29)
            {
                player.GetModPlayer<OSRSplayer>().cluestep = 2;
            }
        }
        public void CompleteClue(int Diff, Player player)
        {
            if (Diff == 1)
            {
                player.GetModPlayer<OSRSplayer>().completedEasy++;
            }
            if (Diff == 2)
            {
                player.GetModPlayer<OSRSplayer>().completedMedium++;
            }
            if (Diff == 3)
            {
                player.GetModPlayer<OSRSplayer>().completedHard++;
            }
            if (Diff == 4)
            {
                player.GetModPlayer<OSRSplayer>().completedElite++;
            }
            if (Diff == 5)
            {
                player.GetModPlayer<OSRSplayer>().completedMaster++;
            }
        }
        public void ClueReset(int type, Player player)
        {
            if (type == mod.ItemType<Items.ClueScroll.EasyClue>())
            {
                player.GetModPlayer<OSRSplayer>().easyClue = 0;
                player.GetModPlayer<OSRSplayer>().easySteps = 0;
                player.GetModPlayer<OSRSplayer>().easyStage = 0;
            }
            if (type == mod.ItemType<Items.ClueScroll.MediumClue>())
            {
                player.GetModPlayer<OSRSplayer>().mediumClue = 0;
                player.GetModPlayer<OSRSplayer>().mediumSteps = 0;
                player.GetModPlayer<OSRSplayer>().mediumStage = 0;
            }
            if (type == mod.ItemType<Items.ClueScroll.HardClue>())
            {
                player.GetModPlayer<OSRSplayer>().hardClue = 0;
                player.GetModPlayer<OSRSplayer>().hardSteps = 0;
                player.GetModPlayer<OSRSplayer>().hardStage = 0;
            }
            if (type == mod.ItemType<Items.ClueScroll.EliteClue>())
            {
                player.GetModPlayer<OSRSplayer>().eliteClue = 0;
                player.GetModPlayer<OSRSplayer>().eliteSteps = 0;
                player.GetModPlayer<OSRSplayer>().eliteStage = 0;
            }
            if (type == mod.ItemType<Items.ClueScroll.MasterClue>())
            {
                player.GetModPlayer<OSRSplayer>().masterClue = 0;
                player.GetModPlayer<OSRSplayer>().masterSteps = 0;
                player.GetModPlayer<OSRSplayer>().masterStage = 0;
            }
        }
        public override void PreUpdate()
        {
            if (player.talkNPC != 1 || player.chest != 1 || player.trashItem.width == 62)
            {
                int[] clues = { mod.ItemType<Items.ClueScroll.EasyClue>(), mod.ItemType<Items.ClueScroll.MediumClue>(), mod.ItemType<Items.ClueScroll.HardClue>(), mod.ItemType<Items.ClueScroll.EliteClue>(), mod.ItemType<Items.ClueScroll.MasterClue>() };
                List<int> cluess = new List<int>(clues);
                if (cluess.Contains(player.trashItem.type))
                {
                    ClueReset(player.trashItem.type, player);
                    player.trashItem.TurnToAir();
                }
                if (Main.npcShop != 0)
                {
                    for (int i = 0; i < 40; i++)
                    {
                        if (cluess.Contains(Main.instance.shop[Main.npcShop].item[i].type))
                        {
                            ClueReset(Main.instance.shop[Main.npcShop].item[i].type, player);
                            Main.instance.shop[Main.npcShop].item[i].TurnToAir();
                        }
                    }
                }
                if (player.chest > -1)
                {
                    for (int i = 0; i < 40; i++)
                    {
                        if (cluess.Contains(Main.chest[player.chest].item[i].type))
                        {
                            ClueReset(Main.chest[player.chest].item[i].type, player);
                            Main.chest[player.chest].item[i].TurnToAir();
                        }
                    }
                }
                if (player.chest == -2)
                {
                    for (int i = 0; i < 40; i++)
                    {
                        if (cluess.Contains(player.bank.item[i].type))
                        {
                            ClueReset(player.bank.item[i].type, player);
                            player.bank.item[i].TurnToAir();
                        }
                    }
                }
                if (player.chest == -3)
                {
                    for (int i = 0; i < 40; i++)
                    {
                        if (cluess.Contains(player.bank2.item[i].type))
                        {
                            ClueReset(player.bank2.item[i].type, player);
                            player.bank2.item[i].TurnToAir();
                        }
                    }
                }
                if (player.chest == -4)
                {
                    for (int i = 0; i < 40; i++)
                    {
                        if (cluess.Contains(player.bank3.item[i].type))
                        {
                            ClueReset(player.bank3.item[i].type, player);
                            player.bank3.item[i].TurnToAir();
                        }
                    }
                }
            }
        }
        public void ClueDig(int blockType)
        {
            if (player.GetModPlayer<OSRSplayer>().easyClue == 1 && blockType == TileID.Grass)
            {
                cluestep = 1;
            }
            if (player.GetModPlayer<OSRSplayer>().easyClue == 2 && blockType == TileID.IceBlock && player.ZoneSnow)
            {
                cluestep = 1;
            }
            if (player.GetModPlayer<OSRSplayer>().easyClue == 3 && blockType == TileID.Sand && player.ZoneDesert)
            {
                cluestep = 1;
            }
            if (player.GetModPlayer<OSRSplayer>().mediumClue == 19 && blockType == TileID.Sunplate && player.ZoneSkyHeight)
            {
                cluestep = 2;
            }
            if (player.GetModPlayer<OSRSplayer>().mediumClue == 20 && (blockType == TileID.BlueDungeonBrick || blockType == TileID.GreenDungeonBrick || blockType == TileID.PinkDungeonBrick))
            {
                cluestep = 2;
            }
            if (player.GetModPlayer<OSRSplayer>().mediumClue == 21 && blockType == TileID.Sand && player.ZoneBeach)
            {
                cluestep = 2;
            }
            if (player.GetModPlayer<OSRSplayer>().mediumClue == 22 && blockType == TileID.JungleGrass && player.ZoneJungle)
            {
                cluestep = 2;
            }
            if (player.GetModPlayer<OSRSplayer>().mediumClue == 23 && blockType == TileID.ObsidianBrick && player.ZoneUnderworldHeight)
            {
                cluestep = 2;
            }
            if (player.GetModPlayer<OSRSplayer>().mediumClue == 24 && blockType == TileID.MushroomGrass && player.ZoneGlowshroom)
            {
                cluestep = 2;
            }
            if (player.GetModPlayer<OSRSplayer>().mediumClue == 25 && blockType == TileID.Marble && !player.ZoneOverworldHeight)
            {
                cluestep = 2;
            }
            if (player.GetModPlayer<OSRSplayer>().mediumClue == 26 && blockType == TileID.Granite && !player.ZoneOverworldHeight)
            {
                cluestep = 2;
            }
        }
    }
}
