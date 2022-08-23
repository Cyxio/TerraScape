using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class Runeplate : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rune Platebody");
        }
        public override void SetDefaults()
        {
            Item.width = 31;
            Item.height = 23;
            Item.value = Item.sellPrice(0, 3, 0, 0);
            Item.rare = ItemRarityID.Orange;
            Item.defense = 8;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "Runitebar", 12);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}