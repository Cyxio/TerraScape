using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;

namespace OldSchoolRuneScape.Projectiles
{
    public class Guthixclaw : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Guthixclaw");
            Main.projFrames[projectile.type] = 9;
        }
        public override void SetDefaults()
        {
            projectile.width = 48;
            projectile.height = 78;
            projectile.aiStyle = -1;
            projectile.tileCollide = false;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.magic = true;
        }
        int i = 0;
        public override void AI()
        {
            Dust.NewDust(projectile.position, 48, 78, 173, 0f, 0f);
            Lighting.AddLight(projectile.position, new Vector3(0, 0, 0));
            i++;
            if (i == 3)
            {
                projectile.frame++;
                i = 0;
                if (projectile.frame > Main.projFrames[projectile.type])
                {
                    projectile.Kill();
                }
            }
            if (projectile.frame > 6)
            {
                projectile.alpha += 25;
            }
        }
    }
}