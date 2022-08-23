using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.NPCs.Barrows.Verac
{
    public class Ghostflail : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Verac's flail");
        }
        public override void SetDefaults()
        {
            Projectile.hostile = true;
            Projectile.width = 42;
            Projectile.height = 42;
            Projectile.scale = 1.3f;
            Projectile.penetrate = -1;
            Projectile.aiStyle = -1;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 60;
            Projectile.alpha = 0;
            Projectile.ai[0] = 3;
            Projectile.ai[1] = 0;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            if (NPC.AnyNPCs(ModContent.NPCType<Verac>()))
            {
                for (int i = 0; i < 200; i++)
                {
                    if (Main.npc[i].type == ModContent.NPCType<Verac>() && Main.npc[i].whoAmI == Projectile.ai[1])
                    {
                        NPC o = Main.npc[i];
                        Vector2 pcent = Projectile.Center;
                        Vector2 vcent = o.Center;
                        Vector2 dist = vcent - pcent;
                        Main.EntitySpriteDraw(ModContent.Request<Texture2D>("NPCs/Barrows/Verac/Veracchain").Value, vcent - Main.screenPosition,
                                new Rectangle(0, 0, 14, (int)dist.Length()), lightColor, dist.ToRotation() + MathHelper.ToRadians(90),
                                new Vector2(7, 0), 1f, SpriteEffects.None, 0);
                        break;
                    }
                }
            }
            return true;
        }

        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            target.AddBuff(BuffID.BrokenArmor, 600);
        }

        public override void AI()
        {
            Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Enchanted_Pink);
            Lighting.AddLight(Projectile.Center + Projectile.velocity, new Vector3(116 * 0.01f, 35 * 0.01f, 78 * 0.01f));
            Projectile.ai[0] += 3;
            Projectile.rotation = MathHelper.ToRadians(Projectile.ai[0]);
            if (NPC.AnyNPCs(ModContent.NPCType<Verac>()))
            {
                Projectile.timeLeft = 3;
                for (int i = 0; i < 200; i++)
                {
                    if (Main.npc[i].type == ModContent.NPCType<Verac>() && Main.npc[i].whoAmI == Projectile.ai[1])
                    {
                        NPC o = Main.npc[i];
                        Projectile.position = o.Center + new Vector2(0, 300 + (100 * o.ai[3])).RotatedBy(MathHelper.ToRadians(Projectile.ai[0]));
                        break;
                    }
                }
            }
        }
    }
}

