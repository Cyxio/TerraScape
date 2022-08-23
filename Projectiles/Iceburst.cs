﻿using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace OldSchoolRuneScape.Projectiles
{
    public class Iceburst : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ice burst");
        }
        public override void SetDefaults()
        {
            Projectile.aiStyle = -1;
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = true;
            Projectile.penetrate = 1;
            Projectile.ai[0] = 0;
            Projectile.extraUpdates = 1;
            Projectile.scale = 0.75f;
            Projectile.timeLeft = 180;
        }
        public override void AI()
        {
            if (Projectile.ai[1] > 0)
            {
                Projectile.tileCollide = false;
                Projectile.ai[1]--;
            }
            else
            {
                Projectile.tileCollide = true;
            }
            Lighting.AddLight(Projectile.position + Projectile.velocity, new Vector3(165, 165, 251) * 0.005f);
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90);
            Dust.NewDustPerfect(Projectile.Center, 31, -Projectile.velocity * 0, 130, Color.SkyBlue, 0.7f);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.NextBool(5))
            {
                target.AddBuff(BuffID.Frostburn, 120);
            }
            if (Projectile.ai[0] == 1 && Main.rand.NextBool(8))
            {
                target.velocity *= 0.5f;
                target.netUpdate = true;
            }
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, 0, 0, 0, Color.SkyBlue, 1);
            }
        }
    }
}
