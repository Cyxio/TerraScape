using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ItemDropRules;
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
            Item.maxStack = 999;
            Item.consumable = true;
            Item.width = 24;
            Item.height = 24;
            Item.rare = ItemRarityID.Yellow;
        }

        public override int BossBagNPC => Mod.Find<ModNPC>("Barrowsspirit").Type;

        public override bool CanRightClick()
        {
            return true;
        }
        public override void OpenBossBag(Player player)
        {
            for (int i = 0; i < 10; i++)
            {
                player.TryGettingDevArmor(player.GetSource_OpenItem(Item.type));
            }         
            Item.NewItem(player.GetSource_OpenItem(Item.type), player.Center, ModContent.ItemType<Tiles.BarrowsMusicBoxItem>(), 1, false, 0);
            player.QuickSpawnItem(player.GetSource_OpenItem(Item.type), ModContent.ItemType<Items.Accessories.Amuletdamned>());
        }
    }
    public class Barrowsspirit : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spirit of Barrows");
            Main.npcFrameCount[NPC.type] = 4;
        }
        public override void SetDefaults()
        {
            NPC.lifeMax = 666;
            NPC.height = 70;
            NPC.width = 50;
            NPC.friendly = false;
            NPC.knockBackResist = 0f;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.alpha = 100;
            NPC.boss = true;
            NPC.aiStyle = -1;
            NPC.value = Item.buyPrice(0, 15);
            NPC.HitSound = SoundID.NPCHit21;
            NPC.DeathSound = SoundID.NPCDeath58;
            Music = OldSchoolRuneScape.barrowsMusic;
        }
        public override void AI()
        {
            NPC.TargetClosest();
            if (!NPC.HasValidTarget)
            {
                NPC.active = false;
            }
            int dustCounter = 0;
            NPC.ai[1]++;
            NPC.dontTakeDamage = false;
            if (NPC.AnyNPCs(ModContent.NPCType<Ahrim.Ahrim>()))
            {
                NPC.dontTakeDamage = true;
                dustCounter++;
            }
            if (NPC.AnyNPCs(ModContent.NPCType<Dharok.Dharok>()))
            {
                NPC.dontTakeDamage = true;
                dustCounter++;
            }
            if (NPC.AnyNPCs(ModContent.NPCType<Torag.Torag>()))
            {
                NPC.dontTakeDamage = true;
                dustCounter++;
            }
            if (NPC.AnyNPCs(ModContent.NPCType<Guthan.Guthan>()))
            {
                NPC.dontTakeDamage = true;
                dustCounter++;
            }
            if (NPC.AnyNPCs(ModContent.NPCType<Verac.Verac>()))
            {
                NPC.dontTakeDamage = true;
                dustCounter++;
            }
            if (NPC.AnyNPCs(ModContent.NPCType<Karil.Karil>()))
            {
                NPC.dontTakeDamage = true;
                dustCounter++;
            }
            for (int i = 0; i < dustCounter; i++)
            {
                Vector2 rotate = new Vector2(0, -130).RotatedBy(MathHelper.ToRadians(60 * i));
                Dust dust = Dust.NewDustPerfect(NPC.Center + rotate.RotatedBy(MathHelper.ToRadians(NPC.ai[1])), 58);
                dust.noGravity = true;
                dust.velocity *= 0f;
            }
        }
        public override void FindFrame(int frameHeight)
        {
            NPC.spriteDirection = -NPC.direction;
            int frameSpeed = 5;
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
                NPC.frame.Y = frameHeight * 3;
            }
            else if (NPC.frameCounter < frameSpeed * 5)
            {
                NPC.frame.Y = frameHeight * 2;
            }
            else if (NPC.frameCounter < frameSpeed * 6)
            {
                NPC.frame.Y = frameHeight;
            }
            else
            {
                NPC.frameCounter = 0;
            }
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<Barrowschest>()));
        }
        public override void OnKill()
        {
            OSRSworld.downedBarSpirit = true;
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            name = "Spirit of Barrows";
            potionType = ItemID.GreaterHealingPotion;
        }
    }
}
