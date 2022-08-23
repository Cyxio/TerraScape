using Microsoft.Xna.Framework;
using System;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using System.Collections.Generic;
using Terraria.GameContent.Generation;
using Terraria.WorldBuilding;

namespace OldSchoolRuneScape.Tiles
{
    public class Lectern : ModTile
    {
        public override void SetStaticDefaults()
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
            DustType = 22;
        }
        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 32, 32, ModContent.ItemType<LecternItem>());
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
            Item.width = 21;
            Item.height = 30;
            Item.createTile = Mod.Find<ModTile>("Lectern").Type;
            Item.useTime = 10;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.maxStack = 99;
            Item.consumable = true;
            Item.placeStyle = 0;
            Item.rare = ItemRarityID.Blue;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Book);
            recipe.AddRecipeGroup("Wood", 10);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}
