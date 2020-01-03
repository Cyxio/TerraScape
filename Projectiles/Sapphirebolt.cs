using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace OldSchoolRuneScape.Projectiles
{
    public class Sapphirebolt : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sapphire bolt");
        }
        public override void SetDefaults()
        {
            projectile.aiStyle = -1;
            projectile.width = 7;
            projectile.height = 7;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.localAI[0] = 0;
            projectile.timeLeft = 2400;
        }

        public override void AI()
        {
            if (projectile.localAI[0] == 0 && projectile.timeLeft == 2399)
            {
                if (Main.rand.Next(10) == 0 || (Main.player[projectile.owner].GetModPlayer<OSRSplayer>().Boltenchant && Main.rand.Next(4) == 0))
                {
                    Player player = Main.player[projectile.owner];
                    projectile.damage += player.statMana / 5;
                    projectile.localAI[0] = 1;
                }
            }
            while (projectile.velocity.X >= 16f || projectile.velocity.X <= -16f || projectile.velocity.Y >= 16f || projectile.velocity.Y < -16f)
            {
                Projectile projectile2 = projectile;
                projectile2.velocity.X = projectile2.velocity.X * 0.97f;
                Projectile projectile3 = projectile;
                projectile3.velocity.Y = projectile3.velocity.Y * 0.97f;
            }
            projectile.velocity.Y += 0.025f;
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90);
            if (projectile.localAI[0] == 1)
            {
                Dust.NewDust(projectile.Center, 0, 0, 59);
                Dust.NewDust(projectile.Center, 0, 0, 59);
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Main.PlaySound(SoundID.Dig, projectile.position);
            return true;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.localAI[0] == 1)
            {
                for (int i = 0; i < 36; i++)
                {
                    Vector2 r = Vector2.One.RotatedBy(MathHelper.ToRadians(10 * i));
                    Dust.NewDust(target.Center, 0, 0, 27, r.X, r.Y, 100, new Color(0, 0, 255), 1f);
                }
            }
        }
    }
}
