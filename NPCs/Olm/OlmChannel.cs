using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
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
            Projectile.width = 1;
            Projectile.height = 1;
            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 300;
        }
        public override void AI()
        {
            Projectile.damage = 0;
            if (Projectile.alpha == 0)
            {
                SoundEngine.PlaySound(SoundID.Item15, Projectile.position);
                Projectile.alpha = 1;
            }
            Projectile.rotation += 0.04f;
            Projectile.scale *= 0.98f;
            for (int i = 0; i < 3; i++)
            {
                int dust = Dust.NewDust(Projectile.position + new Vector2(Projectile.scale * 50).RotatedBy((Projectile.rotation) + MathHelper.ToRadians(i * 120)), Projectile.width, Projectile.height, DustID.TerraBlade);
                Main.dust[dust].scale = 0.75f;
                Main.dust[dust].velocity *= 0f;
                Main.dust[dust].noGravity = true;
            }         
            if (Projectile.scale < 0.1f)
            {
                if (Projectile.ai[0] == 0)
                {
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position - new Vector2(60, 750), new Vector2(0, 12).RotatedBy(Math.PI / 6), ModContent.ProjectileType<OlmLightning>(), 300 / 4, 0f);
                }
                Projectile.Kill();
            }           
        }
    }
}
