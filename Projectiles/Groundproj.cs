using Terraria.Audio;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;

namespace OldSchoolRuneScape.Projectiles
{
    public class Groundproj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Groundproj");
        }
        public override void SetDefaults()
        {
            Projectile.width = 1;
            Projectile.height = 1;
            Projectile.aiStyle = -1;
            Projectile.tileCollide = true;
            Projectile.penetrate = 1;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 1200;
            Projectile.ai[0] = 0;
            Projectile.ai[1] = 0;
        }
        public override void AI()
        {
            Projectile.velocity.X = 0;
            Projectile.velocity.Y = 500;
        }
        public override void Kill(int timeLeft)
        {
            Player p = Main.player[Projectile.owner];
            if (Projectile.ai[0] == 0)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X, Projectile.position.Y - 21, 0, 0, Mod.Find<ModProjectile>("Zamorakflame").Type, p.GetWeaponDamage(p.inventory[p.selectedItem]), 0f, Projectile.owner, 0, 0);
                SoundEngine.PlaySound(SoundID.Item72, Projectile.position);
            }
            if (Projectile.ai[0] == 1)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X, Projectile.position.Y - 32, 0, 0, Mod.Find<ModProjectile>("Guthixclaw").Type, p.GetWeaponDamage(p.inventory[p.selectedItem]), 0f, Projectile.owner, 0, 0);
                SoundEngine.PlaySound(SoundID.Item72, Projectile.position);
            }
        }
    }
}