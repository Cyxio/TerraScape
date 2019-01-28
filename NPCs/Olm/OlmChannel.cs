using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.NPCs.Olm
{
    public class OlmChannel : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Olm's channelproj");
        }
        public override void SetDefaults()
        {
            projectile.width = 1;
            projectile.height = 1;
            projectile.aiStyle = -1;
            projectile.penetrate = -1;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.timeLeft = 300;
        }
        public override void AI()
        {
            projectile.damage = 0;
            if (projectile.alpha == 0)
            {
                Main.PlaySound(SoundID.Item15, projectile.position);
                projectile.alpha = 1;
            }
            projectile.rotation += 0.04f;
            projectile.scale *= 0.98f;
            for (int i = 0; i < 3; i++)
            {
                int dust = Dust.NewDust(projectile.position + new Vector2(projectile.scale * 50).RotatedBy((projectile.rotation) + MathHelper.ToRadians(i * 120)), projectile.width, projectile.height, 107);
                Main.dust[dust].scale = 0.75f;
                Main.dust[dust].velocity *= 0f;
                Main.dust[dust].noGravity = true;
            }         
            if (projectile.scale < 0.1f)
            {
                if (projectile.ai[0] == 0)
                {
                    Projectile.NewProjectile(projectile.position - new Vector2(60, 750), new Vector2(0, 12).RotatedBy(Math.PI / 6), mod.ProjectileType<OlmLightning>(), 300 / 4, 0f);
                }
                projectile.Kill();
            }           
        }
    }
}
