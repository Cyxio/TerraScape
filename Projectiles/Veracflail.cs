using System;
using Terraria;
using Terraria.Audio;
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
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ownerHitCheck = true;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.aiStyle = -1;
            Projectile.ai[0] = 0;
            Projectile.ai[1] = 0;
        }
        public override void AI()
        {            
            Player owner = Main.player[Projectile.owner];
            owner.heldProj = Projectile.whoAmI;
            owner.itemTime = owner.itemAnimation;
            if (owner.itemAnimation < owner.itemAnimationMax / 3)
            {
                Projectile.ai[1] = 1;
            }
            if (Projectile.ai[1] == 1)
            {
                Projectile.tileCollide = false;
                Vector2 back = new Vector2(owner.MountedCenter.X - Projectile.Center.X, owner.MountedCenter.Y - Projectile.Center.Y);
                back.Normalize();
                Projectile.velocity = Projectile.velocity.Length() * back;
                if (Projectile.velocity.Length() < 20f)
                {
                    Projectile.velocity.Normalize();
                    Projectile.velocity *= 20f;
                }
                if (Projectile.velocity.Length() < 30f)
                {
                    Projectile.velocity *= 1.02f;
                }
            }
            if (Projectile.Hitbox.Contains((int)owner.Center.X, (int)owner.Center.Y) && Projectile.ai[1] == 1)
            {
                Projectile.Kill();
            }
            Projectile.rotation += MathHelper.ToRadians(Projectile.velocity.Length());
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.Center);
            Projectile.ai[1] = 1;
            return false;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            Player p = Main.player[Projectile.owner];
            if (crit && p.GetModPlayer<OSRSplayer>().Veracset && p.GetModPlayer<OSRSplayer>().Amuletdamned)
            {
                SoundEngine.PlaySound(SoundID.Item62.WithVolumeScale(0.5f), target.Center);
                damage += target.defense * 10;
                for (int i = 0; i < 36; i++)
                {
                    Vector2 rotata = new Vector2(0, 5).RotatedBy(MathHelper.ToRadians(10 * i));
                    int dust = Dust.NewDust(target.Center + rotata, 0, 0, DustID.Enchanted_Pink, rotata.X, rotata.Y, 0, default(Color), 1f);
                    Main.dust[dust].noGravity = true;
                }
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Vector2 playerCenter = Main.player[Projectile.owner].MountedCenter;
            Vector2 center = Projectile.Center;
            Vector2 distToProj = playerCenter - Projectile.Center;
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
                Main.EntitySpriteDraw(ModContent.Request<Texture2D>("Projectiles/Veracchain").Value, new Vector2(center.X - Main.screenPosition.X, center.Y - Main.screenPosition.Y),
                    new Rectangle(0, 0, 6, 18), drawColor, projRotation,
                    new Vector2(3, 9), 1f, SpriteEffects.None, 0);
            }
            return true;
        }
    }
}
