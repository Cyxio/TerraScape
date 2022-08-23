using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class Greendchaps : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Green Dragonhide Chaps");
            Tooltip.SetDefault("10% increased movement speed");
        }
        public override void SetDefaults()
        {
            Item.width = 14;
            Item.height = 32;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = ItemRarityID.Orange;
            Item.defense = 7;
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.1f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "Greendhide", 13);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}