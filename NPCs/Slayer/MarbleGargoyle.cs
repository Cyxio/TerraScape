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
    public class MarbleSummon : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Charged Stalactite");
            Tooltip.SetDefault("Seems to be made out of marble...");
        }
        public override void SetDefaults()
        {
            Item.value = Item.buyPrice(0, 20, 0, 0);
            Item.consumable = true;
            Item.maxStack = 20;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTime = 45;
            Item.useAnimation = 45;
            Item.rare = ItemRarityID.Lime;
        }
        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(ModContent.NPCType<MarbleGargoyle>()) && !Main.dayTime;
        }
        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<MarbleGargoyle>());
            SoundEngine.PlaySound(SoundID.Roar, player.position);
            return true;
        }
    }

    public class MarbleShard : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Marble Shard");
        }
        public override void SetDefaults()
        {
            Projectile.hostile = true;
            Projectile.friendly = false;
            Projectile.height = 16;
            Projectile.width = 16;
            AIType = -1;
            Projectile.tileCollide = true;
            Projectile.timeLeft = 600;
        }
        public override void AI()
        {
            if (Projectile.timeLeft == 600)
            {
                int ai = (int)Projectile.ai[0];
                switch (ai)
                {
                    case 0:
                        SoundEngine.PlaySound(SoundID.Item118);
                        break;
                    case 1:
                        Projectile.alpha = 128;
                        break;
                    case 2:
                        SoundEngine.PlaySound(SoundID.Item20);
                        break;
                    default:
                        break;
                }
            }
            if (Projectile.ai[0] == 0)
            {
                Projectile.rotation = Projectile.velocity.ToRotation() + (float)(Math.PI / 2f);
            }
            if (Projectile.ai[0] == 1)
            {
                if (Projectile.timeLeft == 584)
                {
                    SoundEngine.PlaySound(SoundID.Item118);
                    Projectile.velocity = new Vector2(0, 22f);
                }
                if (Projectile.timeLeft >= 584)
                {
                    Projectile.rotation -= MathHelper.ToRadians(270/16);
                }
                else
                {
                    Projectile.rotation = Projectile.velocity.ToRotation() + (float)(Math.PI / 2f);
                }
                Projectile.alpha -= 8;
            }
            if (Projectile.ai[0] == 2)
            {
                Dust dust;
                Vector2 position = Projectile.position;
                dust = Main.dust[Terraria.Dust.NewDust(position, 16, 46, DustID.PlatinumCoin, 0f, 0f, 0, new Color(255, 255, 255), 1f)];
                dust.noGravity = true;
                Projectile.height = 46;
                Projectile.rotation = 0;
                if (Projectile.timeLeft > 550)
                {
                    Projectile.timeLeft = Main.rand.Next(475, 550);
                }
                if (Projectile.timeLeft >= 475)
                {
                    Projectile.velocity = new Vector2(0, -12);
                    Projectile.tileCollide = false;
                }
                else
                {
                    Projectile.tileCollide = true;
                    Projectile.velocity = new Vector2(0, 1.5f * (float)Math.Sin(MathHelper.ToRadians(Projectile.timeLeft % 60 * 6)));
                }
            }
            if (Projectile.ai[0] == 3)
            {
                Dust dust;
                Vector2 position = Projectile.Center;
                Vector2 unit = Projectile.velocity;
                unit.Normalize();
                dust = Terraria.Dust.NewDustPerfect(position - unit * 36f, 247, new Vector2(0f, 0f), 0, new Color(255, 255, 255), 1f);
                dust.noGravity = true;
                Projectile.rotation = Projectile.velocity.ToRotation() + (float)(Math.PI / 2f);
                for (int i = 0; i < 200; i++)
                {
                    if (Main.npc[i].type == ModContent.NPCType<MarbleGargoyle>())
                    {
                        Vector2 veloc = Main.npc[i].Center - Projectile.Center;
                        veloc.Normalize();
                        Projectile.velocity += veloc*0.5f;
                    }
                }
                if (Projectile.velocity.Length() > 16f)
                {
                    Projectile.velocity *= 0.985f;
                }
            }
            if (Projectile.ai[0] == 4)
            {
                Projectile.ai[0] = 2;
                Projectile.timeLeft = 450;
            }
        }
    }

    public class StoneBeam : ModProjectile
    {
        public override string Texture { get { return "Terraria/Projectile_" + ProjectileID.ShadowFlame; } }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Stone Beam");
        }
        public override void SetDefaults()
        {
            Projectile.hostile = true;
            Projectile.friendly = false;
            Projectile.height = 16;
            Projectile.width = 16;
            AIType = -1;
            Projectile.tileCollide = true;
            Projectile.timeLeft = 300;
            Projectile.alpha = 255;
        }
        public override void AI()
        {
            Dust dust;
            // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
            Vector2 position = Projectile.Center;
            dust = Terraria.Dust.NewDustPerfect(position, 70, new Vector2(0f, 0f), 0, new Color(255, 255, 255), 2.105263f);
            dust.noGravity = true;
            Projectile.damage = 1;
            if (Projectile.ai[0] == 0)
            {
                Projectile.ai[0] = MathHelper.ToRadians(Main.rand.Next(-30, 30));
            }
            Projectile.velocity += Projectile.velocity.RotatedBy(Projectile.ai[0] / 60f) - Projectile.oldVelocity;
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            Vector2 v2 = Projectile.velocity;
            v2.Normalize();
            float point = 0f;
            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center,
                    Projectile.Center - v2 * 230f, 16, ref point);
        }

        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            target.AddBuff(BuffID.Stoned, 70);
            damage = 0;
        }
    }

    [AutoloadBossHead]
    public class MarbleGargoyle : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Marble Gargoyle");
            Main.npcFrameCount[NPC.type] = 5;
        }
        public override void SetDefaults()
        {
            NPC.width = 100;
            NPC.height = 180;
            NPC.aiStyle = -1;
            NPC.npcSlots = 20f;
            NPC.lavaImmune = true;
            NPC.damage = 80;
            NPC.defense = 35;
            NPC.lifeMax = 23000;
            NPC.knockBackResist = 0f;
            NPC.boss = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.NPCHit7;
            NPC.DeathSound = SoundID.NPCDeath5;
            NPC.value = Item.buyPrice(0, 20, 0, 0) / 2.5f;
            NPC.buffImmune[BuffID.Confused] = true;
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.buffImmune[BuffID.Ichor] = true;
            NPC.buffImmune[BuffID.Frostburn] = true;
            NPC.buffImmune[BuffID.CursedInferno] = true;
            NPC.buffImmune[BuffID.Poisoned] = true;
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
            NPC.lifeMax = 39000 + 5000 * numPlayers;
            NPC.damage = (int)(NPC.damage * 0.7f);
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            name = "Marble Gargoyle";
            potionType = ItemID.GreaterHealingPotion;
        }

        int HealthPrc = 100;

        public override void AI()
        {
            if (Timer > 0)
            {
                Timer--;
            }
            if (Main.netMode != NetmodeID.MultiplayerClient && Main.GameUpdateCount % 180 == 0 && HealthPrc < 67)
            {
                if (HealthPrc < 34)
                {
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center + new Vector2(Main.rand.Next(-100, 100), Main.rand.Next(-100, 100)), new Vector2(0f, 0f), ModContent.ProjectileType<MarbleShard>(), NPC.damage / 4, 1f, 0, 3f, 0f);
                }
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center + new Vector2(Main.rand.Next(-100, 100), Main.rand.Next(-100, 100)), new Vector2(0f, 0f), ModContent.ProjectileType<MarbleShard>(), NPC.damage / 4, 1f, 0, 3f, 0f);
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
                    dust = Main.dust[Terraria.Dust.NewDust(position, NPC.width, NPC.height, DustID.Stone, 0f, -5f, 0, new Color(255, 255, 255), 2.302632f)];
                    dust = Main.dust[Terraria.Dust.NewDust(position, NPC.width, NPC.height, DustID.PlatinumCoin, 0f, -5f, 0, new Color(255, 255, 255), 2.302632f)];
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
                        dust = Main.dust[Terraria.Dust.NewDust(NPC.Center, 10, 10, DustID.Stone, spede.X, spede.Y, 0, new Color(255, 255, 255), 2.960526f)];
                        dust.noGravity = true;
                        dust.fadeIn = 0.9868421f;
                        dust = Main.dust[Terraria.Dust.NewDust(NPC.Center, 10, 10, DustID.PlatinumCoin, spede.X, spede.Y, 0, new Color(255, 255, 255), 2.960526f)];
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
                float speed = 5.5f;
                float acceleration = 0.09f;
                if (HealthPrc < 60)
                {
                    speed = 6f;
                    acceleration = 0.1f;
                }
                if (HealthPrc < 30)
                {
                    speed = 6.5f;
                    acceleration = 0.12f;
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
                    NPC.velocity *= 0f;
                    if (i == 0)
                    {
                        Timer = 120;
                    }
                    else if (i == 1)
                    {
                        Timer = 70;
                    }
                    else if (i == 2)
                    {
                        Timer = 90;
                    }
                    else if (i == 3)
                    {
                        Vector2 veloc = target.Center - NPC.Center;
                        veloc.Normalize();
                        veloc = veloc.RotatedBy(MathHelper.ToRadians(Main.rand.Next(-15, 15)));
                        veloc *= 17f;
                        NPC.velocity = veloc;
                        Timer = 90;
                    }
                    State = Attack;
                    NPC.netUpdate = true;
                }
                if (Main.netMode != NetmodeID.MultiplayerClient && NPC.frameCounter == 24)
                {
                    Vector2 veloc = target.Center - NPC.Center;
                    veloc.Normalize();
                    veloc = veloc.RotatedBy(MathHelper.ToRadians(Main.rand.Next(-15, 15)));
                    veloc *= 22f;
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, veloc, ModContent.ProjectileType<MarbleShard>(), NPC.damage / 4, 1f);
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
                int CD = 120;
                if (AttackNum == 0)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient && Timer % 20 == 0)
                    {
                        Vector2 spawnpos = target.Center + new Vector2(0, -300);
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), spawnpos, new Vector2(0, 0.001f), ModContent.ProjectileType<MarbleShard>(), NPC.damage / 4, 1f, 0, 1f, 0f);
                    }                 
                }
                else if (AttackNum == 1)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient && Timer % 45 == 0)
                    {
                        Vector2 veloc = target.Center - NPC.Center;
                        veloc.Normalize();
                        veloc = veloc.RotatedBy(MathHelper.ToRadians(Main.rand.Next(-8, 8)));
                        veloc *= 16f;
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, veloc, ModContent.ProjectileType<StoneBeam>(), NPC.damage / 4, 1f);
                        if (HealthPrc < 50)
                        {
                            veloc = veloc.RotatedBy(MathHelper.ToRadians(Main.rand.Next(-8, 8)));
                            Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, veloc, ModContent.ProjectileType<StoneBeam>(), NPC.damage / 4, 1f);
                        }
                    }
                }
                else if (AttackNum == 2)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient && Timer % 30 == 0)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            Vector2 spawnpos = target.Center + new Vector2(Main.rand.Next(-300, 300), 500);
                            Projectile.NewProjectile(NPC.GetSource_FromAI(), spawnpos, new Vector2(0, 0.001f), ModContent.ProjectileType<MarbleShard>(), NPC.damage / 4, 1f, 0, 2f, 0f);
                        }                        
                    }
                }
                else if (AttackNum == 3)
                {
                    Dust dust;
                    Vector2 position = NPC.position;
                    dust = Main.dust[Terraria.Dust.NewDust(position, NPC.width, NPC.height, DustID.PlatinumCoin, 0f, 0f, 0, new Color(255, 255, 255), 1f)];
                    dust.noGravity = true;
                    if (Main.netMode != NetmodeID.MultiplayerClient && HealthPrc < 50 && Timer % 10 == 0)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            Vector2 spawnpos = NPC.Center;
                            Projectile.NewProjectile(NPC.GetSource_FromAI(), spawnpos, new Vector2(0, 0.001f), ModContent.ProjectileType<MarbleShard>(), NPC.damage / 4, 1f, 0, 4f, 0f);
                        }
                    }
                    if (Timer < 20)
                    {
                        NPC.velocity *= 0.985f;
                    }
                }
                if (Timer <= 0)
                {
                    State = Move;
                    int i = 0;
                    if (HealthPrc < 50)
                    {
                        i = (int)(CD / 10f);
                    }
                    Timer = CD - (i * 2);
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
            var leadingCondition = new LeadingConditionRule(new SlayerBossFirstKillCondition(4));
            int itemType = ModContent.ItemType<Items.SlayerToken>();
            var parameters = new DropOneByOne.Parameters()
            {
                ChanceNumerator = 1,
                ChanceDenominator = 1,
                MinimumStackPerChunkBase = 1,
                MaximumStackPerChunkBase = 10,
                MinimumItemDropsCount = 100,
                MaximumItemDropsCount = 100,
            };
            leadingCondition.OnSuccess(new DropOneByOne(itemType, parameters));
            npcLoot.Add(leadingCondition);
        }
        public override void OnKill()
        {
            if (OSRSworld.slayBossProgress < 4)
            {
                OSRSworld.slayBossProgress = 4;
            }
            for (int i = 0; i < 200; i++)
            {
                if (Main.npc[i].type == ModContent.NPCType<SlayerMasterChaeldar>())
                {
                    Main.npc[i].life = 1;
                    Main.npc[i].AddBuff(BuffID.ShadowFlame, 180);
                    Main.npc[i].netUpdate = true;
                }
            }
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.rotation = NPC.velocity.X * 0.05f;
            NPC.spriteDirection = NPC.direction;
            if (State != Attack)
            {
                NPC.frameCounter++;
                if (NPC.frameCounter < 8)
                {
                    NPC.frame.Y = 0 * frameHeight;
                }
                else if (NPC.frameCounter < 16)
                {
                    NPC.frame.Y = 1 * frameHeight;
                }
                else if (NPC.frameCounter < 24)
                {
                    NPC.frame.Y = 2 * frameHeight;
                }
                else if (NPC.frameCounter < 32)
                {
                    NPC.frame.Y = 3 * frameHeight;
                }
                else if (NPC.frameCounter < 40)
                {
                    NPC.frame.Y = 4 * frameHeight;
                }
                else
                {
                    NPC.frameCounter = 0;
                }
            }
            else
            {
                NPC.frameCounter++;
                if (NPC.frameCounter < 6)
                {
                    NPC.frame.Y = 1 * frameHeight;
                }
                else if (NPC.frameCounter < 12)
                {
                    NPC.frame.Y = 2 * frameHeight;
                }
                else if (NPC.frameCounter < 18)
                {
                    NPC.frame.Y = 3 * frameHeight;
                }
                else if (NPC.frameCounter < 24)
                {
                    NPC.frame.Y = 2 * frameHeight;
                }
                else
                {
                    NPC.frameCounter = 0;
                }
            }
        }
    }
}