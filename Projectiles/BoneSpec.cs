using System;
using Microsoft.Xna.Framework;
using Terraria;
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
            projectile.aiStyle = 1;
            projectile.width = 7;
            projectile.height = 7;
            projectile.friendly = true;
            projectile.ranged = true;
        }

        public override void AI()
        {
            projectile.velocity.Y = projectile.velocity.Y - 0.025f;
            Lighting.AddLight(projectile.position, new Vector3(3, 0, 0));
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if(target.life == target.lifeMax)
            {
                damage *= 3;
                for(int i = 0; i<10; i++)
                {
                    Dust.NewDust(target.Center, 0, 0, 235);
                }
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Main.PlaySound(SoundID.Dig, projectile.position);
            return true;
        }
    }
}
