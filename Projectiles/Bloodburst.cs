﻿using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace OldSchoolRuneScape.Projectiles
{
    public class Bloodburst : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blood burst");
        }
        public override void SetDefaults()
        {
            projectile.aiStyle = -1;
            projectile.width = 14;
            projectile.height = 14;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.tileCollide = true;
            projectile.penetrate = 1;
            projectile.ai[0] = 0;
            projectile.extraUpdates = 1;
            projectile.scale = 0.75f;
            projectile.timeLeft = 180;
        }
        public override void AI()
        {
            if (projectile.ai[1] > 0)
            {
                projectile.tileCollide = false;
                projectile.ai[1]--;
            }
            else
            {
                projectile.tileCollide = true;
            }
            Lighting.AddLight(projectile.position + projectile.velocity, new Vector3(135, 1, 1) * 0.005f);
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90);
            Dust.NewDustPerfect(projectile.Center, 31, -projectile.velocity * 0, 130, Color.DarkRed, 0.7f);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.Next(8) == 0)
            {
                Main.player[projectile.owner].HealEffect(damage / (int)(10 + projectile.ai[0] * 5));
                Main.player[projectile.owner].statLife += (damage / (int)(10 + projectile.ai[0] * 5));
            }
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 31, 0, 0, 0, Color.DarkRed, 1);
            }
        }
    }
}
