using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.NPCs
{
    public class Smokedevil : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Smoke Devil");
            Main.npcFrameCount[npc.type] = 6;
        }
        public override void SetDefaults()
        {
            npc.width = 50;
            npc.height = 60;
            npc.damage = 100;
            npc.defense = 30;
            npc.lifeMax = 1100;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath26;
            npc.value = Item.buyPrice(0, 4);
            npc.aiStyle = 14;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.knockBackResist = 0.1f;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (Main.hardMode && NPC.downedPlantBoss)
            {
                return SpawnCondition.Underworld.Chance * 0.2f;
            }
            return 0;
        }
        public override void NPCLoot()
        {
            Item.NewItem(npc.Hitbox, ItemID.SmokeBlock, 10);
            if (Main.rand.Next(50) == 0)
            {
                Item.NewItem(npc.Hitbox, ModContent.ItemType<Items.Magic.Smokebattlestaff>());
            }
        }
        public override void AI()
        {
            if (Main.rand.Next(200) == 0 && npc.ai[3] == 0 && Main.netMode != 1)
            {
                npc.ai[3] = 1;
                npc.netUpdate = true;
            }
            if (npc.ai[3] != 0)
            {
                npc.ai[3]++;
            }
            if (npc.ai[3] > 30 && Main.netMode != 1)
            {
                npc.TargetClosest(true);
                Player target = Main.player[npc.target];
                float speedX = target.MountedCenter.X - npc.Center.X;
                float speedY = target.MountedCenter.Y - npc.Center.Y;
                Vector2 spd = new Vector2(speedX, speedY);
                spd.Normalize();
                spd *= 14;
                Projectile.NewProjectile(npc.Center, spd, ModContent.ProjectileType<Projectiles.Smokedevil>(), npc.damage / 4, 0f);
                npc.ai[3] = 0;
                npc.netUpdate = true;
            }
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life > 0)
            {
                for (int i = 0; i < 5; i++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 31, 0, 0, 100, default(Color), 1.2f);
                }
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 31, 0, 0, 100, default(Color), 1.2f);
                    Gore.NewGore(npc.Center, new Vector2(Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f)), Main.rand.Next(61, 64), 1f);
                }
            }
            
        }
        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = npc.direction;
            if (npc.ai[3] == 0)
            {
                int frameSpeed = 7;
                npc.frameCounter++;
                if (npc.frameCounter < frameSpeed)
                {
                    npc.frame.Y = 0;
                }
                else if (npc.frameCounter < frameSpeed * 2)
                {
                    npc.frame.Y = frameHeight;
                }
                else if (npc.frameCounter < frameSpeed * 3)
                {
                    npc.frame.Y = frameHeight * 2;
                }
                else if (npc.frameCounter < frameSpeed * 4)
                {
                    npc.frame.Y = frameHeight;
                }
                else
                {
                    npc.frameCounter = 0;
                }
            }
            else
            {
                if (npc.ai[3] < 10)
                {
                    npc.frame.Y = frameHeight * 3;
                }
                else if (npc.ai[3] < 20)
                {
                    npc.frame.Y = frameHeight * 4;
                }
                else if (npc.ai[3] < 30)
                {
                    npc.frame.Y = frameHeight * 5;
                }
            }
        }
    }
}