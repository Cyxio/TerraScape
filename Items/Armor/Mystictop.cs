using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class Mystictop : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mystic Robe Top");
            Tooltip.SetDefault("Increases maximum mana by 40\n4% increased magic critical strike chance");
        }
        public override void SetDefaults()
        {
            Item.width = 31;
            Item.height = 23;
            Item.value = Item.sellPrice(0, 3, 0, 0);
            Item.rare = ItemRarityID.Orange;
            Item.defense = 8;
        }

        public override void UpdateEquip(Player player)
        {
            player.statManaMax2 += 40;
            player.GetCritChance(DamageClass.Magic) += 4;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Mysticcomponents>(), 5);
            recipe.AddIngredient(ItemID.Silk, 14);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}