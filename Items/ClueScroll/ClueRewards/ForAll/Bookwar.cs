using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.ClueScroll.ClueRewards.ForAll
{
    [AutoloadEquip(EquipType.HandsOn)]
    public class Bookwar : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Book of War");
            Tooltip.SetDefault("25% increased melee damage");
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
            player.meleeDamage += 0.25f;
        }
        public override void AddRecipes()
        {
            ModRecipe r = new ModRecipe(mod);
            r.AddIngredient(ModContent.ItemType<Bandospage1>());
            r.AddIngredient(ModContent.ItemType<Bandospage2>());
            r.AddIngredient(ModContent.ItemType<Bandospage3>());
            r.AddIngredient(ModContent.ItemType<Bandospage4>());
            r.AddTile(TileID.WorkBenches);
            r.SetResult(this);
            r.AddRecipe();
        }
    }
}
