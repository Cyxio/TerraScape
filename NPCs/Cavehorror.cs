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
            Main.npcFrameCount[NPC.type] = 3;
        }
        public override void SetDefaults()
        {
            NPC.scale = 1f;
            NPC.width = 42;
            NPC.height = 62;
            NPC.damage = 50;
            NPC.defense = 22;
            NPC.lifeMax = 280;
            NPC.knockBackResist = 0.5f;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath8;
            NPC.value = Item.buyPrice(0, 0, 5);
            NPC.aiStyle = -1;
        }
        public override void OnKill()
        {

        }
        public override void HitEffect(int hitDirection, double damage)
        {
            NPC.ai[0] = 0;
            if (Main.netMode == NetmodeID.Server)
            {
                return;
            }
            if (NPC.life > 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.WoodFurniture, 1f, 1f, 0, new Color(84, 84, 66));
                }
            }
            else
            {
                for (int i = 0; i < 6; i++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.WoodFurniture, 1f, 1f, 0, new Color(84, 84, 66));
                }
                for (int i = 1; i < 5; i++)
                {
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * Main.rand.NextFloat(0.9f, 1.1f), Mod.Find<ModGore>("Cavehorror" + i).Type, NPC.scale);
                }
            }

        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return 0f; // SpawnCondition.Cavern.Chance * (Main.hardMode ? 0.2f : 0f);
        }
        public override void AI()
        {
            float lifeMult = 2f - (NPC.life / (float)NPC.lifeMax);
            NPC.TargetClosest(true);
            if (NPC.HasValidTarget)
            {
                if (NPC.collideY)
                {
                    NPC.velocity.X *= 0.95f;
                    if (Main.player[NPC.target].position.X < NPC.position.X)
                    {
                        NPC.ai[0]--;
                    }
                    else if (Main.player[NPC.target].position.X > NPC.position.X)
                    {
                        NPC.ai[0]++;
                    }
                    if (Math.Abs(NPC.ai[0]) > 30)
                    {
                        float dist = Main.player[NPC.target].position.X - NPC.position.X;
                        NPC.velocity = new Vector2(dist / 120f, -7f* lifeMult);
                        NPC.ai[0] = 0;
                        NPC.netUpdate = true;
                    }
                }
                else
                {
                    if (Math.Abs(NPC.velocity.X) < 5f * lifeMult)
                    {
                        NPC.velocity.X += 0.1f * NPC.direction;
                    }
                    NPC.ai[0] += 0.5f * NPC.direction;
                    if (Math.Abs(NPC.ai[0]) > 120)
                    {
                        float dist = Main.player[NPC.target].position.X - NPC.position.X;
                        NPC.velocity = new Vector2(dist / 120f, -7f * lifeMult);
                        NPC.ai[0] = 0;
                        NPC.netUpdate = true;
                    }
                }
            }
        }
        public override void FindFrame(int frameHeight)
        {
            NPC.spriteDirection = NPC.direction;
            if (Math.Abs(NPC.ai[0]) < 5)
            {
                NPC.frame.Y = 0;
            }
            else if (Math.Abs(NPC.ai[0]) < 15)
            {
                NPC.frame.Y = frameHeight;
            }
            else
            {
                NPC.frame.Y = frameHeight*2;
            }
        }
    }
}