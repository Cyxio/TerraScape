using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.NPCs
{
    public class Impling : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Impling");
            Main.npcFrameCount[npc.type] = 33;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return SpawnCondition.OverworldDayBirdCritter.Chance * 0.2f + SpawnCondition.TownCritter.Chance * 0.1f;
        }
        public override void SetDefaults()
        {
            npc.width = 20;
            npc.height = 30;
            npc.aiStyle = 65;
            npc.damage = 0;
            npc.defense = 0;
            npc.lifeMax = 5;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.npcSlots = 0.25f;
            npc.noGravity = true;
            npc.catchItem = (short)mod.ItemType<Items.Onyx>();
            npc.alpha = 1;
        }
        public override void AI()
        {
            if (npc.alpha == 1 && Main.netMode != 1)
            {
                npc.ai[2] = Main.rand.Next(1, 8);
                if (Main.hardMode && Main.rand.Next(10) == 0)
                {
                    npc.ai[2] = 8;
                }
                if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && Main.rand.Next(20) == 0)
                {
                    npc.ai[2] = 10;
                }
                if (NPC.downedPlantBoss && Main.rand.Next(30) == 0)
                {
                    npc.ai[2] = 9;
                }
                if (NPC.downedPlantBoss && Main.rand.Next(120) == 0)
                {
                    npc.ai[2] = 10;
                }
                npc.alpha = 0;
                npc.netUpdate = true;         
            }
            string mmmmm = ("Implingjar" + ((int)npc.ai[2] - 1));
            npc.catchItem = (short)mod.ItemType(mmmmm);
            if (npc.ai[2] == 5 && Main.rand.Next(5) == 0)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, 187);
            }
            if (npc.ai[2] == 8 && Main.rand.Next(5) == 0)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, 204);
            }
            if (npc.ai[2] == 9 && Main.rand.Next(5) == 0)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, 199);
            }
            if (npc.ai[2] == 10 && Main.rand.Next(5) == 0)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, 162);
            }
            if (npc.ai[2] == 11 && Main.rand.Next(5) == 0)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, 254);
            }
        }
        public override void FindFrame(int frameHeight)
        {
            int num117 = 7;
            npc.rotation = npc.velocity.X * 0.3f;
            npc.spriteDirection = npc.direction;
            npc.frameCounter = npc.frameCounter + 1.0 + (double)((Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y)) / 2f);
            if (npc.frameCounter < (double)num117)
            {
                npc.frame.Y = 0;
            }
            else if (npc.frameCounter < (double)(num117 * 2))
            {
                npc.frame.Y = frameHeight;
            }
            else if (npc.frameCounter < (double)(num117 * 3))
            {
                npc.frame.Y = frameHeight * 2;
            }
            else
            {
                npc.frame.Y = frameHeight;
                if (npc.frameCounter >= (double)(num117 * 4 - 1))
                {
                    npc.frameCounter = 0.0;
                }
            }
            npc.frame.Y = npc.frame.Y + (int)((float)(frameHeight * 3) * (npc.ai[2] - 1f));
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, 245);
            }
        }
    }
}
