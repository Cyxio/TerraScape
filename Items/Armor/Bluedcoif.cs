using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class Bluedcoif : ModItem
    {
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == Mod.Find<ModItem>("Bluedbody").Type && legs.type == Mod.Find<ModItem>("Bluedchaps").Type;
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "10% increased ranged damage\n20% chance not to consume ammo";
            player.GetDamage(DamageClass.Ranged) += 0.1f;
            player.ammoCost80 = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blue Dragonhide Coif");
            Tooltip.SetDefault("10% increased ranged critical strike chance\n20% increased arrow damage");
        }
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 20;
            Item.value = Item.sellPrice(0, 4, 0, 0);
            Item.rare = ItemRarityID.LightRed;
            Item.defense = 8;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetCritChance(DamageClass.Ranged) += 10;
            player.arrowDamage += 0.2f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "Bluedhide", 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}