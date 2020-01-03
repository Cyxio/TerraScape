using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.NPCs.Barrows
{
    public class Barrowschest : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Barrows Chest");
            Tooltip.SetDefault("Right click to open");
        }
        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.consumable = true;
            item.width = 24;
            item.height = 24;
            item.rare = 8;
        }

        public override int BossBagNPC => mod.NPCType("Barrowsspirit");

        public override bool CanRightClick()
        {
            return true;
        }

        public override void OpenBossBag(Player player)
        {
            for (int i = 0; i < 10; i++)
            {
                player.TryGettingDevArmor();
            }         
            Item.NewItem(player.Center, ModContent.ItemType<Tiles.BarrowsMusicBoxItem>(), 1, false, 0);
            player.QuickSpawnItem(ModContent.ItemType<Items.Accessories.Amuletdamned>());
        }
    }
    public class Barrowsspirit : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spirit of Barrows");
            Main.npcFrameCount[npc.type] = 4;
        }
        public override void SetDefaults()
        {
            npc.lifeMax = 666;
            npc.height = 70;
            npc.width = 50;
            npc.friendly = false;
            npc.knockBackResist = 0f;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.alpha = 100;
            npc.boss = true;
            npc.aiStyle = -1;
            npc.value = Item.buyPrice(0, 15);
            npc.HitSound = SoundID.NPCHit21;
            npc.DeathSound = SoundID.NPCDeath58;
            music = OldSchoolRuneScape.barrowsMusic;
            bossBag = ModContent.ItemType<Barrowschest>();
        }
        public override void AI()
        {
            npc.TargetClosest();
            if (!npc.HasValidTarget)
            {
                npc.active = false;
            }
            int dustCounter = 0;
            npc.ai[1]++;
            npc.dontTakeDamage = false;
            if (NPC.AnyNPCs(ModContent.NPCType<Ahrim.Ahrim>()))
            {
                npc.dontTakeDamage = true;
                dustCounter++;
            }
            if (NPC.AnyNPCs(ModContent.NPCType<Dharok.Dharok>()))
            {
                npc.dontTakeDamage = true;
                dustCounter++;
            }
            if (NPC.AnyNPCs(ModContent.NPCType<Torag.Torag>()))
            {
                npc.dontTakeDamage = true;
                dustCounter++;
            }
            if (NPC.AnyNPCs(ModContent.NPCType<Guthan.Guthan>()))
            {
                npc.dontTakeDamage = true;
                dustCounter++;
            }
            if (NPC.AnyNPCs(ModContent.NPCType<Verac.Verac>()))
            {
                npc.dontTakeDamage = true;
                dustCounter++;
            }
            if (NPC.AnyNPCs(ModContent.NPCType<Karil.Karil>()))
            {
                npc.dontTakeDamage = true;
                dustCounter++;
            }
            for (int i = 0; i < dustCounter; i++)
            {
                Vector2 rotate = new Vector2(0, -130).RotatedBy(MathHelper.ToRadians(60 * i));
                Dust dust = Dust.NewDustPerfect(npc.Center + rotate.RotatedBy(MathHelper.ToRadians(npc.ai[1])), 58);
                dust.noGravity = true;
                dust.velocity *= 0f;
            }
        }
        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = -npc.direction;
            int frameSpeed = 5;
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
                npc.frame.Y = frameHeight * 3;
            }
            else if (npc.frameCounter < frameSpeed * 5)
            {
                npc.frame.Y = frameHeight * 2;
            }
            else if (npc.frameCounter < frameSpeed * 6)
            {
                npc.frame.Y = frameHeight;
            }
            else
            {
                npc.frameCounter = 0;
            }
        }
        public override void NPCLoot()
        {
            OSRSworld.downedBarSpirit = true;
            npc.DropBossBags();
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            name = "Spirit of Barrows";
            potionType = ItemID.GreaterHealingPotion;
        }
    }
}
