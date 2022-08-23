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
            Item.width = 32;
            Item.height = 30;
            Item.rare = ItemRarityID.Orange;
            Item.accessory = true;
            Item.value = Item.sellPrice(0, 10);
        }
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Melee) += 0.25f;
        }
        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ModContent.ItemType<Bandospage1>());
            r.AddIngredient(ModContent.ItemType<Bandospage2>());
            r.AddIngredient(ModContent.ItemType<Bandospage3>());
            r.AddIngredient(ModContent.ItemType<Bandospage4>());
            r.AddTile(TileID.WorkBenches);
            r.Register();
        }
    }
}
