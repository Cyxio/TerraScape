using System;
using Microsoft.Xna.Framework;
using Terraria;
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
            item.value = Item.buyPrice(0, 20, 0, 0);
            item.consumable = true;
            item.maxStack = 20;
            item.useStyle = 4;
            item.useTime = 45;
            item.useAnimation = 45;
            item.rare = 7;
        }
        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(ModContent.NPCType<MarbleGargoyle>()) && !Main.dayTime;
        }
        public override bool UseItem(Player player)
        {
            NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<MarbleGargoyle>());
            Main.PlaySound(SoundID.Roar, player.position, 0);
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
            projectile.hostile = true;
            projectile.friendly = false;
            projectile.height = 16;
            projectile.width = 16;
            aiType = -1;
            projectile.tileCollide = true;
            projectile.timeLeft = 600;
        }
        public override void AI()
        {
            if (projectile.timeLeft == 600)
            {
                int ai = (int)projectile.ai[0];
                switch (ai)
                {
                    case 0:
                        Main.PlaySound(SoundID.Item118);
                        break;
                    case 1:
                        projectile.alpha = 128;
                        break;
                    case 2:
                        Main.PlaySound(SoundID.Item20);
                        break;
                    default:
                        break;
                }
            }
            if (projectile.ai[0] == 0)
            {
                projectile.rotation = projectile.velocity.ToRotation() + (float)(Math.PI / 2f);
            }
            if (projectile.ai[0] == 1)
            {
                if (projectile.timeLeft == 584)
                {
                    Main.PlaySound(SoundID.Item118);
                    projectile.velocity = new Vector2(0, 22f);
                }
                if (projectile.timeLeft >= 584)
                {
                    projectile.rotation -= MathHelper.ToRadians(270/16);
                }
                else
                {
                    projectile.rotation = projectile.velocity.ToRotation() + (float)(Math.PI / 2f);
                }
                projectile.alpha -= 8;
            }
            if (projectile.ai[0] == 2)
            {
                Dust dust;
                Vector2 position = projectile.position;
                dust = Main.dust[Terraria.Dust.NewDust(position, 16, 46, 247, 0f, 0f, 0, new Color(255, 255, 255), 1f)];
                dust.noGravity = true;
                projectile.height = 46;
                projectile.rotation = 0;
                if (projectile.timeLeft > 550)
                {
                    projectile.timeLeft = Main.rand.Next(475, 550);
                }
                if (projectile.timeLeft >= 475)
                {
                    projectile.velocity = new Vector2(0, -12);
                    projectile.tileCollide = false;
                }
                else
                {
                    projectile.tileCollide = true;
                    projectile.velocity = new Vector2(0, 1.5f * (float)Math.Sin(MathHelper.ToRadians(projectile.timeLeft % 60 * 6)));
                }
            }
            if (projectile.ai[0] == 3)
            {
                Dust dust;
                Vector2 position = projectile.Center;
                Vector2 unit = projectile.velocity;
                unit.Normalize();
                dust = Terraria.Dust.NewDustPerfect(position - unit * 36f, 247, new Vector2(0f, 0f), 0, new Color(255, 255, 255), 1f);
                dust.noGravity = true;
                projectile.rotation = projectile.velocity.ToRotation() + (float)(Math.PI / 2f);
                for (int i = 0; i < 200; i++)
                {
                    if (Main.npc[i].type == ModContent.NPCType<MarbleGargoyle>())
                    {
                        Vector2 veloc = Main.npc[i].Center - projectile.Center;
                        veloc.Normalize();
                        projectile.velocity += veloc*0.5f;
                    }
                }
                if (projectile.velocity.Length() > 16f)
                {
                    projectile.velocity *= 0.985f;
                }
            }
            if (projectile.ai[0] == 4)
            {
                projectile.ai[0] = 2;
                projectile.timeLeft = 450;
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
            projectile.hostile = true;
            projectile.friendly = false;
            projectile.height = 16;
            projectile.width = 16;
            aiType = -1;
            projectile.tileCollide = true;
            projectile.timeLeft = 300;
            projectile.alpha = 255;
        }
        public override void AI()
        {
            Dust dust;
            // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
            Vector2 position = projectile.Center;
            dust = Terraria.Dust.NewDustPerfect(position, 70, new Vector2(0f, 0f), 0, new Color(255, 255, 255), 2.105263f);
            dust.noGravity = true;
            projectile.damage = 1;
            if (projectile.ai[0] == 0)
            {
                projectile.ai[0] = MathHelper.ToRadians(Main.rand.Next(-30, 30));
            }
            projectile.velocity += projectile.velocity.RotatedBy(projectile.ai[0] / 60f) - projectile.oldVelocity;
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            Vector2 v2 = projectile.velocity;
            v2.Normalize();
            float point = 0f;
            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), projectile.Center,
                    projectile.Center - v2 * 230f, 16, ref point);
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
            Main.npcFrameCount[npc.type] = 5;
        }
        public override void SetDefaults()
        {
            npc.width = 100;
            npc.height = 180;
            npc.aiStyle = -1;
            npc.npcSlots = 20f;
            npc.lavaImmune = true;
            npc.damage = 80;
            npc.defense = 35;
            npc.lifeMax = 23000;
            npc.knockBackResist = 0f;
            npc.boss = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit7;
            npc.DeathSound = SoundID.NPCDeath5;
            npc.value = Item.buyPrice(0, 20, 0, 0) / 2.5f;
            npc.buffImmune[BuffID.Confused] = true;
            npc.buffImmune[BuffID.OnFire] = true;
            npc.buffImmune[BuffID.Ichor] = true;
            npc.buffImmune[BuffID.Frostburn] = true;
            npc.buffImmune[BuffID.CursedInferno] = true;
            npc.buffImmune[BuffID.Poisoned] = true;
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
            npc.lifeMax = 39000 + 5000 * numPlayers;
            npc.damage = (int)(npc.damage * 0.7f);
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
            if (Main.netMode != 1 && Main.GameUpdateCount % 180 == 0 && HealthPrc < 67)
            {
                if (HealthPrc < 34)
                {
                    Projectile.NewProjectile(npc.Center + new Vector2(Main.rand.Next(-100, 100), Main.rand.Next(-100, 100)), new Vector2(0f, 0f), ModContent.ProjectileType<MarbleShard>(), npc.damage / 4, 1f, 0, 3f, 0f);
                }
                Projectile.NewProjectile(npc.Center + new Vector2(Main.rand.Next(-100, 100), Main.rand.Next(-100, 100)), new Vector2(0f, 0f), ModContent.ProjectileType<MarbleShard>(), npc.damage / 4, 1f, 0, 3f, 0f);
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
                    if (OSRSworld.slayBossProgress < 4)
                    {
                        Item.NewItem(npc.position, new Vector2(npc.width, npc.height), ModContent.ItemType<Items.SlayerToken>(), 20);
                    }
                }
                if (Main.rand.NextFloat() < 1f)
                {
                    Dust dust;
                    // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                    Vector2 position = npc.position;
                    dust = Main.dust[Terraria.Dust.NewDust(position, npc.width, npc.height, 1, 0f, -5f, 0, new Color(255, 255, 255), 2.302632f)];
                    dust = Main.dust[Terraria.Dust.NewDust(position, npc.width, npc.height, 247, 0f, -5f, 0, new Color(255, 255, 255), 2.302632f)];
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
                        dust = Main.dust[Terraria.Dust.NewDust(npc.Center, 10, 10, 1, spede.X, spede.Y, 0, new Color(255, 255, 255), 2.960526f)];
                        dust.noGravity = true;
                        dust.fadeIn = 0.9868421f;
                        dust = Main.dust[Terraria.Dust.NewDust(npc.Center, 10, 10, 247, spede.X, spede.Y, 0, new Color(255, 255, 255), 2.960526f)];
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
                    npc.velocity *= 0f;
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
                        Vector2 veloc = target.Center - npc.Center;
                        veloc.Normalize();
                        veloc = veloc.RotatedBy(MathHelper.ToRadians(Main.rand.Next(-15, 15)));
                        veloc *= 17f;
                        npc.velocity = veloc;
                        Timer = 90;
                    }
                    State = Attack;
                    npc.netUpdate = true;
                }
                if (Main.netMode != 1 && npc.frameCounter == 24)
                {
                    Vector2 veloc = target.Center - npc.Center;
                    veloc.Normalize();
                    veloc = veloc.RotatedBy(MathHelper.ToRadians(Main.rand.Next(-15, 15)));
                    veloc *= 22f;
                    Projectile.NewProjectile(npc.Center, veloc, ModContent.ProjectileType<MarbleShard>(), npc.damage / 4, 1f);
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
                int CD = 120;
                if (AttackNum == 0)
                {
                    if (Main.netMode != 1 && Timer % 20 == 0)
                    {
                        Vector2 spawnpos = target.Center + new Vector2(0, -300);
                        Projectile.NewProjectile(spawnpos, new Vector2(0, 0.001f), ModContent.ProjectileType<MarbleShard>(), npc.damage / 4, 1f, 0, 1f, 0f);
                    }                 
                }
                else if (AttackNum == 1)
                {
                    if (Main.netMode != 1 && Timer % 45 == 0)
                    {
                        Vector2 veloc = target.Center - npc.Center;
                        veloc.Normalize();
                        veloc = veloc.RotatedBy(MathHelper.ToRadians(Main.rand.Next(-8, 8)));
                        veloc *= 16f;
                        Projectile.NewProjectile(npc.Center, veloc, ModContent.ProjectileType<StoneBeam>(), npc.damage / 4, 1f);
                        if (HealthPrc < 50)
                        {
                            veloc = veloc.RotatedBy(MathHelper.ToRadians(Main.rand.Next(-8, 8)));
                            Projectile.NewProjectile(npc.Center, veloc, ModContent.ProjectileType<StoneBeam>(), npc.damage / 4, 1f);
                        }
                    }
                }
                else if (AttackNum == 2)
                {
                    if (Main.netMode != 1 && Timer % 30 == 0)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            Vector2 spawnpos = target.Center + new Vector2(Main.rand.Next(-300, 300), 500);
                            Projectile.NewProjectile(spawnpos, new Vector2(0, 0.001f), ModContent.ProjectileType<MarbleShard>(), npc.damage / 4, 1f, 0, 2f, 0f);
                        }                        
                    }
                }
                else if (AttackNum == 3)
                {
                    Dust dust;
                    Vector2 position = npc.position;
                    dust = Main.dust[Terraria.Dust.NewDust(position, npc.width, npc.height, 247, 0f, 0f, 0, new Color(255, 255, 255), 1f)];
                    dust.noGravity = true;
                    if (Main.netMode != 1 && HealthPrc < 50 && Timer % 10 == 0)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            Vector2 spawnpos = npc.Center;
                            Projectile.NewProjectile(spawnpos, new Vector2(0, 0.001f), ModContent.ProjectileType<MarbleShard>(), npc.damage / 4, 1f, 0, 4f, 0f);
                        }
                    }
                    if (Timer < 20)
                    {
                        npc.velocity *= 0.985f;
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
            npc.rotation = npc.velocity.X * 0.05f;
            npc.spriteDirection = npc.direction;
            if (State != Attack)
            {
                npc.frameCounter++;
                if (npc.frameCounter < 8)
                {
                    npc.frame.Y = 0 * frameHeight;
                }
                else if (npc.frameCounter < 16)
                {
                    npc.frame.Y = 1 * frameHeight;
                }
                else if (npc.frameCounter < 24)
                {
                    npc.frame.Y = 2 * frameHeight;
                }
                else if (npc.frameCounter < 32)
                {
                    npc.frame.Y = 3 * frameHeight;
                }
                else if (npc.frameCounter < 40)
                {
                    npc.frame.Y = 4 * frameHeight;
                }
                else
                {
                    npc.frameCounter = 0;
                }
            }
            else
            {
                npc.frameCounter++;
                if (npc.frameCounter < 6)
                {
                    npc.frame.Y = 1 * frameHeight;
                }
                else if (npc.frameCounter < 12)
                {
                    npc.frame.Y = 2 * frameHeight;
                }
                else if (npc.frameCounter < 18)
                {
                    npc.frame.Y = 3 * frameHeight;
                }
                else if (npc.frameCounter < 24)
                {
                    npc.frame.Y = 2 * frameHeight;
                }
                else
                {
                    npc.frameCounter = 0;
                }
            }
        }
    }
}