using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.ClueScroll.ClueRewards.ForAll
{
    [AutoloadEquip(EquipType.HandsOn)]
    public class Booklaw : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Book of Law");
            Tooltip.SetDefault("25% increased ranged damage");
        }
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 30;
            item.rare = 3;
            item.accessory = true;
            item.value = Item.sellPrice(0, 10);
        }
        public override void UpdateEquip(Player player)
        {
            player.rangedDamage += 0.25f;
        }
        public override void AddRecipes()
        {
            ModRecipe r = new ModRecipe(mod);
            r.AddIngredient(mod.ItemType<Armadylpage1>());
            r.AddIngredient(mod.ItemType<Armadylpage2>());
            r.AddIngredient(mod.ItemType<Armadylpage3>());
            r.AddIngredient(mod.ItemType<Armadylpage4>());
            r.AddTile(TileID.WorkBenches);
            r.SetResult(this);
            r.AddRecipe();
        }
    }
}
