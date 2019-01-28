using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;

namespace OldSchoolRuneScape.Projectiles
{
    public class D2Hspec2 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("D2Hspec2");
            Main.projFrames[projectile.type] = 3;
        }
        public override void SetDefaults()
        {
            projectile.width = 48;
            projectile.height = 36;
            projectile.aiStyle = -1;
            projectile.tileCollide = false;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 60;
            projectile.melee = true;
        }
        public override void AI()
        {
            projectile.spriteDirection = projectile.direction;
            Dust.NewDust(projectile.position, 48, 36, 55, 0, 0, 0, new Color(255, 0, 0), 0.5f);
            Lighting.AddLight(projectile.position, new Vector3(1, 0, 0));
            if (projectile.timeLeft < 50)
            {
                projectile.alpha += 5;
            }
            projectile.frameCounter++;
            if (projectile.frameCounter >= 12)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
            }
            if (projectile.frame >= Main.projFrames[projectile.type])
            {
                projectile.frame = 0;
            }
        }
    }
}