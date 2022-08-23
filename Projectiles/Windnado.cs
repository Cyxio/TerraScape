using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace OldSchoolRuneScape.Projectiles
{
    public class Windnado : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wind Tornado");
        }
        public override string Texture => "OldSchoolRuneScape/Projectiles/Tormentedsoul";
        public override void SetDefaults()
        {
            Projectile.width = 44;
            Projectile.height = 44;
            Projectile.timeLeft = 51;
            Projectile.penetrate = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.aiStyle = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 15;
        }
        Vector3 x = new Vector3(150, 150, 150);
        public override void AI()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 1; j < 6; j++)
                {
                    Vector2 spawn = Projectile.Center + new Vector2(-4.5f*j, 0.5f*j*j).RotatedBy((Math.PI / 2 * i) + (MathHelper.TwoPi / 50 * Projectile.timeLeft));
                    Dust dust;
                    dust = Terraria.Dust.NewDustPerfect(spawn, 263, Vector2.Zero, (55 - Projectile.timeLeft)*4, new Color(255, 255, 255), 0.5f + (0.1f * j));
                    dust.noGravity = true;
                }  
            }
        }
        public override bool? CanDamage()/* tModPorter Suggestion: Return null instead of true */
        {
            return Projectile.timeLeft < 40;
        }
    }
}