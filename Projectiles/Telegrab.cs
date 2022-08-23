using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace OldSchoolRuneScape.Projectiles
{
    public class Telegrab : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Telegrab");
            Main.projFrames[Projectile.type] = 4;
        }
        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.timeLeft = 180;
            Projectile.penetrate = -1;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.aiStyle = -1;
            Projectile.scale = 1f;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(170, 170, 170, 0);
        }
        public override void AI()
        {
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
            Projectile.rotation += MathHelper.ToRadians(8);
            Projectile.damage = 0;
            for (int i = 0; i < Main.maxItems; i++)
            {
                Item target = Main.item[i];
                if (Projectile.Colliding(Projectile.Hitbox, target.Hitbox))
                {
                    SoundEngine.PlaySound(new SoundStyle("OldSchoolRuneScape/Sounds/Item/Telegrab"), target.position);
                    target.position = Main.player[Projectile.owner].position;
                }
            }
        }
    }
}