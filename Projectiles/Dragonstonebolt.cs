using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
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
            Projectile.aiStyle = -1;
            Projectile.width = 7;
            Projectile.height = 7;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.ai[0] = 0;
            Projectile.timeLeft = 2400;
        }

        public override void AI()
        {
            if (Projectile.ai[0] == 0 && Projectile.timeLeft == 2399)
            {
                if (Main.rand.NextBool(10)|| (Main.player[Projectile.owner].GetModPlayer<OSRSplayer>().Boltenchant && Main.rand.NextBool(4)))
                {
                    Projectile.damage *= 5;
                    Projectile.ai[0] = 1;
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
            if (Projectile.ai[0] == 1)
            {
                Dust.NewDust(Projectile.Center, 0, 0, DustID.PurpleTorch);
                Dust.NewDust(Projectile.Center, 0, 0, DustID.PurpleTorch);
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            return true;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Projectile.ai[0] == 1)
            {
                SoundEngine.PlaySound(new SoundStyle("OldSchoolRuneScape/Sounds/Item/DBolt"), Projectile.position);
                Dust.NewDust(new Vector2(target.Center.X - 21, target.position.Y - 35), 0, 0, Mod.Find<ModDust>("Dragonstonebolt").Type);
                target.AddBuff(BuffID.OnFire, 1800);
            }
        }
    }
}
