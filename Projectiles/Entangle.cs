using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace OldSchoolRuneScape.Projectiles
{
    public class Entangle : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Entangle");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.timeLeft = 360;
            Projectile.penetrate = 1;
            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
            Projectile.aiStyle = -1;
            Projectile.scale = 1f;
            Projectile.light = 0.2f;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(200, 200, 200, 0);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY) - Projectile.velocity;
                Main.EntitySpriteDraw(TextureAssets.Projectile[Projectile.type].Value, drawPos, null, Projectile.GetAlpha(lightColor), Projectile.rotation, drawOrigin, (4f / (4f + k)), SpriteEffects.None, 0);
            }
            return true;
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + (float)(Math.PI / 2f);
            Projectile.velocity.Y += 0.1f;
            Projectile.damage = 0;
            for (int i = 0; i < 200; i++)
            {
                NPC target = Main.npc[i];
                if (Projectile.Colliding(Projectile.Hitbox, target.Hitbox) && target.active && !target.friendly && target.type != NPCID.TargetDummy)
                {
                    SoundEngine.PlaySound(new SoundStyle("OldSchoolRuneScape/Sounds/Item/Snared"), target.position);
                    if (target.boss)
                    {
                        target.AddBuff(ModContent.BuffType<Buffs.Snared>(), 60);
                    }
                    else
                    {
                        target.AddBuff(ModContent.BuffType<Buffs.Snared>(), 180);
                    }
                    for (int o = 0; o < 36; o++)
                    {
                        int dust = Dust.NewDust(target.Center, 0, 0, DustID.GemRuby);
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].scale = 2f;
                        Main.dust[dust].velocity = new Vector2(4).RotatedBy(MathHelper.ToRadians(10 * o));
                    }
                    Projectile.active = false;
                }
            }
        }
    }
}