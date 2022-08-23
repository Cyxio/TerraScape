using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.ID;

namespace OldSchoolRuneScape.Projectiles
{
    public class BoneSpec : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Enchanced bone bolt");
        }
        public override void SetDefaults()
        {
            Projectile.aiStyle = 1;
            Projectile.width = 7;
            Projectile.height = 7;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
        }

        public override void AI()
        {
            Projectile.velocity.Y = Projectile.velocity.Y - 0.025f;
            Lighting.AddLight(Projectile.position, new Vector3(3, 0, 0));
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if(target.life == target.lifeMax)
            {
                damage *= 3;
                for(int i = 0; i<10; i++)
                {
                    Dust.NewDust(target.Center, 0, 0, DustID.LifeDrain);
                }
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            return true;
        }
    }
}
