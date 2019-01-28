using Microsoft.Xna.Framework;
using System;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using System.Collections.Generic;
using Terraria.World.Generation;
using Terraria.GameContent.Generation;

namespace OldSchoolRuneScape.Tiles
{
    public class Lectern : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.DrawYOffset = 2;
            TileObjectData.addTile(Type);
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Lectern");
            AddMapEntry(new Color(94, 70, 43), name);
            dustType = 22;
        }
        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 32, 32, mod.ItemType<LecternItem>());
        }
    }
    class LecternItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lectern");
            Tooltip.SetDefault("A wooden lectern suitable for crafting tablets");
        }
        public override void SetDefaults()
        {
            item.width = 21;
            item.height = 30;
            item.createTile = mod.TileType("Lectern");
            item.useTime = 10;
            item.useAnimation = 15;
            item.useStyle = 1;
            item.useTurn = true;
            item.autoReuse = true;
            item.maxStack = 99;
            item.consumable = true;
            item.placeStyle = 0;
            item.rare = 1;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Book);
            recipe.AddRecipeGroup("Wood", 10);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
