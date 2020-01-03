using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.NPCs.Olm
{
    public class OlmRock : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Olm's Boulder");
            Main.projFrames[projectile.type] = 4;
        }
        public override void SetDefaults()
        {
            projectile.width = 60;
            projectile.height = 60;
            projectile.aiStyle = -1;
            projectile.hostile = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.timeLeft = 60;
        }
        public override void AI()
        {
            if (projectile.alpha == 0)
            {
                Main.PlaySound(SoundID.Item80, projectile.position);
                projectile.alpha = 20;
            }
            projectile.rotation += 0.12f;
            Lighting.AddLight(projectile.Center, new Vector3(0, 0.8f, 0));
            projectile.frameCounter++;
            if (projectile.frameCounter > 5)
            {
                projectile.frame++;
                if (projectile.frame >= Main.projFrames[projectile.type])
                {
                    projectile.frame = 0;
                }
                projectile.frameCounter = 0;
            }
            if (projectile.timeLeft < 5 && projectile.alpha == 20)
            {
                projectile.position -= projectile.Size;
                projectile.width *= 2;
                projectile.height *= 2;
                projectile.alpha = 255;
            }
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item14, projectile.position);
            for (int o = 0; o < 36; o++)
            {
                int dust = Dust.NewDust(projectile.Center, 0, 0, 107);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].scale = 2f;
                Main.dust[dust].velocity = new Vector2(6).RotatedBy(MathHelper.ToRadians(10 * o));
            }
        }
    }
}
