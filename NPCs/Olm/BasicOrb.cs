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
    public class BasicOrb : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Olm's Crystal Orb");
            Main.projFrames[Projectile.type] = 4;
        }
        public override void SetDefaults()
        {
            Projectile.width = 45;
            Projectile.height = 45;
            Projectile.aiStyle = -1;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 480;
        }
        public override void AI()
        {
            if (Projectile.alpha == 0)
            {
                SoundEngine.PlaySound(SoundID.Item8, Projectile.position);
                Projectile.alpha = 20;
            }
            Projectile.rotation += 0.14f;
            Lighting.AddLight(Projectile.Center, new Vector3(0, 0.8f, 0));
            Projectile.velocity *= 0.97f;
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
        }
        public override void Kill(int timeLeft)
        {
            for (int o = 0; o < 36; o++)
            {
                int dust = Dust.NewDust(Projectile.Center, 0, 0, DustID.TerraBlade);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].scale = 1f;
                Main.dust[dust].velocity = new Vector2(4).RotatedBy(MathHelper.ToRadians(10 * o));
            }
            if (Projectile.ai[0] > 1)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<CrystalShatter>(), 300 / 4, 0f);
            }
        }
    }
}
