using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace OldSchoolRuneScape.Items.Implingshit
{
    public class Implingjar0 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Baby Impling Jar");
            Tooltip.SetDefault("<right> to open");
        }
        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.rare = 1;
            item.value = Item.sellPrice(0, 0, 0, 50);
        }
        public override bool CanRightClick()
        {
            return true;
        }
        public override void RightClick(Player player)
        {
            int item = 0;
            int stack = 1;
            int ch = Main.rand.Next(10);
            switch (ch)
            {
                case 0:
                    item = ItemID.CopperBar;
                    stack = 3;
                    break;
                case 1:
                    item = ItemID.IronBar;
                    stack = 2;
                    break;
                case 2:
                    item = ItemID.GoldBar;
                    break;
                case 3:
                    item = ItemID.HealingPotion;
                    break;
                case 4:
                    item = ItemID.LesserHealingPotion;
                    stack = 2;
                    break;
                case 5:
                    item = ItemID.ManaPotion;
                    break;
                case 6:
                    item = ItemID.LesserManaPotion;
                    stack = 2;
                    break;
                case 7:
                    item = ItemID.TinCan;
                    break;
                case 8:
                    item = ItemID.OldShoe;
                    break;
                default:
                    item = ItemID.FishingSeaweed;
                    break;
            }
            player.QuickSpawnItem(item, stack);
        }
    }
    public class Implingjar1 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Young Impling Jar");
            Tooltip.SetDefault("<right> to open");
        }
        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.rare = 1;
            item.value = Item.sellPrice(0, 0, 1, 50);
        }
        public override bool CanRightClick()
        {
            return true;
        }
        public override void RightClick(Player player)
        {
            int item = 0;
            int stack = 1;
            int ch = Main.rand.Next(10);
            switch (ch)
            {
                case 0:
                    item = ItemID.IronBar;
                    stack = 3;
                    break;
                case 1:
                    item = ItemID.GoldBar;
                    stack = 2;
                    break;
                case 2:
                    item = ItemID.GoldBar;
                    break;
                case 3:
                    item = ItemID.HealingPotion;
                    break;
                case 4:
                    item = ItemID.HealingPotion;
                    stack = 2;
                    break;
                case 5:
                    item = ItemID.ManaPotion;
                    break;
                case 6:
                    item = ItemID.ManaPotion;
                    stack = 2;
                    break;
                case 7:
                    item = ItemID.IronskinPotion;
                    break;
                case 8:
                    item = ItemID.RegenerationPotion;
                    break;
                default:
                    item = ItemID.SwiftnessPotion;
                    break;
            }
            player.QuickSpawnItem(item, stack);
        }
    }
    public class Implingjar2 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gourmet Impling Jar");
            Tooltip.SetDefault("<right> to open");
        }
        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.rare = 2;
            item.value = Item.sellPrice(0, 0, 5);
        }
        public override bool CanRightClick()
        {
            return true;
        }
        public override void RightClick(Player player)
        {
            int item = 0;
            int stack = 1;
            int ch = Main.rand.Next(10);
            switch (ch)
            {
                case 0:
                    item = ItemID.PumpkinPie;
                    break;
                case 1:
                    item = ItemID.CookedFish;
                    break;
                case 2:
                    item = ItemID.CookedMarshmallow;
                    break;
                case 3:
                    item = ItemID.CookedShrimp;
                    break;
                case 4:
                    item = ItemID.SpecularFish;
                    stack = 2;
                    break;
                case 5:
                    item = ItemID.VariegatedLardfish;
                    break;
                case 6:
                    item = ItemID.Ebonkoi;
                    break;
                case 7:
                    item = ItemID.Hemopiranha;
                    break;
                case 8:
                    item = ItemID.Prismite;
                    break;
                default:
                    item = ItemID.DoubleCod;
                    break;
            }
            player.QuickSpawnItem(item, stack);
        }
    }
    public class Implingjar3 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Earth Impling Jar");
            Tooltip.SetDefault("<right> to open");
        }
        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.rare = 3;
            item.value = Item.sellPrice(0, 0, 10);
        }
        public override bool CanRightClick()
        {
            return true;
        }
        public override void RightClick(Player player)
        {
            int item = 0;
            int stack = Main.rand.Next(4, 8);
            int ch = Main.rand.Next(7);
            switch (ch)
            {
                case 0:
                    item = ItemID.Deathweed;
                    break;
                case 1:
                    item = ItemID.Daybloom;
                    break;
                case 2:
                    item = ItemID.Moonglow;
                    break;
                case 3:
                    item = ItemID.Blinkroot;
                    break;
                case 4:
                    item = ItemID.Waterleaf;
                    break;
                case 5:
                    item = ItemID.Fireblossom;
                    break;
                case 6:
                    item = ItemID.Shiverthorn;
                    break;
                default:
                    item = ItemID.FishingSeaweed;
                    break;
            }
            player.QuickSpawnItem(item, stack);
        }
    }
    public class Implingjar4 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Essence Impling Jar");
            Tooltip.SetDefault("<right> to open");
        }
        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.rare = 4;
            item.value = Item.sellPrice(0, 0, 25);
        }
        public override bool CanRightClick()
        {
            return true;
        }
        public override void RightClick(Player player)
        {
            int item = 0;
            int stack = 1;
            int ch = Main.rand.Next(10);
            switch (ch)
            {
                case 0:
                    item = ModContent.ItemType<Magic.aaRuneessence>();
                    stack = 50;
                    break;
                case 1:
                    item = ModContent.ItemType<Magic.abPureessence>();
                    stack = 25;
                    break;
                case 2:
                    item = ModContent.ItemType<Magic.aDarkessence>();
                    stack = 10;
                    break;
                case 3:
                    item = ModContent.ItemType<Magic.Airrune>();
                    stack = 50;
                    player.QuickSpawnItem(item, stack);
                    item = ModContent.ItemType<Magic.Waterrune>();
                    player.QuickSpawnItem(item, stack);
                    item = ModContent.ItemType<Magic.Earthrune>();
                    player.QuickSpawnItem(item, stack);
                    item = ModContent.ItemType<Magic.Firerune>();
                    break;
                case 4:
                    item = ModContent.ItemType<Magic.Mindrune>();
                    stack = 50;
                    break;
                case 5:
                    item = ModContent.ItemType<Magic.Chaosrune>();
                    stack = 50;
                    break;
                case 6:
                    item = ModContent.ItemType<Magic.Cosmicrune>();
                    stack = 50;
                    break;
                case 7:
                    item = ModContent.ItemType<Magic.Naturerune>();
                    stack = 25;
                    break;
                case 8:
                    item = ModContent.ItemType<Magic.Bodyrune>();
                    stack = 50;
                    break;
                case 9:
                    item = ModContent.ItemType<Magic.Lawrune>();
                    stack = 10;
                    break;
            }
            player.QuickSpawnItem(item, stack);
        }
    }
    public class Implingjar5 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eclectic Impling Jar");
            Tooltip.SetDefault("<right> to open");
        }
        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.rare = 5;
            item.value = Item.sellPrice(0, 0, 50);
        }
        public override bool CanRightClick()
        {
            return true;
        }
        public override void RightClick(Player player)
        {
            int item = 0;
            int stack = 1;
            int ch = Main.rand.Next(10);
            switch (ch)
            {
                case 0:
                    item = ItemID.WoodenCrate;
                    stack = 3;
                    break;
                case 1:
                    item = ItemID.IronCrate;
                    stack = 2;
                    break;
                case 2:
                    item = ItemID.GoldenCrate;
                    break;
                case 3:
                    item = ItemID.GoldCoin;
                    stack = Main.rand.Next(10, 100);
                    break;
                case 4:
                    item = ItemID.GoldenKey;
                    stack = 5;
                    break;
                case 5:
                    item = ItemID.SharkFin;
                    stack = 5;
                    break;
                case 6:
                    item = ItemID.AntlionMandible;
                    stack = 5;
                    break;
                case 7:
                    item = ItemID.CursedFlame;
                    stack = 1;
                    break;
                case 8:
                    item = ItemID.Ichor;
                    stack = 1;
                    break;
                default:
                    item = ItemID.FallenStar;
                    stack = 20;
                    break;
            }
            player.QuickSpawnItem(item, stack);
        }
    }
    public class Implingjar6 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nature Impling Jar");
            Tooltip.SetDefault("<right> to open");
        }
        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.rare = 6;
            item.value = Item.sellPrice(0, 1);
        }
        public override bool CanRightClick()
        {
            return true;
        }
        public override void RightClick(Player player)
        {
            int item = 0;
            int stack = 1;
            int ch = Main.rand.Next(14);
            switch (ch)
            {
                case 0:
                    item = ItemID.Wood;
                    stack = 250;
                    break;
                case 1:
                    item = ItemID.PalmWood;
                    stack = 250;
                    break;
                case 2:
                    item = ItemID.BorealWood;
                    stack = 250;
                    break;
                case 3:
                    item = ItemID.Pearlwood;
                    stack = 250;
                    break;
                case 4:
                    item = ItemID.Shadewood;
                    stack = 250;
                    break;
                case 5:
                    item = ItemID.Ebonwood;
                    stack = 250;
                    break;
                case 6:
                    item = ItemID.RichMahogany;
                    stack = 250;
                    break;
                case 7:
                    item = ItemID.Sapphire;
                    stack = 10;
                    break;
                case 8:
                    item = ItemID.Emerald;
                    stack = 10;
                    break;
                case 9:
                    item = ItemID.Topaz;
                    stack = 10;
                    break;
                case 10:
                    item = ItemID.Amethyst;
                    stack = 10;
                    break;
                case 11:
                    item = ItemID.Ruby;
                    stack = 10;
                    break;
                case 12:
                    item = ItemID.Diamond;
                    stack = 10;
                    break;
                default:
                    item = ItemID.Amber;
                    stack = 10;
                    break;
            }
            player.QuickSpawnItem(item, stack);
        }
    }
    public class Implingjar7 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Magpie Impling Jar");
            Tooltip.SetDefault("<right> to open");
        }
        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.rare = 7;
            item.value = Item.sellPrice(0, 2);
        }
        public override bool CanRightClick()
        {
            return true;
        }
        public override void RightClick(Player player)
        {
            int item = 0;
            int stack = 1;
            int ch = Main.rand.Next(10);
            switch (ch)
            {
                case 0:
                    item = ItemID.SoulofFlight;
                    stack = 10;
                    break;
                case 1:
                    item = ItemID.SoulofNight;
                    stack = 10;
                    break;
                case 2:
                    item = ItemID.SoulofLight;
                    stack = 10;
                    break;
                case 3:
                    item = ItemID.PixieDust;
                    stack = 30;
                    break;
                case 4:
                    item = ItemID.UnicornHorn;
                    stack = 5;
                    break;
                case 5:
                    item = ItemID.GreaterHealingPotion;
                    stack = 6;
                    break;
                case 6:
                    item = ItemID.GreaterManaPotion;
                    stack = 6;
                    break;
                case 7:
                    item = ItemID.MechanicalEye;
                    player.QuickSpawnItem(item, stack);
                    item = ItemID.MechanicalWorm;
                    player.QuickSpawnItem(item, stack);
                    item = ItemID.MechanicalSkull;
                    break;
                case 8:
                    item = ItemID.PumpkinMoonMedallion;
                    break;
                default:
                    item = ItemID.NaughtyPresent;
                    break;
            }
            player.QuickSpawnItem(item, stack);
        }
    }
    public class Implingjar8 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ninja Impling Jar");
            Tooltip.SetDefault("<right> to open");
        }
        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.rare = 8;
            item.value = Item.sellPrice(0, 5);
        }
        public override bool CanRightClick()
        {
            return true;
        }
        public override void RightClick(Player player)
        {
            int item = 0;
            int stack = 1;
            int ch = Main.rand.Next(10);
            switch (ch)
            {
                case 0:
                    item = ItemID.Tabi;
                    break;
                case 1:
                    item = ItemID.BlackBelt;
                    break;
                case 2:
                    item = ItemID.Ectoplasm;
                    stack = 10;
                    break;
                case 3:
                    item = ItemID.BrokenHeroSword;
                    break;
                case 4:
                    item = ItemID.SoulofFright;
                    stack = 20;
                    player.QuickSpawnItem(item, stack);
                    item = ItemID.SoulofSight;
                    player.QuickSpawnItem(item, stack);
                    item = ItemID.SoulofMight;
                    break;
                case 5:
                    item = ItemID.HallowedBar;
                    stack = 50;
                    break;
                case 6:
                    item = ItemID.ChlorophyteBar;
                    stack = 25;
                    break;
                case 7:
                    item = 3783;
                    stack = 3;
                    break;
                case 8:
                    item = ItemID.FrostCore;
                    stack = 3;
                    break;
                default:
                    item = ItemID.TurtleShell;
                    stack = 3;
                    break;
            }
            player.QuickSpawnItem(item, stack);
        }
    }
    public class Implingjar9 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragon Impling Jar");
            Tooltip.SetDefault("<right> to open");
        }
        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.rare = 9;
            item.value = Item.sellPrice(0, 10);
        }
        public override bool CanRightClick()
        {
            return true;
        }
        public override void RightClick(Player player)
        {
            if (player.GetModPlayer<OSRSplayer>().masterClue == 31)
            {
                player.GetModPlayer<OSRSplayer>().cluestep = 5;
            }
            int item = 0;
            int stack = 1;
            int ch = Main.rand.Next(10);
            switch (ch)
            {
                case 0:
                    item = ModContent.ItemType<Ammo.Dragonstonebolt>();
                    stack = 250;
                    break;
                case 1:
                    item = ModContent.ItemType<Dragonstone>();
                    stack = 10;
                    break;
                case 2:
                    item = ModContent.ItemType<Onyx>();
                    break;
                case 3:
                    item = ModContent.ItemType<Accessories.Amuletfury>();
                    break;
                case 4:
                    item = ModContent.ItemType<Magic.Astralrune>();
                    stack = 250;
                    player.QuickSpawnItem(item, stack);
                    item = ModContent.ItemType<Magic.Bloodrune>();
                    player.QuickSpawnItem(item, stack);
                    item = ModContent.ItemType<Magic.Soulrune>();
                    break;
                case 5:
                    item = ItemID.PlatinumCoin;
                    break;
                case 6:
                    item = ItemID.SuperHealingPotion;
                    stack = 30;
                    player.QuickSpawnItem(item, stack);
                    item = ItemID.SuperManaPotion;
                    break;
                case 7:
                    item = ItemID.CelestialSigil;
                    break;
                case 8:
                    item = ItemID.LifeFruit;
                    stack = 8;
                    break;
                default:
                    item = ItemID.FragmentNebula;
                    player.QuickSpawnItem(item, stack);
                    item = ItemID.FragmentStardust;
                    player.QuickSpawnItem(item, stack);
                    item = ItemID.FragmentVortex;
                    player.QuickSpawnItem(item, stack);
                    item = ItemID.FragmentSolar;
                    break;
            }
            player.QuickSpawnItem(item, stack);
        }
    }
    public class Implingjar10 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lucky Impling Jar");
            Tooltip.SetDefault("<right> to open");
        }
        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.rare = 10;
            item.value = Item.sellPrice(0, 15);
        }
        public override bool CanRightClick()
        {
            return true;
        }
        public override void RightClick(Player player)
        {
            int item = 0;
            int ch = Main.rand.Next(4);
            switch (ch)
            {
                case 0:
                    item = ModContent.ItemType<_3rdagebow>();
                    break;
                case 1:
                    item = ModContent.ItemType<_3rdagelong>();
                    break;
                case 2:
                    item = ModContent.ItemType<_3rdagewand>();
                    break;
                case 3:
                    item = ItemID.OldShoe;
                    break;
            }
            player.QuickSpawnItem(item);
        }
    }
}
