using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.ClueScroll.ClueRewards.ForAll
{
    [AutoloadEquip(EquipType.HandsOn)]
    public class Bookdarkness : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Book of Darkness");
            Tooltip.SetDefault("25% increased magic damage");
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
            player.GetDamage(DamageClass.Magic) += 0.25f;
        }
        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ModContent.ItemType<Ancientpage1>());
            r.AddIngredient(ModContent.ItemType<Ancientpage2>());
            r.AddIngredient(ModContent.ItemType<Ancientpage3>());
            r.AddIngredient(ModContent.ItemType<Ancientpage4>());
            r.AddTile(TileID.WorkBenches);
            r.Register();
        }
    }
}
