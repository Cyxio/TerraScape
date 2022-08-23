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
    public class OlmFire : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Olm's Flames");
            Main.projFrames[Projectile.type] = 3;
        }
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 40;
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
                SoundEngine.PlaySound(SoundID.Item89, Projectile.position);
                Projectile.alpha = 20;
                for (int o = 0; o < 12; o++)
                {
                    int dust = Dust.NewDust(Projectile.Center, 0, 0, DustID.CursedTorch);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].scale = 1.5f;
                    Main.dust[dust].velocity = new Vector2(5).RotatedBy(MathHelper.ToRadians(30 * o));
                }
            }
            Lighting.AddLight(Projectile.Center, new Vector3(0, 1.2f, 0));
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
            if (Projectile.ai[0] == 77 && Projectile.timeLeft < 472)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), new Vector2(Projectile.Center.X, Projectile.Center.Y - Projectile.height), Vector2.Zero, Projectile.type, Projectile.damage, 0f, 0, -1, 0);
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), new Vector2(Projectile.Center.X, Projectile.Center.Y + Projectile.height), Vector2.Zero, Projectile.type, Projectile.damage, 0f, 0, 1, 0);
                Projectile.ai[0] = 0;
            }
            if (Projectile.timeLeft % 6 == 1)
            {
                if (Projectile.ai[0] < 0 && Projectile.ai[0] > -15)
                {
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), new Vector2(Projectile.Center.X, Projectile.Center.Y - Projectile.height), Vector2.Zero, Projectile.type, Projectile.damage, 0f, 0, Projectile.ai[0] - 1, 0);
                    Projectile.ai[0] = 0;
                }
                if (Projectile.ai[0] > 0 && Projectile.ai[0] < 15)
                {
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), new Vector2(Projectile.Center.X, Projectile.Center.Y + Projectile.height), Vector2.Zero, Projectile.type, Projectile.damage, 0f, 0, Projectile.ai[0] + 1, 0);
                    Projectile.ai[0] = 0;
                }
            }
            Projectile.position.Y += Main.player[(int)(Projectile.ai[1])].velocity.Y;
        }
        public override void Kill(int timeLeft)
        {
            for (int o = 0; o < 12; o++)
            {
                int dust = Dust.NewDust(Projectile.Center, 0, 0, DustID.CursedTorch);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].scale = 1.5f;
                Main.dust[dust].velocity = new Vector2(5).RotatedBy(MathHelper.ToRadians(30 * o));
            }
        }
    }
}
