using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace OldSchoolRuneScape.Items
{
    public abstract class Talisman : ModItem
    {
        protected abstract int getRarity();
        protected abstract int getValue();
        protected abstract int getAltar();
        protected abstract Color getColor();
        public override void SetDefaults()
        {
            Item.maxStack = 1;
            Item.width = 28;
            Item.height = 32;
            Item.rare = getRarity();
            Item.UseSound = SoundID.DD2_BookStaffCast;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useAnimation = 120;
            Item.useTime = 120;
            Item.value = getValue();
        }

        private Vector2 altarPos = Vector2.Zero;
        private Vector2 castPos = Vector2.Zero;
        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            if (player.itemAnimation >= 119)
            {
                altarPos = Vector2.Zero;
                for (int j = 0; j < Main.maxTilesY; j++)
                {
                    if (altarPos == Vector2.Zero)
                    {
                        for (int i = 0; i < Main.maxTilesX; i++)
                        {
                            if (Main.tile[i, j].TileType == getAltar())
                            {
                                altarPos = new Vector2(i * 16, j * 16);
                                break;
                            }
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            Color c = getColor();
            float max = Math.Max(c.R, Math.Max(c.G, c.B));
            Lighting.AddLight(player.Center, new Vector3(c.R/max, c.G/max, c.B/max));
            player.itemLocation = player.MountedCenter + new Vector2(12 * player.direction, -16) + new Vector2(-14 * player.direction, 16).RotatedBy(MathHelper.ToRadians(player.itemAnimation * 9));
            player.itemRotation = MathHelper.ToRadians(player.itemAnimation * 9);
            if (player.itemAnimation < 100)
            {
                int i = player.itemAnimation;
                Vector2 vec = altarPos == Vector2.Zero ? Vector2.Zero : altarPos - castPos;
                vec.Normalize();
                Dust dust;
                int area = i;
                Vector2 position = castPos + vec * (700 - (i * 7)) - new Vector2(area / 2, area / 2);
                dust = Main.dust[Terraria.Dust.NewDust(position, area, area, DustID.AncientLight, 0f, 0f, 0, c, 0.1f)];
                dust.noGravity = true;
                dust.fadeIn = 0.75f;
            }
            else
            {
                castPos = player.Center;
            }
        }
    }
    public class AirTalisman : Talisman
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Air Talisman");
            Tooltip.SetDefault("'A mysterious power emanates from the talisman...'\nGuides you towards the air altar");
        }
        protected override int getAltar()
        {
            return ModContent.TileType<Tiles.AirAltar>();
        }
        protected override Color getColor()
        {
            return new Color(255, 255, 255);
        }
        protected override int getRarity()
        {
            return 1;
        }
        protected override int getValue()
        {
            return Item.sellPrice(silver: 10);
        }
    }
    public class MindTalisman : Talisman
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mind Talisman");
            Tooltip.SetDefault("'A mysterious power emanates from the talisman...'\nGuides you towards the mind altar");
        }
        protected override int getAltar()
        {
            return ModContent.TileType<Tiles.MindAltar>();
        }
        protected override Color getColor()
        {
            return new Color(241, 147, 29);
        }
        protected override int getRarity()
        {
            return 1;
        }
        protected override int getValue()
        {
            return Item.sellPrice(silver: 10);
        }
    }
    public class WaterTalisman : Talisman
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Water Talisman");
            Tooltip.SetDefault("'A mysterious power emanates from the talisman...'\nGuides you towards the water altar");
        }
        protected override int getAltar()
        {
            return ModContent.TileType<Tiles.WaterAltar>();
        }
        protected override Color getColor()
        {
            return new Color(21, 26, 183);
        }
        protected override int getRarity()
        {
            return 1;
        }
        protected override int getValue()
        {
            return Item.sellPrice(silver: 10);
        }
    }
    public class EarthTalisman : Talisman
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Earth Talisman");
            Tooltip.SetDefault("'A mysterious power emanates from the talisman...'\nGuides you towards the earth altar");
        }
        protected override int getAltar()
        {
            return ModContent.TileType<Tiles.EarthAltar>();
        }
        protected override Color getColor()
        {
            return new Color(116, 80, 13);
        }
        protected override int getRarity()
        {
            return 1;
        }
        protected override int getValue()
        {
            return Item.sellPrice(silver: 10);
        }
    }
    public class FireTalisman : Talisman
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fire Talisman");
            Tooltip.SetDefault("'A mysterious power emanates from the talisman...'\nGuides you towards the fire altar");
        }
        protected override int getAltar()
        {
            return ModContent.TileType<Tiles.FireAltar>();
        }
        protected override Color getColor()
        {
            return new Color(183, 38, 21);
        }
        protected override int getRarity()
        {
            return 1;
        }
        protected override int getValue()
        {
            return Item.sellPrice(silver: 10);
        }
    }
    public class BodyTalisman : Talisman
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Body Talisman");
            Tooltip.SetDefault("'A mysterious power emanates from the talisman...'\nGuides you towards the body altar");
        }
        protected override int getAltar()
        {
            return ModContent.TileType<Tiles.BodyAltar>();
        }
        protected override Color getColor()
        {
            return new Color(26, 34, 216);
        }
        protected override int getRarity()
        {
            return 1;
        }
        protected override int getValue()
        {
            return Item.sellPrice(silver: 10);
        }
    }
    public class CosmicTalisman : Talisman
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cosmic Talisman");
            Tooltip.SetDefault("'A mysterious power emanates from the talisman...'\nGuides you towards the cosmic altar");
        }
        protected override int getAltar()
        {
            return ModContent.TileType<Tiles.CosmicAltar>();
        }
        protected override Color getColor()
        {
            return new Color(241, 238, 29);
        }
        protected override int getRarity()
        {
            return 1;
        }
        protected override int getValue()
        {
            return Item.sellPrice(silver: 10);
        }
    }
    public class ChaosTalisman : Talisman
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chaos Talisman");
            Tooltip.SetDefault("'A mysterious power emanates from the talisman...'\nGuides you towards the chaos altar");
        }
        protected override int getAltar()
        {
            return ModContent.TileType<Tiles.ChaosAltar>();
        }
        protected override Color getColor()
        {
            return new Color(241, 181, 29);
        }
        protected override int getRarity()
        {
            return 1;
        }
        protected override int getValue()
        {
            return Item.sellPrice(silver: 10);
        }
    }
    public class NatureTalisman : Talisman
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nature Talisman");
            Tooltip.SetDefault("'A mysterious power emanates from the talisman...'\nGuides you towards the nature altar");
        }
        protected override int getAltar()
        {
            return ModContent.TileType<Tiles.NatureAltar>();
        }
        protected override Color getColor()
        {
            return new Color(17, 152, 24);
        }
        protected override int getRarity()
        {
            return 1;
        }
        protected override int getValue()
        {
            return Item.sellPrice(silver: 10);
        }
    }
    public class LawTalisman : Talisman
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Law Talisman");
            Tooltip.SetDefault("'A mysterious power emanates from the talisman...'\nGuides you towards the law altar");
        }
        protected override int getAltar()
        {
            return ModContent.TileType<Tiles.LawAltar>();
        }
        protected override Color getColor()
        {
            return new Color(29, 38, 241);
        }
        protected override int getRarity()
        {
            return 1;
        }
        protected override int getValue()
        {
            return Item.sellPrice(silver: 10);
        }
    }
    public class BloodTalisman : Talisman
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blood Talisman");
            Tooltip.SetDefault("'A mysterious power emanates from the talisman...'\nGuides you towards the blood altar");
        }
        protected override int getAltar()
        {
            return ModContent.TileType<Tiles.BloodAltar>();
        }
        protected override Color getColor()
        {
            return new Color(152, 32, 17);
        }
        protected override int getRarity()
        {
            return 1;
        }
        protected override int getValue()
        {
            return Item.sellPrice(silver: 10);
        }
    }
    public class SoulTalisman : Talisman
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Talisman");
            Tooltip.SetDefault("'A mysterious power emanates from the talisman...'\nGuides you towards the soul altar");
        }
        protected override int getAltar()
        {
            return ModContent.TileType<Tiles.SoulAltar>();
        }
        protected override Color getColor()
        {
            return new Color(174, 175, 226);
        }
        protected override int getRarity()
        {
            return 1;
        }
        protected override int getValue()
        {
            return Item.sellPrice(silver: 10);
        }
    }
    public class DeathTalisman : Talisman
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Death Talisman");
            Tooltip.SetDefault("'A mysterious power emanates from the talisman...'\nGuides you towards the death altar");
        }
        protected override int getAltar()
        {
            return ModContent.TileType<Tiles.DeathAltar>();
        }
        protected override Color getColor()
        {
            return new Color(184, 174, 173);
        }
        protected override int getRarity()
        {
            return 1;
        }
        protected override int getValue()
        {
            return Item.sellPrice(silver: 10);
        }
    }
    public class WrathTalisman : Talisman
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wrath Talisman");
            Tooltip.SetDefault("'A mysterious power emanates from the talisman...'\nGuides you towards the wrath altar");
        }
        protected override int getAltar()
        {
            return ModContent.TileType<Tiles.WrathAltar>();
        }
        protected override Color getColor()
        {
            return new Color(135, 13, 72);
        }
        protected override int getRarity()
        {
            return 1;
        }
        protected override int getValue()
        {
            return Item.sellPrice(silver: 10);
        }
    }
    public class AstralTalisman : Talisman
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Astral Talisman");
            Tooltip.SetDefault("'A mysterious power emanates from the talisman...'\nGuides you towards the astral altar");
        }
        protected override int getAltar()
        {
            return ModContent.TileType<Tiles.AstralAltar>();
        }
        protected override Color getColor()
        {
            return new Color(207, 182, 207);
        }
        protected override int getRarity()
        {
            return 1;
        }
        protected override int getValue()
        {
            return Item.sellPrice(silver: 10);
        }
    }
}
