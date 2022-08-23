using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
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
                    Player player = Main.player[Projectile.owner];
                    Projectile.damage += player.statMana / 5;
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
                Dust.NewDust(Projectile.Center, 0, 0, DustID.BlueTorch);
                Dust.NewDust(Projectile.Center, 0, 0, DustID.BlueTorch);
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            return true;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Projectile.localAI[0] == 1)
            {
                for (int i = 0; i < 36; i++)
                {
                    Vector2 r = Vector2.One.RotatedBy(MathHelper.ToRadians(10 * i));
                    Dust.NewDust(target.Center, 0, 0, DustID.Shadowflame, r.X, r.Y, 100, new Color(0, 0, 255), 1f);
                }
            }
        }
    }
}
