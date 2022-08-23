using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
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
                    Projectile.damage = (int)(Projectile.damage * 0.75);
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
                Dust.NewDust(Projectile.Center, 0, 0, DustID.Blood, 0, 0, 0, Color.Black);
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
                SoundEngine.PlaySound(new SoundStyle("OldSchoolRuneScape/Sounds/Item/Onyxbolt"), Projectile.position);
                for (int i = 0; i < 32; i++)
                {
                    Dust.NewDust(target.Center, 0, 0, DustID.Demonite, 0, 0, 0, Color.Black);
                }
                for (int i = 0; i < Main.rand.Next(4, 7); i++)
                {
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), target.Center + new Vector2(0, 32).RotateRandom(2 * Math.PI), Projectile.velocity, Mod.Find<ModProjectile>("Onyxheal").Type, damage / 10, 0f, Projectile.owner);
                }
            }
        }
    }
}
