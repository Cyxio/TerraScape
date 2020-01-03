using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace OldSchoolRuneScape.Projectiles
{
    public class Ballistaproj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ballista javelin");
        }
        public override void SetDefaults()
        {
            projectile.aiStyle = -1;
            projectile.penetrate = 1;
            projectile.width = 10;
            projectile.height = 10;
            projectile.timeLeft = 600;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 10;
        }
        NPC targett = null;
        List<NPC> targets = new List<NPC>();
        public override void AI()
        {            
            if (projectile.ai[0] == 0 || projectile.ai[0] == 1 || projectile.ai[0] == 3)
            {
                projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90);
                projectile.velocity.Y += 0.12f;
            }            
            if (projectile.ai[0] == 2 && targett != null)
            {
                if (!targett.active)
                {
                    projectile.ai[0] = 0;
                }
                projectile.velocity = Vector2.Zero;
                projectile.position += targett.velocity;
                if (projectile.timeLeft < 4 && projectile.friendly == false)
                {
                    Main.PlaySound(SoundID.Item62, projectile.position);
                    projectile.friendly = true;
                    for (int o = 0; o < 36; o++)
                    {
                        Vector2 vel = new Vector2(3).RotatedBy(MathHelper.ToRadians(10 * o));
                        int dust = Dust.NewDust(projectile.Center, 0, 0, 31, vel.X, vel.Y, 100);
                        Main.dust[dust].scale = 1.5f;
                        dust = Dust.NewDust(projectile.Center, 0, 0, 6, vel.X, vel.Y);
                        Main.dust[dust].scale = 1.5f;
                        Main.dust[dust].noGravity = true;
                    }
                }
            }
            if (projectile.ai[0] == 1 || projectile.ai[0] == 3)
            {
                int dust = Dust.NewDust(projectile.position, 0, 0, 60);
                Main.dust[dust].scale = 2.5f;
                Main.dust[dust].velocity *= 0f;
                Main.dust[dust].noGravity = true;
            }
            if (projectile.ai[0] == 3)
            {
                if (projectile.timeLeft > 300)
                {
                    projectile.timeLeft = 300;
                }
                projectile.localNPCHitCooldown = 90;
                projectile.penetrate = -1;
                foreach (NPC npc in targets)
                {
                    npc.position = projectile.position - new Vector2(npc.width / 2, npc.height / 2);
                    npc.netUpdate = true;
                }
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.ai[0] == 1)
            {
                projectile.velocity *= 0;
                projectile.penetrate = 10;
                projectile.friendly = false;
                projectile.timeLeft = 70;
                projectile.ai[0] = 2;
                targett = target;
            }
            if (projectile.ai[0] == 3)
            {
                targets.Add(target);
                projectile.velocity *= 0.98f;
            }
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Dig, projectile.position);
            targets.Clear();
        }
    }
}
