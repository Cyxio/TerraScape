using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

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
            projectile.hostile = true;
            projectile.width = 18;
            projectile.height = 18;
            projectile.scale = 1f;
            projectile.penetrate = 6;
            projectile.aiStyle = -1;
            projectile.tileCollide = false;
            projectile.timeLeft = 60;
            projectile.alpha = 120;
        }
        public override void AI()
        {
            projectile.rotation += MathHelper.ToRadians(projectile.velocity.X);
            projectile.velocity = projectile.velocity.RotatedBy(projectile.ai[0]);
            projectile.alpha += 2;
            Dust dust;
            Vector2 position = projectile.position;
            dust = Main.dust[Terraria.Dust.NewDust(position, 18, 18, 109, 0f, 0f, projectile.alpha, new Color(255, 255, 255), 1f)];
            dust.noGravity = true;
            dust.fadeIn = 1.2f;
        }
    }
    public class Banshee : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Banshee");
            Main.npcFrameCount[npc.type] = 3;
        }
        public override void SetDefaults()
        {
            npc.scale = 1f;
            npc.friendly = false;
            npc.lifeMax = 180;
            npc.defense = 10;
            npc.damage = 20;
            npc.knockBackResist = 0.4f;
            npc.noGravity = true;
            npc.width = 40;
            npc.height = 80;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath52;
            npc.value = 1000f;
            npc.aiStyle = -1;
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
            Vector2 shootpos = npc.Center + new Vector2(0, -30);
            Vector2 vel = targetpos - shootpos;
            vel.Normalize();
            float ai = Main.rand.NextFloat(-0.0015f, 0.0015f);
            Projectile.NewProjectile(shootpos, vel * 6f, ModContent.ProjectileType<BansheeProj>(), npc.damage / 4, 1f, 0, ai, 0);
            npc.ai[1] = 0;
        }
        public override void AI()
        {
            npc.TargetClosest(true);
            Player target = Main.player[npc.target];
            if (npc.ai[2] > 0)
            {
                npc.velocity *= 0f;
                if (npc.ai[2] % 4 == 0)
                {
                    Main.PlaySound(29, npc.Center, 97);
                    doAttack();
                }
                npc.ai[2]--;
            }
            else if (npc.ai[1] > 180)
            {
                npc.ai[2] = 45;
                npc.ai[1] = 0;
                targetpos = target.MountedCenter;
                npc.netUpdate = true;
            }
            else if (!Collision.CanHitLine(npc.position, 20, 20, target.position, target.width, target.height))
            {
                if (npc.ai[0] <= 0)
                {
                    Vector2 oldTarg = moveTarget;
                    for (int j = 100; j < 500; j+=10)
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            Vector2 newpos = target.position + new Vector2(j, 0).RotatedBy(MathHelper.ToRadians(360 / 8 * i));
                            if (Collision.CanHitLine(newpos, 20, 20, npc.position, npc.width, npc.height))
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
                    npc.ai[0] = 60;
                    npc.netUpdate = true;
                }
            }
            else
            {
                if (npc.ai[1] > 120)
                {
                    npc.ai[2] = 90;
                    npc.ai[1] = 0;
                    targetpos = target.MountedCenter;
                    npc.netUpdate = true;
                }
                moveTarget = target.MountedCenter;
            }
            npc.ai[0]--;
            npc.ai[1]++;
            Dust dust;
            // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
            Vector2 position = moveTarget;
            dust = Terraria.Dust.NewDustPerfect(position, 66, new Vector2(0f, 0f), 0, new Color(255, 255, 255), 1.776316f);
            dust.noGravity = true;
            Vector2 vel = moveTarget - npc.Center;
            vel.Normalize();
            vel *= 0.3f;
            npc.velocity += vel;
            if (npc.velocity.Length() > maxSpeed)
            {
                Vector2 vel2 = npc.velocity;
                vel2.Normalize();
                npc.velocity = vel2 * maxSpeed;
            }
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life > 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 7, 1f, 1f, 0, Color.Black);
                }
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 7, 1f, 1f, 0, Color.DarkOliveGreen);
                }
                for (int i = 1; i < 5; i++)
                {
                    Gore.NewGore(npc.position, npc.velocity * Main.rand.NextFloat(0.9f, 1.1f), mod.GetGoreSlot("Gores/Banshee" + i), npc.scale);
                }
            }
        }
        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = npc.direction;
            npc.rotation = npc.velocity.X * 0.04f;
            int frameSpeed = 7;
            npc.frameCounter++;
            if (npc.frameCounter < frameSpeed)
            {
                npc.frame.Y = 0;
            }
            else if (npc.frameCounter < frameSpeed * 2)
            {
                npc.frame.Y = frameHeight;
            }
            else if (npc.frameCounter < frameSpeed * 3)
            {
                npc.frame.Y = frameHeight * 2;
            }
            else if (npc.frameCounter < frameSpeed * 4)
            {
                npc.frame.Y = frameHeight;
            }
            else
            {
                npc.frameCounter = 0;
            }
        }
    }
}
