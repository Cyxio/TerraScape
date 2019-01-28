using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using System;
using Terraria.ID;
using System.Linq;
using ReLogic;
using ReLogic.Graphics;

namespace OldSchoolRuneScape.UI
{
    internal class ClueRewardUI : UIState
    {
        internal static bool visible = false;
        internal static int[] rewards = new int[9];
        internal static int[] stacks = new int[9];
        internal static string texture = "OldSchoolRuneScape/Items/ClueScroll/ClueReward";
        public override void OnInitialize()
        {
            UIimage parent = new UIimage();
            parent.Height.Set(296, 0f);
            parent.Width.Set(324, 0f);
            parent.Left.Set(Main.screenWidth / 2 - parent.Width.Pixels / 2, 0f);
            parent.Top.Set(Main.screenHeight / 2 - parent.Height.Pixels / 2, 0f);
            parent.backgroundColor = new Color(255, 255, 255, 255);

            base.Append(parent);
        }
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            CalculatedStyle dimensions = new CalculatedStyle(Main.screenWidth / 2 - 324 / 2, Main.screenHeight / 2 - 296 / 2, 324, 296);
            Point point1 = new Point((int)dimensions.X, (int)dimensions.Y);
            int width = (int)Math.Ceiling(dimensions.Width);
            int height = (int)Math.Ceiling(dimensions.Height);
            spriteBatch.Draw(ModLoader.GetTexture(texture), new Rectangle(point1.X, point1.Y, width, height), Color.White);
            for (int i = 0; i < 9; i++)
            {
                if (rewards[i] != 0)
                {
                    int positionX = 40 * (i % 3);
                    int positionY = 64;
                    if (i > 2)
                    {
                        positionY += 40;
                    }
                    if (i > 5)
                    {
                        positionY += 40;
                    }
                    if (rewards[i] == ItemID.SoulofLight || rewards[i] == ItemID.SoulofNight)
                    {
                        spriteBatch.Draw(Main.itemTexture[rewards[i]], new Vector2(point1.X + (width / 2) + positionX, point1.Y + positionY),
                            new Rectangle(0, 0, 22, 22), Color.White, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
                    }
                    else
                    {
                        float widthScale = 1f;
                        float heightScale = 1f;
                        while (Main.itemTexture[rewards[i]].Width * widthScale > 36)
                        {
                            widthScale *= 0.99f;
                        }
                        while (Main.itemTexture[rewards[i]].Height * heightScale > 36)
                        {
                            heightScale *= 0.99f;
                        }
                        float scale = Math.Min(widthScale, heightScale);
                        spriteBatch.Draw(Main.itemTexture[rewards[i]], new Vector2(point1.X + (width / 2) + positionX, point1.Y + positionY),
                            null, Color.White, 0f, default(Vector2), scale, SpriteEffects.None, 0f);
                    }
                    if (stacks[i] > 1)
                    {
                        spriteBatch.DrawString(Main.fontItemStack, stacks[i].ToString(), new Vector2(point1.X + (width / 2) + positionX, point1.Y + positionY + 19), Color.White);
                    }
                }
            }
        }
        public override void Click(UIMouseEvent evt)
        {
            SpawnReward();
        }
        public override void Update(GameTime gameTime)
        {
            if (Main.playerInventory)
            {
                SpawnReward();
            }
        }
        internal void SpawnReward()
        {
            for (int i = 0; i < 9; i++)
            {
                if (rewards[i] != 0)
                {
                    Main.player[Main.myPlayer].QuickSpawnItem(rewards[i], stacks[i]);
                    rewards[i] = 0;
                }
            }
            visible = false;
        }
        public static void GetRewards(int Diff)
        {
            for (int i = 0; i < 9; i++)
            {
                rewards[i] = 0;
                stacks[i] = 0;
            }
            int amount = Main.rand.Next(Diff, Diff + 4);
            for (int i = 0; i < amount; i++)
            {
                int[] e = RewardPool(Diff);
                rewards[i] = e[0];
                stacks[i] = e[1];
            }
        }
        private static int[] RewardPool(int Diff)
        {
            Mod mod = ModLoader.GetMod("OldSchoolRuneScape");
            int t = 0;
            int y = 1;
            if (Main.rand.Next(3000 - 500 * Diff) == 0)
            {
                switch (Main.rand.Next(6))
                {
                    case 0:
                        t = mod.ItemType("Bluepartyhat");
                        break;
                    case 1:
                        t = mod.ItemType("Redpartyhat");
                        break;
                    case 2:
                        t = mod.ItemType("Yellowpartyhat");
                        break;
                    case 3:
                        t = mod.ItemType("Purplepartyhat");
                        break;
                    case 4:
                        t = mod.ItemType("Greenpartyhat");
                        break;
                    default:
                        t = mod.ItemType("Whitepartyhat");
                        break;
                }
                return new int[] { t, y };
            }
            if (Main.rand.Next(64) == 0)
            {
                string s = "";
                switch (Main.rand.Next(6))
                {
                    case 0:
                        s = "Guthixpage";
                        break;
                    case 1:
                        s = "Zamorakpage";
                        break;
                    case 2:
                        s = "Saradominpage";
                        break;
                    case 3:
                        s = "Armadylpage";
                        break;
                    case 4:
                        s = "Ancientpage";
                        break;
                    case 5:
                        s = "Bandospage";
                        break;
                }
                s = s + Main.rand.Next(1, 5);
                t = mod.ItemType(s);
                return new int[] { t, y };
            }
            if (Main.rand.Next(8) == 0)
            {
                int ch = Main.rand.Next(7);
                y = Main.rand.Next(5, 21);
                if (Diff >= 2)
                {
                    ch += 3;
                    y = Main.rand.Next(25, 51);
                }
                if (Diff >= 3)
                {
                    ch += 1;
                    y = Main.rand.Next(40, 81);
                }
                if (Diff >= 4)
                {
                    ch += 3;
                    y = Main.rand.Next(50, 101);
                }
                if (ch == 0) { t = mod.ItemType<Items.Magic.Mindrune>(); }
                if (ch == 1) { t = mod.ItemType<Items.Magic.Bodyrune>(); }
                if (ch == 2) { t = mod.ItemType<Items.Magic.Airrune>(); }
                if (ch == 3) { t = mod.ItemType<Items.Magic.Firerune>(); }
                if (ch == 4) { t = mod.ItemType<Items.Magic.Earthrune>(); }
                if (ch == 5) { t = mod.ItemType<Items.Magic.Waterrune>(); }
                if (ch == 6) { t = mod.ItemType<Items.Magic.Cosmicrune>(); }
                if (ch == 7) { t = mod.ItemType<Items.Magic.Chaosrune>(); }
                if (ch == 8) { t = mod.ItemType<Items.Magic.Lawrune>(); }
                if (ch == 9) { t = mod.ItemType<Items.Magic.Naturerune>(); }
                if (ch == 10) { t = mod.ItemType<Items.Magic.Deathrune>(); }
                if (ch == 11) { t = mod.ItemType<Items.Magic.Bloodrune>(); }
                if (ch == 12) { t = mod.ItemType<Items.Magic.Soulrune>(); }
                if (ch == 13) { t = mod.ItemType<Items.Magic.Astralrune>(); }
                return new int[] { t, y };
            }
            if (Diff == 1)
            {
                int item = Main.rand.Next(43);
                if (item == 0) { t = ItemID.WoodenArrow; y = Main.rand.Next(10, 51); }
                if (item == 1) { t = ItemID.FallenStar; y = Main.rand.Next(3, 11); }
                if (item == 2) { t = ItemID.SlimeCrown; }
                if (item == 3) { t = ItemID.SuspiciousLookingEye; }
                if (item == 4) { t = ItemID.SilverCoin; y = Main.rand.Next(10, 51); }
                if (item == 5) { t = ItemID.CopperCoin; y = Main.rand.Next(1, 101); }
                if (item == 6) { t = ItemID.GoldCoin; y = Main.rand.Next(1, 5); }
                if (item == 7) { t = ItemID.LesserManaPotion; y = Main.rand.Next(1, 11); }
                if (item == 8) { t = ItemID.LesserHealingPotion; y = Main.rand.Next(1, 11); }
                if (item == 9) { t = ItemID.SwiftnessPotion; y = Main.rand.Next(1, 3); }
                if (item == 10) { t = ItemID.RegenerationPotion; y = Main.rand.Next(1, 3); }
                if (item == 11) { t = ItemID.IronskinPotion; y = Main.rand.Next(1, 3); }
                if (item == 12) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Easy.BronzelegsT>(); }
                if (item == 13) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Easy.IronbodyG>(); }
                if (item == 14) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Easy.IronhelmG>(); }
                if (item == 15) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Easy.IronlegsG>(); }
                if (item == 16) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Easy.IronbodyT>(); }
                if (item == 17) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Easy.IronhelmT>(); }
                if (item == 18) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Easy.IronlegsT>(); }
                if (item == 19) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Easy.BlackbodyG>(); }
                if (item == 20) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Easy.BlackhelmG>(); }
                if (item == 21) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Easy.BlacklegsG>(); }
                if (item == 22) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Easy.BlackbodyT>(); }
                if (item == 23) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Easy.BlackhelmT>(); }
                if (item == 24) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Easy.BlacklegsT>(); }
                if (item == 25) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Easy.BronzehelmG>(); }
                if (item == 26) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Easy.BronzebodyG>(); }
                if (item == 27) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Easy.BlackwizbodyG>(); }
                if (item == 28) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Easy.BlackwizhatG>(); }
                if (item == 29) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Easy.BlackwizlegsG>(); }
                if (item == 30) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Easy.BlackwizbodyT>(); }
                if (item == 31) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Easy.BlackwizhatT>(); }
                if (item == 32) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Easy.BlackwizlegsT>(); }
                if (item == 33) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Easy.BluewizbodyG>(); }
                if (item == 34) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Easy.BluewizhatG>(); }
                if (item == 35) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Easy.BluewizlegsG>(); }
                if (item == 36) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Easy.BluewizbodyT>(); }
                if (item == 37) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Easy.BluewizhatT>(); }
                if (item == 38) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Easy.BluewizlegsT>(); }
                if (item == 39) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Easy.Goblinmask>(); }
                if (item == 40) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Easy.BronzehelmT>(); }
                if (item == 41) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Easy.BronzebodyT>(); }
                if (item == 42) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Easy.BronzelegsG>(); }
            }
            if (Diff == 2)
            {
                if (Main.rand.Next(256) == 0)
                {
                    switch (Main.rand.Next(3))
                    {
                        case 0:
                            t = mod.ItemType<Items.ClueScroll.ClueRewards.Medium.Wizardboots>();
                            break;
                        case 1:
                            t = mod.ItemType<Items.ClueScroll.ClueRewards.Medium.Rangerboots>();
                            break;
                        case 2:
                            t = mod.ItemType<Items.ClueScroll.ClueRewards.Medium.Holysandals>();
                            break;
                    }
                    return new int[] { t, y };
                }
                int item = Main.rand.Next(25);
                if (item == 0) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Medium.AdamantbodyG>(); }
                if (item == 1) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Medium.AdamanthelmG>(); }
                if (item == 2) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Medium.AdamantlegsG>(); }
                if (item == 3) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Medium.AdamantbodyT>(); }
                if (item == 4) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Medium.AdamanthelmT>(); }
                if (item == 5) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Medium.AdamantlegsT>(); }
                if (item == 6) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Medium.MithrilbodyG>(); }
                if (item == 7) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Medium.MithrilhelmG>(); }
                if (item == 8) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Medium.MithrillegsG>(); }
                if (item == 9) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Medium.MithrilbodyT>(); }
                if (item == 10) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Medium.MithrilhelmT>(); }
                if (item == 11) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Medium.MithrillegsT>(); }
                if (item == 12) { t = ItemID.SilverCoin; y = Main.rand.Next(50, 101); }
                if (item == 13) { t = ItemID.GoldCoin; y = Main.rand.Next(3, 9); }
                if (item == 14) { t = ItemID.HealingPotion; y = Main.rand.Next(1, 11); }
                if (item == 15) { t = ItemID.ManaPotion; y = Main.rand.Next(1, 11); }
                if (item == 16) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Medium.Lesserdemonmask>(); }
                if (item == 17) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Medium.Greaterdemonmask>(); }
                if (item == 18) { t = ItemID.MiningPotion; y = Main.rand.Next(1, 3); }
                if (item == 19) { t = ItemID.ObsidianSkinPotion; y = Main.rand.Next(1, 3); }
                if (item == 20) { t = ItemID.SpelunkerPotion; y = Main.rand.Next(1, 3); }
                if (item == 21) { t = mod.ItemType<Items.Accessories.Amuletdefence>(); }
                if (item == 22) { t = mod.ItemType<Items.Accessories.Amuletmagic>(); }
                if (item == 23) { t = mod.ItemType<Items.Accessories.Amuletpower>(); }
                if (item == 24) { t = mod.ItemType<Items.Accessories.Amuletstrength>(); }
            }
            if (Diff == 3)
            {
                if (Main.rand.Next(128) == 0)
                {
                    switch (Main.rand.Next(4))
                    {
                        case 0:
                            t = mod.ItemType<Items.ClueScroll.ClueRewards.Hard.RunebodyGIL>();
                            break;
                        case 1:
                            t = mod.ItemType<Items.ClueScroll.ClueRewards.Hard.RunehelmGIL>();
                            break;
                        case 2:
                            t = mod.ItemType<Items.ClueScroll.ClueRewards.Hard.RunelegsGIL>();
                            break;
                        case 3:
                            t = mod.ItemType<Items.ClueScroll.ClueRewards.Hard.Robinhat>();
                            break;
                    }
                    return new int[] { t, y };
                }
                int item = Main.rand.Next(42);
                if (item == 0) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Hard.RunebodyT>(); }
                if (item == 1) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Hard.RunehelmT>(); }
                if (item == 2) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Hard.RunelegsT>(); }
                if (item == 3) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Hard.RunebodyG>(); }
                if (item == 4) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Hard.RunehelmG>(); }
                if (item == 5) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Hard.RunelegsG>(); }
                if (item == 6) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Hard.RunebodyAN>(); }
                if (item == 7) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Hard.RunehelmAN>(); }
                if (item == 8) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Hard.RunelegsAN>(); }
                if (item == 9) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Hard.RunebodyB>(); }
                if (item == 10) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Hard.RunelegsB>(); }
                if (item == 11) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Hard.RunehelmB>(); }
                if (item == 12) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Hard.RunebodyAR>(); }
                if (item == 13) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Hard.RunehelmAR>(); }
                if (item == 14) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Hard.RunelegsAR>(); }
                if (item == 15) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Hard.RunebodyZ>(); }
                if (item == 16) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Hard.RunehelmZ>(); }
                if (item == 17) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Hard.RunelegsZ>(); }
                if (item == 18) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Hard.RunebodyS>(); }
                if (item == 19) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Hard.RunehelmS>(); }
                if (item == 20) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Hard.RunelegsS>(); }
                if (item == 21) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Hard.RunebodyGX>(); }
                if (item == 22) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Hard.RunehelmGX>(); }
                if (item == 23) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Hard.RunelegsGX>(); }
                if (item == 24) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Hard.Blackdemonmask>(); }
                if (item == 25) { t = ItemID.GoldCoin; y = Main.rand.Next(10, 21); }
                if (item == 26) { t = ItemID.GreaterHealingPotion; y = Main.rand.Next(1, 11); }
                if (item == 27) { t = ItemID.GreaterManaPotion; y = Main.rand.Next(1, 11); }
                if (item == 28) { t = ItemID.SoulofLight; y = Main.rand.Next(5, 16); }
                if (item == 29) { t = ItemID.SoulofNight; y = Main.rand.Next(5, 16); }
                if (item == 30) { t = ItemID.TurtleShell; }
                if (item == 31) { t = ItemID.FrostCore; }
                if (item == 32) { t = 3783; }
                if (item == 33) { t = ItemID.RagePotion; y = Main.rand.Next(1, 3); }
                if (item == 34) { t = ItemID.WrathPotion; y = Main.rand.Next(1, 3); }
                if (item == 35) { t = ItemID.EndurancePotion; y = Main.rand.Next(1, 3); }
                if (item == 36) { t = ItemID.LifeforcePotion; y = Main.rand.Next(1, 3); }
                if (item == 37) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Hard.Greendragonmask>(); }
                if (item == 38) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Hard.Bluedragonmask>(); }
                if (item == 39) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Hard.Blackdragonmask>(); }
                if (item == 40) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Hard.Reddragonmask>(); }
                if (item == 41) { t = mod.ItemType<Items.Dragonstone>(); }
            }
            if (Diff == 4)
            {
                if (Main.rand.Next(1000) == 0)
                {
                    switch (Main.rand.Next(2))
                    {
                        case 0:
                            t = mod.ItemType<Items._3rdagebow>();
                            break;
                        case 1:
                            t = mod.ItemType<Items._3rdagelong>();
                            break;
                        case 2:
                            t = mod.ItemType<Items._3rdagewand>();
                            break;
                    }
                    return new int[] { t, y };
                }
                if (Main.rand.Next(128) == 0)
                {
                    return new int[] { mod.ItemType<Items.ClueScroll.ClueRewards.Elite.Rangerstunic>(), y };
                }
                int item = Main.rand.Next(11);
                if (item == 0) { t = ItemID.GoldCoin; y = Main.rand.Next(25, 51); }
                if (item == 1) { t = ItemID.GreaterManaPotion; y = 30; }
                if (item == 2) { t = ItemID.GreaterHealingPotion; y = 30; }
                if (item == 3) { t = ItemID.BrokenHeroSword; }
                if (item == 4) { t = ItemID.SolarTablet; }
                if (item == 5) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Elite.Lavadragonmask>(); }
                if (item == 6) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Elite.Deerstalker>(); }
                if (item == 7) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Elite.Buckethelm>(); }
                if (item == 8) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Elite.Briefcase>(); }
                if (item == 9) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Elite.Ringnature>(); }
                if (item == 10) { t = mod.ItemType<Items.Onyx>(); }
            }
            if (Diff == 5)
            {
                if (Main.rand.Next(1000) == 0)
                {
                    return new int[] { mod.ItemType<Items.ClueScroll.ClueRewards.Master.BloodhoundItem>(), 1 };
                }
                int item = Main.rand.Next(14);
                if (item == 0) { t = ItemID.PlatinumCoin; }
                if (item == 1) { t = ItemID.SuperHealingPotion; y = 30; }
                if (item == 2) { t = ItemID.SuperManaPotion; y = 30; }
                if (item == 3) { t = ItemID.FragmentVortex; y = 50; }
                if (item == 4) { t = ItemID.FragmentStardust; y = 50; }
                if (item == 5) { t = ItemID.FragmentSolar; y = 50; }
                if (item == 6) { t = ItemID.FragmentNebula; y = 50; }
                if (item == 7) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Master.BuckethelmG>(); }
                if (item == 8) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Master.Ankoubody>(); }
                if (item == 9) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Master.Ankouhead>(); }
                if (item == 10) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Master.Ankoulegs>(); }
                if (item == 11) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Master.Bowlwig>(); }
                if (item == 12) { t = mod.ItemType<Items.ClueScroll.ClueRewards.Master.Ringcoins>(); }
                if (item == 13) { t = mod.ItemType<Items.Zenyteshard>(); }
            }
            return new int[] { t, y };
        }
    }
}
