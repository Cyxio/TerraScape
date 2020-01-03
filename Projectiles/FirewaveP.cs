using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace OldSchoolRuneScape.Projectiles
{
    public class FirewaveP : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Firewave");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
            Main.projFrames[projectile.type] = 3;
        }
        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 30;
            projectile.timeLeft = 1200;
            projectile.penetrate = 1;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.aiStyle = -1;
            projectile.scale = 1f;
            projectile.light = 0.2f;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            //Redraw the projectile with the color not influenced by light
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY) - projectile.velocity;
                Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                float scale = projectile.scale * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                spriteBatch.Draw(mod.GetTexture("Projectiles/Wavetrail"), drawPos, new Rectangle(0, 54, 30, 18), color, projectile.rotation, drawOrigin, scale, SpriteEffects.None, 0f);
            }
            return true;
        }
        Color c = new Color(202, 30, 21);
        Vector3 x = new Vector3(202, 30, 21);
        public override void AI()
        {
            projectile.frameCounter++;
            if (projectile.frameCounter > 4)
            {
                projectile.frame++;
                if (projectile.frame >= Main.projFrames[projectile.type])
                {
                    projectile.frame = 0;
                }
                projectile.frameCounter = 0;
            }
            projectile.rotation = projectile.velocity.ToRotation() + (float)(Math.PI / 2f);
            Lighting.AddLight(projectile.position, x * 0.005f);
            projectile.damage = 0;
            for (int i = 0; i < 200; i++)
            {
                NPC target = Main.npc[i];
                if (projectile.Colliding(projectile.Hitbox, target.Hitbox) && target.active && !target.friendly)
                {
                    if (Main.rand.Next(5) == 0)
                    {
                        target.AddBuff(BuffID.OnFire, 240);
                    }
                    Player p = Main.player[projectile.owner];
                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Wave"), projectile.position);
                    Projectile.NewProjectile(projectile.Center, new Vector2(0), mod.ProjectileType("WaveExplode"), p.GetWeaponDamage(p.inventory[p.selectedItem]), 0f, projectile.owner, 2);
                    for (int o = 0; o < 18; ++o)
                    {
                        Vector2 perturbedSpeed = new Vector2(projectile.velocity.X, projectile.velocity.Y).RotatedByRandom(MathHelper.ToRadians(360));
                        Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType("Windwave"), perturbedSpeed.X * 0.7f, perturbedSpeed.Y * 0.7f, 50, c);
                    }
                    Lighting.AddLight(projectile.position, x * 0.005f);
                    projectile.active = false;
                }
            }
        }
        public override void Kill(int timeLeft)
        {
            Player p = Main.player[projectile.owner];
            Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Wave"), projectile.position);
            Projectile.NewProjectile(projectile.Center, new Vector2(0), mod.ProjectileType("WaveExplode"), p.GetWeaponDamage(p.inventory[p.selectedItem]), 0f, projectile.owner, 2);
            for (int i = 0; i < 18; ++i)
            {
                Vector2 perturbedSpeed = new Vector2(projectile.velocity.X, projectile.velocity.Y).RotatedByRandom(MathHelper.ToRadians(360));
                Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType("Windwave"), perturbedSpeed.X * 0.7f, perturbedSpeed.Y * 0.7f, 50, c);
            }
            Lighting.AddLight(projectile.position, x * 0.005f);
        }
    }
}