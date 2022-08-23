using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class Mystichat : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mystic Hat");
            Tooltip.SetDefault("Increases maximum mana by 20");
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == Mod.Find<ModItem>("Mystictop").Type && legs.type == Mod.Find<ModItem>("Mysticbottom").Type;
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "10% increased magic damage\n18% reduced mana usage";
            player.GetDamage(DamageClass.Magic) += 0.1f;
            player.manaCost -= 0.18f;
        }

        public override void SetDefaults()
        {
            Item.width = 19;
            Item.height = 30;
            Item.value = Item.sellPrice(0, 3, 0, 0);
            Item.rare = ItemRarityID.Orange;
            Item.defense = 5;
        }

        public override void UpdateEquip(Player player)
        {
            player.statManaMax2 += 20;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Mysticcomponents>(), 4);
            recipe.AddIngredient(ItemID.Silk, 8);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}