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
            projectile.hostile = true;
            projectile.width = 42;
            projectile.height = 42;
            projectile.scale = 1.3f;
            projectile.penetrate = -1;
            projectile.aiStyle = -1;
            projectile.tileCollide = false;
            projectile.timeLeft = 60;
            projectile.alpha = 0;
            projectile.ai[0] = 3;
            projectile.ai[1] = 0;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            if (NPC.AnyNPCs(ModContent.NPCType<Verac>()))
            {
                for (int i = 0; i < 200; i++)
                {
                    if (Main.npc[i].type == ModContent.NPCType<Verac>() && Main.npc[i].whoAmI == projectile.ai[1])
                    {
                        NPC o = Main.npc[i];
                        Vector2 pcent = projectile.Center;
                        Vector2 vcent = o.Center;
                        Vector2 dist = vcent - pcent;
                        spriteBatch.Draw(mod.GetTexture("NPCs/Barrows/Verac/Veracchain"), vcent - Main.screenPosition,
                                new Rectangle(0, 0, 14, (int)dist.Length()), lightColor, dist.ToRotation() + MathHelper.ToRadians(90),
                                new Vector2(7, 0), 1f, SpriteEffects.None, 0f);
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
            Dust.NewDust(projectile.position, projectile.width, projectile.height, 58);
            Lighting.AddLight(projectile.Center + projectile.velocity, new Vector3(116 * 0.01f, 35 * 0.01f, 78 * 0.01f));
            projectile.ai[0] += 3;
            projectile.rotation = MathHelper.ToRadians(projectile.ai[0]);
            if (NPC.AnyNPCs(ModContent.NPCType<Verac>()))
            {
                projectile.timeLeft = 3;
                for (int i = 0; i < 200; i++)
                {
                    if (Main.npc[i].type == ModContent.NPCType<Verac>() && Main.npc[i].whoAmI == projectile.ai[1])
                    {
                        NPC o = Main.npc[i];
                        projectile.position = o.Center + new Vector2(0, 300 + (100 * o.ai[3])).RotatedBy(MathHelper.ToRadians(projectile.ai[0]));
                        break;
                    }
                }
            }
        }
    }
}

