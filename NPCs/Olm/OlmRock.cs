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
    public class OlmRock : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Olm's Boulder");
            Main.projFrames[Projectile.type] = 4;
        }
        public override void SetDefaults()
        {
            Projectile.width = 60;
            Projectile.height = 60;
            Projectile.aiStyle = -1;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 60;
        }
        public override void AI()
        {
            if (Projectile.alpha == 0)
            {
                SoundEngine.PlaySound(SoundID.Item80, Projectile.position);
                Projectile.alpha = 20;
            }
            Projectile.rotation += 0.12f;
            Lighting.AddLight(Projectile.Center, new Vector3(0, 0.8f, 0));
            Projectile.frameCounter++;
            if (Projectile.frameCounter > 5)
            {
                Projectile.frame++;
                if (Projectile.frame >= Main.projFrames[Projectile.type])
                {
                    Projectile.frame = 0;
                }
                Projectile.frameCounter = 0;
            }
            if (Projectile.timeLeft < 5 && Projectile.alpha == 20)
            {
                Projectile.position -= Projectile.Size;
                Projectile.width *= 2;
                Projectile.height *= 2;
                Projectile.alpha = 255;
            }
        }
        public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
            for (int o = 0; o < 36; o++)
            {
                int dust = Dust.NewDust(Projectile.Center, 0, 0, DustID.TerraBlade);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].scale = 2f;
                Main.dust[dust].velocity = new Vector2(6).RotatedBy(MathHelper.ToRadians(10 * o));
            }
        }
    }
}
