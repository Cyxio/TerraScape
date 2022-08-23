using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace OldSchoolRuneScape.NPCs
{
    public class BansheeProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Scream");
        }
        public override void SetDefaults()
        {
            Projectile.hostile = true;
            Projectile.width = 18;
            Projectile.height = 18;
            Projectile.scale = 1f;
            Projectile.penetrate = 6;
            Projectile.aiStyle = -1;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 60;
            Projectile.alpha = 120;
        }
        public override void AI()
        {
            Projectile.rotation += MathHelper.ToRadians(Projectile.velocity.X);
            Projectile.velocity = Projectile.velocity.RotatedBy(Projectile.ai[0]);
            Projectile.alpha += 2;
            Dust dust;
            Vector2 position = Projectile.position;
            dust = Main.dust[Terraria.Dust.NewDust(position, 18, 18, DustID.Asphalt, 0f, 0f, Projectile.alpha, new Color(255, 255, 255), 1f)];
            dust.noGravity = true;
            dust.fadeIn = 1.2f;
        }
    }
    public class Banshee : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Banshee");
            Main.npcFrameCount[NPC.type] = 3;
        }
        public override void SetDefaults()
        {
            NPC.scale = 1f;
            NPC.friendly = false;
            NPC.lifeMax = 180;
            NPC.defense = 10;
            NPC.damage = 20;
            NPC.knockBackResist = 0.4f;
            NPC.noGravity = true;
            NPC.width = 40;
            NPC.height = 80;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath52;
            NPC.value = 1000f;
            NPC.aiStyle = -1;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return SpawnCondition.OverworldNightMonster.Chance * 0.05f + SpawnCondition.Cavern.Chance * 0.15f;
        }
        private float maxSpeed = 2.4f;
        Vector2 moveTarget;
        Vector2 targetpos;

        private void doAttack()
        {
            Vector2 shootpos = NPC.Center + new Vector2(0, -30);
            Vector2 vel = targetpos - shootpos;
            vel.Normalize();
            float ai = Main.rand.NextFloat(-0.0015f, 0.0015f);
            Projectile.NewProjectile(NPC.GetSource_FromAI(), shootpos, vel * 6f, ModContent.ProjectileType<BansheeProj>(), NPC.damage / 4, 1f, 0, ai, 0);
            NPC.ai[1] = 0;
        }
        public override void AI()
        {
            NPC.TargetClosest(true);
            Player target = Main.player[NPC.target];
            if (NPC.ai[2] > 0)
            {
                NPC.velocity *= 0f;
                if (NPC.ai[2] % 4 == 0)
                {
                    SoundEngine.PlaySound(SoundID.Zombie97, NPC.Center);
                    doAttack();
                }
                NPC.ai[2]--;
            }
            else if (NPC.ai[1] > 180)
            {
                NPC.ai[2] = 45;
                NPC.ai[1] = 0;
                targetpos = target.MountedCenter;
                NPC.netUpdate = true;
            }
            else if (!Collision.CanHitLine(NPC.position, 20, 20, target.position, target.width, target.height))
            {
                if (NPC.ai[0] <= 0)
                {
                    Vector2 oldTarg = moveTarget;
                    for (int j = 100; j < 500; j+=10)
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            Vector2 newpos = target.position + new Vector2(j, 0).RotatedBy(MathHelper.ToRadians(360 / 8 * i));
                            if (Collision.CanHitLine(newpos, 20, 20, NPC.position, NPC.width, NPC.height))
                            {
                                moveTarget = newpos;
                                break;
                            }
                            if (i == 7 && Main.netMode != NetmodeID.MultiplayerClient)
                            {
                                moveTarget = target.position + new Vector2(j, 0).RotatedBy(MathHelper.ToRadians(360 / 8 * Main.rand.Next(8)));
                            }
                        }
                        if (moveTarget != oldTarg)
                        {
                            break;
                        }
                    }
                    NPC.ai[0] = 60;
                    NPC.netUpdate = true;
                }
            }
            else
            {
                if (NPC.ai[1] > 120)
                {
                    NPC.ai[2] = 90;
                    NPC.ai[1] = 0;
                    targetpos = target.MountedCenter;
                    NPC.netUpdate = true;
                }
                moveTarget = target.MountedCenter;
            }
            NPC.ai[0]--;
            NPC.ai[1]++;
            Dust dust;
            // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
            Vector2 position = moveTarget;
            dust = Terraria.Dust.NewDustPerfect(position, 66, new Vector2(0f, 0f), 0, new Color(255, 255, 255), 1.776316f);
            dust.noGravity = true;
            Vector2 vel = moveTarget - NPC.Center;
            vel.Normalize();
            vel *= 0.3f;
            NPC.velocity += vel;
            if (NPC.velocity.Length() > maxSpeed)
            {
                Vector2 vel2 = NPC.velocity;
                vel2.Normalize();
                NPC.velocity = vel2 * maxSpeed;
            }
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                return;
            }
            if (NPC.life > 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.WoodFurniture, 1f, 1f, 0, Color.Black);
                }
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.WoodFurniture, 1f, 1f, 0, Color.DarkOliveGreen);
                }
                for (int i = 1; i < 5; i++)
                {
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * Main.rand.NextFloat(0.9f, 1.1f), Mod.Find<ModGore>("Banshee" + i).Type, NPC.scale);
                }
            }
        }
        public override void FindFrame(int frameHeight)
        {
            NPC.spriteDirection = NPC.direction;
            NPC.rotation = NPC.velocity.X * 0.04f;
            int frameSpeed = 7;
            NPC.frameCounter++;
            if (NPC.frameCounter < frameSpeed)
            {
                NPC.frame.Y = 0;
            }
            else if (NPC.frameCounter < frameSpeed * 2)
            {
                NPC.frame.Y = frameHeight;
            }
            else if (NPC.frameCounter < frameSpeed * 3)
            {
                NPC.frame.Y = frameHeight * 2;
            }
            else if (NPC.frameCounter < frameSpeed * 4)
            {
                NPC.frame.Y = frameHeight;
            }
            else
            {
                NPC.frameCounter = 0;
            }
        }
    }
}
