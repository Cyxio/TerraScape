using Microsoft.Xna.Framework;
using Terraria;
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
            Main.projFrames[projectile.type] = 4;
        }
        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 32;
            projectile.timeLeft = 180;
            projectile.penetrate = -1;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.aiStyle = -1;
            projectile.scale = 1f;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(170, 170, 170, 0);
        }
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
            projectile.rotation += MathHelper.ToRadians(8);
            projectile.damage = 0;
            for (int i = 0; i < Main.maxItems; i++)
            {
                Item target = Main.item[i];
                if (projectile.Colliding(projectile.Hitbox, target.Hitbox))
                {
                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Telegrab"), target.position);
                    target.position = Main.player[projectile.owner].position;
                }
            }
        }
    }
}