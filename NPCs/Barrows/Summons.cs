using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.NPCs.Barrows
{
    public class Cryptspade : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crypt Spade");
            Tooltip.SetDefault("'You dare enter the crypts...?'");
        }
        public override void SetDefaults()
        {
            item.consumable = true;
            item.maxStack = 20;
            item.useStyle = 4;
            item.useTime = 45;
            item.useAnimation = 45;
            item.rare = 5;
        }
        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(mod.NPCType("Ahrim")) && !NPC.AnyNPCs(mod.NPCType("Dharok")) && !NPC.AnyNPCs(mod.NPCType("Guthan")) && !NPC.AnyNPCs(mod.NPCType("Torag")) && !NPC.AnyNPCs(mod.NPCType("Verac")) && !NPC.AnyNPCs(mod.NPCType("Karil")) && !Main.dayTime;
        }
        public override bool UseItem(Player player)
        {
            string boss = "Ahrim";
            int ch = Main.rand.Next(6);
            switch (ch)
            {
                case 0:
                    boss = "Dharok";
                    break;
                case 1:
                    boss = "Torag";
                    break;
                case 2:
                    boss = "Guthan";
                    break;
                case 3:
                    boss = "Verac";
                    break;
                case 4:
                    boss = "Karil";
                    break;
                default:
                    boss = "Ahrim";
                    break;
            }
            NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType(boss));
            Main.PlaySound(SoundID.Roar, player.position, 0);
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("Wood", 10);
            recipe.AddIngredient(ItemID.IronBar, 2);
            recipe.AddIngredient(ItemID.SoulofLight, 3);
            recipe.SetResult(this);
            recipe.AddRecipe();
            recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("Wood", 10);
            recipe.AddIngredient(ItemID.LeadBar, 2);
            recipe.AddIngredient(ItemID.SoulofLight, 3);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
    public class Ahrimsummon : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cryptic Stone");
            Tooltip.SetDefault("'It has a staff carved onto it' \nSummons Ahrim");
        }
        public override void SetDefaults()
        {
            item.consumable = true;
            item.maxStack = 20;
            item.useStyle = 4;
            item.useTime = 45;
            item.useAnimation = 45;
            item.rare = 5;
        }
        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(mod.NPCType("Ahrim")) && !Main.dayTime;
        }
        public override bool UseItem(Player player)
        {
            NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("Ahrim"));
            Main.PlaySound(SoundID.Roar, player.position, 0);
            return true;
        }
    }
    public class Guthansummon : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cryptic Stone");
            Tooltip.SetDefault("'It has a spear carved onto it' \nSummons Guthan");
        }
        public override void SetDefaults()
        {
            item.consumable = true;
            item.maxStack = 20;
            item.useStyle = 4;
            item.useTime = 45;
            item.useAnimation = 45;
            item.rare = 5;
        }
        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(mod.NPCType("Guthan")) && !Main.dayTime;
        }
        public override bool UseItem(Player player)
        {
            NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("Guthan"));
            Main.PlaySound(SoundID.Roar, player.position, 0);
            return true;
        }
    }
    public class Dharoksummon : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cryptic Stone");
            Tooltip.SetDefault("'It has an axe carved onto it' \nSummons Dharok");
        }
        public override void SetDefaults()
        {
            item.consumable = true;
            item.maxStack = 20;
            item.useStyle = 4;
            item.useTime = 45;
            item.useAnimation = 45;
            item.rare = 5;
        }
        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(mod.NPCType("Dharok")) && !Main.dayTime;
        }
        public override bool UseItem(Player player)
        {
            NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("Dharok"));
            Main.PlaySound(SoundID.Roar, player.position, 0);
            return true;
        }
    }
    public class Veracsummon : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cryptic Stone");
            Tooltip.SetDefault("'It has a flail carved onto it' \nSummons Verac");
        }
        public override void SetDefaults()
        {
            item.consumable = true;
            item.maxStack = 20;
            item.useStyle = 4;
            item.useTime = 45;
            item.useAnimation = 45;
            item.rare = 5;
        }
        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(mod.NPCType("Verac")) && !Main.dayTime;
        }
        public override bool UseItem(Player player)
        {
            NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("Verac"));
            Main.PlaySound(SoundID.Roar, player.position, 0);
            return true;
        }
    }
    public class Toragsummon : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cryptic Stone");
            Tooltip.SetDefault("'It has a hammer carved onto it' \nSummons Torag");
        }
        public override void SetDefaults()
        {
            item.consumable = true;
            item.maxStack = 20;
            item.useStyle = 4;
            item.useTime = 45;
            item.useAnimation = 45;
            item.rare = 5;
        }
        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(mod.NPCType("Torag")) && !Main.dayTime;
        }
        public override bool UseItem(Player player)
        {
            NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("Torag"));
            Main.PlaySound(SoundID.Roar, player.position, 0);
            return true;
        }
    }
    public class Karilsummon : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cryptic Stone");
            Tooltip.SetDefault("'It has a crossbow carved onto it' \nSummons Karil");
        }
        public override void SetDefaults()
        {
            item.consumable = true;
            item.maxStack = 20;
            item.useStyle = 4;
            item.useTime = 45;
            item.useAnimation = 45;
            item.rare = 5;
        }
        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(mod.NPCType("Karil")) && !Main.dayTime;
        }
        public override bool UseItem(Player player)
        {
            NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("Karil"));
            Main.PlaySound(SoundID.Roar, player.position, 0);
            return true;
        }
    }
    public class Rockcluster : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Stack of stones");
            Tooltip.SetDefault("'Why would you...?'");
        }
        public override void SetDefaults()
        {
            item.consumable = true;
            item.maxStack = 20;
            item.useStyle = 4;
            item.useTime = 45;
            item.useAnimation = 45;
            item.rare = 6;
            item.scale = 0.75f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType<Dharoksummon>());
            recipe.AddIngredient(mod.ItemType<Guthansummon>());
            recipe.AddIngredient(mod.ItemType<Veracsummon>());
            recipe.AddIngredient(mod.ItemType<Toragsummon>());
            recipe.AddIngredient(mod.ItemType<Ahrimsummon>());
            recipe.AddIngredient(mod.ItemType<Karilsummon>());
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(mod.NPCType("Ahrim")) && !NPC.AnyNPCs(mod.NPCType("Dharok")) && !NPC.AnyNPCs(mod.NPCType("Guthan")) && !NPC.AnyNPCs(mod.NPCType("Torag")) && !NPC.AnyNPCs(mod.NPCType("Verac")) && !NPC.AnyNPCs(mod.NPCType("Karil")) && !Main.dayTime;
        }
        public override bool UseItem(Player player)
        {
            Vector2 pos = new Vector2(0, -1000);
            NPC.NewNPC((int)(player.MountedCenter.X + pos.X), (int)(player.MountedCenter.Y + pos.Y), mod.NPCType<Dharok.Dharok>());
            NPC.NewNPC((int)(player.MountedCenter.X), (int)(player.MountedCenter.Y - 120), mod.NPCType<Barrowsspirit>());
            pos = pos.RotatedBy(MathHelper.ToRadians(60));
            NPC.NewNPC((int)(player.MountedCenter.X + pos.X), (int)(player.MountedCenter.Y + pos.Y), mod.NPCType<Verac.Verac>());
            pos = pos.RotatedBy(MathHelper.ToRadians(60));
            NPC.NewNPC((int)(player.MountedCenter.X + pos.X), (int)(player.MountedCenter.Y + pos.Y), mod.NPCType<Torag.Torag>());
            pos = pos.RotatedBy(MathHelper.ToRadians(60));
            NPC.NewNPC((int)(player.MountedCenter.X + pos.X), (int)(player.MountedCenter.Y + pos.Y), mod.NPCType<Guthan.Guthan>());
            pos = pos.RotatedBy(MathHelper.ToRadians(60));
            NPC.NewNPC((int)(player.MountedCenter.X + pos.X), (int)(player.MountedCenter.Y + pos.Y), mod.NPCType<Ahrim.Ahrim>());
            pos = pos.RotatedBy(MathHelper.ToRadians(60));
            NPC.NewNPC((int)(player.MountedCenter.X + pos.X), (int)(player.MountedCenter.Y + pos.Y), mod.NPCType<Karil.Karil>());
            Main.PlaySound(SoundID.Roar, player.position, 0);
            return true;
        }
    }
}
