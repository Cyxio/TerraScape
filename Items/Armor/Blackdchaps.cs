using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class Blackdchaps : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Black Dragonhide Chaps");
            Tooltip.SetDefault("8% increased ranged critical strike chance\n14% increased movement speed");
        }
        public override void SetDefaults()
        {
            item.width = 14;
            item.height = 32;
            item.value = Item.sellPrice(0, 4, 0, 0);
            item.rare = 6;
            item.defense = 18;
        }

        public override void UpdateEquip(Player player)
        {
            player.rangedCrit += 8;
            player.moveSpeed += 0.14f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Blackdhide", 13);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}