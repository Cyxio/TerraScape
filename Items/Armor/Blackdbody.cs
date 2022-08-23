using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OldSchoolRuneScape.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class Blackdbody : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Black Dragonhide Body");
            Tooltip.SetDefault("7% increased ranged damage & critical strike chance");
            ArmorIDs.Body.Sets.HidesHands[Item.bodySlot] = false;
        }
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 28;
            Item.value = Item.sellPrice(0, 4, 0, 0);
            Item.rare = ItemRarityID.LightPurple;
            Item.defense = 20;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetCritChance(DamageClass.Ranged) += 7;
            player.GetDamage(DamageClass.Ranged) += 0.07f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "Blackdhide", 22);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}