using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class Mysticbottom : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mystic Robe Bottom");
            Tooltip.SetDefault("8% increased magic critical strike chance");
        }
        public override void SetDefaults()
        {
            item.width = 12;
            item.height = 29;
            item.value = Item.sellPrice(0, 3, 0, 0);
            item.rare = 3;
            item.defense = 6;
        }

        public override void UpdateEquip(Player player)
        {
            player.magicCrit += 8;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType<Mysticcomponents>(), 5);
            recipe.AddIngredient(ItemID.Silk, 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}