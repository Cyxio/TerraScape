using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.NPCs.Olm
{
    public class BasicOrb : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Olm's Crystal Orb");
            Main.projFrames[projectile.type] = 4;
        }
        public override void SetDefaults()
        {
            projectile.width = 45;
            projectile.height = 45;
            projectile.aiStyle = -1;
            projectile.hostile = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.timeLeft = 480;
        }
        public override void AI()
        {
            if (projectile.alpha == 0)
            {
                Main.PlaySound(SoundID.Item8, projectile.position);
                projectile.alpha = 20;
            }
            projectile.rotation += 0.14f;
            Lighting.AddLight(projectile.Center, new Vector3(0, 0.8f, 0));
            projectile.velocity *= 0.97f;
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
        }
        public override void Kill(int timeLeft)
        {
            for (int o = 0; o < 36; o++)
            {
                int dust = Dust.NewDust(projectile.Center, 0, 0, 107);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].scale = 1f;
                Main.dust[dust].velocity = new Vector2(4).RotatedBy(MathHelper.ToRadians(10 * o));
            }
            if (projectile.ai[0] > 1)
            {
                Projectile.NewProjectile(projectile.Center, Vector2.Zero, mod.ProjectileType<CrystalShatter>(), 300 / 4, 0f);
            }
        }
    }
}
