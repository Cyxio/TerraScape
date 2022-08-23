using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Projectiles
{
    public class SGSspec : ModProjectile
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("SGSpec");
        }
        public override void SetDefaults()
        {
            Projectile.width = 2;
            Projectile.height = 2;
            Projectile.extraUpdates = 0;
            Projectile.timeLeft = 60;
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
                swingDir = player.direction;
            }
            if (Main.rand.NextFloat() < 1f && Projectile.timeLeft < 60)
            {
                Dust dust;
                Vector2 position = Projectile.position;
                    Vector2 offset = new Vector2(4, 4).RotatedBy(Projectile.rotation);
                    dust = Terraria.Dust.NewDustPerfect(position, ModContent.DustType<Dusts.SGSdust>(), new Vector2(1).RotatedBy(Projectile.rotation - MathHelper.ToRadians(45f)), 0, new Color(255, 255, 255), 1f);
            }

            if (Projectile.timeLeft > 15)
            {
                float x = 90f - (180f * ((Projectile.timeLeft) / 60f));
                double sinX = Math.Sin(MathHelper.ToRadians(x));
                rotationPlace = (int)(90f + 90f * sinX);
                float xd = 180f - (180f * ((Projectile.timeLeft) / 60f));
                player.velocity = new Vector2(0, (-10f * (float)Math.Sin(MathHelper.ToRadians(xd))));
            }

            Vector2 place = new Vector2(70, 80).RotatedBy(MathHelper.ToRadians(-135 + -rotationPlace * swingDir));
            Projectile.position = player.RotatedRelativePoint(player.MountedCenter, true) - place;
            Vector2 rotation = Projectile.position - player.MountedCenter;
            Projectile.rotation = rotation.ToRotation() + MathHelper.ToRadians(135);
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

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            crit = false;
            if (Main.player[Projectile.owner].GetCritChance(DamageClass.Generic) * 2 > Main.rand.Next(100))
            {
                crit = true;
            }
            int heal = (int)(damage / 10f);
            if (crit) { heal *= 2; }
            Main.player[Projectile.owner].HealEffect(heal);
            Main.player[Projectile.owner].statLife += heal;
        }
    }
}