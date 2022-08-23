using System;
using Microsoft.Xna.Framework;
using OldSchoolRuneScape.Common.DropRules;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.ItemDropRules;
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
            Item.value = Item.buyPrice(0, 5, 0, 0);
            Item.consumable = true;
            Item.maxStack = 20;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTime = 45;
            Item.useAnimation = 45;
            Item.rare = ItemRarityID.Green;
        }
        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(ModContent.NPCType<CrushingHand>()) && !Main.dayTime;
        }
        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<ScreamingBanshee>());
            SoundEngine.PlaySound(SoundID.Roar, player.position);
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
            Projectile.CloneDefaults(ProjectileID.ShadowFlame);
            Projectile.hostile = true;
            AIType = ProjectileID.ShadowFlame;
        }
        public override void AI()
        {
            Projectile.hostile = true;
            Projectile.friendly = false;
            if (Projectile.ai[0] == 0)
            {
                Projectile.ai[0] = Main.rand.NextFloat(-0.2f, 0.2f);
            }
            if (Projectile.ai[1] == 0)
            {
                Projectile.ai[1] = Main.rand.NextFloat(-0.2f, 0.2f);
            }
        }
    }

    [AutoloadBossHead]
    public class ScreamingBanshee : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Screaming Banshee");
            Main.npcFrameCount[NPC.type] = 4;
        }
        public override void SetDefaults()
        {
            NPC.width = 100;
            NPC.height = 200;
            NPC.aiStyle = -1;
            NPC.npcSlots = 10f;
            NPC.lavaImmune = true;
            NPC.damage = 35;
            NPC.defense = 0;
            NPC.lifeMax = 4000;
            NPC.knockBackResist = 0f;
            NPC.boss = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.NPCHit54;
            NPC.DeathSound = SoundID.NPCDeath52;
            NPC.value = Item.buyPrice(0, 5) / 2.5f;
            NPC.buffImmune[BuffID.Confused] = true;
            Music = OldSchoolRuneScape.slayerMusic;
        }

        const int Move = 0;
        const int Attack = 1;
        const int Flee = 2;

        public float State
        {
            get { return NPC.ai[0]; }
            set { NPC.ai[0] = value; }
        }

        public float Timer
        {
            get { return NPC.ai[1]; }
            set { NPC.ai[1] = value; }
        }

        public float AttackNum
        {
            get { return NPC.ai[2]; }
            set { NPC.ai[2] = value; }
        }

        public override bool CheckDead()
        {
            if (NPC.ai[3] == 0f)
            {
                NPC.ai[3] = 1f;
                NPC.damage = 0;
                NPC.life = NPC.lifeMax;
                NPC.dontTakeDamage = true;
                NPC.netUpdate = true;
                return false;
            }
            return true;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            NPC.lifeMax = 5200 + 1000 * numPlayers;
            NPC.damage = (int)(NPC.damage * 0.7f);
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
            NPC.TargetClosest(true);
            Player target = Main.player[NPC.target];
            HealthPrc = (int)(100f * (NPC.life / (float)NPC.lifeMax));
            if (NPC.ai[3] > 0f)
            {
                State = 4;
                NPC.alpha++;
                NPC.velocity *= 0.96f;
                NPC.ai[3]++;
                if (NPC.ai[3] % 20 == 0)
                {
                    SoundEngine.PlaySound(NPC.HitSound.Value);
                }
                if (Main.rand.NextFloat() < 1f)
                {
                    Dust dust;
                    // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                    Vector2 position = NPC.position;
                    dust = Main.dust[Terraria.Dust.NewDust(position, NPC.width, NPC.height, DustID.Asphalt, 0f, -5f, 0, new Color(255, 255, 255), 2.302632f)];
                }
                if (NPC.ai[3] > 180f)
                {
                    for (int i = 0; i < 180; i++)
                    {
                        Dust dust;
                        // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                        Vector2 spede = new Vector2(0, 8);
                        spede = spede.RotatedBy(MathHelper.ToRadians(i * 2));
                        spede *= Main.rand.NextFloat(1f, 2f);
                        dust = Main.dust[Terraria.Dust.NewDust(NPC.Center, 10, 10, DustID.Wraith, spede.X, spede.Y, 0, new Color(255, 255, 255), 2.960526f)];
                        dust.noGravity = true;
                        dust.fadeIn = 0.9868421f;
                    }
                    NPC.life = 0;
                    NPC.HitEffect(0, 0);
                    NPC.checkDead();
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
                Vector2 toTarget = target.MountedCenter - NPC.Center;
                toTarget.Normalize();
                toTarget *= (speed * acceleration);
                NPC.velocity += toTarget;
                if (NPC.velocity.Length() > speed)
                {
                    NPC.velocity *= 0.95f;
                }
                if (Timer <= 0 && HealthPrc < 100 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    int i = Main.rand.Next(4);
                    AttackNum = i;
                    if (i == 0)
                    {
                        //Main.NewText("dash");
                        Timer = 90;
                        NPC.velocity = new Vector2(10 * NPC.direction, 0);
                    }
                    else if (i == 1)
                    {
                        //Main.NewText("tentac");
                        Timer = 101;
                        NPC.velocity *= 0;
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
                    NPC.netUpdate = true;
                }
                if (Main.dayTime || !NPC.HasValidTarget)
                {
                    State = Flee;
                    Timer = 85;
                    NPC.netUpdate = true;
                }
            }
            if (State == Attack)
            {
                int CD = 60;
                if (AttackNum == 0)
                {
                    NPC.velocity *= 0.985f;
                }
                else if (AttackNum == 1)
                {
                    Vector2 velo = target.Center - NPC.Center;
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
                        SoundEngine.PlaySound(SoundID.Item103, NPC.Center);
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            for (int a = 0; a < 3; a++)
                            {
                                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center + new Vector2(41 * NPC.direction, 41), velo, ModContent.ProjectileType<BansheeTentacle>(), NPC.damage / 4, 1f, 255, Main.rand.NextFloat(-0.2f, 0.2f), Main.rand.NextFloat(-0.2f, 0.2f));
                            }
                        }
                    }
                    CD = 60;
                }
                else if (AttackNum == 2)
                {
                    if (Timer > 45 && Vector2.Distance(target.Center, NPC.Center) > 200f)
                    {
                        Vector2 velo = target.Center - NPC.Center;
                        velo.Normalize();
                        NPC.velocity += velo;
                    }
                    else
                    {
                        NPC.velocity *= 0.995f;
                    }
                    if (NPC.velocity.Length() > 5f)
                    {
                        NPC.velocity *= 0.93f;
                    }
                    CD = 60;
                }
                else if (AttackNum == 3)
                {
                    Vector2 target1 = target.Center + new Vector2(0, 450).RotatedBy(MathHelper.ToRadians(Timer * 2));
                    Vector2 velo = target1 - NPC.Center;
                    velo.Normalize();
                    NPC.velocity += velo;
                    if (NPC.velocity.Length() > 9f)
                    {
                        NPC.velocity *= 0.93f;
                    }
                    if (HealthPrc < 50 && Timer % 90 == 0)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            for (int a = 0; a < 3; a++)
                            {
                                Vector2 veloc = target.Center - NPC.Center;
                                veloc.Normalize();
                                veloc = veloc.RotatedBy(Main.rand.NextFloat((float)Math.PI / -10f, (float)Math.PI / 10f));
                                veloc *= 19f;
                                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center + new Vector2(41 * NPC.direction, 41), veloc, ModContent.ProjectileType<BansheeTentacle>(), NPC.damage / 4, 1f, 255, Main.rand.NextFloat(-0.2f, 0.2f), Main.rand.NextFloat(-0.2f, 0.2f));
                            }
                        }
                        SoundEngine.PlaySound(SoundID.Item103, NPC.Center);
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
                    NPC.netUpdate = true;
                }
            }
            if (State == Flee)
            {
                NPC.alpha = (85 - (int)(Timer)) * 3;
                if (Timer < 10)
                {
                    NPC.active = false;
                    NPC.netUpdate = true;
                }
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            var leadingCondition = new LeadingConditionRule(new SlayerBossFirstKillCondition(2));
            int itemType = ModContent.ItemType<Items.SlayerToken>();
            var parameters = new DropOneByOne.Parameters()
            {
                ChanceNumerator = 1,
                ChanceDenominator = 1,
                MinimumStackPerChunkBase = 1,
                MaximumStackPerChunkBase = 1,
                MinimumItemDropsCount = 25,
                MaximumItemDropsCount = 25,
            };
            leadingCondition.OnSuccess(new DropOneByOne(itemType, parameters));
            npcLoot.Add(leadingCondition);
        }
        public override void OnKill()
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
            NPC.spriteDirection = NPC.direction;
            if (State == Attack)
            {
                NPC.frameCounter++;
                if (NPC.frameCounter < 6)
                {
                    NPC.frame.Y = 0 * frameHeight;
                }
                else if (NPC.frameCounter < 12)
                {
                    NPC.frame.Y = 1 * frameHeight;
                }
                else if (NPC.frameCounter < 18)
                {
                    NPC.frame.Y = 2 * frameHeight;
                }
                else
                {
                    NPC.frameCounter = 0;
                }
            }
            else
            {
                NPC.frameCounter++;
                if (NPC.frameCounter < 10)
                {
                    NPC.frame.Y = 0 * frameHeight;
                }
                else if (NPC.frameCounter < 20)
                {
                    NPC.frame.Y = 1 * frameHeight;
                }
                else if (NPC.frameCounter < 30)
                {
                    NPC.frame.Y = 2 * frameHeight;
                }
                else if (NPC.frameCounter < 40)
                {
                    NPC.frame.Y = 3 * frameHeight;
                }
                else
                {
                    NPC.frameCounter = 0;
                }
            }
        }
    }
}