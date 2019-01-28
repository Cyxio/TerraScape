using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OldSchoolRuneScape.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class Reddbody : ModItem
    {
        public override void DrawHands(ref bool drawHands, ref bool drawArms)
        {
            drawHands = true;
            drawArms = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Red Dragonhide Body");
            Tooltip.SetDefault("6% increased ranged critical strike chance");
        }
        public override void SetDefaults()
        {
            item.width = 31;
            item.height = 23;
            item.value = Item.sellPrice(0, 4, 0, 0);
            item.rare = 4;
            item.defense = 12;
        }

        public override void UpdateEquip(Player player)
        {
            player.rangedCrit += 6;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Reddhide", 22);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}