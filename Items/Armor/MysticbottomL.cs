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
            Item.width = 12;
            Item.height = 29;
            Item.value = Item.sellPrice(0, 3, 0, 0);
            Item.rare = ItemRarityID.LightRed;
            Item.defense = 9;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetCritChance(DamageClass.Magic) += 8;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "Mysticbottom");
            recipe.AddIngredient(ItemID.SoulofLight, 16);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}