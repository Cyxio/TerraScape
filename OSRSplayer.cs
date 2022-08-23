﻿using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Localization;
using Terraria.Audio;
using Terraria.GameInput;
using OldSchoolRuneScape.Items.Magic;
using OldSchoolRuneScape.UI;

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
        public bool shouldBC = false;

        public SpellCopy? selectedSpell;
        public int spellCooldown = 0;

        public override void UpdateLifeRegen()
        {
            if (Player.bleed)
            {
                Player.lifeRegen = 0;
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
            HidePlayer = false;
            BloodHound = false;
            shouldBC = false;
        }
        public override bool CanUseItem(Item item)
        {
            if (spellCooldown > 0)
            {
                return false;
            }
            return base.CanUseItem(item);
        }
        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (triggersSet.MouseLeft && selectedSpell != null)
            {
                var item = selectedSpell.Item;
                if (Main.myPlayer == Player.whoAmI && selectedSpell.ConsumeRunes(Main.LocalPlayer))
                {
                    Vector2 velocity = (Main.MouseWorld - Player.Center).SafeNormalize(Vector2.One);
                    SoundEngine.PlaySound(item.UseSound, Player.Center);
                    Projectile.NewProjectile(Player.GetSource_ItemUse(item), Player.Center, velocity * item.shootSpeed, item.shoot,
                        item.damage, item.knockBack, Player.whoAmI, 0f, 0f);
                }
                selectedSpell = null;
                spellCooldown = 15;
            }
            if (OldSchoolRuneScape.ToggleSpellbookHotKey.JustPressed)
            {
                SpellbookUI.Visible = !SpellbookUI.Visible;
            }
        }

        public override void PostUpdate()
        {
            if (spellCooldown > 0)
            {
                spellCooldown--;
            }
            if (TortureTime > 0)
            {
                TortureTime--;
            }
            if (MessageTime > 0)
            {
                Dust.NewDust(new Vector2(Player.Center.X - MessageDust.Texture2D.Width() * 0.375f, Player.position.Y - 40), 0, 0, MessageDust.Type);
                MessageTime--;
            }
            if (slayerText.Equals(""))
            {
                SlayerTextUpdate();
            }

        }

        public override void OnHitByNPC(NPC npc, int damage, bool crit)
        {
            if (AnguishTime > 0)
            {
                AnguishTime = 0;
            }
            if (Amulettorture && TortureTime == 0 && Player.statLife < 150)
            {
                TortureTime = 18000;
                Player.HealEffect(Player.statLifeMax2 - Player.statLife);
                Player.statLife = Player.statLifeMax2;
                Player.AddBuff(BuffID.Wrath, 18000);
                Player.AddBuff(BuffID.Rage, 18000);
            }
            if (Vengeance)
            {
                MessageTime = 90;
                MessageDust = ModContent.GetInstance<Dusts.VengMessage>();
                Dust.NewDust(new Vector2(Player.Center.X - 86, Player.position.Y - 40), 0, 0, ModContent.DustType<Dusts.VengMessage>());
                Player.ApplyDamageToNPC(npc, damage * 10, 0f, 0, false);
                npc.netUpdate = true;
                Vengeance = false;
                Player.ClearBuff(ModContent.BuffType<Buffs.Vengeance>());
                for (int o = 0; o < 36; o++)
                {
                    Vector2 rotate = new Vector2(2).RotatedBy(MathHelper.ToRadians(10 * o));
                    int dust = Dust.NewDust(Player.Center, 0, 0, DustID.GemRuby);
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
                Player.Hurt(PlayerDeathReason.ByCustomReason(Player.name + " got killed by vengeance lmao"), damage * 10, 0, true);
                target.GetModPlayer<OSRSplayer>().Vengeance = false;
                target.ClearBuff(ModContent.BuffType<Buffs.Vengeance>());
                target.GetModPlayer<OSRSplayer>().MessageDust = ModContent.GetInstance<Dusts.VengMessage>();
                target.GetModPlayer<OSRSplayer>().MessageTime = 90;
                for (int o = 0; o < 36; o++)
                {
                    Vector2 rotate = new Vector2(2).RotatedBy(MathHelper.ToRadians(10 * o));
                    int dust = Dust.NewDust(Player.Center, 0, 0, DustID.GemRuby);
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
                Player.Hurt(PlayerDeathReason.ByCustomReason(Player.name + " got killed by vengeance lmao"), damage * 10, 0, true);
                target.GetModPlayer<OSRSplayer>().Vengeance = false;
                target.ClearBuff(ModContent.BuffType<Buffs.Vengeance>());
                target.GetModPlayer<OSRSplayer>().MessageDust = ModContent.GetInstance<Dusts.VengMessage>();
                target.GetModPlayer<OSRSplayer>().MessageTime = 90;
                for (int o = 0; o < 36; o++)
                {
                    Vector2 rotate = new Vector2(2).RotatedBy(MathHelper.ToRadians(10 * o));
                    int dust = Dust.NewDust(Player.Center, 0, 0, DustID.GemRuby);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].scale = 1f;
                    Main.dust[dust].velocity = rotate;
                }
            }
        }

        public override bool Shoot(Item item, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (Karilset && item.CountsAsClass(DamageClass.Ranged) && Main.rand.Next(60) < item.useTime)
            {
                Projectile.NewProjectile(source, position + velocity * 2, velocity, type, damage, knockback, Player.whoAmI);
            }
            return true;
        }

        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (Guthanset && item.CountsAsClass(DamageClass.Melee) && (Main.rand.NextBool(10)|| (Amuletdamned && Main.rand.NextBool(7))) && target.type != NPCID.TargetDummy)
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
                Player.HealEffect(heal);
                Player.statLife += heal;
                Dust.NewDust(target.Center, 0, 0, ModContent.DustType<Dusts.Guthanset>());
            }
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (Tormentedbracelet && Main.rand.NextBool(10)&& proj.CountsAsClass(DamageClass.Magic))
            {
                float amount = Player.statLifeMax2 / (float)Player.statLife;
                if (amount > 6)
                {
                    amount = 6;
                }
                for (int i = 0; i < amount; i++)
                {
                    Vector2 rotate = new Vector2(target.height / 2).RotateRandom(Math.PI);
                    Vector2 spd = rotate;
                    spd.Normalize();
                    Projectile.NewProjectile(Player.GetSource_FromThis(), target.Center + rotate, spd * 3, ModContent.ProjectileType<Projectiles.Tormentedsoul>(), 75, 0f, Player.whoAmI, target.whoAmI);
                }
            }
            if (proj.CountsAsClass(DamageClass.Ranged) && Necklaceanguish && AnguishTime < 48)
            {
                AnguishTime++;
            }
            if (Guthanset && proj.CountsAsClass(DamageClass.Melee) && (Main.rand.NextBool(20)|| (Amuletdamned && Main.rand.NextBool(10))) && target.type != NPCID.TargetDummy)
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
                Player.HealEffect(heal);
                Player.statLife += heal;
                Dust.NewDust(target.Center, 0, 0, ModContent.DustType<Dusts.Guthanset>());
            }
            if (Ahrimset && proj.CountsAsClass(DamageClass.Magic))
            {
                target.AddBuff(BuffID.ShadowFlame, 360);
            }
            if (TomeFire && proj.CountsAsClass(DamageClass.Magic))
            {
                target.AddBuff(BuffID.OnFire, 120);
                target.AddBuff(BuffID.Frostburn, 120);
                target.AddBuff(BuffID.CursedInferno, 120);
                target.AddBuff(BuffID.ShadowFlame, 120);
            }
        }

        //SLayer

        public int slayTasksComplete = 0;
        public int slayerMob = 0;
        public int slayerLeft = 0;
        public int slayerGiven = 0;
        public int slayerDifficulty = 0;
        public static string slayerText = "";

        public void SlayerTask(string master1)
        {
            int master = 0;
            switch (master1)
            {
                case "Turael":
                    master = 0;
                    break;
                case "Mazchna":
                    master = 1;
                    break;
                case "Vannaka":
                    master = 2;
                    break;
                case "Chaeldar":
                    master = 3;
                    break;
                case "Nieve":
                    master = 4;
                    break;
                default:
                    break;
            }
            if (master > 0 && Main.rand.NextBool(3))
            {
                master--;
            }
            int[] slay = SlayerMobs(master);

            slayerMob = slay[0];
            slayerLeft = slay[1];
            slayerGiven = slay[1];
            slayerDifficulty = slay[2];
            string name = Lang.GetNPCName(slayerMob).ToString();
            name = FixEndings(name, slayerLeft);
            Main.npcChatText = "Your new task is to slay " + slayerLeft + " " + name + ".";
            Player.GetModPlayer<OSRSplayer>().SlayerTextUpdate();
        }

        private int[] SlayerMobs(int master)
        {
            int mob = 0;
            int amount = 0;
            int difficulty = 1;
            if (master == 0)
            {
                amount = Main.rand.Next(10, 26);
                switch (Main.rand.Next(10))
                {
                    case 0: mob = NPCID.BlueSlime; break;
                    case 1: mob = ModContent.NPCType<NPCs.Goblin>(); break;
                    case 2: mob = NPCID.CaveBat; difficulty++; break;
                    case 3: mob = NPCID.JungleBat; difficulty++; break;
                    case 4: mob = NPCID.Vulture; difficulty++; break;
                    case 5: mob = NPCID.IceBat; difficulty++; break;
                    case 6: mob = NPCID.Bunny; break;
                    case 7: mob = NPCID.Zombie; break;
                    case 8: mob = NPCID.DemonEye; break;
                    case 9: mob = NPCID.Skeleton; difficulty++; break;
                    default:
                        break;
                }
            }
            else if (master == 1)
            {
                difficulty = 2;
                amount = Main.rand.Next(15, 36);
                switch (Main.rand.Next(15))
                {
                    case 0: mob = WorldGen.crimson ? NPCID.Crimera : NPCID.EaterofSouls; break;
                    case 1: mob = ModContent.NPCType<NPCs.Hillgiant>(); break;
                    case 2: mob = ModContent.NPCType<NPCs.Gargoyle>(); break;
                    case 3: mob = NPCID.GraniteGolem; difficulty++; break;
                    case 4: mob = 481; difficulty++; break; //Hoplites
                    case 5: mob = NPCID.Harpy; difficulty++; break;
                    case 6: mob = NPCID.FireImp; difficulty++; break;
                    case 7: mob = NPCID.Shark; difficulty++; break;
                    case 8: mob = NPCID.IceSlime; break;
                    case 9: mob = NPCID.SandSlime; break;
                    case 10: mob = NPCID.PinkJellyfish; break;
                    case 11: mob = NPCID.WalkingAntlion; break;
                    case 12: mob = NPCID.Bee; break;
                    case 13: mob = NPCID.Piranha; break;
                    case 14: mob = NPCID.Hornet; break;
                    default:
                        break;
                }
            }
            else if (master == 2)
            {
                difficulty = 4;
                amount = Main.rand.Next(25, 51);
                switch (Main.rand.Next(15))
                {
                    case 0: mob = NPCID.Hellbat; break;
                    case 1: mob = ModContent.NPCType<NPCs.Greendragon>(); break;
                    case 2: mob = ModContent.NPCType<NPCs.Greaterdemon>(); break;
                    case 3: mob = NPCID.Demon; difficulty++; break;
                    case 4: mob = NPCID.WallCreeper; break;
                    case 5: mob = NPCID.BoneSerpentHead; difficulty++; amount = Main.rand.Next(20, 31); break;
                    case 6: mob = NPCID.GoblinScout; difficulty++; amount = Main.rand.Next(10, 21); break;
                    case 7: mob = NPCID.Harpy; difficulty++; break;
                    case 8: mob = NPCID.DungeonSlime; difficulty++; amount = Main.rand.Next(15, 26); break; 
                    case 9: mob = NPCID.CursedSkull; break;
                    case 10: mob = NPCID.DarkCaster; break;
                    case 11: mob = NPCID.FlyingAntlion; break;
                    case 12: mob = NPCID.LavaSlime; break;
                    case 13: mob = NPCID.AngryBones; break;
                    case 14: mob = NPCID.TombCrawlerBody; break;
                    default:
                        break;
                }
            }
            else if (master == 3)
            {
                difficulty = 6;
                amount = Main.rand.Next(50, 76);
                switch (Main.rand.Next(15))
                {
                    case 0: mob = NPCID.Wraith; break;
                    case 1: mob = ModContent.NPCType<NPCs.Bluedragon>(); break;
                    case 2: mob = ModContent.NPCType<NPCs.Reddragon>(); break;
                    case 3: mob = NPCID.Mummy; break;
                    case 4: mob = NPCID.DesertGhoul; break;
                    case 5: mob = NPCID.DesertBeast; difficulty++; break;
                    case 6: mob = NPCID.EnchantedSword; difficulty++; amount = Main.rand.Next(20, 31); break;
                    case 7: mob = NPCID.DesertDjinn; difficulty++; break;
                    case 8: mob = NPCID.DungeonSlime; difficulty++; amount = Main.rand.Next(15, 26); break;
                    case 9: mob = NPCID.Arapaima; break;
                    case 10: mob = WorldGen.crimson ? NPCID.FloatyGross : NPCID.Corruptor; break;
                    case 11: mob = NPCID.MossHornet; break;
                    case 12: mob = WorldGen.crimson ? NPCID.IchorSticker : NPCID.Clinger; difficulty++; break;
                    case 13: mob = NPCID.Medusa; amount = Main.rand.Next(15, 31); break;
                    case 14: mob = NPCID.WyvernBody; amount = Main.rand.Next(10, 21); break;
                    default:
                        break;
                }
            }
            else if (master == 4)
            {
                difficulty = 8;
                amount = Main.rand.Next(75, 101);
                switch (Main.rand.Next(13))
                {
                    case 0: mob = NPCID.GiantTortoise; break;
                    case 1: mob = ModContent.NPCType<NPCs.Blackdragon>(); break;
                    case 2: mob = ModContent.NPCType<NPCs.Demonicgorilla>(); difficulty++; amount = Main.rand.Next(5, 11); break;
                    case 3: mob = NPCID.IceTortoise; break;
                    case 4: mob = NPCID.DungeonSpirit; break;
                    case 5: mob = NPCID.Paladin; difficulty++; amount = Main.rand.Next(10, 21); break;
                    case 6: mob = NPCID.Mimic; difficulty++; amount = Main.rand.Next(5, 11); break;
                    case 7: mob = NPCID.RedDevil; difficulty++; break;
                    case 8: mob = NPCID.BoneLee; difficulty++; amount = Main.rand.Next(15, 26); break;
                    case 9: mob = NPCID.GiantCursedSkull; break;
                    case 10: mob = NPCID.SandElemental; difficulty++; amount = Main.rand.Next(10, 21); break;
                    case 11: mob = NPCID.HeadlessHorseman; difficulty++; amount = Main.rand.Next(15, 26); break;
                    case 12: mob = NPCID.DuneSplicerBody; break;
                    default:
                        break;
                }
            }
            return new int[] { mob, amount, difficulty };
        }

        public static string FixEndings(string s, int remaining)
        {
            string ns = s;
            if (ns.EndsWith("man") && remaining > 1)
            {
                string nss = ns.Replace("man", "men");
                return nss;
            }
            if (ns.EndsWith("sh"))
            {
                return ns;
            }
            if (ns.EndsWith("y") && remaining > 1)
            {
                string nss = ns.Replace("y", "ies");
                return nss;
            }
            if (ns.EndsWith("s"))
            {
                return ns;
            }
            if (remaining > 1)
            {
                ns = s + "s";
            }
            return ns;
        }

        public void SlayerReward(string master)
        {
            Player.GetModPlayer<OSRSplayer>().slayTasksComplete++;
            int amount = 2;
            switch (master)
            {
                case "Turael": amount = 2;
                    break;
                case "Mazchna": amount = 4;
                    break;
                case "Vannaka": amount = 6;
                    break;
                case "Chaeldar": amount = 10;
                    break;
                case "Nieve": amount = 15;
                    break;
                default:
                    break;
            }
            if (slayTasksComplete % 10 == 0)
            {
                switch (master)
                {
                    case "Turael":
                        amount += 5;
                        break;
                    case "Mazchna":
                        amount += 20;
                        break;
                    case "Vannaka":
                        amount += 35;
                        break;
                    case "Chaeldar":
                        amount += 50;
                        break;
                    case "Nieve":
                        amount += 75;
                        break;
                    default:
                        break;
                }
            }
            Player.QuickSpawnItem(Player.GetSource_FromThis(), ModContent.ItemType<Items.SlayerToken>(), amount);
            Color color = Color.White;
            switch (slayerDifficulty)
            {
                case 0: color = Colors.RarityNormal; break;
                case 1: color = Colors.RarityBlue; break;
                case 2: color = Colors.RarityGreen; break;
                case 3: color = Colors.RarityOrange; break;
                case 4: color = Colors.RarityRed; break;
                case 5: color = Colors.RarityPink; break;
                case 6: color = Colors.RarityPurple; break;
                case 7: color = Colors.RarityLime; break;
                case 8: color = Colors.RarityYellow; break;
                case 9: color = Colors.RarityCyan; break;
                case 10: color = new Color(225, 6, 67); break;
                default:
                    break;
            }
            string end = "th";
            switch (slayTasksComplete % 10)
            {
                case 1:
                    end = "st";
                    break;
                case 2:
                    end = "nd";
                    break;
                case 3:
                    end = "rd";
                    break;
                default:
                    break;
            }
            Main.NewText("You have completed your " + slayTasksComplete + end + " slayer task and are rewarded " + amount + " Slayer Tokens.", color);
            Player.GetModPlayer<OSRSplayer>().ResetSlayer();
        }

        public void ResetSlayer()
        {
            slayerMob = 0;
            slayerLeft = 0;
            slayerGiven = 0;
            slayerDifficulty = 0;
            Player.GetModPlayer<OSRSplayer>().SlayerTextUpdate();
        }

        public void SlayerTextUpdate()
        {
            if (slayerMob == 0)
            {
                slayerText = "Current task: None";
            }
            else
            {
                string name = Lang.GetNPCName(slayerMob).ToString();
                name = FixEndings(name, slayerLeft);
                slayerText = "Current task: " + name +
                    "\nAssigned: " + slayerGiven +
                    "\nSlain: " + (slayerGiven - slayerLeft) +
                    "\nRemaining: " + slayerLeft;
            }
        }
        public void SlayMessage()
        {
            string name = Lang.GetNPCName(slayerMob).ToString();
            name = FixEndings(name, slayerLeft);
            if (slayerLeft % 10 == 0)
            {
                Color color = Color.White;
                switch (slayerDifficulty)
                {
                    case 0: color = Colors.RarityNormal; ; break;
                    case 1: color = Colors.RarityBlue; ; break;
                    case 2: color = Colors.RarityGreen; ; break;
                    case 3: color = Colors.RarityOrange; ; break;
                    case 4: color = Colors.RarityRed; ; break;
                    case 5: color = Colors.RarityPink; ; break;
                    case 6: color = Colors.RarityPurple; ; break;
                    case 7: color = Colors.RarityLime; ; break;
                    case 8: color = Colors.RarityYellow; ; break;
                    case 9: color = Colors.RarityCyan; ; break;
                    case 10: color = new Color(225, 6, 67); ; break;
                    default:
                        break;
                }
                if (slayerLeft == 0)
                {
                    Main.NewText("You have completed your assignment; return to a slayer master to claim your reward.", color);
                }
                else
                {
                    Main.NewText("You have to kill " + slayerLeft + " more " + name + " to complete your assignment.", color);
                }
            }
        }
        
        //clueshit goes HERE
        public bool RingCoins = false;
        public bool RingNature = false;
        public bool BloodHound = false;
        public bool HidePlayer = false;

        public override void HideDrawLayers(PlayerDrawSet drawInfo)
        {
            if (RingCoins || RingNature || HidePlayer)
            {
                foreach (PlayerDrawLayer layer in PlayerDrawLayerLoader.Layers)
                {
                    layer.Hide();
                }
            }
        }
        public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
        {
            if (RingCoins || RingNature || HidePlayer)
            {
                if (RingCoins)
                {
                    Main.spriteBatch.Draw(Mod.Assets.Request<Texture2D>("Items/ClueScroll/ClueRewards/Master/Coinstack").Value,
                    new Vector2(Main.screenWidth / 2 - 30, Main.screenHeight / 2 - 7), null, Color.White, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
                }
                else if (RingNature)
                {
                    
                    Main.spriteBatch.Draw(Mod.Assets.Request<Texture2D>("Items/ClueScroll/ClueRewards/Elite/NatureBush").Value,
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

        public override void SaveData(TagCompound tag)
        {
            tag.Set("slayTasksComplete", slayTasksComplete);
            tag.Set("slayerDifficulty", slayerDifficulty);
            tag.Set("slayerMob", slayerMob);
            tag.Set("slayerLeft", slayerLeft);
            tag.Set("slayerGiven", slayerGiven);
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
        }
        public override void LoadData(TagCompound tag)
        {
            slayTasksComplete = tag.GetInt("slayTasksComplete");
            slayerDifficulty = tag.GetInt("slayerDifficulty");
            slayerMob = tag.GetInt("slayerMob");
            slayerLeft = tag.GetInt("slayerLeft");
            slayerGiven = tag.GetInt("slayerGiven");
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

        public void ClueMessage(string message)
        {
            Terraria.Chat.ChatHelper.SendChatMessageFromClient(new Terraria.Chat.ChatMessage("/ClueMessage " + message));
        }
        public void ClueAnswer(Player player)
        {
            player.GetModPlayer<OSRSplayer>().cluestep = challengeDiff;
            player.GetModPlayer<OSRSplayer>().challengeDiff = 0;
            player.GetModPlayer<OSRSplayer>().challengeAns = 0;
        }
        public override void AnglerQuestReward(float rareMultiplier, List<Item> rewardItems)
        {
            if (Player.GetModPlayer<OSRSplayer>().mediumClue == 29)
            {
                Player.GetModPlayer<OSRSplayer>().cluestep = 2;
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
            if (type == ModContent.ItemType<Items.ClueScroll.EasyClue>())
            {
                player.GetModPlayer<OSRSplayer>().easyClue = 0;
                player.GetModPlayer<OSRSplayer>().easySteps = 0;
                player.GetModPlayer<OSRSplayer>().easyStage = 0;
            }
            if (type == ModContent.ItemType<Items.ClueScroll.MediumClue>())
            {
                player.GetModPlayer<OSRSplayer>().mediumClue = 0;
                player.GetModPlayer<OSRSplayer>().mediumSteps = 0;
                player.GetModPlayer<OSRSplayer>().mediumStage = 0;
            }
            if (type == ModContent.ItemType<Items.ClueScroll.HardClue>())
            {
                player.GetModPlayer<OSRSplayer>().hardClue = 0;
                player.GetModPlayer<OSRSplayer>().hardSteps = 0;
                player.GetModPlayer<OSRSplayer>().hardStage = 0;
            }
            if (type == ModContent.ItemType<Items.ClueScroll.EliteClue>())
            {
                player.GetModPlayer<OSRSplayer>().eliteClue = 0;
                player.GetModPlayer<OSRSplayer>().eliteSteps = 0;
                player.GetModPlayer<OSRSplayer>().eliteStage = 0;
            }
            if (type == ModContent.ItemType<Items.ClueScroll.MasterClue>())
            {
                player.GetModPlayer<OSRSplayer>().masterClue = 0;
                player.GetModPlayer<OSRSplayer>().masterSteps = 0;
                player.GetModPlayer<OSRSplayer>().masterStage = 0;
            }
        }
        public override void PreUpdate()
        {
            if (Player.talkNPC != 1 || Player.chest != 1 || Player.trashItem.width == 62)
            {
                int[] clues = { ModContent.ItemType<Items.ClueScroll.EasyClue>(), ModContent.ItemType<Items.ClueScroll.MediumClue>(), ModContent.ItemType<Items.ClueScroll.HardClue>(), ModContent.ItemType<Items.ClueScroll.EliteClue>(), ModContent.ItemType<Items.ClueScroll.MasterClue>() };
                List<int> cluess = new List<int>(clues);
                if (cluess.Contains(Player.trashItem.type))
                {
                    ClueReset(Player.trashItem.type, Player);
                    Player.trashItem.TurnToAir();
                }
                if (Main.npcShop != 0)
                {
                    for (int i = 0; i < 40; i++)
                    {
                        if (cluess.Contains(Main.instance.shop[Main.npcShop].item[i].type))
                        {
                            ClueReset(Main.instance.shop[Main.npcShop].item[i].type, Player);
                            Main.instance.shop[Main.npcShop].item[i].TurnToAir();
                        }
                    }
                }
                if (Player.chest > -1)
                {
                    for (int i = 0; i < 40; i++)
                    {
                        if (cluess.Contains(Main.chest[Player.chest].item[i].type))
                        {
                            ClueReset(Main.chest[Player.chest].item[i].type, Player);
                            Main.chest[Player.chest].item[i].TurnToAir();
                        }
                    }
                }
                if (Player.chest == -2)
                {
                    for (int i = 0; i < 40; i++)
                    {
                        if (cluess.Contains(Player.bank.item[i].type))
                        {
                            ClueReset(Player.bank.item[i].type, Player);
                            Player.bank.item[i].TurnToAir();
                        }
                    }
                }
                if (Player.chest == -3)
                {
                    for (int i = 0; i < 40; i++)
                    {
                        if (cluess.Contains(Player.bank2.item[i].type))
                        {
                            ClueReset(Player.bank2.item[i].type, Player);
                            Player.bank2.item[i].TurnToAir();
                        }
                    }
                }
                if (Player.chest == -4)
                {
                    for (int i = 0; i < 40; i++)
                    {
                        if (cluess.Contains(Player.bank3.item[i].type))
                        {
                            ClueReset(Player.bank3.item[i].type, Player);
                            Player.bank3.item[i].TurnToAir();
                        }
                    }
                }
            }
        }
        public void ClueDig(int blockType)
        {
            if (Player.GetModPlayer<OSRSplayer>().easyClue == 1 && blockType == TileID.Grass)
            {
                cluestep = 1;
            }
            if (Player.GetModPlayer<OSRSplayer>().easyClue == 2 && blockType == TileID.IceBlock && Player.ZoneSnow)
            {
                cluestep = 1;
            }
            if (Player.GetModPlayer<OSRSplayer>().easyClue == 3 && blockType == TileID.Sand && Player.ZoneDesert)
            {
                cluestep = 1;
            }
            if (Player.GetModPlayer<OSRSplayer>().mediumClue == 19 && blockType == TileID.Sunplate && Player.ZoneSkyHeight)
            {
                cluestep = 2;
            }
            if (Player.GetModPlayer<OSRSplayer>().mediumClue == 20 && (blockType == TileID.BlueDungeonBrick || blockType == TileID.GreenDungeonBrick || blockType == TileID.PinkDungeonBrick))
            {
                cluestep = 2;
            }
            if (Player.GetModPlayer<OSRSplayer>().mediumClue == 21 && blockType == TileID.Sand && Player.ZoneBeach)
            {
                cluestep = 2;
            }
            if (Player.GetModPlayer<OSRSplayer>().mediumClue == 22 && blockType == TileID.JungleGrass && Player.ZoneJungle)
            {
                cluestep = 2;
            }
            if (Player.GetModPlayer<OSRSplayer>().mediumClue == 23 && blockType == TileID.ObsidianBrick && Player.ZoneUnderworldHeight)
            {
                cluestep = 2;
            }
            if (Player.GetModPlayer<OSRSplayer>().mediumClue == 24 && blockType == TileID.MushroomGrass && Player.ZoneGlowshroom)
            {
                cluestep = 2;
            }
            if (Player.GetModPlayer<OSRSplayer>().mediumClue == 25 && blockType == TileID.Marble && !Player.ZoneOverworldHeight)
            {
                cluestep = 2;
            }
            if (Player.GetModPlayer<OSRSplayer>().mediumClue == 26 && blockType == TileID.Granite && !Player.ZoneOverworldHeight)
            {
                cluestep = 2;
            }
        }
    }
}
