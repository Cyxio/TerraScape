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
    public abstract class RuneAltar : ModTile
    {
        protected abstract Color getLightColor();
        protected abstract int getColumn();
        protected abstract int getRow();
        protected abstract string getAltarName();
        protected abstract int getAltarItem();
        protected abstract int getRune();
        public override bool Autoload(ref string name, ref string texture)
        {
            texture = "OldSchoolRuneScape/Tiles/Runealtar";
            return base.Autoload(ref name, ref texture);
        }
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
            TileObjectData.newTile.DrawYOffset = 2;
            TileObjectData.addTile(Type);
            ModTranslation name = CreateMapEntryName();
            name.SetDefault(getAltarName());
            AddMapEntry(getLightColor(), name);
            dustType = 30;
        }
        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 48, 32, getAltarItem());
        }
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Color lightColor = getLightColor();
            float Sin = (float)(Math.Sin(Math.PI * 2 * ((float)Main.time % 300f) / 299f));
            Lighting.AddLight(new Vector2(i * 16, j * 16), 0.5f * new Vector3(lightColor.R * Sin, lightColor.G * Sin, lightColor.B * Sin)
                / Math.Max(lightColor.R, Math.Max(lightColor.G, lightColor.B)));
            Tile tile = Main.tile[i, j];
            Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
            if (Main.drawToScreen)
            {
                zero = Vector2.Zero;
            }
            for (int h = 0; h < 4; h++)
            {
                //rect size 54 by 34
                int Column = getColumn();
                int Row = getRow();
                int c = (int)((255 - 70 * h) * Sin);
                spriteBatch.Draw(mod.GetTexture("Tiles/AltarEffect"), new Vector2(i * 16, j * 16 - (2 * h) + 2) - Main.screenPosition + zero,
                new Rectangle(tile.frameX + (54 * Column), tile.frameY + (34 * Row), 16, 16), new Color(c, c, c, c), 0f, default, 1f, SpriteEffects.None, 0f);
            }
        }
        public override void MouseOver(int i, int j)
        {
            Player player = Main.LocalPlayer;
            if (player.inventory[player.selectedItem].type == ModContent.ItemType<Items.Magic.aaRuneessence>()
                && player.inventory[player.selectedItem].stack > 0)
            {
                player.noThrow = 2;
                player.showItemIcon = true;
                player.showItemIcon2 = getRune();
                player.showItemIconText = "\n" + player.inventory[player.selectedItem].stack;
            }   
        }
    }
    public abstract class RuneAltarItem : ModItem
    {
        protected abstract int getColumn();
        protected abstract int getRow();
        protected abstract int getTile();
        protected abstract int getRarity();
        public override string Texture => "OldSchoolRuneScape/Tiles/RunealtarItem";
        public override void SetDefaults()
        {
            item.width = 48;
            item.height = 28;
            item.useStyle = 1;
            item.useTime = 10;
            item.useAnimation = 15;
            item.consumable = true;
            item.autoReuse = true;
            item.placeStyle = 0;
            item.maxStack = 1;
            item.value = Item.sellPrice(gold: 1);
            item.createTile = getTile();
            item.rare = getRarity();
        }
        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            int Column = getColumn();
            int Row = getRow();
            float Sin = (float)(Math.Sin(Math.PI * 2 * ((float)Main.time % 300f) / 299f));
            int c = (int)(255 * Sin);
            spriteBatch.Draw(mod.GetTexture("Tiles/AltarItemEffect"), item.position - Main.screenPosition + new Vector2(24, 16),
                new Rectangle(48 * Column, 28 * Row, 48, 28), new Color(c, c, c, c), rotation, new Vector2(24, 14), scale, SpriteEffects.None, 0f);
        }
        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            int Column = getColumn();
            int Row = getRow();
            float Sin = (float)(Math.Sin(Math.PI * 2 * ((float)Main.time % 300f) / 299f));
            int c = (int)(255 * Sin);
            spriteBatch.Draw(mod.GetTexture("Tiles/AltarItemEffect"), position,
                new Rectangle(48 * Column, 28 * Row, 48, 28), new Color(c, c, c, c), 0f, origin, scale, SpriteEffects.None, 0f);
        }
    }
    public class AirAltarItem : RuneAltarItem
    {
        protected override int getColumn()
        {
            return 0;
        }
        protected override int getRow()
        {
            return 0;
        }
        protected override int getTile()
        {
            return ModContent.TileType<AirAltar>();
        }
        protected override int getRarity()
        {
            return 1;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Air Altar");
            Tooltip.SetDefault("Used for crafting air runes");
        }
    }
    public class AirAltar : RuneAltar
    {
        protected override Color getLightColor()
        {
            return new Color(255, 255, 255);
        }
        protected override string getAltarName()
        {
            return "Air Altar";
        }
        protected override int getAltarItem()
        {
            return ModContent.ItemType<AirAltarItem>();
        }
        protected override int getColumn()
        {
            return 0;
        }
        protected override int getRow()
        {
            return 0;
        }
        protected override int getRune()
        {
            return ModContent.ItemType<Items.Magic.Airrune>();
        }
    }
    public class MindAltarItem : RuneAltarItem
    {
        protected override int getColumn()
        {
            return 1;
        }
        protected override int getRow()
        {
            return 0;
        }
        protected override int getTile()
        {
            return ModContent.TileType<MindAltar>();
        }
        protected override int getRarity()
        {
            return 1;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mind Altar");
            Tooltip.SetDefault("Used for crafting mind runes");
        }
    }
    public class MindAltar : RuneAltar
    {
        protected override Color getLightColor()
        {
            return new Color(241, 147, 29);
        }
        protected override string getAltarName()
        {
            return "Mind Altar";
        }
        protected override int getAltarItem()
        {
            return ModContent.ItemType<MindAltarItem>();
        }
        protected override int getColumn()
        {
            return 1;
        }
        protected override int getRow()
        {
            return 0;
        }
        protected override int getRune()
        {
            return ModContent.ItemType<Items.Magic.Mindrune>();
        }
    }
    public class WaterAltarItem : RuneAltarItem
    {
        protected override int getColumn()
        {
            return 0;
        }
        protected override int getRow()
        {
            return 1;
        }
        protected override int getTile()
        {
            return ModContent.TileType<WaterAltar>();
        }
        protected override int getRarity()
        {
            return 1;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Water Altar");
            Tooltip.SetDefault("Used for crafting water runes");
        }
    }
    public class WaterAltar : RuneAltar
    {
        protected override Color getLightColor()
        {
            return new Color(21, 26, 183);
        }
        protected override string getAltarName()
        {
            return "Water Altar";
        }
        protected override int getAltarItem()
        {
            return ModContent.ItemType<WaterAltarItem>();
        }
        protected override int getColumn()
        {
            return 0;
        }
        protected override int getRow()
        {
            return 1;
        }
        protected override int getRune()
        {
            return ModContent.ItemType<Items.Magic.Waterrune>();
        }
    }
    public class EarthAltarItem : RuneAltarItem
    {
        protected override int getColumn()
        {
            return 1;
        }
        protected override int getRow()
        {
            return 1;
        }
        protected override int getTile()
        {
            return ModContent.TileType<EarthAltar>();
        }
        protected override int getRarity()
        {
            return 1;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Earth Altar");
            Tooltip.SetDefault("Used for crafting earth runes");
        }
    }
    public class EarthAltar : RuneAltar
    {
        protected override Color getLightColor()
        {
            return new Color(116, 80, 13);
        }
        protected override string getAltarName()
        {
            return "Earth Altar";
        }
        protected override int getAltarItem()
        {
            return ModContent.ItemType<EarthAltarItem>();
        }
        protected override int getColumn()
        {
            return 1;
        }
        protected override int getRow()
        {
            return 1;
        }
        protected override int getRune()
        {
            return ModContent.ItemType<Items.Magic.Earthrune>();
        }
    }
    public class FireAltarItem : RuneAltarItem
    {
        protected override int getColumn()
        {
            return 0;
        }
        protected override int getRow()
        {
            return 2;
        }
        protected override int getTile()
        {
            return ModContent.TileType<FireAltar>();
        }
        protected override int getRarity()
        {
            return 1;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fire Altar");
            Tooltip.SetDefault("Used for crafting fire runes");
        }
    }
    public class FireAltar : RuneAltar
    {
        protected override Color getLightColor()
        {
            return new Color(183, 38, 21);
        }
        protected override string getAltarName()
        {
            return "Fire Altar";
        }
        protected override int getAltarItem()
        {
            return ModContent.ItemType<FireAltarItem>();
        }
        protected override int getColumn()
        {
            return 0;
        }
        protected override int getRow()
        {
            return 2;
        }
        protected override int getRune()
        {
            return ModContent.ItemType<Items.Magic.Firerune>();
        }
    }
    public class BodyAltarItem : RuneAltarItem
    {
        protected override int getColumn()
        {
            return 1;
        }
        protected override int getRow()
        {
            return 2;
        }
        protected override int getTile()
        {
            return ModContent.TileType<BodyAltar>();
        }
        protected override int getRarity()
        {
            return 1;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Body Altar");
            Tooltip.SetDefault("Used for crafting body runes");
        }
    }
    public class BodyAltar : RuneAltar
    {
        protected override Color getLightColor()
        {
            return new Color(26, 34, 216);
        }
        protected override string getAltarName()
        {
            return "Body Altar";
        }
        protected override int getAltarItem()
        {
            return ModContent.ItemType<BodyAltarItem>();
        }
        protected override int getColumn()
        {
            return 1;
        }
        protected override int getRow()
        {
            return 2;
        }
        protected override int getRune()
        {
            return ModContent.ItemType<Items.Magic.Bodyrune>();
        }
    }
    public class CosmicAltarItem : RuneAltarItem
    {
        protected override int getColumn()
        {
            return 2;
        }
        protected override int getRow()
        {
            return 0;
        }
        protected override int getTile()
        {
            return ModContent.TileType<CosmicAltar>();
        }
        protected override int getRarity()
        {
            return 4;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cosmic Altar");
            Tooltip.SetDefault("Used for crafting cosmic runes");
        }
    }
    public class CosmicAltar : RuneAltar
    {
        protected override Color getLightColor()
        {
            return new Color(241, 238, 29);
        }
        protected override string getAltarName()
        {
            return "Cosmic Altar";
        }
        protected override int getAltarItem()
        {
            return ModContent.ItemType<CosmicAltarItem>();
        }
        protected override int getColumn()
        {
            return 2;
        }
        protected override int getRow()
        {
            return 0;
        }
        protected override int getRune()
        {
            return ModContent.ItemType<Items.Magic.Cosmicrune>();
        }
    }
    public class ChaosAltarItem : RuneAltarItem
    {
        protected override int getColumn()
        {
            return 3;
        }
        protected override int getRow()
        {
            return 0;
        }
        protected override int getTile()
        {
            return ModContent.TileType<ChaosAltar>();
        }
        protected override int getRarity()
        {
            return 1;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chaos Altar");
            Tooltip.SetDefault("Used for crafting chaos runes");
        }
    }
    public class ChaosAltar : RuneAltar
    {
        protected override Color getLightColor()
        {
            return new Color(241, 181, 29);
        }
        protected override string getAltarName()
        {
            return "Chaos Altar";
        }
        protected override int getAltarItem()
        {
            return ModContent.ItemType<ChaosAltarItem>();
        }
        protected override int getColumn()
        {
            return 3;
        }
        protected override int getRow()
        {
            return 0;
        }
        protected override int getRune()
        {
            return ModContent.ItemType<Items.Magic.Chaosrune>();
        }
    }
    public class NatureAltarItem : RuneAltarItem
    {
        protected override int getColumn()
        {
            return 2;
        }
        protected override int getRow()
        {
            return 1;
        }
        protected override int getTile()
        {
            return ModContent.TileType<NatureAltar>();
        }
        protected override int getRarity()
        {
            return 1;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nature Altar");
            Tooltip.SetDefault("Used for crafting nature runes");
        }
    }
    public class NatureAltar : RuneAltar
    {
        protected override Color getLightColor()
        {
            return new Color(17, 152, 24);
        }
        protected override string getAltarName()
        {
            return "Nature Altar";
        }
        protected override int getAltarItem()
        {
            return ModContent.ItemType<NatureAltarItem>();
        }
        protected override int getColumn()
        {
            return 2;
        }
        protected override int getRow()
        {
            return 1;
        }
        protected override int getRune()
        {
            return ModContent.ItemType<Items.Magic.Naturerune>();
        }
    }
    public class LawAltarItem : RuneAltarItem
    {
        protected override int getColumn()
        {
            return 3;
        }
        protected override int getRow()
        {
            return 1;
        }
        protected override int getTile()
        {
            return ModContent.TileType<LawAltar>();
        }
        protected override int getRarity()
        {
            return 1;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Law Altar");
            Tooltip.SetDefault("Used for crafting law runes");
        }
    }
    public class LawAltar : RuneAltar
    {
        protected override Color getLightColor()
        {
            return new Color(29, 38, 241);
        }
        protected override string getAltarName()
        {
            return "Law Altar";
        }
        protected override int getAltarItem()
        {
            return ModContent.ItemType<LawAltarItem>();
        }
        protected override int getColumn()
        {
            return 3;
        }
        protected override int getRow()
        {
            return 1;
        }
        protected override int getRune()
        {
            return ModContent.ItemType<Items.Magic.Lawrune>();
        }
    }
    public class BloodAltarItem : RuneAltarItem
    {
        protected override int getColumn()
        {
            return 2;
        }
        protected override int getRow()
        {
            return 2;
        }
        protected override int getTile()
        {
            return ModContent.TileType<BloodAltar>();
        }
        protected override int getRarity()
        {
            return 1;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blood Altar");
            Tooltip.SetDefault("Used for crafting blood runes");
        }
    }
    public class BloodAltar : RuneAltar
    {
        protected override Color getLightColor()
        {
            return new Color(152, 32, 17);
        }
        protected override string getAltarName()
        {
            return "Blood Altar";
        }
        protected override int getAltarItem()
        {
            return ModContent.ItemType<BloodAltarItem>();
        }
        protected override int getColumn()
        {
            return 2;
        }
        protected override int getRow()
        {
            return 2;
        }
        protected override int getRune()
        {
            return ModContent.ItemType<Items.Magic.Bloodrune>();
        }
    }
    public class SoulAltarItem : RuneAltarItem
    {
        protected override int getColumn()
        {
            return 3;
        }
        protected override int getRow()
        {
            return 2;
        }
        protected override int getTile()
        {
            return ModContent.TileType<SoulAltar>();
        }
        protected override int getRarity()
        {
            return 1;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Altar");
            Tooltip.SetDefault("Used for crafting soul runes");
        }
    }
    public class SoulAltar : RuneAltar
    {
        protected override Color getLightColor()
        {
            return new Color(174, 175, 226);
        }
        protected override string getAltarName()
        {
            return "Soul Altar";
        }
        protected override int getAltarItem()
        {
            return ModContent.ItemType<SoulAltarItem>();
        }
        protected override int getColumn()
        {
            return 3;
        }
        protected override int getRow()
        {
            return 2;
        }
        protected override int getRune()
        {
            return ModContent.ItemType<Items.Magic.Soulrune>();
        }
    }
    public class DeathAltarItem : RuneAltarItem
    {
        protected override int getColumn()
        {
            return 2;
        }
        protected override int getRow()
        {
            return 3;
        }
        protected override int getTile()
        {
            return ModContent.TileType<DeathAltar>();
        }
        protected override int getRarity()
        {
            return 1;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Death Altar");
            Tooltip.SetDefault("Used for crafting death runes");
        }
    }
    public class DeathAltar : RuneAltar
    {
        protected override Color getLightColor()
        {
            return new Color(184, 174, 173);
        }
        protected override string getAltarName()
        {
            return "Death Altar";
        }
        protected override int getAltarItem()
        {
            return ModContent.ItemType<DeathAltarItem>();
        }
        protected override int getColumn()
        {
            return 2;
        }
        protected override int getRow()
        {
            return 3;
        }
        protected override int getRune()
        {
            return ModContent.ItemType<Items.Magic.Deathrune>();
        }
    }
    public class WrathAltarItem : RuneAltarItem
    {
        protected override int getColumn()
        {
            return 3;
        }
        protected override int getRow()
        {
            return 3;
        }
        protected override int getTile()
        {
            return ModContent.TileType<WrathAltar>();
        }
        protected override int getRarity()
        {
            return 1;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wrath Altar");
            Tooltip.SetDefault("Used for crafting wrath runes");
        }
    }
    public class WrathAltar : RuneAltar
    {
        protected override Color getLightColor()
        {
            return new Color(135, 13, 72);
        }
        protected override string getAltarName()
        {
            return "Wrath Altar";
        }
        protected override int getAltarItem()
        {
            return ModContent.ItemType<WrathAltarItem>();
        }
        protected override int getColumn()
        {
            return 3;
        }
        protected override int getRow()
        {
            return 3;
        }
        protected override int getRune()
        {
            return ModContent.ItemType<Items.Magic.Wrathrune>();
        }
    }
    public class AstralAltarItem : RuneAltarItem
    {
        protected override int getColumn()
        {
            return 1;
        }
        protected override int getRow()
        {
            return 3;
        }
        protected override int getTile()
        {
            return ModContent.TileType<AstralAltar>();
        }
        protected override int getRarity()
        {
            return 1;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Astral Altar");
            Tooltip.SetDefault("Used for crafting astral runes");
        }
    }
    public class AstralAltar : RuneAltar
    {
        protected override Color getLightColor()
        {
            return new Color(207, 182, 207);
        }
        protected override string getAltarName()
        {
            return "Astral Altar";
        }
        protected override int getAltarItem()
        {
            return ModContent.ItemType<AstralAltarItem>();
        }
        protected override int getColumn()
        {
            return 1;
        }
        protected override int getRow()
        {
            return 3;
        }
        protected override int getRune()
        {
            return ModContent.ItemType<Items.Magic.Astralrune>();
        }
    }
}