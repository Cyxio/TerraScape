using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Projectiles
{
    public class Smokebattlestaff : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Smoke");
            Main.projFrames[Projectile.type] = 4;
        }
        public override void SetDefaults()
        {
            Projectile.width = 28;
            Projectile.height = 28;
            Projectile.timeLeft = 840;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.aiStyle = -1;
            Projectile.tileCollide = true;
            Projectile.penetrate = -1;
            Projectile.alpha = 80;
        }
        public override void AI()
        {
            float distance = Projectile.Distance(new Vector2(Projectile.ai[0], Projectile.ai[1]));
            float slowDistance = 200f;
            float shootSpeed = Main.player[Projectile.owner].inventory[Main.player[Projectile.owner].selectedItem].shootSpeed;
            if (distance < slowDistance)
            {
                if (Projectile.velocity.Length() > 0.01f)
                {
                    Projectile.velocity.Normalize();
                    Projectile.velocity *= shootSpeed * (distance / slowDistance);
                }
                else
                {
                    Projectile.velocity *= 0;
                }
            }
            Projectile.rotation += MathHelper.ToRadians(2);
            int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Smoke, 0f, -2f, 100, default(Color), 1f);
            Main.dust[dustIndex].scale = 0.1f + (float)Main.rand.Next(5) * 0.1f;
            Main.dust[dustIndex].fadeIn = 1.5f + (float)Main.rand.Next(5) * 0.1f;
            Main.dust[dustIndex].noGravity = true;
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
    }
}
