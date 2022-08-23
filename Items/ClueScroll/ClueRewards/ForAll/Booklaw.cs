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
            Item.width = 32;
            Item.height = 30;
            Item.rare = ItemRarityID.Orange;
            Item.accessory = true;
            Item.value = Item.sellPrice(0, 10);
        }
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Ranged) += 0.25f;
        }
        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ModContent.ItemType<Armadylpage1>());
            r.AddIngredient(ModContent.ItemType<Armadylpage2>());
            r.AddIngredient(ModContent.ItemType<Armadylpage3>());
            r.AddIngredient(ModContent.ItemType<Armadylpage4>());
            r.AddTile(TileID.WorkBenches);
            r.Register();
        }
    }
}
