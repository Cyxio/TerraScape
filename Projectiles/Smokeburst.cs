﻿using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace OldSchoolRuneScape.Projectiles
{
    public class Smokeburst : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Smoke burst");
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
            Lighting.AddLight(projectile.position + projectile.velocity, new Vector3(128, 128, 128) * 0.005f);
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90);
            Dust.NewDustPerfect(projectile.Center, 31, -projectile.velocity * 0, 130, Color.LightGray, 0.7f);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.Next(3) == 0)
            {
                target.AddBuff(BuffID.Poisoned, 120);
            }
            if (projectile.ai[0] == 1 && Main.rand.Next(5) == 0)
            {
                target.AddBuff(BuffID.Venom, 180);
            }
        }
        public override void Kill(int timeLeft)
        {
            for(int i = 0; i < 10; i++)
            {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 31);
            }
        }
    }
}
