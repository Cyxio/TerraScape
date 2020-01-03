using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.NPCs.Chaoselemental
{
    public class Chaostele : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chaotic teleport");
            Main.projFrames[projectile.type] = 8;
        }
        public override void SetDefaults()
        {
            projectile.hostile = true;
            projectile.width = 50;
            projectile.height = 50;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 1;
            projectile.penetrate = 1;
            projectile.aiStyle = -1;
            projectile.tileCollide = false;
            projectile.timeLeft = 200;
        }
        public override void AI()
        {
            projectile.damage = 0;
            projectile.rotation = projectile.velocity.ToRotation();
            Lighting.AddLight(projectile.Center + projectile.velocity, new Vector3(255 * 0.005f, 0, 0));
            if (Main.rand.Next(2) == 0)
            {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 164);
            }
            projectile.frameCounter++;
            if (projectile.frameCounter > 4)
            {
                projectile.frame++;
                if (projectile.frame >= Main.projFrames[projectile.type])
                {
                    projectile.frame = 0;
                }
                projectile.frameCounter = 0;
            }
            Player target = Main.player[0];
            for (int k = 0; k < Main.ActivePlayersCount; k++)
            {
                if (projectile.Distance(Main.player[k].position) < projectile.Distance(target.position))
                {
                    target = Main.player[k];
                }
            }
            if (projectile.ai[0] == 0)
            {
                float speedX = target.MountedCenter.X - projectile.Center.X;
                float speedY = target.MountedCenter.Y - projectile.Center.Y;
                Vector2 spd = new Vector2(speedX, speedY);
                spd.Normalize();
                projectile.velocity = spd * 15f;
                if (projectile.Distance(target.MountedCenter) < 100)
                {
                    projectile.ai[0] = 1;
                }
            }
            if (projectile.Colliding(projectile.Hitbox, target.Hitbox))
            {
                target.Teleport(new Vector2(target.position.X + Main.rand.Next(-700, 700), target.position.Y + Main.rand.Next(-500, 100)), 1, 0);
                projectile.active = false;
            }
        }
    }
}