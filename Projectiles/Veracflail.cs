using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OldSchoolRuneScape.Projectiles
{
    public class Veracflail : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Verac's flail");
        }
        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 30;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.melee = true;
            projectile.ownerHitCheck = true;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.aiStyle = -1;
            projectile.ai[0] = 0;
            projectile.ai[1] = 0;
        }
        public override void AI()
        {            
            Player owner = Main.player[projectile.owner];
            owner.heldProj = projectile.whoAmI;
            owner.itemTime = owner.itemAnimation;
            if (owner.itemAnimation < owner.itemAnimationMax / 3)
            {
                projectile.ai[1] = 1;
            }
            if (projectile.ai[1] == 1)
            {
                projectile.tileCollide = false;
                Vector2 back = new Vector2(owner.MountedCenter.X - projectile.Center.X, owner.MountedCenter.Y - projectile.Center.Y);
                back.Normalize();
                projectile.velocity = projectile.velocity.Length() * back;
                if (projectile.velocity.Length() < 20f)
                {
                    projectile.velocity.Normalize();
                    projectile.velocity *= 20f;
                }
                if (projectile.velocity.Length() < 30f)
                {
                    projectile.velocity *= 1.02f;
                }
            }
            if (projectile.Hitbox.Contains((int)owner.Center.X, (int)owner.Center.Y) && projectile.ai[1] == 1)
            {
                projectile.Kill();
            }
            projectile.rotation += MathHelper.ToRadians(projectile.velocity.Length());
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Main.PlaySound(SoundID.Dig, projectile.Center);
            projectile.ai[1] = 1;
            return false;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            Player p = Main.player[projectile.owner];
            if (crit && p.GetModPlayer<OSRSplayer>().Veracset && p.GetModPlayer<OSRSplayer>().Amuletdamned)
            {
                Main.PlaySound(SoundID.Item62.WithVolume(0.5f), target.Center);
                damage += target.defense * 10;
                for (int i = 0; i < 36; i++)
                {
                    Vector2 rotata = new Vector2(0, 5).RotatedBy(MathHelper.ToRadians(10 * i));
                    int dust = Dust.NewDust(target.Center + rotata, 0, 0, 58, rotata.X, rotata.Y, 0, default(Color), 1f);
                    Main.dust[dust].noGravity = true;
                }
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 playerCenter = Main.player[projectile.owner].MountedCenter;
            Vector2 center = projectile.Center;
            Vector2 distToProj = playerCenter - projectile.Center;
            float projRotation = distToProj.ToRotation() - 1.57f;
            float distance = distToProj.Length();
            while (distance > 30f && !float.IsNaN(distance))
            {
                distToProj.Normalize();
                distToProj *= 18;
                center += distToProj;
                distToProj = playerCenter - center;
                distance = distToProj.Length();
                Color drawColor = lightColor;

                //Draw chain
                spriteBatch.Draw(mod.GetTexture("Projectiles/Veracchain"), new Vector2(center.X - Main.screenPosition.X, center.Y - Main.screenPosition.Y),
                    new Rectangle(0, 0, 6, 18), drawColor, projRotation,
                    new Vector2(3, 9), 1f, SpriteEffects.None, 0f);
            }
            return true;
        }
    }
}
