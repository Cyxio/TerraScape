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
            Item.maxStack = 999;
            Item.consumable = true;
            Item.width = 24;
            Item.height = 24;
            Item.rare = ItemRarityID.Red;
            Item.expert = true;
        }

        public override int BossBagNPC => Mod.Find<ModNPC>("Olm").Type;

        public override bool CanRightClick()
        {
            return true;
        }

        public override void OpenBossBag(Player player)
        {
            var source = player.GetSource_OpenItem(Item.type);
            Item.NewItem(source, player.Center, ModContent.ItemType<Tiles.OlmMusicBoxItem>(), 1, false, 0);
            int ch = Main.rand.Next(4);
            if (ch == 0)
            {
                player.QuickSpawnItem(source, ModContent.ItemType<Items.Magic.Kodaiinsignia>());
            }
            if (ch == 1)
            {
                player.QuickSpawnItem(source, ModContent.ItemType<Items.Weapons.Melee.Dinhbulwark>());
            }
            if (ch == 2)
            {
                player.QuickSpawnItem(source, ModContent.ItemType<Items.Weapons.Ranged.Twistedbow>());
            }
            if (ch == 3)
            {
                player.QuickSpawnItem(source, ModContent.ItemType<Items.Weapons.Melee.Dclaws>());
            }
            if (Main.rand.NextBool(3))
            {
                int supply = Main.rand.Next(3);
                if (supply == 0)
                {
                    player.QuickSpawnItem(source, Mod.Find<ModItem>("Dragonstone").Type, Main.rand.Next(1, 5));
                }
                if (supply == 1)
                {
                    player.QuickSpawnItem(source, Mod.Find<ModItem>("Onyx").Type, Main.rand.Next(1, 5));
                }
                if (supply == 2)
                {
                    player.QuickSpawnItem(source, Mod.Find<ModItem>("Zenyte").Type, Main.rand.Next(1, 5));
                }
            }
        }
    }
}
