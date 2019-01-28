using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.NPCs.Olm
{
    public class OlmBag : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Treasure Bag");
            Tooltip.SetDefault("Right click to open");
        }
        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.consumable = true;
            item.width = 24;
            item.height = 24;
            item.rare = 10;
            item.expert = true;
            bossBagNPC = mod.NPCType("Olm");
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void OpenBossBag(Player player)
        {
            Item.NewItem(player.Center, mod.ItemType<Tiles.OlmMusicBoxItem>(), 1, false, 0);
            int ch = Main.rand.Next(4);
            if (ch == 0)
            {
                player.QuickSpawnItem(mod.ItemType<Items.Magic.Kodaiinsignia>());
            }
            if (ch == 1)
            {
                player.QuickSpawnItem(mod.ItemType<Items.Dinhbulwark>());
            }
            if (ch == 2)
            {
                player.QuickSpawnItem(mod.ItemType<Items.Twistedbow>());
            }
            if (ch == 3)
            {
                player.QuickSpawnItem(mod.ItemType<Items.Dclaws>());
            }
            if (Main.rand.Next(3) == 0)
            {
                int supply = Main.rand.Next(3);
                if (supply == 0)
                {
                    player.QuickSpawnItem(mod.ItemType("Dragonstone"), Main.rand.Next(1, 5));
                }
                if (supply == 1)
                {
                    player.QuickSpawnItem(mod.ItemType("Onyx"), Main.rand.Next(1, 5));
                }
                if (supply == 2)
                {
                    player.QuickSpawnItem(mod.ItemType("Zenyte"), Main.rand.Next(1, 5));
                }
            }
        }
    }
}
