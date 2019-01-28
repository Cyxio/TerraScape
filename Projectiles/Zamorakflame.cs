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
            Main.projFrames[projectile.type] = 5;
        }
        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 50;
            projectile.aiStyle = -1;
            projectile.tileCollide = false;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.magic = true;
        }
        int i = 0;
        public override void AI()
        {
            Dust.NewDust(projectile.BottomLeft, 32, 0, 174, 0, -5f);
            Lighting.AddLight(projectile.position, new Vector3(0, 0, 0));
            i++;
            if (i == 5)
            {
                projectile.frame++;
                i = 0;
                if (projectile.frame > Main.projFrames[projectile.type])
                {
                    projectile.Kill();
                }
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.Next(6) == 0)
            {
                target.AddBuff(BuffID.OnFire, 300);
            }
        }
    }
}