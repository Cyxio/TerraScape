using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.NPCs
{
    public class Cavehorror : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cave Horror");
            Main.npcFrameCount[npc.type] = 3;
        }
        public override void SetDefaults()
        {
            npc.scale = 1f;
            npc.width = 42;
            npc.height = 62;
            npc.damage = 50;
            npc.defense = 22;
            npc.lifeMax = 280;
            npc.knockBackResist = 0.5f;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath8;
            npc.value = Item.buyPrice(0, 0, 5);
            npc.aiStyle = -1;
        }
        public override void NPCLoot()
        {

        }
        public override void HitEffect(int hitDirection, double damage)
        {
            npc.ai[0] = 0;
            if (npc.life > 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 7, 1f, 1f, 0, new Color(84, 84, 66));
                }
            }
            else
            {
                for (int i = 0; i < 6; i++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 7, 1f, 1f, 0, new Color(84, 84, 66));
                }
                for (int i = 1; i < 5; i++)
                {
                    Gore.NewGore(npc.position, npc.velocity * Main.rand.NextFloat(0.9f, 1.1f), mod.GetGoreSlot("Gores/Cavehorror" + i), npc.scale);
                }
            }

        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return 0f; // SpawnCondition.Cavern.Chance * (Main.hardMode ? 0.2f : 0f);
        }
        public override void AI()
        {
            float lifeMult = 2f - (npc.life / (float)npc.lifeMax);
            npc.TargetClosest(true);
            if (npc.HasValidTarget)
            {
                if (npc.collideY)
                {
                    npc.velocity.X *= 0.95f;
                    if (Main.player[npc.target].position.X < npc.position.X)
                    {
                        npc.ai[0]--;
                    }
                    else if (Main.player[npc.target].position.X > npc.position.X)
                    {
                        npc.ai[0]++;
                    }
                    if (Math.Abs(npc.ai[0]) > 30)
                    {
                        float dist = Main.player[npc.target].position.X - npc.position.X;
                        npc.velocity = new Vector2(dist / 120f, -7f* lifeMult);
                        npc.ai[0] = 0;
                        npc.netUpdate = true;
                    }
                }
                else
                {
                    if (Math.Abs(npc.velocity.X) < 5f * lifeMult)
                    {
                        npc.velocity.X += 0.1f * npc.direction;
                    }
                    npc.ai[0] += 0.5f * npc.direction;
                    if (Math.Abs(npc.ai[0]) > 120)
                    {
                        float dist = Main.player[npc.target].position.X - npc.position.X;
                        npc.velocity = new Vector2(dist / 120f, -7f * lifeMult);
                        npc.ai[0] = 0;
                        npc.netUpdate = true;
                    }
                }
            }
        }
        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = npc.direction;
            if (Math.Abs(npc.ai[0]) < 5)
            {
                npc.frame.Y = 0;
            }
            else if (Math.Abs(npc.ai[0]) < 15)
            {
                npc.frame.Y = frameHeight;
            }
            else
            {
                npc.frame.Y = frameHeight*2;
            }
        }
    }
}