using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.NPCs.Barrows.Verac
{
    public class Ghostball : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Verac's ball");
        }
        public override void SetDefaults()
        {
            Projectile.hostile = true;
            Projectile.width = 42;
            Projectile.height = 42;
            Projectile.scale = 1f;
            Projectile.penetrate = -1;
            Projectile.aiStyle = -1;
            Projectile.tileCollide = true;
            Projectile.timeLeft = 600;
            Projectile.alpha = 0;
            Projectile.ai[0] = 0;
            Projectile.ai[1] = 0;
        }
        public override void AI()
        {
            if (Projectile.alpha == 0)
            {
                SoundEngine.PlaySound(SoundID.Item1, Projectile.position);
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
                Projectile.rotation += Projectile.velocity.X * 0.025f;
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Enchanted_Pink);
                Projectile.velocity.Y += 0.15f;
            }
            if (Projectile.ai[0] == 1)
            {
                Projectile.rotation += Projectile.velocity.X * 0.025f;
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Enchanted_Pink);
                Projectile.velocity.Y += 0.15f;
            }
            if (Projectile.ai[0] == 2)
            {
                Projectile.velocity = Projectile.velocity.RotatedBy(MathHelper.ToRadians(5));
                Projectile.rotation = Projectile.velocity.ToRotation();
                Projectile.position.X += Projectile.velocity.X * 2f;
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.ai[0] == 0)
            {
                if (Projectile.velocity.X != oldVelocity.X)
                {
                    Projectile.velocity.X = -oldVelocity.X;
                }
                if (Projectile.velocity.Y != oldVelocity.Y)
                {
                    Projectile.velocity.Y = -oldVelocity.Y;
                }
                Projectile.velocity *= 0.9f;
                SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
                return false;
            }
            if (Projectile.ai[0] == 1)
            {
                SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
                if (Projectile.velocity.X != oldVelocity.X)
                {
                    Projectile.velocity.X = -oldVelocity.X;
                }
                if (Projectile.velocity.Y != oldVelocity.Y)
                {
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, new Vector2(oldVelocity.X, -oldVelocity.Y * 0.75f), Projectile.type, Projectile.damage, Projectile.knockBack);
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, new Vector2(-oldVelocity.X, -oldVelocity.Y * 0.75f), Projectile.type, Projectile.damage, Projectile.knockBack);
                }
                return true;
            }
            return true;
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
