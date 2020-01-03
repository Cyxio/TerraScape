using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class MystictopD : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dark Mystic Robe Top");
            Tooltip.SetDefault("Increases maximum mana by 40\n8% increased magic critical strike chance");
        }
        public override void SetDefaults()
        {
            item.width = 31;
            item.height = 23;
            item.value = Item.sellPrice(0, 3, 0, 0);
            item.rare = 4;
            item.defense = 9;
        }

        public override void UpdateEquip(Player player)
        {
            player.statManaMax2 += 40;
            player.magicCrit += 8;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Mystictop");
            recipe.AddIngredient(ItemID.SoulofNight, 20);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}