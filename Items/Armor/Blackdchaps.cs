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
            Item.width = 14;
            Item.height = 32;
            Item.value = Item.sellPrice(0, 4, 0, 0);
            Item.rare = ItemRarityID.LightPurple;
            Item.defense = 18;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetCritChance(DamageClass.Ranged) += 8;
            player.moveSpeed += 0.14f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "Blackdhide", 13);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}