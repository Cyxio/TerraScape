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
    public class OlmLightning : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Olm's Lightning Bolt");
        }
        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.aiStyle = -1;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.timeLeft = 360;
            Projectile.extraUpdates = 5;
        }
        public override void AI()
        {
            if (Projectile.alpha == 0)
            {
                SoundEngine.PlaySound(SoundID.Item72, Projectile.position);
                Projectile.alpha = 255;
            }
            int dust = Dust.NewDust(Projectile.Center, 0, 0, DustID.TerraBlade);
            Main.dust[dust].scale = 1.8f;
            Main.dust[dust].velocity *= 0f;
            if (Projectile.timeLeft % 30 == 0)
            {
                Projectile.velocity = Projectile.velocity.RotatedBy(Math.PI / -3);
            }
            if (Projectile.timeLeft % 30 == 15)
            {
                Projectile.velocity = Projectile.velocity.RotatedBy(Math.PI / 3);
            }
        }
    }
}
