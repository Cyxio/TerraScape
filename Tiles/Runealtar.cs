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
    public class Runealtar : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
            TileObjectData.newTile.DrawYOffset = 2;
            TileObjectData.addTile(Type);
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Basic Rune Altar");
            AddMapEntry(new Color(70, 70, 70), name);
            dustType = 30;
        }
        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 48, 32, mod.ItemType<RunealtarItem>());
        }
    }

    

    public class RunealtarItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Basic Rune Altar");
            Tooltip.SetDefault("'Used for basic level runecrafting'");
        }
        public override void SetDefaults()
        {
            item.width = 36;
            item.height = 24;
            item.createTile = mod.TileType("Runealtar");
            item.useStyle = 1;
            item.useTime = 10;
            item.useAnimation = 15;
            item.consumable = true;
            item.autoReuse = true;
            item.placeStyle = 0;
            item.useTurn = true;
            item.maxStack = 99;
            item.rare = 1;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.StoneBlock, 30);
            recipe.AddIngredient(null, "aaRuneessence", 30);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}