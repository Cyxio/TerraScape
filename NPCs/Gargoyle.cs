using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.NPCs
{
    public class Gargoyle : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gargoyle");
            Main.npcFrameCount[npc.type] = 3;
        }
        public override void SetDefaults()
        {
            npc.scale = 1.2f;
            npc.friendly = false;
            npc.lifeMax = 100;
            npc.defense = 10;
            npc.damage = 40;
            npc.knockBackResist = 0.3f;
            npc.noGravity = true;
            npc.width = 44;
            npc.height = 44;
            npc.HitSound = SoundID.NPCHit7;
            npc.DeathSound = SoundID.NPCDeath44;
            npc.value = 250f;
            npc.aiStyle = 14;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return SpawnCondition.Cavern.Chance * (Main.hardMode ? 0.15f : 0.5f);
        }
        public override void NPCLoot()
        {
            if (Main.rand.Next(33) == 0)
            {
                Item.NewItem(npc.Hitbox, mod.ItemType<Items.Granitemaul>());
            }
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life > 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 7, 1f, 1f, 0, Color.LightGray);
                }
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 7, 1f, 1f, 0, Color.LightGray);
                }
                Gore.NewGore(npc.position, npc.velocity * Main.rand.NextFloat(0.9f, 1.1f), mod.GetGoreSlot("Gores/Gargoyle1"), 1f);
                Gore.NewGore(npc.position, npc.velocity * Main.rand.NextFloat(0.9f, 1.1f), mod.GetGoreSlot("Gores/Gargoyle"), 1f);
            }
        }
        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = npc.direction;
            npc.rotation = npc.velocity.X * 0.1f;
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
            else
            {
                npc.frameCounter = 0;
            }
        }
    }
}
