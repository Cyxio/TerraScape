using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.NPCs
{
    public class Blackdemon : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Black demon");
            Main.npcFrameCount[npc.type] = 3;
        }
        public override void SetDefaults()
        {
            npc.scale = 1.2f;
            npc.friendly = false;
            npc.lifeMax = 300;
            npc.defense = 20;
            npc.damage = 60;
            npc.knockBackResist = 0.2f;
            npc.noGravity = true;
            npc.width = 44;
            npc.height = 64;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Demon");
            npc.value = 1000f;
            npc.aiStyle = 14;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (!Main.dayTime && Main.hardMode)
            {
                return SpawnCondition.OverworldNightMonster.Chance * 0.05f;
            }
            else
            {
                return 0;
            }
        }
        public override void NPCLoot()
        {
            int ch = Main.rand.Next(100);
            if (ch < 20)
            {
                Item.NewItem(npc.position, new Vector2(44, 58), mod.ItemType("Firerune"), Main.rand.Next(30, 120));
            }
            else if (ch < 25)
            {
                Item.NewItem(npc.position, new Vector2(44, 58), mod.ItemType("Lawrune"), 12);
            }
            else if (ch < 30)
            {
                Item.NewItem(npc.position, new Vector2(44, 58), mod.ItemType("Chaosrune"), 12);
            }
            else if (ch < 35)
            {
                Item.NewItem(npc.position, new Vector2(44, 58), mod.ItemType("Deathrune"), 3);
            }
            else if (ch < 36)
            {
                Item.NewItem(npc.position, new Vector2(44, 58), mod.ItemType("Runeplate"));
            }
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life > 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 7, 1f, 1f, 0, Color.Black);
                }
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 7, 1f, 1f, 0, Color.Black);
                }
                Gore.NewGore(npc.position, npc.velocity * Main.rand.NextFloat(0.9f, 1.1f), mod.GetGoreSlot("Gores/Blackdemon"), npc.scale);
                Gore.NewGore(npc.position, npc.velocity * Main.rand.NextFloat(0.9f, 1.1f), mod.GetGoreSlot("Gores/Blackdemon1"), npc.scale);
                Gore.NewGore(npc.position, npc.velocity * Main.rand.NextFloat(0.9f, 1.1f), mod.GetGoreSlot("Gores/Blackdemon1"), npc.scale);
            }
        }
        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = -npc.direction;
            npc.rotation = npc.velocity.X * 0.08f;
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
