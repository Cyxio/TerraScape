using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace OldSchoolRuneScape.NPCs
{
    public class Smokedevil : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Smoke Devil");
            Main.npcFrameCount[NPC.type] = 6;
        }
        public override void SetDefaults()
        {
            NPC.width = 50;
            NPC.height = 60;
            NPC.damage = 100;
            NPC.defense = 30;
            NPC.lifeMax = 1100;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath26;
            NPC.value = Item.buyPrice(0, 4);
            NPC.aiStyle = 14;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.knockBackResist = 0.1f;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (Main.hardMode && NPC.downedPlantBoss)
            {
                return SpawnCondition.Underworld.Chance * 0.2f;
            }
            return 0;
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Magic.Smokebattlestaff>(), 50));
            npcLoot.Add(ItemDropRule.Common(ItemID.SmokeBlock, 1, 10, 20));
        }
        public override void AI()
        {
            NPC.TargetClosest(true);
            Player target = Main.player[NPC.target];
            if (Collision.CanHitLine(NPC.position, NPC.width, NPC.height, target.position, target.width, target.height))
            {
                NPC.ai[3]++;
            }
            if (NPC.ai[3] > 120 && Main.netMode != NetmodeID.MultiplayerClient)
            {
                float speedX = target.MountedCenter.X - NPC.Center.X;
                float speedY = target.MountedCenter.Y - NPC.Center.Y;
                Vector2 spd = new Vector2(speedX, speedY);
                spd.Normalize();
                spd *= 14;
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, spd, ModContent.ProjectileType<Projectiles.Smokedevil>(), NPC.damage / 4, 0f);
                NPC.ai[3] = Main.rand.Next(0, 30);
                NPC.netUpdate = true;
            }
        }
        public override void OnHitByProjectile(Projectile projectile, int damage, float knockback, bool crit)
        {
            NPC.ai[3] += 10;
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                return;
            }
            if (NPC.life > 0)
            {
                for (int i = 0; i < 5; i++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Smoke, 0, 0, 100, default(Color), 1.2f);
                }
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Smoke, 0, 0, 100, default(Color), 1.2f);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.Center, new Vector2(Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f)), Main.rand.Next(61, 64), 1f);
                }
            }
            
        }
        public override void FindFrame(int frameHeight)
        {
            NPC.spriteDirection = NPC.direction;
            if (NPC.ai[3] <= 90)
            {
                int frameSpeed = 7;
                NPC.frameCounter++;
                if (NPC.frameCounter < frameSpeed)
                {
                    NPC.frame.Y = 0;
                }
                else if (NPC.frameCounter < frameSpeed * 2)
                {
                    NPC.frame.Y = frameHeight;
                }
                else if (NPC.frameCounter < frameSpeed * 3)
                {
                    NPC.frame.Y = frameHeight * 2;
                }
                else if (NPC.frameCounter < frameSpeed * 4)
                {
                    NPC.frame.Y = frameHeight;
                }
                else
                {
                    NPC.frameCounter = 0;
                }
            }
            else
            {
                if (NPC.ai[3] < 100)
                {
                    NPC.frame.Y = frameHeight * 3;
                }
                else if (NPC.ai[3] < 110)
                {
                    NPC.frame.Y = frameHeight * 4;
                }
                else if (NPC.ai[3] < 120)
                {
                    NPC.frame.Y = frameHeight * 5;
                }
            }
        }
    }
}