using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Projectiles
{
    public class BGSspec : ModProjectile
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("BGSpec");
        }
        public override void SetDefaults()
        {
            projectile.width = 2;
            projectile.height = 2;
            projectile.extraUpdates = 0;
            projectile.timeLeft = 360;
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
                swingDir = player.direction * -1;
            }
            if (Main.rand.NextFloat() < 1f && projectile.timeLeft < 360)
            {
                /*Dust dust;
                Vector2 position = projectile.position;
                for (int i = 0; i < 10; i++)
                {
                    Vector2 offset = new Vector2(3, 3).RotatedBy(projectile.rotation);
                    dust = Terraria.Dust.NewDustPerfect(position - offset*i, 43, new Vector2(0f, 0f), 0, new Color(255, 255, 255), 1f);
                }*/

                Dust dust;
                Vector2 position = projectile.position;
                Vector2 offset = new Vector2(4, 4).RotatedBy(projectile.rotation);
                dust = Terraria.Dust.NewDustPerfect(position, ModContent.DustType<Dusts.BGSdust>(), new Vector2(1).RotatedBy(projectile.rotation - MathHelper.ToRadians(45f)), 0, new Color(255, 255, 255), 1f);
            }

            if (projectile.timeLeft > 300)
            {
                float x = 90f + (180f * ((projectile.timeLeft - 300f) / 60f));
                double sinX = Math.Sin(MathHelper.ToRadians(x));
                rotationPlace = (int)(120f + 180f * sinX);
            }
            if (projectile.timeLeft == 345)
            {
                player.velocity.Y = -10f;
            }
            if (projectile.timeLeft == 315)
            {
                player.velocity.X += 15f * player.direction;
            }
            if (projectile.timeLeft < 300 && player.velocity.Y == 0)
            {
                for (int i = 1; i < 4; i++)
                {
                    for (int j = 0; j < 12; j++)
                    {
                        Dust dust;
                        // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                        Vector2 position = projectile.position;
                        dust = Terraria.Dust.NewDustPerfect(position, 66, new Vector2(3f * i, 3f * i).RotatedBy(MathHelper.ToRadians(30 * j)), 0, new Color(255, 255, 255), 1f);
                        dust.noGravity = true;
                        Main.PlaySound(Terraria.ID.SoundID.Item69);
                    }
                }
                projectile.Kill();
            }
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

        public override void Kill(int timeLeft)
        {

            base.Kill(timeLeft);
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (target.defense >= 40)
            {
                damage *= 5;
            }
            else if (target.defense >= 8)
            {
                int increase = (int)(5f * (target.defense / 40f));
                damage *= increase;
            }
            crit = false;
            if (Main.player[projectile.owner].meleeCrit * 2 > Main.rand.Next(100))
            {
                crit = true;
            }
        }
    }
}