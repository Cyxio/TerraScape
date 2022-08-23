using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.NPCs.Barrows
{
    public class Barrowsdamageproj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("BarrowsDamage");
        }
        public override void SetDefaults()
        {
            Projectile.aiStyle = -1;
            Projectile.extraUpdates = 1;
            Projectile.penetrate = 1;
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 1;
        }
        public override void AI()
        {
            Dust dust = Dust.NewDustPerfect(Projectile.Center, 58);
            dust.noGravity = true;
            dust.velocity *= 0f;
            dust.scale = 1f;
        }
        public override bool? CanHitNPC(NPC target)
        {
            return target.type == ModContent.NPCType<Barrowsspirit>();
        }
    }
}
