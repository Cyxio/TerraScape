using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.NPCs.Olm
{
    public class OlmLightning : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Olm's Lightning Bolt");
        }
        public override void SetDefaults()
        {
            projectile.width = 20;
            projectile.height = 20;
            projectile.aiStyle = -1;
            projectile.hostile = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = true;
            projectile.timeLeft = 360;
            projectile.extraUpdates = 5;
        }
        public override void AI()
        {
            if (projectile.alpha == 0)
            {
                Main.PlaySound(SoundID.Item72, projectile.position);
                projectile.alpha = 255;
            }
            int dust = Dust.NewDust(projectile.Center, 0, 0, 107);
            Main.dust[dust].scale = 1.8f;
            Main.dust[dust].velocity *= 0f;
            if (projectile.timeLeft % 30 == 0)
            {
                projectile.velocity = projectile.velocity.RotatedBy(Math.PI / -3);
            }
            if (projectile.timeLeft % 30 == 15)
            {
                projectile.velocity = projectile.velocity.RotatedBy(Math.PI / 3);
            }
        }
    }
}
