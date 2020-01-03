using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.NPCs.Slayer
{
    public class SpectreSummon : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Oozing Slime");
            Tooltip.SetDefault("Smells like death...");
        }
        public override void SetDefaults()
        {
            item.value = Item.buyPrice(0, 10, 0, 0);
            item.consumable = true;
            item.maxStack = 20;
            item.useStyle = 4;
            item.useTime = 45;
            item.useAnimation = 45;
            item.rare = 4;
        }
        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(ModContent.NPCType<AbhorrentSpectre>()) && !Main.dayTime;
        }
        public override bool UseItem(Player player)
        {
            NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<AbhorrentSpectre>());
            Main.PlaySound(SoundID.Roar, player.position, 0);
            return true;
        }
    }

    public class SpectreStationary : ModProjectile
    {
        public override string Texture { get { return "Terraria/Projectile_" + ProjectileID.ShadowBeamFriendly; } }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Poison Spawn");
        }
        public override void SetDefaults()
        {
            projectile.timeLeft = 540;
            projectile.alpha = 0;
        }
        public override void AI()
        {
            if (projectile.alpha == 0)
            {
                for (int i = 0; i < 60; i++)
                {
                    Dust dust;
                    // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                    Vector2 spede = new Vector2(0, 4);
                    spede = spede.RotatedBy(MathHelper.ToRadians(i * 6));
                    spede *= Main.rand.NextFloat(1f, 2f);
                    dust = Main.dust[Terraria.Dust.NewDust(projectile.Center, 6, 6, 163, spede.X, spede.Y, 0, new Color(255, 255, 255), 2f)];
                    dust.noGravity = true;
                    dust.fadeIn = 1f;
                }
                Main.PlaySound(SoundID.Item20, projectile.position);
                projectile.alpha = 255;
            }
            if (Main.rand.NextFloat() < 1f)
            {
                Dust dust;
                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                Vector2 position = projectile.position;
                dust = Main.dust[Terraria.Dust.NewDust(position, 6, 6, 163, 0f, 0f, 0, new Color(255, 255, 255), 2f)];
                dust.noGravity = true;
                dust.fadeIn = 1f;
            }
            if (projectile.ai[0] == 0)
            {
                projectile.ai[0] = Main.rand.Next(1, 4);
            }
            if (projectile.ai[0] == 1 && projectile.timeLeft < 480)
            {
                if (Main.netMode != 1 && projectile.timeLeft % 30 == 0)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        Vector2 spede = new Vector2(0, 3);
                        spede = spede.RotatedBy(MathHelper.ToRadians(i * 120));
                        spede = spede.RotateRandom(MathHelper.ToRadians(120));
                        spede *= Main.rand.NextFloat(1f, 1.3f);
                        Projectile.NewProjectile(projectile.Center, spede, ModContent.ProjectileType<SpectreSpore>(), projectile.damage, 1f);
                    }
                }
            }
            if (projectile.ai[0] == 2 && projectile.timeLeft < 480)
            {
                if (Main.netMode != 1 && projectile.timeLeft % 15 == 0)
                {
                    for (int i = 0; i < 1; i++)
                    {
                        Vector2 spede = new Vector2(0, 4);
                        spede = spede.RotateRandom(MathHelper.ToRadians(360));
                        spede *= Main.rand.NextFloat(1f, 2f);
                        Projectile.NewProjectile(projectile.Center, spede, ModContent.ProjectileType<SpectreCloud>(), projectile.damage, 1f);
                    }
                }
            }
            if (projectile.ai[0] == 3 && projectile.timeLeft < 480)
            {
                if (Main.netMode != 1 && projectile.timeLeft % 60 == 0)
                {
                    for (int i = 0; i < 1; i++)
                    {
                        Vector2 spede = new Vector2(0, 3);
                        spede = spede.RotateRandom(MathHelper.ToRadians(360));
                        spede *= Main.rand.NextFloat(1f, 1.3f);
                        Projectile.NewProjectile(projectile.Center, spede, ModContent.ProjectileType<SpectreOrb>(), projectile.damage, 1f);
                    }
                }
            }
        }
    }

    public class SpectreCloud : ModProjectile
    {
        public override string Texture { get { return "Terraria/Projectile_" + 228; } }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Poison Cloud");
            Main.projFrames[projectile.type] = 5;
        }
        public override void SetDefaults()
        {
            projectile.CloneDefaults(228);
            projectile.timeLeft = 360;
        }
        public override void AI()
        {
            projectile.hostile = true;
            projectile.friendly = false;
            projectile.velocity *= 0.985f;
            if (projectile.timeLeft < 120)
            {
                projectile.alpha += 2;
            }
        }
    }

    public class SpectreSpore : ModProjectile
    {
        public override string Texture { get { return "Terraria/Projectile_" + 568; } }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Poison Spore");
        }
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.timeLeft = 170;
            projectile.hostile = true;
            projectile.friendly = false;
        }
        public override void AI()
        {
            projectile.alpha += 2;
            projectile.rotation += projectile.velocity.Length() * 0.01f;
            projectile.velocity *= 0.98f;
        }
    }

    public class SpectreOrb : ModProjectile
    {
        public override string Texture { get { return "Terraria/Projectile_" + ProjectileID.ChlorophyteOrb; } }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Poison Orb");
            Main.projFrames[projectile.type] = 4;
        }
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.ChlorophyteOrb);
            projectile.timeLeft = 300;
        }
        Player target = null;
        public override void AI()
        {
            projectile.hostile = true;
            projectile.friendly = false;
            if (target == null)
            {
                for (int i = 0; i < 200; i++)
                {
                    if (Main.player[i].active && Vector2.Distance(projectile.position, Main.player[i].position) < 750f)
                    {
                        target = Main.player[i];
                    }
                }
            }
            if (target != null)
            {
                Vector2 toTarg = target.MountedCenter - projectile.Center;
                toTarg.Normalize();
                projectile.velocity += toTarg * 0.1f;
            }
            if (projectile.velocity.Length() > 6f)
            {
                Dust dust;
                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                Vector2 position = projectile.position;
                dust = Main.dust[Terraria.Dust.NewDust(position, projectile.width, projectile.height, 163, 0f, 0f, 0, new Color(255, 255, 255), 2f)];
                dust.noGravity = true;
            }
            if (projectile.velocity.Length() > 7f)
            {
                projectile.velocity *= 0.95f;
            }
        }
    }

    [AutoloadBossHead]
    public class AbhorrentSpectre : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Abhorrent Spectre");
            Main.npcFrameCount[npc.type] = 4;
        }
        public override void SetDefaults()
        {
            npc.width = 130;
            npc.height = 300;
            npc.aiStyle = -1;
            npc.npcSlots = 10f;
            npc.lavaImmune = true;
            npc.damage = 40;
            npc.defense = 20;
            npc.lifeMax = 9000;
            npc.knockBackResist = 0f;
            npc.boss = true;
            npc.noGravity = false;
            npc.noTileCollide = false;
            npc.HitSound = SoundID.NPCHit9;
            npc.DeathSound = SoundID.NPCDeath23;
            npc.value = Item.buyPrice(0, 10, 0, 0) / 2.5f;
            npc.buffImmune[BuffID.Confused] = true;
            music = OldSchoolRuneScape.slayerMusic;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 12000 + 2000 * numPlayers;
            npc.damage = (int)(npc.damage * 0.7f);
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            name = "Abhorrent Spectre";
            potionType = ItemID.GreaterHealingPotion;
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

        public float speed = 3f;
        public float accelerate = 0.05f;
        public bool halfHealth = false;

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

        public override void AI()
        {
            if (Timer > 0)
            {
                Timer--;
            }
            npc.TargetClosest(true);
            Player target = Main.player[npc.target];
            int HealthPrc = (int)(100f * (npc.life / (float)npc.lifeMax));
            if (npc.ai[3] > 0f)
            {
                State = 4;
                npc.alpha++;
                npc.velocity *= 0.96f;
                npc.ai[3]++;
                if (npc.ai[3] % 20 == 0)
                {
                    Main.PlaySound(npc.HitSound);
                    if (OSRSworld.slayBossProgress < 3)
                    {
                        Item.NewItem(npc.position, new Vector2(npc.width, npc.height), ModContent.ItemType<Items.SlayerToken>(), 10);
                    }
                }
                if (Main.rand.NextFloat() < 1f)
                {
                    Dust dust;
                    // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                    Vector2 position = npc.position;
                    dust = Main.dust[Terraria.Dust.NewDust(position, npc.width, npc.height, 163, 0f, -5f, 0, new Color(255, 255, 255), 2.302632f)];
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
                        dust = Main.dust[Terraria.Dust.NewDust(npc.Center, 10, 10, 163, spede.X, spede.Y, 0, new Color(255, 255, 255), 2.960526f)];
                        dust.noGravity = true;
                        dust.fadeIn = 0.9868421f;
                    }
                    npc.life = 0;
                    npc.HitEffect(0, 0);
                    npc.checkDead();
                }
            }
            if (Vector2.Distance(target.Center, npc.Center) > 1200f && HealthPrc < 100)
            {
                if (Main.netMode != 1 && Timer % 20 == 0)
                {
                    Vector2 velo = new Vector2(0, 250);
                    velo = velo.RotateRandom(Math.PI * 2);
                    Projectile.NewProjectile(target.Center + velo, Vector2.Zero, ModContent.ProjectileType<SpectreOrb>(), npc.damage / 4, 1f);
                }
            }
            if (State == Move)
            {
                if (target.dead || !target.active || !npc.HasValidTarget || Main.dayTime)
                {
                    npc.velocity *= 0;
                    Timer = 85;
                    State = Flee;
                    npc.netUpdate = true;
                }
                if ((target.Top.Y > npc.Bottom.Y) && npc.collideY) //platforms
                {
                    npc.position.Y++;
                }
                if (npc.collideX)
                {
                    npc.velocity.Y = -4f;
                }
                if (HealthPrc < 60)
                {
                    speed = 3.5f;
                    accelerate = 0.055f;
                }
                if (HealthPrc < 30)
                {
                    speed = 4f;
                    accelerate = 0.06f;
                }
                Vector2 toTarget = target.MountedCenter - npc.Center;
                toTarget.Normalize();
                toTarget *= (speed * accelerate);
                toTarget.Y = 0;
                npc.velocity += toTarget;
                if (npc.velocity.Length() > speed)
                {
                    npc.velocity *= 0.95f;
                }
                if (Timer <= 0 && HealthPrc < 100 && Main.netMode != 1)
                {
                    int i = Main.rand.Next(4);
                    if (HealthPrc <= 50 && !halfHealth)
                    {
                        halfHealth = true;
                        i = 4;
                    }
                    AttackNum = i;
                    if (i == 0)
                    {
                        //Main.NewText("orb");
                        Timer = 90;
                    }
                    else if (i == 1)
                    {
                        //Main.NewText("spore");
                        Timer = 90;
                    }
                    else if (i == 2)
                    {
                        //Main.NewText("cloud");
                        Timer = 90;
                    }
                    else if (i == 3)
                    {
                        //Main.NewText("stationary");
                        Timer = 90;
                    }
                    else if (i == 4)
                    {
                        //Main.NewText("Half Health");
                        Timer = 600;
                        npc.dontTakeDamage = true;
                    }
                    State = Attack;
                    npc.netUpdate = true;
                }
            }
            if (State == Attack)
            {
                int CD = 60;
                npc.velocity *= 0.985f;
                if (AttackNum == 0)
                {
                    if (Timer % 45 == 0)
                    {
                        Main.PlaySound(SoundID.Item20, npc.Center);
                        if (Main.netMode != 1)
                        {
                            Vector2 velo = target.Center - npc.Center;
                            velo.Normalize();
                            Projectile.NewProjectile(npc.Center, velo * 5f, ModContent.ProjectileType<SpectreOrb>(), npc.damage / 4, 1f);
                        }
                    }
                }
                if (AttackNum == 1)
                {
                    if (Timer % 20 == 0)
                    {
                        Main.PlaySound(SoundID.Item20, npc.Center);
                        if (Main.netMode != 1)
                        {
                            for (int i = 0; i < 6; i++)
                            {
                                Vector2 velo = target.Center - npc.Center;
                                velo.Normalize();
                                velo = velo.RotatedBy(MathHelper.ToRadians(Main.rand.Next(-30, 30)));
                                Projectile.NewProjectile(npc.Center, velo * 17f, ModContent.ProjectileType<SpectreSpore>(), npc.damage / 4, 1f);
                            }
                        }
                    }
                    CD = 120;
                }
                if (AttackNum == 2)
                {
                    if (Timer % 50 == 0)
                    {
                        Main.PlaySound(SoundID.Item20, npc.Center);
                        if (Main.netMode != 1)
                        {
                            for (int i = 0; i < 12; i++)
                            {
                                Vector2 spede = new Vector2(0, 10);
                                spede = spede.RotatedBy(MathHelper.ToRadians(i * 30));
                                spede *= Main.rand.NextFloat(1f, 1.5f);
                                Projectile.NewProjectile(npc.Center, spede, ModContent.ProjectileType<SpectreCloud>(), npc.damage / 4, 1f);
                            }
                        }
                    }
                }
                if (AttackNum == 3 || HealthPrc < 20)
                {
                    if (Timer == 45)
                    {
                        Main.PlaySound(SoundID.Item20, npc.Center);
                        if (Main.netMode != 1)
                        {
                            Vector2 spede = new Vector2(0, 200);
                            spede = spede.RotateRandom(Math.PI * 2);
                            Projectile.NewProjectile(target.MountedCenter + spede, Vector2.Zero, ModContent.ProjectileType<SpectreStationary>(), npc.damage / 4, 1f);
                        }
                        Timer = 0;
                    }
                    CD = 90;
                }
                if (AttackNum == 4)
                {
                    if (Timer % 100 == 0)
                    {
                        Main.PlaySound(SoundID.Item20, npc.Center);
                        if (Main.netMode != 1)
                        {
                            Vector2 spede = new Vector2(0, 200);
                            spede = spede.RotateRandom(Math.PI * 2);
                            Projectile.NewProjectile(target.MountedCenter + spede, Vector2.Zero, ModContent.ProjectileType<SpectreStationary>(), npc.damage / 4, 1f);
                        }
                    }
                    else if (Timer % 100 == 25)
                    {
                        Main.PlaySound(SoundID.Item20, npc.Center);
                        if (Main.netMode != 1)
                        {
                            for (int i = 0; i < 12; i++)
                            {
                                Vector2 spede = new Vector2(0, 10);
                                spede = spede.RotatedBy(MathHelper.ToRadians(i * 30));
                                spede *= Main.rand.NextFloat(1f, 1.5f);
                                Projectile.NewProjectile(npc.Center, spede, ModContent.ProjectileType<SpectreCloud>(), npc.damage / 4, 1f);
                            }
                        }
                    }
                    else if (Timer % 100 == 50)
                    {
                        Main.PlaySound(SoundID.Item20, npc.Center);
                        if (Main.netMode != 1)
                        {
                            for (int i = 0; i < 6; i++)
                            {
                                Vector2 velo = target.Center - npc.Center;
                                velo.Normalize();
                                velo = velo.RotatedBy(MathHelper.ToRadians(Main.rand.Next(-30, 30)));
                                Projectile.NewProjectile(npc.Center, velo * 17f, ModContent.ProjectileType<SpectreSpore>(), npc.damage / 4, 1f);
                            }
                        }
                    }
                    else if (Timer % 100 == 75)
                    {
                        Main.PlaySound(SoundID.Item20, npc.Center);
                        if (Main.netMode != 1)
                        {
                            Vector2 velo = target.Center - npc.Center;
                            velo.Normalize();
                            Projectile.NewProjectile(npc.Center, velo * 5f, ModContent.ProjectileType<SpectreOrb>(), npc.damage / 4, 1f);
                        }
                    }
                    CD = 180;
                }
                if (Timer <= 0)
                {
                    State = Move;
                    npc.dontTakeDamage = false;
                    int i = 0;
                    if (HealthPrc < 50)
                    {
                        i = (int)(CD / 10f);
                    }
                    Timer = CD - (i * 3);
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
        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = npc.direction;
            npc.frame = new Rectangle(0, 0, 150, 313);
            if (State == Attack)
            {
                npc.frame.X = 150;
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
                else
                {
                    npc.frameCounter = 0;
                }
            }
            else
            {
                float amount = npc.velocity.X / speed;    
                npc.frameCounter += Math.Abs(amount);
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

        public override void NPCLoot()
        {
            if (OSRSworld.slayBossProgress < 3)
            {
                OSRSworld.slayBossProgress = 3;
            }
            for (int i = 0; i < 200; i++)
            {
                if (Main.npc[i].type == ModContent.NPCType<SlayerMasterVannaka>())
                {
                    Main.npc[i].life = 1;
                    Main.npc[i].AddBuff(BuffID.ShadowFlame, 180);
                    Main.npc[i].netUpdate = true;
                }
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            SpriteEffects e = SpriteEffects.None;
            if (npc.direction == 1)
            {
                e = SpriteEffects.FlipHorizontally;
            }
            double Sin = Math.Sin(MathHelper.ToRadians(360f * ((float)Main.time % 60f) / 59f));
            float scales = (float)(1.06f + (0.03f*Sin));
            Color c = npc.GetAlpha(Color.White);
            spriteBatch.Draw(mod.GetTexture("NPCs/Slayer/AbhorrentSpectreBG"), npc.Center - Main.screenPosition, npc.frame, Color.White, npc.rotation, new Vector2(75, 156), scales, e, 0f);
            spriteBatch.Draw(Main.npcTexture[npc.type], npc.Center - Main.screenPosition, npc.frame, c, npc.rotation, new Vector2(75, 156), npc.scale, e, 0f);
            if (Vector2.Distance(Main.player[npc.target].Center, npc.Center) > 1200f && npc.life < npc.lifeMax)
            {
                Vector2 endPoint = Main.player[npc.target].Center;
                Vector2 unit = endPoint - npc.Center;
                float length = unit.Length();
                unit.Normalize();
                for (float k = 0; k <= length; k += 8f)
                {
                    Vector2 drawPos = npc.Center + unit * k - Main.screenPosition;
                    Color alpha = Color.White;
                    spriteBatch.Draw(mod.GetTexture("NPCs/Slayer/AbhorrentSpectreBG"), drawPos, new Rectangle(66, 4, 4, 4), alpha, k, new Vector2(2, 2), 1f, SpriteEffects.None, 0f);
                }
            }
            return false;
        }
    }
}
