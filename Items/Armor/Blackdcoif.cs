using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class Blackdcoif : ModItem
    {
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == Mod.Find<ModItem>("Blackdbody").Type && legs.type == Mod.Find<ModItem>("Blackdchaps").Type;
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "18% increased ranged damage\n20% chance not to consume ammo";
            player.GetDamage(DamageClass.Ranged) += 0.18f;
            player.ammoCost80 = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Black Dragonhide Coif");
            Tooltip.SetDefault("10% increased ranged critical strike chance");
        }
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 20;
            Item.value = Item.sellPrice(0, 4, 0, 0);
            Item.rare = ItemRarityID.LightPurple;
            Item.defense = 12;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetCritChance(DamageClass.Ranged) += 10;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "Blackdhide", 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}