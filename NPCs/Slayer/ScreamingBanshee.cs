using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.NPCs.Slayer
{
    public class BansheeSummon : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Torn Cloth");
            Tooltip.SetDefault("Ripped from a screaming entity...");
        }
        public override void SetDefaults()
        {
            item.value = Item.buyPrice(0, 5, 0, 0);
            item.consumable = true;
            item.maxStack = 20;
            item.useStyle = 4;
            item.useTime = 45;
            item.useAnimation = 45;
            item.rare = 2;
        }
        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(ModContent.NPCType<CrushingHand>()) && !Main.dayTime;
        }
        public override bool UseItem(Player player)
        {
            NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<ScreamingBanshee>());
            Main.PlaySound(SoundID.Roar, player.position, 0);
            return true;
        }
    }

    public class BansheeTentacle : ModProjectile
    {
        public override string Texture { get { return "Terraria/Projectile_" + ProjectileID.ShadowFlame; } }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Banshee's Tentacle");
        }
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.ShadowFlame);
            projectile.hostile = true;
            aiType = ProjectileID.ShadowFlame;
        }
        public override void AI()
        {
            projectile.hostile = true;
            projectile.friendly = false;
            if (projectile.ai[0] == 0)
            {
                projectile.ai[0] = Main.rand.NextFloat(-0.2f, 0.2f);
            }
            if (projectile.ai[1] == 0)
            {
                projectile.ai[1] = Main.rand.NextFloat(-0.2f, 0.2f);
            }
        }
    }

    [AutoloadBossHead]
    public class ScreamingBanshee : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Screaming Banshee");
            Main.npcFrameCount[npc.type] = 4;
        }
        public override void SetDefaults()
        {
            npc.width = 100;
            npc.height = 200;
            npc.aiStyle = -1;
            npc.npcSlots = 10f;
            npc.lavaImmune = true;
            npc.damage = 35;
            npc.defense = 0;
            npc.lifeMax = 4000;
            npc.knockBackResist = 0f;
            npc.boss = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit54;
            npc.DeathSound = SoundID.NPCDeath52;
            npc.value = Item.buyPrice(0, 5) / 2.5f;
            npc.buffImmune[BuffID.Confused] = true;
            music = OldSchoolRuneScape.slayerMusic;
        }

        const int Move = 0;
        const int Attack = 1;
        const int Flee = 2;

        public float State
        {
            get { return npc.ai[0]; }
            set { npc.ai[0] = value; }
        }

        public float Timer
        {
            get { return npc.ai[1]; }
            set { npc.ai[1] = value; }
        }

        public float AttackNum
        {
            get { return npc.ai[2]; }
            set { npc.ai[2] = value; }
        }

        public override bool CheckDead()
        {
            if (npc.ai[3] == 0f)
            {
                npc.ai[3] = 1f;
                npc.damage = 0;
                npc.life = npc.lifeMax;
                npc.dontTakeDamage = true;
                npc.netUpdate = true;
                return false;
            }
            return true;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 5200 + 1000 * numPlayers;
            npc.damage = (int)(npc.damage * 0.7f);
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            name = "Screaming Banshee";
            potionType = ItemID.HealingPotion;
        }

        int HealthPrc = 100;

        public override void AI()
        {
            if (Timer > 0)
            {
                Timer--;
            }
            npc.TargetClosest(true);
            Player target = Main.player[npc.target];
            HealthPrc = (int)(100f * (npc.life / (float)npc.lifeMax));
            if (npc.ai[3] > 0f)
            {
                State = 4;
                npc.alpha++;
                npc.velocity *= 0.96f;
                npc.ai[3]++;
                if (npc.ai[3] % 20 == 0)
                {
                    Main.PlaySound(npc.HitSound);
                    if (OSRSworld.slayBossProgress < 2)
                    {
                        Item.NewItem(npc.position, new Vector2(npc.width, npc.height), ModContent.ItemType<Items.SlayerToken>(), 4);
                    }
                }
                if (Main.rand.NextFloat() < 1f)
                {
                    Dust dust;
                    // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                    Vector2 position = npc.position;
                    dust = Main.dust[Terraria.Dust.NewDust(position, npc.width, npc.height, 109, 0f, -5f, 0, new Color(255, 255, 255), 2.302632f)];
                }
                if (npc.ai[3] > 180f)
                {
                    for (int i = 0; i < 180; i++)
                    {
                        Dust dust;
                        // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                        Vector2 spede = new Vector2(0, 8);
                        spede = spede.RotatedBy(MathHelper.ToRadians(i * 2));
                        spede *= Main.rand.NextFloat(1f, 2f);
                        dust = Main.dust[Terraria.Dust.NewDust(npc.Center, 10, 10, 54, spede.X, spede.Y, 0, new Color(255, 255, 255), 2.960526f)];
                        dust.noGravity = true;
                        dust.fadeIn = 0.9868421f;
                    }
                    npc.life = 0;
                    npc.HitEffect(0, 0);
                    npc.checkDead();
                }
            }
            if (State == Move)
            {
                float speed = 4.5f;
                float acceleration = 0.04f;
                if (HealthPrc < 60)
                {
                    speed = 5f;
                    acceleration = 0.05f;
                }
                if (HealthPrc < 30)
                {
                    speed = 5.5f;
                    acceleration = 0.06f;
                }
                Vector2 toTarget = target.MountedCenter - npc.Center;
                toTarget.Normalize();
                toTarget *= (speed * acceleration);
                npc.velocity += toTarget;
                if (npc.velocity.Length() > speed)
                {
                    npc.velocity *= 0.95f;
                }
                if (Timer <= 0 && HealthPrc < 100 && Main.netMode != 1)
                {
                    int i = Main.rand.Next(4);
                    AttackNum = i;
                    if (i == 0)
                    {
                        //Main.NewText("dash");
                        Timer = 90;
                        npc.velocity = new Vector2(10 * npc.direction, 0);
                    }
                    else if (i == 1)
                    {
                        //Main.NewText("tentac");
                        Timer = 101;
                        npc.velocity *= 0;
                    }
                    else if (i == 2)
                    {
                        //Main.NewText("getClose");
                        Timer = 90;
                    }
                    else if (i == 3)
                    {
                        //Main.NewText("spin");
                        Timer = 270;
                    }
                    State = Attack;
                    npc.netUpdate = true;
                }
                if (Main.dayTime || !npc.HasValidTarget)
                {
                    State = Flee;
                    Timer = 85;
                    npc.netUpdate = true;
                }
            }
            if (State == Attack)
            {
                int CD = 60;
                if (AttackNum == 0)
                {
                    npc.velocity *= 0.985f;
                }
                else if (AttackNum == 1)
                {
                    Vector2 velo = target.Center - npc.Center;
                    velo.Normalize();
                    velo = velo.RotatedBy(Main.rand.NextFloat((float)Math.PI / -10f, (float)Math.PI / 10f));
                    if (Timer > 89)
                    {
                        velo *= 10;
                    }
                    else if (Timer > 59)
                    {
                        velo *= 13;
                    }
                    else if (Timer > 29)
                    {
                        velo *= 16;
                    }
                    else
                    {
                        velo *= 19;
                    }
                    if (Timer % 30 == 0)
                    {
                        Main.PlaySound(SoundID.Item103, npc.Center);
                        if (Main.netMode != 1)
                        {
                            for (int a = 0; a < 3; a++)
                            {
                                Projectile.NewProjectile(npc.Center + new Vector2(41 * npc.direction, 41), velo, ModContent.ProjectileType<BansheeTentacle>(), npc.damage / 4, 1f, 255, Main.rand.NextFloat(-0.2f, 0.2f), Main.rand.NextFloat(-0.2f, 0.2f));
                            }
                        }
                    }
                    CD = 60;
                }
                else if (AttackNum == 2)
                {
                    if (Timer > 45 && Vector2.Distance(target.Center, npc.Center) > 200f)
                    {
                        Vector2 velo = target.Center - npc.Center;
                        velo.Normalize();
                        npc.velocity += velo;
                    }
                    else
                    {
                        npc.velocity *= 0.995f;
                    }
                    if (npc.velocity.Length() > 5f)
                    {
                        npc.velocity *= 0.93f;
                    }
                    CD = 60;
                }
                else if (AttackNum == 3)
                {
                    Vector2 target1 = target.Center + new Vector2(0, 450).RotatedBy(MathHelper.ToRadians(Timer * 2));
                    Vector2 velo = target1 - npc.Center;
                    velo.Normalize();
                    npc.velocity += velo;
                    if (npc.velocity.Length() > 9f)
                    {
                        npc.velocity *= 0.93f;
                    }
                    if (HealthPrc < 50 && Timer % 90 == 0)
                    {
                        if (Main.netMode != 1)
                        {
                            for (int a = 0; a < 3; a++)
                            {
                                Vector2 veloc = target.Center - npc.Center;
                                veloc.Normalize();
                                veloc = veloc.RotatedBy(Main.rand.NextFloat((float)Math.PI / -10f, (float)Math.PI / 10f));
                                veloc *= 19f;
                                Projectile.NewProjectile(npc.Center + new Vector2(41 * npc.direction, 41), veloc, ModContent.ProjectileType<BansheeTentacle>(), npc.damage / 4, 1f, 255, Main.rand.NextFloat(-0.2f, 0.2f), Main.rand.NextFloat(-0.2f, 0.2f));
                            }
                        }
                        Main.PlaySound(SoundID.Item103, npc.Center);
                    }
                    CD = 30;
                }
                if (Timer <= 0)
                {
                    State = Move;
                    int i = 0;
                    if (HealthPrc < 40)
                    {
                        i = (int)(CD / 10f);
                    }
                    Timer = CD - (i*5);
                    npc.netUpdate = true;
                }
            }
            if (State == Flee)
            {
                npc.alpha = (85 - (int)(Timer)) * 3;
                if (Timer < 10)
                {
                    npc.active = false;
                    npc.netUpdate = true;
                }
            }
        }


        public override void NPCLoot()
        {
            if (OSRSworld.slayBossProgress < 2)
            {
                OSRSworld.slayBossProgress = 2;
            }
            for (int i = 0; i < 200; i++)
            {
                if (Main.npc[i].type == ModContent.NPCType<SlayerMasterMazchna>())
                {
                    Main.npc[i].life = 1;
                    Main.npc[i].AddBuff(BuffID.ShadowFlame, 180);
                    Main.npc[i].netUpdate = true;
                }
            }
        }

        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = npc.direction;
            if (State == Attack)
            {
                npc.frameCounter++;
                if (npc.frameCounter < 6)
                {
                    npc.frame.Y = 0 * frameHeight;
                }
                else if (npc.frameCounter < 12)
                {
                    npc.frame.Y = 1 * frameHeight;
                }
                else if (npc.frameCounter < 18)
                {
                    npc.frame.Y = 2 * frameHeight;
                }
                else
                {
                    npc.frameCounter = 0;
                }
            }
            else
            {
                npc.frameCounter++;
                if (npc.frameCounter < 10)
                {
                    npc.frame.Y = 0 * frameHeight;
                }
                else if (npc.frameCounter < 20)
                {
                    npc.frame.Y = 1 * frameHeight;
                }
                else if (npc.frameCounter < 30)
                {
                    npc.frame.Y = 2 * frameHeight;
                }
                else if (npc.frameCounter < 40)
                {
                    npc.frame.Y = 3 * frameHeight;
                }
                else
                {
                    npc.frameCounter = 0;
                }
            }
        }
    }
}