using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
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
                Dust.NewDust(Projectile.Center, 0, 0, DustID.WhiteTorch);
                Dust.NewDust(Projectile.Center, 0, 0, DustID.WhiteTorch);
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            return true;
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (Projectile.localAI[0] == 1 && target.defense < 999)
            {
                SoundEngine.PlaySound(SoundID.Tink, target.Center);
                damage += 5 * target.defense;
                Dust.NewDust(new Vector2(target.Center.X - 27, target.position.Y - 50), 0, 0, Mod.Find<ModDust>("Diamondbolt").Type);
            }
        }
    }
}
