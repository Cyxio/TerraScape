using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class Runehelm : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rune Full Helm");
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == Mod.Find<ModItem>("Runeplate").Type && legs.type == Mod.Find<ModItem>("Runeplatelegs").Type;
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "20% increased melee damage";
            player.GetDamage(DamageClass.Melee) += 0.2f;
        }

        public override void SetDefaults()
        {
            Item.width = 19;
            Item.height = 30;
            Item.value = Item.sellPrice(0, 3, 0, 0);
            Item.rare = ItemRarityID.Orange;
            Item.defense = 7;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "Runitebar", 8);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}