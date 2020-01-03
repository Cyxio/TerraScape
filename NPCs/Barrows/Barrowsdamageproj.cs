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
            projectile.aiStyle = -1;
            projectile.extraUpdates = 1;
            projectile.penetrate = 1;
            projectile.width = 10;
            projectile.height = 10;
            projectile.friendly = true;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 1;
        }
        public override void AI()
        {
            Dust dust = Dust.NewDustPerfect(projectile.Center, 58);
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
