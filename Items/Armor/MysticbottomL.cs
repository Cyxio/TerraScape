using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class MysticbottomL : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Light Mystic Robe Bottom");
            Tooltip.SetDefault("8% increased magic critical strike chance");
        }
        public override void SetDefaults()
        {
            item.width = 12;
            item.height = 29;
            item.value = Item.sellPrice(0, 3, 0, 0);
            item.rare = 4;
            item.defense = 9;
        }

        public override void UpdateEquip(Player player)
        {
            player.magicCrit += 8;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Mysticbottom");
            recipe.AddIngredient(ItemID.SoulofLight, 16);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}