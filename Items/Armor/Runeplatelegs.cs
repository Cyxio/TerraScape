using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class Runeplatelegs : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rune Platelegs");
        }
        public override void SetDefaults()
        {
            Item.width = 12;
            Item.height = 29;
            Item.value = Item.sellPrice(0, 3, 0, 0);
            Item.rare = ItemRarityID.Orange;
            Item.defense = 7;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "Runitebar", 10);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}