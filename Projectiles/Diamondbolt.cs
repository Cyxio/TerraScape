using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace OldSchoolRuneScape.Projectiles
{
    public class Diamondbolt : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Diamond bolt");
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
                Dust.NewDust(projectile.Center, 0, 0, 63);
                Dust.NewDust(projectile.Center, 0, 0, 63);
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Main.PlaySound(SoundID.Dig, projectile.position);
            return true;
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (projectile.localAI[0] == 1 && target.defense < 999)
            {
                Main.PlaySound(SoundID.Tink, target.Center);
                damage += 5 * target.defense;
                Dust.NewDust(new Vector2(target.Center.X - 27, target.position.Y - 50), 0, 0, mod.DustType("Diamondbolt"));
            }
        }
    }
}
