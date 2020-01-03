using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace OldSchoolRuneScape.Items
{
    public class Faladorteleport : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Falador Teleport");
            Tooltip.SetDefault("Teleports you to the dungeon\n-Not Working in Multiplayer-");
        }
        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.width = 28;
            item.height = 26;
            item.consumable = true;
            item.noUseGraphic = true;
            item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Teleport");
            item.useAnimation = 114;
            item.useTime = 114;
            item.useStyle = 2;
            item.rare = 1;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Airrune", 3);
            recipe.AddIngredient(null, "Waterrune", 1);
            recipe.AddIngredient(null, "Lawrune", 1);
            recipe.AddIngredient(ItemID.ClayBlock);
            recipe.AddTile(null, "Lectern");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        private List<Vector2> blocks = new List<Vector2>();
        public override void UseStyle(Player player)
        {
            Dust.NewDust(player.BottomLeft, 26, 0, 45, 0, -6f);
            if (player.itemAnimation == player.itemAnimationMax - 1)
            {
                for (int y = 0; y < Main.maxTilesY / 5; y++)
                {
                    for (int x = 0; x < Main.maxTilesX; x++)
                    {
                        if (Main.tile[x, y].type == TileID.BlueDungeonBrick || Main.tile[x, y].type == TileID.GreenDungeonBrick || Main.tile[x, y].type == TileID.PinkDungeonBrick)
                        {
                            blocks.Add(new Vector2(x, y));
                        }
                    }
                }
            }
            if (player.itemAnimation == 5)
            {
                Vector2[] blockks = blocks.ToArray();
                Vector2 teleport = blockks[(Main.rand.Next(0, blockks.Length))];
                blocks.Clear();
                for (int i = 0; i < 300; i++)
                {
                    if (Main.tile[(int)teleport.X, (int)teleport.Y - i].type == 0 && !Main.tile[(int)teleport.X, (int)teleport.Y - i].active())
                    {
                        teleport = new Vector2(teleport.X * 16, (teleport.Y - i) * 16 - 56);
                        player.Teleport(teleport, 3);
                        player.inventory[player.selectedItem].stack -= 1;
                        break;
                    }
                }
            }
            if (player.itemAnimation < 5)
            {
                for (int o = 0; o < 5; o++)
                {
                    Dust.NewDust(player.position, 32, 48, 45);
                }
            }
        }
    }
}
