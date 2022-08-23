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
            Item.width = 14;
            Item.height = 32;
            Item.value = Item.sellPrice(0, 4, 0, 0);
            Item.rare = ItemRarityID.LightRed;
            Item.defense = 11;
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.14f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "Bluedhide", 13);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}