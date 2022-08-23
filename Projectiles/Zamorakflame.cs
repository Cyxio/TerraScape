using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;

namespace OldSchoolRuneScape.Projectiles
{
    public class Zamorakflame : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zamorakflame");
            Main.projFrames[Projectile.type] = 5;
        }
        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 50;
            Projectile.aiStyle = -1;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Magic;
        }
        int i = 0;
        public override void AI()
        {
            Dust.NewDust(Projectile.BottomLeft, 32, 0, DustID.InfernoFork, 0, -5f);
            Lighting.AddLight(Projectile.position, new Vector3(0, 0, 0));
            i++;
            if (i == 5)
            {
                Projectile.frame++;
                i = 0;
                if (Projectile.frame > Main.projFrames[Projectile.type])
                {
                    Projectile.Kill();
                }
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.NextBool(6))
            {
                target.AddBuff(BuffID.OnFire, 300);
            }
        }
    }
}