using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace OldSchoolRuneScape.Projectiles
{
    public class Dragonstonebolt : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragon bolt");
        }
        public override void SetDefaults()
        {
            projectile.aiStyle = -1;
            projectile.width = 7;
            projectile.height = 7;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.ai[0] = 0;
            projectile.timeLeft = 2400;
        }

        public override void AI()
        {
            if (projectile.ai[0] == 0 && projectile.timeLeft == 2399)
            {
                if (Main.rand.Next(10) == 0 || (Main.player[projectile.owner].GetModPlayer<OSRSplayer>(mod).Boltenchant && Main.rand.Next(4) == 0))
                {
                    projectile.damage *= 5;
                    projectile.ai[0] = 1;
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
            if (projectile.ai[0] == 1)
            {
                Dust.NewDust(projectile.Center, 0, 0, 62);
                Dust.NewDust(projectile.Center, 0, 0, 62);
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Main.PlaySound(SoundID.Dig, projectile.position);
            return true;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.ai[0] == 1)
            {
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/DBolt"), projectile.position);
                Dust.NewDust(new Vector2(target.Center.X - 21, target.position.Y - 35), 0, 0, mod.DustType("Dragonstonebolt"));
                target.AddBuff(BuffID.OnFire, 1800);
            }
        }
    }
}
