using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.NPCs.Barrows.Guthan
{
    public class Ghostspear : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Guthan's Spear");
        }
        public override void SetDefaults()
        {
            Projectile.hostile = true;
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.scale = 1.5f;
            Projectile.damage = 0;
            Projectile.penetrate = -1;
            Projectile.aiStyle = -1;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 480;
            Projectile.alpha = 0;
            Projectile.ai[0] = 0;
            Projectile.ai[1] = 0;
        }
        public override void AI()
        {
            if (Projectile.alpha == 0)
            {
                SoundEngine.PlaySound(SoundID.Item20, Projectile.position);
            }
            Lighting.AddLight(Projectile.Center + Projectile.velocity, new Vector3(116 * 0.01f, 35 * 0.01f, 78 * 0.01f));
            if (Projectile.alpha < 10)
            {
                Projectile.ai[1] = 9;
            }
            if (Projectile.alpha > 200)
            {
                Projectile.ai[1] = -9;
            }
            Projectile.alpha += (int)Projectile.ai[1];
            if(Projectile.ai[0] == 0)
            {
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90);
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Enchanted_Pink);
            }
            if (Projectile.ai[0] == 1)
            {
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90);
                Projectile.height = 120;
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Enchanted_Pink);
            }
            if (Projectile.ai[0] == 2)
            {
                // useless lmao
                Projectile.ai[0] = 0;
            }
            if (Projectile.ai[0] == 3)
            {
                Projectile.width = 120;
                Projectile.height = 120;
                Projectile.rotation += 0.25f;
                if (Projectile.timeLeft < 200)
                {
                    Projectile.width = 30;
                    Projectile.height = 30;
                    Player target = Main.player[0];
                    for (int k = 0; k < Main.CurrentFrameFlags.ActivePlayersCount; k++)
                    {
                        if (Projectile.Distance(Main.player[k].position) < Projectile.Distance(target.position))
                        {
                            target = Main.player[k];
                        }
                    }
                    float speedX = target.MountedCenter.X - Projectile.Center.X;
                    float speedY = target.MountedCenter.Y - Projectile.Center.Y;
                    Vector2 spd = new Vector2(speedX, speedY);
                    spd.Normalize();
                    Projectile.velocity = spd * 16f;
                    Projectile.ai[0] = 0;
                }
            }
            if (Projectile.ai[0] == 4)
            {
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90);
                Projectile.timeLeft = 36;
                Projectile.ai[0] = 0;
            }
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 20; i++)
            {
                Dust.NewDust(Projectile.Center, 0, 0, DustID.Enchanted_Pink, Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-2, 2));
            }
        }
    }
}
