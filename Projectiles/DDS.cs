using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Projectiles
{
    public class DDS : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("DDS");
            Main.projFrames[Projectile.type] = 8;
        }
        public override void SetDefaults()
        {
            Projectile.width = 40;
            Projectile.height = 40;
            Projectile.timeLeft = 64;
            Projectile.penetrate = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.aiStyle = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 32;
            Projectile.ownerHitCheck = true;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Projectile.position = player.RotatedRelativePoint(player.MountedCenter, true) - (Projectile.Size / 2f) + Projectile.velocity;
            Projectile.rotation = Projectile.velocity.ToRotation();
            Lighting.AddLight(Projectile.Center, new Vector3(1, 0, 0));
            if (Projectile.direction == -1)
            {
                Projectile.rotation = Projectile.velocity.ToRotation() - MathHelper.Pi;
            }
            Projectile.spriteDirection = Projectile.direction;
            player.heldProj = Projectile.whoAmI;
            player.itemAnimation = 2;
            player.itemTime = 2;
            player.ChangeDir(Projectile.direction);
            if (Projectile.timeLeft % 4 == 0)
            {
                if (Projectile.timeLeft <= 32)
                {
                    Projectile.frame--;
                }
                else
                {
                    Projectile.frame++;
                }               
            }
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            crit = true;
        }
    }
}