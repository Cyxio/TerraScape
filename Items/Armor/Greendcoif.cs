using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class Greendcoif : ModItem
    {
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == Mod.Find<ModItem>("Greendbody").Type && legs.type == Mod.Find<ModItem>("Greendchaps").Type;
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "15% increased ranged damage\n20% chance not to consume ammo";
            player.GetDamage(DamageClass.Ranged) += 0.15f;
            player.ammoCost80 = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Green Dragonhide Coif");
            Tooltip.SetDefault("4% increased ranged critical strike chance");
        }
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 20;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = ItemRarityID.Orange;
            Item.defense = 5;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetCritChance(DamageClass.Ranged) += 4;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "Greendhide", 10);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}