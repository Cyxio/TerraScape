using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace OldSchoolRuneScape.Projectiles
{
    public class Onyxbolt : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Onyx bolt");
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
                    projectile.damage = (int)(projectile.damage * 0.75);
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
                Dust.NewDust(projectile.Center, 0, 0, 5, 0, 0, 0, Color.Black);
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
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Onyxbolt"), projectile.position);
                for (int i = 0; i < 32; i++)
                {
                    Dust.NewDust(target.Center, 0, 0, 14, 0, 0, 0, Color.Black);
                }
                for (int i = 0; i < Main.rand.Next(4, 7); i++)
                {
                    Projectile.NewProjectile(target.Center + new Vector2(0, 32).RotateRandom(2 * Math.PI), projectile.velocity, mod.ProjectileType("Onyxheal"), damage / 10, 0f, projectile.owner);
                }
            }
        }
    }
}
