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
        public override void DrawHands(ref bool drawHands, ref bool drawArms)
        {
            drawHands = true;
            drawArms = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Black Dragonhide Body");
            Tooltip.SetDefault("7% increased ranged damage & critical strike chance");
        }
        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 28;
            item.value = Item.sellPrice(0, 4, 0, 0);
            item.rare = 6;
            item.defense = 20;
        }

        public override void UpdateEquip(Player player)
        {
            player.rangedCrit += 7;
            player.rangedDamage += 0.07f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Blackdhide", 22);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}