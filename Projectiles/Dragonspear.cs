using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace OldSchoolRuneScape.Projectiles
{
    public class Dragonspear : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragon spear");
        }

        public override void SetDefaults()
        {
            projectile.width = 18;
            projectile.height = 18;
            projectile.aiStyle = 19;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.hide = true;
            projectile.ownerHitCheck = true;
            projectile.melee = true;
            projectile.scale = 1.1f;
            projectile.alpha = 0;
            projectile.ai[1] = 0;
        }
        public float movementFactor
        {
            get { return projectile.ai[0]; }
            set { projectile.ai[0] = value; }
        }
        public override void AI()
        {
            Player projOwner = Main.player[projectile.owner];
            Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
            projectile.direction = projOwner.direction;
            projOwner.heldProj = projectile.whoAmI;
            projOwner.itemTime = projOwner.itemAnimation;
            projectile.position.X = ownerMountedCenter.X - (float)(projectile.width / 2);
            projectile.position.Y = ownerMountedCenter.Y - (float)(projectile.height / 2);
            if (!projOwner.frozen)
            {
                if (movementFactor == 0f)
                {
                    movementFactor = 3f;
                    projectile.netUpdate = true;
                }
                if (projOwner.itemAnimation < projOwner.itemAnimationMax / 3)
                {
                    movementFactor -= 3.2f;
                }
                else
                {
                    movementFactor += 2.9f;
                }
            }
            projectile.position += projectile.velocity * movementFactor;
            if (projOwner.itemAnimation == 0)
            {
                projectile.Kill();
            }
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + MathHelper.ToRadians(135f);
            if (projectile.spriteDirection == -1)
            {
                projectile.rotation -= MathHelper.ToRadians(90f);
            }
            if (projectile.ai[1] == 1)
            {
                Main.projectileTexture[projectile.type] = mod.GetTexture("Projectiles/DragonspearS");
            }
            else
            {
                Main.projectileTexture[projectile.type] = mod.GetTexture("Projectiles/Dragonspear");
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.ai[1] == 1 && target.type != NPCID.TargetDummy)
            {
                Player projOwner = Main.player[projectile.owner];
                Vector2 spd = target.position - projOwner.MountedCenter;
                spd.Normalize();
                target.velocity = spd * 18f;
                target.netUpdate = true;
            }
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            if (projectile.ai[1] == 1)
            {
                Player projOwner = Main.player[projectile.owner];
                Vector2 spd = target.position - projOwner.MountedCenter;
                spd.Normalize();
                target.velocity += spd * 18f;
            }
        }
    }
}
