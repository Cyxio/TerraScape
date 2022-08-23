using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
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
            Projectile.width = 2;
            Projectile.height = 2;
            Projectile.extraUpdates = 0;
            Projectile.timeLeft = 360;
            Projectile.penetrate = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.aiStyle = -1;
            Projectile.ownerHitCheck = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 32;
        }

        int rotationPlace = 0;
        int swingDir = 0;

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (swingDir == 0)
            {
                swingDir = player.direction * -1;
            }
            if (Main.rand.NextFloat() < 1f && Projectile.timeLeft < 360)
            {
                /*Dust dust;
                Vector2 position = projectile.position;
                for (int i = 0; i < 10; i++)
                {
                    Vector2 offset = new Vector2(3, 3).RotatedBy(projectile.rotation);
                    dust = Terraria.Dust.NewDustPerfect(position - offset*i, 43, new Vector2(0f, 0f), 0, new Color(255, 255, 255), 1f);
                }*/

                Dust dust;
                Vector2 position = Projectile.position;
                Vector2 offset = new Vector2(4, 4).RotatedBy(Projectile.rotation);
                dust = Terraria.Dust.NewDustPerfect(position, ModContent.DustType<Dusts.BGSdust>(), new Vector2(1).RotatedBy(Projectile.rotation - MathHelper.ToRadians(45f)), 0, new Color(255, 255, 255), 1f);
            }

            if (Projectile.timeLeft > 300)
            {
                float x = 90f + (180f * ((Projectile.timeLeft - 300f) / 60f));
                double sinX = Math.Sin(MathHelper.ToRadians(x));
                rotationPlace = (int)(120f + 180f * sinX);
            }
            if (Projectile.timeLeft == 345)
            {
                player.velocity.Y = -10f;
            }
            if (Projectile.timeLeft == 315)
            {
                player.velocity.X += 15f * player.direction;
            }
            if (Projectile.timeLeft < 300 && player.velocity.Y == 0)
            {
                for (int i = 1; i < 4; i++)
                {
                    for (int j = 0; j < 12; j++)
                    {
                        Dust dust;
                        // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                        Vector2 position = Projectile.position;
                        dust = Terraria.Dust.NewDustPerfect(position, 66, new Vector2(3f * i, 3f * i).RotatedBy(MathHelper.ToRadians(30 * j)), 0, new Color(255, 255, 255), 1f);
                        dust.noGravity = true;
                        SoundEngine.PlaySound(Terraria.ID.SoundID.Item69);
                    }
                }
                Projectile.Kill();
            }
            Vector2 place = new Vector2(70, 80).RotatedBy(MathHelper.ToRadians(-135 + -rotationPlace * swingDir));
            Projectile.position = player.RotatedRelativePoint(player.MountedCenter, true) - place;
            Vector2 rotation = Projectile.position - player.MountedCenter;
            Projectile.rotation = rotation.ToRotation() + MathHelper.ToRadians(135);
            Lighting.AddLight(Projectile.Center, new Vector3(1, 1, 1));
            player.heldProj = Projectile.whoAmI;
            player.itemAnimation = 2;
            player.itemTime = 2;
            player.immune = true;
            player.immuneNoBlink = true;
            player.immuneTime = 10;
            DrawOffsetX = 8 * player.direction;
            DrawOriginOffsetX = -38;
            DrawOriginOffsetY = -3;
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            Player player = Main.player[Projectile.owner];
            float point = 0f;
            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), player.MountedCenter,
            Projectile.position, 2, ref point);
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
            if (Main.player[Projectile.owner].GetCritChance(DamageClass.Generic) * 2 > Main.rand.Next(100))
            {
                crit = true;
            }
        }
    }
}