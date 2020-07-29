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
using Microsoft.Xna.Framework.Graphics;

namespace OldSchoolRuneScape.Tiles
{
    public class ARunealtar : ModTile
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
            name.SetDefault("Advanced rune altar");
            AddMapEntry(new Color(80, 80, 80), name);
            dustType = 30;
            Main.tileLighted[Type] = true;
        }
        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 48, 32, ModContent.ItemType<ARunealtarItem>());
        }
        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            frameCounter++;
            if (frameCounter > 12)
            {
                frameCounter = 0;
                frame++;
                if (frame > 6)
                {
                    frame = 0;
                }
            }
        }
        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Tile tile = Main.tile[i, j];
            Texture2D texture = Main.tileTexture[Type];
            Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
            if (Main.drawToScreen)
            {
                zero = Vector2.Zero;
            }
            int animate = Main.tileFrame[Type] * 34;
            Main.spriteBatch.Draw(
                texture,
                new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y + 2) + zero,
                new Rectangle(tile.frameX, tile.frameY + animate, 16, 16),
                Lighting.GetColor(i, j), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);

            return false;
        }
    }

    public class ARunealtarItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Advanced Rune Altar");
            Tooltip.SetDefault("'Used for advanced runecrafting'");
        }
        public override void SetDefaults()
        {
            item.width = 36;
            item.height = 24;
            item.createTile = mod.TileType("ARunealtar");
            item.useStyle = 1;
            item.useTime = 10;
            item.useAnimation = 15;
            item.consumable = true;
            item.autoReuse = true;
            item.placeStyle = 0;
            item.useTurn = true;
            item.maxStack = 99;
            item.rare = 3;
        }
    }
}