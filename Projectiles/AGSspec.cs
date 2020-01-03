using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Projectiles
{
    public class AGSspec : ModProjectile
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("AGSpec");
        }
        public override void SetDefaults()
        {
            projectile.width = 2;
            projectile.height = 2;
            projectile.extraUpdates = 0;
            projectile.timeLeft = 60;
            projectile.penetrate = -1;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.aiStyle = -1;
            projectile.ownerHitCheck = true;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 32;
        }

        int rotationPlace = 0;
        int swingDir = 0;

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            if (swingDir == 0)
            {
                swingDir = player.direction;
            }
            if (Main.rand.NextFloat() < 1f && projectile.timeLeft < 60)
            {
                Dust dust;
                Vector2 position = projectile.position;
                for (int i = 0; i < 2; i++)
                {
                    Vector2 offset = new Vector2(4, 4).RotatedBy(projectile.rotation);
                    dust = Terraria.Dust.NewDustPerfect(position - offset*i, 43, new Vector2(0f, 0f), 0, new Color(255, 255, 255), 1f);
                }
            }

            if (projectile.timeLeft > 30)
            {
                float x = 90f - (180f * ((projectile.timeLeft - 28f) / 30f));
                double sinX = Math.Sin(MathHelper.ToRadians(x));
                rotationPlace = (int)(90f + 90f * sinX);
            }
            else
            {
                int total = 180 + (int)(-30f * Math.Sin(Math.PI * 2f * (projectile.timeLeft / 30f)));
                if (projectile.timeLeft % 15 == 0)
                {
                    player.direction *= -1;
                }
                rotationPlace = total;
            }
            float xd = 180f - (180f * ((projectile.timeLeft) / 60f));
            player.velocity = new Vector2(0, (-10f * (float)Math.Sin(MathHelper.ToRadians(xd))));
            Vector2 place = new Vector2(70, 80).RotatedBy(MathHelper.ToRadians(-135 + -rotationPlace * swingDir));
            projectile.position = player.RotatedRelativePoint(player.MountedCenter, true) - place;
            Vector2 rotation = projectile.position - player.MountedCenter;
            projectile.rotation = rotation.ToRotation() + MathHelper.ToRadians(135);
            Lighting.AddLight(projectile.Center, new Vector3(1, 1, 1));
            player.heldProj = projectile.whoAmI;
            player.itemAnimation = 2;
            player.itemTime = 2;
            player.immune = true;
            player.immuneNoBlink = true;
            player.immuneTime = 10;
            drawOffsetX = 8 * player.direction;
            drawOriginOffsetX = -38;
            drawOriginOffsetY = -3;
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            Player player = Main.player[projectile.owner];
            float point = 0f;
            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), player.MountedCenter,
            projectile.position, 2, ref point);
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (target.boss)
            {
                damage += target.lifeMax / 16;
            }
            crit = false;
            if (Main.player[projectile.owner].meleeCrit * 2 > Main.rand.Next(100))
            {
                crit = true;
            }
        }
    }
}