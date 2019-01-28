using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace OldSchoolRuneScape.Projectiles
{
    public class Rubybolt : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ruby bolt");
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
                if (Main.rand.Next(10) == 0 || (Main.player[projectile.owner].GetModPlayer<OSRSplayer>(mod).Boltenchant && Main.rand.Next(4) == 0))
                {
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
                Dust.NewDust(projectile.Center, 0, 0, 60);
                Dust.NewDust(projectile.Center, 0, 0, 60);
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Main.PlaySound(SoundID.Dig, projectile.position);
            return true;
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (projectile.localAI[0] == 1)
            {
                Player player = Main.player[projectile.owner];
                int dmg = 10 + (player.statLife / 10);
                for (int i = 0; i < 36; i++)
                {
                    Dust.NewDust(target.Bottom, 0, 0, 105, 0, -8f);
                    Dust.NewDust(player.Bottom, 0, 0, 105, 0, -8f);
                }
                player.Hurt(Terraria.DataStructures.PlayerDeathReason.ByCustomReason("'s blood was forfeited"), dmg + (int)(player.statDefense * 0.5f), 0, false, true);
                damage += 4 * dmg;
            }
        }
    }
}
