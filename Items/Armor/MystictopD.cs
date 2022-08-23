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
            Item.width = 31;
            Item.height = 23;
            Item.value = Item.sellPrice(0, 3, 0, 0);
            Item.rare = ItemRarityID.LightRed;
            Item.defense = 9;
        }

        public override void UpdateEquip(Player player)
        {
            player.statManaMax2 += 40;
            player.GetCritChance(DamageClass.Magic) += 8;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "Mystictop");
            recipe.AddIngredient(ItemID.SoulofNight, 20);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}