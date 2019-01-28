using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace OldSchoolRuneScape.Projectiles
{
    public class Snare : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Snare");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.timeLeft = 360;
            projectile.penetrate = 1;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.aiStyle = -1;
            projectile.scale = 1f;
            projectile.light = 0.2f;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(160, 160, 160, 0);
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY) - projectile.velocity;
                spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, projectile.GetAlpha(lightColor), projectile.rotation, drawOrigin, (4f / (4f + k)), SpriteEffects.None, 0f);
            }
            return true;
        }
        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + (float)(Math.PI / 2f);
            projectile.velocity.Y += 0.1f;
            projectile.damage = 0;
            for (int i = 0; i < 200; i++)
            {
                NPC target = Main.npc[i];
                if (projectile.Colliding(projectile.Hitbox, target.Hitbox) && target.active && !target.friendly && target.type != NPCID.TargetDummy)
                {
                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Snared"), target.position);
                    if (target.boss)
                    {
                        target.AddBuff(mod.BuffType<Buffs.Snared>(), 40);
                    }
                    else
                    {
                        target.AddBuff(mod.BuffType<Buffs.Snared>(), 120);
                    }
                    for (int o = 0; o < 36; o++)
                    {
                        int dust = Dust.NewDust(target.Center, 0, 0, 90);
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].scale = 1.5f;
                        Main.dust[dust].velocity = new Vector2(3).RotatedBy(MathHelper.ToRadians(10 * o));
                    }
                    projectile.active = false;
                }
            }
        }
    }
}