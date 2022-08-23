using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.NPCs.Barrows.Ahrim
{
    public class Ghostspell : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ahrim's spell");
            Main.projFrames[Projectile.type] = 4;
        }
        public override void SetDefaults()
        {
            Projectile.hostile = true;
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.scale = 1f;
            Projectile.penetrate = -1;
            Projectile.aiStyle = -1;
            Projectile.tileCollide = true;
            Projectile.timeLeft = 480;
            Projectile.alpha = 0;
            Projectile.ai[0] = 0;
            Projectile.ai[1] = 0;
        }
        public override void AI()
        {
            Lighting.AddLight(Projectile.Center + Projectile.velocity, new Vector3(116 * 0.01f, 35 * 0.01f, 78 * 0.01f));
            Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Enchanted_Pink);
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90);

            Projectile.frameCounter++;
            if (Projectile.frameCounter > 4)
            {
                Projectile.frame++;
                if (Projectile.frame >= Main.projFrames[Projectile.type])
                {
                    Projectile.frame = 0;
                }
                Projectile.frameCounter = 0;
            }
            if (Projectile.alpha == 0)
            {
                SoundEngine.PlaySound(SoundID.Item20, Projectile.position);
            }
            if (Projectile.alpha < 10)
            {
                Projectile.ai[1] = 9;
            }
            if (Projectile.alpha > 200)
            {
                Projectile.ai[1] = -9;
            }
            Projectile.alpha += (int)Projectile.ai[1];

            if (Projectile.ai[0] == 0)
            {
                if (Projectile.timeLeft > 408)
                {
                    Projectile.velocity = Projectile.velocity.RotatedBy(MathHelper.ToRadians(5));
                }
            }
            if (Projectile.ai[0] == 1)
            {
                Projectile.scale = 1.5f;
                if (Main.rand.NextBool(Projectile.timeLeft))
                {
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Projectile.velocity.RotatedBy(MathHelper.ToRadians(90)) * 1.5f, ModContent.ProjectileType<Ghostspell>(), Projectile.damage, 0f, 0, 99, 0); ;
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Projectile.velocity.RotatedBy(MathHelper.ToRadians(-90)) * 1.5f, ModContent.ProjectileType<Ghostspell>(), Projectile.damage, 0f, 0, 99, 0);
                    Projectile.Kill();
                }
            }
            if (Projectile.ai[0] == 2)
            {
                if (Projectile.timeLeft > 461)
                {
                    Projectile.velocity = Projectile.velocity.RotatedBy(MathHelper.ToRadians(5));
                }
            }
            if (Projectile.ai[0] == 3)
            {
                if (Projectile.timeLeft > 461)
                {
                    Projectile.velocity = Projectile.velocity.RotatedBy(MathHelper.ToRadians(-5));
                }                
            }
            if (Projectile.ai[0] == 4)
            {
                Projectile.tileCollide = false;
            }  
        }
        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            if (Projectile.ai[0] == 2 || Projectile.ai[0] == 3)
            {
                damage = (int)((10 + (target.statDefense * 0.75f)) / 4f);
            }
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (Projectile.ai[0] == 2 || Projectile.ai[0] == 3)
            {
                target.immuneTime = 0;
            }
        }
    }
}