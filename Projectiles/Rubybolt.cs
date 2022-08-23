using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
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
            Projectile.aiStyle = -1;
            Projectile.width = 7;
            Projectile.height = 7;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.localAI[0] = 0;
            Projectile.timeLeft = 2400;
        }

        public override void AI()
        {
            if (Projectile.localAI[0] == 0 && Projectile.timeLeft == 2399)
            {
                if (Main.rand.NextBool(10)|| (Main.player[Projectile.owner].GetModPlayer<OSRSplayer>().Boltenchant && Main.rand.NextBool(4)))
                {
                    Projectile.localAI[0] = 1;
                }
            }
            while (Projectile.velocity.X >= 16f || Projectile.velocity.X <= -16f || Projectile.velocity.Y >= 16f || Projectile.velocity.Y < -16f)
            {
                Projectile projectile2 = Projectile;
                projectile2.velocity.X = projectile2.velocity.X * 0.97f;
                Projectile projectile3 = Projectile;
                projectile3.velocity.Y = projectile3.velocity.Y * 0.97f;
            }
            Projectile.velocity.Y += 0.025f;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90);
            if (Projectile.localAI[0] == 1)
            {
                Dust.NewDust(Projectile.Center, 0, 0, DustID.RedTorch);
                Dust.NewDust(Projectile.Center, 0, 0, DustID.RedTorch);
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            return true;
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (Projectile.localAI[0] == 1)
            {
                Player player = Main.player[Projectile.owner];
                int dmg = 10 + (player.statLife / 10);
                for (int i = 0; i < 36; i++)
                {
                    Dust.NewDust(target.Bottom, 0, 0, DustID.Water_BloodMoon, 0, -8f);
                    Dust.NewDust(player.Bottom, 0, 0, DustID.Water_BloodMoon, 0, -8f);
                }
                player.Hurt(Terraria.DataStructures.PlayerDeathReason.ByCustomReason("'s blood was forfeited"), dmg + (int)(player.statDefense * 0.5f), 0, false, true);
                damage += 4 * dmg;
            }
        }
    }
}
