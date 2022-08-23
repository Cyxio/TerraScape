using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class MystictopL : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Light Mystic Robe Top");
            Tooltip.SetDefault("Increases maximum mana by 60\n4% increased magic critical strike chance");
        }
        public override void SetDefaults()
        {
            Item.width = 31;
            Item.height = 23;
            Item.value = Item.sellPrice(0, 3, 0, 0);
            Item.rare = ItemRarityID.LightRed;
            Item.defense = 11;
        }

        public override void UpdateEquip(Player player)
        {
            player.statManaMax2 += 60;
            player.GetCritChance(DamageClass.Magic) += 4;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "Mystictop");
            recipe.AddIngredient(ItemID.SoulofLight, 20);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}