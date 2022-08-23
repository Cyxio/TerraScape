using Terraria.ID;
using Terraria.Localization;
using Terraria;
using Terraria.ModLoader;
using OldSchoolRuneScape.UI;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;
using Terraria.DataStructures;
using Terraria.GameContent.UI;
using OldSchoolRuneScape.Items.Weapons.Ranged;

namespace OldSchoolRuneScape.Items
{
    public class ClueItem : GlobalItem
    {
        public override bool OnPickup(Item item, Player player)
        {
            if ((player.GetModPlayer<OSRSplayer>().easyClue != 0 || player.GetModPlayer<OSRSplayer>().mediumClue != 0 || player.GetModPlayer<OSRSplayer>().hardClue != 0 || player.GetModPlayer<OSRSplayer>().eliteClue != 0 || player.GetModPlayer<OSRSplayer>().masterClue != 0))
            {
                if (item.type == ItemID.PlatinumCoin && player.goldRing && player.GetModPlayer<OSRSplayer>().hardClue == 15)
                {
                    player.GetModPlayer<OSRSplayer>().cluestep = 3;
                }
                if (item.type == ItemID.Heart && player.GetModPlayer<OSRSplayer>().easyClue == 23)
                {
                    player.GetModPlayer<OSRSplayer>().cluestep = 1;
                }
                if (item.type == ItemID.Star && player.GetModPlayer<OSRSplayer>().easyClue == 24)
                {
                    player.GetModPlayer<OSRSplayer>().cluestep = 1;
                }
            }
            return base.OnPickup(item, player);
        }
        public override void OnCreate(Item item, ItemCreationContext context)
        {
            Player player = Main.player[item.playerIndexTheItemIsReservedFor];
            if ((player.GetModPlayer<OSRSplayer>().easyClue != 0 || 
                player.GetModPlayer<OSRSplayer>().mediumClue != 0 || 
                player.GetModPlayer<OSRSplayer>().hardClue != 0 || 
                player.GetModPlayer<OSRSplayer>().eliteClue != 0 || 
                player.GetModPlayer<OSRSplayer>().masterClue != 0))
            {
                if (item.type == ItemID.ShroomiteBar && player.GetModPlayer<OSRSplayer>().eliteClue == 15)
                {
                    player.GetModPlayer<OSRSplayer>().cluestep = 4;
                }
                if (item.type == ItemID.GenderChangePotion && player.GetModPlayer<OSRSplayer>().hardClue == 27)
                {
                    player.GetModPlayer<OSRSplayer>().cluestep = 3;
                }
                if (item.type == ItemID.BowlofSoup && player.GetModPlayer<OSRSplayer>().easyClue == 29)
                {
                    player.GetModPlayer<OSRSplayer>().cluestep = 1;
                }
                if (item.type == ModContent.ItemType<Zenyte>() && player.GetModPlayer<OSRSplayer>().masterClue == 32)
                {
                    player.GetModPlayer<OSRSplayer>().cluestep = 5;
                }
            }
        }
        public override bool CanConsumeAmmo(Item weapon, Item ammo, Player player)
        {
            if (player.GetModPlayer<OSRSplayer>().easyClue != 0 || player.GetModPlayer<OSRSplayer>().mediumClue != 0 || player.GetModPlayer<OSRSplayer>().hardClue != 0 || player.GetModPlayer<OSRSplayer>().eliteClue != 0 || player.GetModPlayer<OSRSplayer>().masterClue != 0)
            {
                if (ammo.type == ItemID.PlatinumCoin && player.GetModPlayer<OSRSplayer>().masterClue == 11)
                {
                    player.GetModPlayer<OSRSplayer>().cluestep = 5;
                }
                if (ammo.type == ItemID.GreenSolution && player.GetModPlayer<OSRSplayer>().hardClue == 19)
                {
                    player.GetModPlayer<OSRSplayer>().cluestep = 3;
                }
            }
            return base.CanConsumeAmmo(weapon, ammo, player);
        }
        public override bool ConsumeItem(Item item, Player player)
        {
            if ((player.GetModPlayer<OSRSplayer>().easyClue != 0 || player.GetModPlayer<OSRSplayer>().mediumClue != 0 || player.GetModPlayer<OSRSplayer>().hardClue != 0 || player.GetModPlayer<OSRSplayer>().eliteClue != 0 || player.GetModPlayer<OSRSplayer>().masterClue != 0))
            {
                if (item.type == ItemID.Ale && player.GetModPlayer<OSRSplayer>().easyClue == 22)
                {
                    player.GetModPlayer<OSRSplayer>().cluestep = 1;
                }
                if (item.type == ItemID.CookedMarshmallow && player.GetModPlayer<OSRSplayer>().easyClue == 28)
                {
                    player.GetModPlayer<OSRSplayer>().cluestep = 1;
                }
                if ((item.type >= ItemID.LifeHairDye && item.type <= ItemID.SpeedHairDye) || (item.type == ItemID.MartianHairDye || item.type == ItemID.TwilightHairDye) && player.GetModPlayer<OSRSplayer>().mediumClue == 22)
                {
                    player.GetModPlayer<OSRSplayer>().cluestep = 2;
                }
                if (item.type == ItemID.PirateMap && player.GetModPlayer<OSRSplayer>().hardClue == 18)
                {
                    player.GetModPlayer<OSRSplayer>().cluestep = 3;
                }
                if (item.type == ItemID.SolarTablet && player.GetModPlayer<OSRSplayer>().eliteClue == 22)
                {
                    player.GetModPlayer<OSRSplayer>().cluestep = 4;
                }
            }
            return true;
        }
        public override void UpdateEquip(Item item, Player player)
        {
            if ((player.GetModPlayer<OSRSplayer>().easyClue != 0 || player.GetModPlayer<OSRSplayer>().mediumClue != 0 || player.GetModPlayer<OSRSplayer>().hardClue != 0 || player.GetModPlayer<OSRSplayer>().eliteClue != 0 || player.GetModPlayer<OSRSplayer>().masterClue != 0))
            {
                if (player.GetModPlayer<OSRSplayer>().eliteClue == 11)
                {
                    if (player.armor[10].type == ItemID.PlumbersHat && player.armor[11].type == ItemID.PlumbersShirt && player.armor[12].type == ItemID.PlumbersPants)
                    {
                        player.GetModPlayer<OSRSplayer>().cluestep = 4;
                    }
                }
                if (player.GetModPlayer<OSRSplayer>().eliteClue == 14 && item.type == ItemID.AnglerTackleBag)
                {
                    if (player.armor[0].type == ItemID.AnglerHat && player.armor[1].type == ItemID.AnglerVest && player.armor[2].type == ItemID.AnglerPants)
                    {
                        player.GetModPlayer<OSRSplayer>().cluestep = 4;
                    }
                }
                if (player.GetModPlayer<OSRSplayer>().masterClue == 8 && item.type == ItemID.DiamondRing)
                {
                    player.GetModPlayer<OSRSplayer>().cluestep = 5;
                }
                if (player.GetModPlayer<OSRSplayer>().masterClue == 15 && player.miscEquips[4].type == ItemID.LunarHook)
                {
                    player.GetModPlayer<OSRSplayer>().cluestep = 5;
                }
                if (player.GetModPlayer<OSRSplayer>().hardClue == 30 && player.miscEquips[3].type == ItemID.FuzzyCarrot)
                {
                    player.GetModPlayer<OSRSplayer>().cluestep = 3;
                }
                if (player.GetModPlayer<OSRSplayer>().masterClue == 16 && player.miscEquips[3].type == ItemID.DrillContainmentUnit)
                {
                    player.GetModPlayer<OSRSplayer>().cluestep = 5;
                }
                if (player.GetModPlayer<OSRSplayer>().masterClue == 25 && player.miscEquips[3].type == ItemID.BrainScrambler)
                {
                    player.GetModPlayer<OSRSplayer>().cluestep = 5;
                }
                if (player.GetModPlayer<OSRSplayer>().masterClue == 24 && player.miscEquips[0].type == ItemID.BoneKey)
                {
                    player.GetModPlayer<OSRSplayer>().cluestep = 5;
                }
                if (player.GetModPlayer<OSRSplayer>().masterClue == 26 && player.miscEquips[0].type == ItemID.LizardEgg)
                {
                    player.GetModPlayer<OSRSplayer>().cluestep = 5;
                }
                if (player.GetModPlayer<OSRSplayer>().masterClue == 27 && player.miscEquips[0].type == ItemID.CompanionCube)
                {
                    player.GetModPlayer<OSRSplayer>().cluestep = 5;
                }
                if (player.GetModPlayer<OSRSplayer>().eliteClue == 28 && player.miscEquips[0].type == ItemID.ParrotCracker)
                {
                    player.GetModPlayer<OSRSplayer>().cluestep = 4;
                }
                if (player.GetModPlayer<OSRSplayer>().eliteClue == 29 && player.miscEquips[1].type == ItemID.WispinaBottle)
                {
                    player.GetModPlayer<OSRSplayer>().cluestep = 4;
                }
                if (player.GetModPlayer<OSRSplayer>().eliteClue == 35 && item.type == ModContent.ItemType<Accessories.Amuletdamned>())
                {
                    player.GetModPlayer<OSRSplayer>().cluestep = 4;
                }
                if (player.GetModPlayer<OSRSplayer>().hardClue == 10 && item.type == ItemID.SunMask)
                {
                    player.GetModPlayer<OSRSplayer>().cluestep = 3;
                }
                if (player.GetModPlayer<OSRSplayer>().masterClue == 30 && item.type == ItemID.PeddlersHat)
                {
                    player.GetModPlayer<OSRSplayer>().cluestep = 5;
                }
                if (player.GetModPlayer<OSRSplayer>().hardClue == 28 && item.type == ItemID.PocketMirror)
                {
                    player.GetModPlayer<OSRSplayer>().cluestep = 3;
                }
                if (player.GetModPlayer<OSRSplayer>().eliteClue == 21 && item.type == ItemID.Hoverboard)
                {
                    player.GetModPlayer<OSRSplayer>().cluestep = 4;
                }
                if (player.GetModPlayer<OSRSplayer>().eliteClue == 24 && item.type == ItemID.ArchitectGizmoPack)
                {
                    player.GetModPlayer<OSRSplayer>().cluestep = 4;
                }
                if (player.GetModPlayer<OSRSplayer>().hardClue == 22)
                {
                    if (player.armor[10].type == ItemID.ClownHat && player.armor[11].type == ItemID.ClownShirt && player.armor[12].type == ItemID.ClownPants)
                    {
                        player.GetModPlayer<OSRSplayer>().cluestep = 3;
                    }
                }
                if (player.GetModPlayer<OSRSplayer>().hardClue == 36)
                {
                    if (player.armor[0].type == ModContent.ItemType<Armor.MystichatL>() && player.armor[1].type == ModContent.ItemType<Armor.MystictopL>() && player.armor[2].type == ModContent.ItemType<Armor.MysticbottomL>())
                    {
                        player.GetModPlayer<OSRSplayer>().cluestep = 3;
                    }
                }
                if (player.GetModPlayer<OSRSplayer>().masterClue == 17)
                {
                    if (player.armor[10].type == ItemID.PedguinHat && player.armor[11].type == ItemID.PedguinShirt && player.armor[12].type == ItemID.PedguinPants)
                    {
                        player.GetModPlayer<OSRSplayer>().cluestep = 5;
                    }
                }
                if (player.GetModPlayer<OSRSplayer>().masterClue == 18 && (item.type == ItemID.CelestialStone || item.type == ItemID.CelestialShell))
                {
                    player.GetModPlayer<OSRSplayer>().cluestep = 5;
                }
                if (player.GetModPlayer<OSRSplayer>().hardClue == 39 && item.type == ModContent.ItemType<Accessories.Tomeoffire>())
                {
                    player.GetModPlayer<OSRSplayer>().cluestep = 3;
                }
                if (player.GetModPlayer<OSRSplayer>().hardClue == 38 && item.type == ModContent.ItemType<Accessories.Amuletglory>())
                {
                    player.GetModPlayer<OSRSplayer>().cluestep = 3;
                }
                if (player.GetModPlayer<OSRSplayer>().eliteClue == 38 && item.type == ModContent.ItemType<Accessories.Amuletfury>())
                {
                    player.GetModPlayer<OSRSplayer>().cluestep = 4;
                }
            }
        }
        public override void UseStyle(Item item, Player player, Rectangle heldItemFrame)
        {
            if ((player.GetModPlayer<OSRSplayer>().easyClue != 0 || player.GetModPlayer<OSRSplayer>().mediumClue != 0 || player.GetModPlayer<OSRSplayer>().hardClue != 0 || player.GetModPlayer<OSRSplayer>().eliteClue != 0 || player.GetModPlayer<OSRSplayer>().masterClue != 0))
            {
                if (item.type == ItemID.BoneJavelin && player.GetModPlayer<OSRSplayer>().mediumClue == 30)
                {
                    if (player.armor[0].type == ItemID.FossilHelm && player.armor[1].type == ItemID.FossilShirt && player.armor[2].type == ItemID.FossilPants)
                    {
                        player.GetModPlayer<OSRSplayer>().cluestep = 2;
                    }
                }
                if (item.type == ItemID.Frostbrand && player.GetModPlayer<OSRSplayer>().hardClue == 17)
                {
                    if (player.armor[0].type == ItemID.FrostHelmet && player.armor[1].type == ItemID.FrostBreastplate && player.armor[2].type == ItemID.FrostLeggings)
                    {
                        player.GetModPlayer<OSRSplayer>().cluestep = 3;
                    }
                }
                if (item.type == ItemID.RodofDiscord && player.armor[10].type == ItemID.Sunglasses && player.GetModPlayer<OSRSplayer>().masterClue == 6)
                {
                    player.GetModPlayer<OSRSplayer>().cluestep = 5;
                }
                if (item.type == ItemID.SpiritFlame && player.armor[12].type == ItemID.DjinnsCurse && player.GetModPlayer<OSRSplayer>().eliteClue == 12)
                {
                    player.GetModPlayer<OSRSplayer>().cluestep = 4;
                }
                if (item.type == ItemID.CellPhone && player.GetModPlayer<OSRSplayer>().masterClue == 10)
                {
                    player.GetModPlayer<OSRSplayer>().cluestep = 5;
                }
                if (item.type == ItemID.PortalGun && player.GetModPlayer<OSRSplayer>().masterClue == 19)
                {
                    player.GetModPlayer<OSRSplayer>().cluestep = 5;
                }
                if (item.type == ItemID.TheAxe && player.GetModPlayer<OSRSplayer>().eliteClue == 17)
                {
                    player.GetModPlayer<OSRSplayer>().cluestep = 4;
                }
                if (item.type == ModContent.ItemType<Magic.Ancientstaff>() && player.altFunctionUse == 2 && player.GetModPlayer<OSRSplayer>().eliteClue == 36)
                {
                    player.GetModPlayer<OSRSplayer>().cluestep = 4;
                }
                if ((item.type == ModContent.ItemType<Lightballista>() || item.type == ModContent.ItemType<Heavyballista>()) && player.GetModPlayer<OSRSplayer>().Necklaceanguish && player.GetModPlayer<OSRSplayer>().eliteClue == 34)
                {
                    player.GetModPlayer<OSRSplayer>().cluestep = 4;
                }
                if (item.type == ItemID.TerraBlade && player.GetModPlayer<OSRSplayer>().eliteClue == 23)
                {
                    if (player.armor[0].type == ItemID.BeetleHelmet && (player.armor[1].type == ItemID.BeetleScaleMail || player.armor[1].type == ItemID.BeetleShell) && player.armor[2].type == ItemID.BeetleLeggings)
                    {
                        player.GetModPlayer<OSRSplayer>().cluestep = 4;
                    }
                }
            }
        }
        public override void OnConsumeItem(Item item, Player player)
        {
            if ((player.GetModPlayer<OSRSplayer>().easyClue != 0 || player.GetModPlayer<OSRSplayer>().mediumClue != 0 || player.GetModPlayer<OSRSplayer>().hardClue != 0 || player.GetModPlayer<OSRSplayer>().eliteClue != 0 || player.GetModPlayer<OSRSplayer>().masterClue != 0))
            {
                if (item.type == ItemID.DirtBlock && player.GetModPlayer<OSRSplayer>().easyClue == 19)
                {
                    player.GetModPlayer<OSRSplayer>().cluestep = 1;
                }
                if (item.type == ItemID.GoldCoin && player.GetModPlayer<OSRSplayer>().easyClue == 21)
                {
                    player.GetModPlayer<OSRSplayer>().cluestep = 1;
                }
                if (item.type == ItemID.Acorn && player.GetModPlayer<OSRSplayer>().easyClue == 30)
                {
                    player.GetModPlayer<OSRSplayer>().cluestep = 1;
                }
                if (item.type == ItemID.PlatinumCoin && player.GetModPlayer<OSRSplayer>().hardClue == 8)
                {
                    player.GetModPlayer<OSRSplayer>().cluestep = 3;
                }
                if (item.type == ItemID.CrystalBall && player.GetModPlayer<OSRSplayer>().hardClue == 16)
                {
                    player.GetModPlayer<OSRSplayer>().cluestep = 3;
                }
                if (item.type == ItemID.Teleporter && player.GetModPlayer<OSRSplayer>().hardClue == 21)
                {
                    player.GetModPlayer<OSRSplayer>().cluestep = 3;
                }
                if (item.type == ItemID.SnowCloudBlock && player.GetModPlayer<OSRSplayer>().hardClue == 26)
                {
                    player.GetModPlayer<OSRSplayer>().cluestep = 3;
                }
                if (player.GetModPlayer<OSRSplayer>().masterClue == 7 && (item.type == ItemID.HallowedChest || item.type == ItemID.CrimsonChest || item.type == ItemID.JungleChest || item.type == ItemID.CorruptionChest || item.type == ItemID.FrozenChest))
                {
                    if (player.armor[10].type == ItemID.WhiteLunaticHood && player.armor[11].type == ItemID.WhiteLunaticRobe)
                    {
                        player.GetModPlayer<OSRSplayer>().cluestep = 5;
                    }
                }
                if (item.type == ItemID.LihzahrdAltar && player.GetModPlayer<OSRSplayer>().eliteClue == 13)
                {
                    player.GetModPlayer<OSRSplayer>().cluestep = 4;
                }
                if ((item.type >= ItemID.VortexMonolith && item.type <= ItemID.SolarMonolith) && player.GetModPlayer<OSRSplayer>().masterClue == 14)
                {
                    player.GetModPlayer<OSRSplayer>().cluestep = 5;
                }
            }
        }
    }
    public class ClueTile : GlobalTile
    {
        public override void PlaceInWorld(int i, int j, int type, Item item)
        {

        }
        public override void KillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            Player player = Main.player[0];
            for (int s = 0; s < Main.CurrentFrameFlags.ActivePlayersCount; s++)
            {
                if (Main.player[s].Distance(new Vector2(i*16, j*16)) < player.Distance(new Vector2(i * 16, j * 16)))
                {
                    player = Main.player[s];
                }
            }
            if (type == TileID.Trees && !fail && player.GetModPlayer<OSRSplayer>().easyClue == 20)
            {
                player.GetModPlayer<OSRSplayer>().cluestep = 1;
            }
        }
    }
}
