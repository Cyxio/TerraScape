using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class MysticbottomD : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dark Mystic Robe Bottom");
            Tooltip.SetDefault("12% increased magic critical strike chance");
        }
        public override void SetDefaults()
        {
            Item.width = 12;
            Item.height = 29;
            Item.value = Item.sellPrice(0, 3, 0, 0);
            Item.rare = ItemRarityID.LightRed;
            Item.defense = 7;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetCritChance(DamageClass.Magic) += 12;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "Mysticbottom");
            recipe.AddIngredient(ItemID.SoulofNight, 16);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}