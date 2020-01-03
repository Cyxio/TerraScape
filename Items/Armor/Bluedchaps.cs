using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class Bluedchaps : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blue Dragonhide Chaps");
            Tooltip.SetDefault("14% increased movement speed");
        }
        public override void SetDefaults()
        {
            item.width = 14;
            item.height = 32;
            item.value = Item.sellPrice(0, 4, 0, 0);
            item.rare = 4;
            item.defense = 11;
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.14f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Bluedhide", 13);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}