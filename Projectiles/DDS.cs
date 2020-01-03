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
            Main.projFrames[projectile.type] = 8;
        }
        public override void SetDefaults()
        {
            projectile.width = 40;
            projectile.height = 40;
            projectile.timeLeft = 64;
            projectile.penetrate = -1;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.aiStyle = -1;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 32;
            projectile.ownerHitCheck = true;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            projectile.position = player.RotatedRelativePoint(player.MountedCenter, true) - (projectile.Size / 2f) + projectile.velocity;
            projectile.rotation = projectile.velocity.ToRotation();
            Lighting.AddLight(projectile.Center, new Vector3(1, 0, 0));
            if (projectile.direction == -1)
            {
                projectile.rotation = projectile.velocity.ToRotation() - MathHelper.Pi;
            }
            projectile.spriteDirection = projectile.direction;
            player.heldProj = projectile.whoAmI;
            player.itemAnimation = 2;
            player.itemTime = 2;
            player.ChangeDir(projectile.direction);
            if (projectile.timeLeft % 4 == 0)
            {
                if (projectile.timeLeft <= 32)
                {
                    projectile.frame--;
                }
                else
                {
                    projectile.frame++;
                }               
            }
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            crit = true;
        }
    }
}