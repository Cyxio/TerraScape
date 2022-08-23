using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
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
            Item.consumable = true;
            Item.maxStack = 20;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTime = 45;
            Item.useAnimation = 45;
            Item.rare = ItemRarityID.Pink;
        }
        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(Mod.Find<ModNPC>("Ahrim").Type) && !NPC.AnyNPCs(Mod.Find<ModNPC>("Dharok").Type) && !NPC.AnyNPCs(Mod.Find<ModNPC>("Guthan").Type) && !NPC.AnyNPCs(Mod.Find<ModNPC>("Torag").Type) && !NPC.AnyNPCs(Mod.Find<ModNPC>("Verac").Type) && !NPC.AnyNPCs(Mod.Find<ModNPC>("Karil").Type) && !Main.dayTime;
        }
        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
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
            NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>(boss).Type);
            SoundEngine.PlaySound(SoundID.Roar, player.position);
            return true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("Wood", 10);
            recipe.AddIngredient(ItemID.IronBar, 2);
            recipe.AddIngredient(ItemID.SoulofLight, 3);
            recipe.Register();
            recipe = CreateRecipe();
            recipe.AddRecipeGroup("Wood", 10);
            recipe.AddIngredient(ItemID.LeadBar, 2);
            recipe.AddIngredient(ItemID.SoulofLight, 3);
            recipe.Register();
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
            Item.consumable = true;
            Item.maxStack = 20;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTime = 45;
            Item.useAnimation = 45;
            Item.rare = ItemRarityID.Pink;
        }
        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(Mod.Find<ModNPC>("Ahrim").Type) && !Main.dayTime;
        }
        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>("Ahrim").Type);
            SoundEngine.PlaySound(SoundID.Roar, player.position);
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
            Item.consumable = true;
            Item.maxStack = 20;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTime = 45;
            Item.useAnimation = 45;
            Item.rare = ItemRarityID.Pink;
        }
        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(Mod.Find<ModNPC>("Guthan").Type) && !Main.dayTime;
        }
        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>("Guthan").Type);
            SoundEngine.PlaySound(SoundID.Roar, player.position);
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
            Item.consumable = true;
            Item.maxStack = 20;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTime = 45;
            Item.useAnimation = 45;
            Item.rare = ItemRarityID.Pink;
        }
        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(Mod.Find<ModNPC>("Dharok").Type) && !Main.dayTime;
        }
        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>("Dharok").Type);
            SoundEngine.PlaySound(SoundID.Roar, player.position);
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
            Item.consumable = true;
            Item.maxStack = 20;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTime = 45;
            Item.useAnimation = 45;
            Item.rare = ItemRarityID.Pink;
        }
        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(Mod.Find<ModNPC>("Verac").Type) && !Main.dayTime;
        }
        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>("Verac").Type);
            SoundEngine.PlaySound(SoundID.Roar, player.position);
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
            Item.consumable = true;
            Item.maxStack = 20;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTime = 45;
            Item.useAnimation = 45;
            Item.rare = ItemRarityID.Pink;
        }
        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(Mod.Find<ModNPC>("Torag").Type) && !Main.dayTime;
        }
        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>("Torag").Type);
            SoundEngine.PlaySound(SoundID.Roar, player.position);
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
            Item.consumable = true;
            Item.maxStack = 20;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTime = 45;
            Item.useAnimation = 45;
            Item.rare = ItemRarityID.Pink;
        }
        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(Mod.Find<ModNPC>("Karil").Type) && !Main.dayTime;
        }
        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>("Karil").Type);
            SoundEngine.PlaySound(SoundID.Roar, player.position);
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
            Item.consumable = true;
            Item.maxStack = 20;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTime = 45;
            Item.useAnimation = 45;
            Item.rare = ItemRarityID.LightPurple;
            Item.scale = 0.75f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Dharoksummon>());
            recipe.AddIngredient(ModContent.ItemType<Guthansummon>());
            recipe.AddIngredient(ModContent.ItemType<Veracsummon>());
            recipe.AddIngredient(ModContent.ItemType<Toragsummon>());
            recipe.AddIngredient(ModContent.ItemType<Ahrimsummon>());
            recipe.AddIngredient(ModContent.ItemType<Karilsummon>());
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(Mod.Find<ModNPC>("Ahrim").Type) && !NPC.AnyNPCs(Mod.Find<ModNPC>("Dharok").Type) && !NPC.AnyNPCs(Mod.Find<ModNPC>("Guthan").Type) && !NPC.AnyNPCs(Mod.Find<ModNPC>("Torag").Type) && !NPC.AnyNPCs(Mod.Find<ModNPC>("Verac").Type) && !NPC.AnyNPCs(Mod.Find<ModNPC>("Karil").Type) && !Main.dayTime;
        }
        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            Vector2 pos = new Vector2(0, -1000);
            NPC.NewNPC(player.GetSource_ItemUse(Item), (int)(player.MountedCenter.X + pos.X), (int)(player.MountedCenter.Y + pos.Y), ModContent.NPCType<Dharok.Dharok>());
            NPC.NewNPC(player.GetSource_ItemUse(Item), (int)(player.MountedCenter.X), (int)(player.MountedCenter.Y - 120), ModContent.NPCType<Barrowsspirit>());
            pos = pos.RotatedBy(MathHelper.ToRadians(60));
            NPC.NewNPC(player.GetSource_ItemUse(Item), (int)(player.MountedCenter.X + pos.X), (int)(player.MountedCenter.Y + pos.Y), ModContent.NPCType<Verac.Verac>());
            pos = pos.RotatedBy(MathHelper.ToRadians(60));
            NPC.NewNPC(player.GetSource_ItemUse(Item), (int)(player.MountedCenter.X + pos.X), (int)(player.MountedCenter.Y + pos.Y), ModContent.NPCType<Torag.Torag>());
            pos = pos.RotatedBy(MathHelper.ToRadians(60));
            NPC.NewNPC(player.GetSource_ItemUse(Item), (int)(player.MountedCenter.X + pos.X), (int)(player.MountedCenter.Y + pos.Y), ModContent.NPCType<Guthan.Guthan>());
            pos = pos.RotatedBy(MathHelper.ToRadians(60));
            NPC.NewNPC(player.GetSource_ItemUse(Item), (int)(player.MountedCenter.X + pos.X), (int)(player.MountedCenter.Y + pos.Y), ModContent.NPCType<Ahrim.Ahrim>());
            pos = pos.RotatedBy(MathHelper.ToRadians(60));
            NPC.NewNPC(player.GetSource_ItemUse(Item), (int)(player.MountedCenter.X + pos.X), (int)(player.MountedCenter.Y + pos.Y), ModContent.NPCType<Karil.Karil>());
            SoundEngine.PlaySound(SoundID.Roar, player.position);
            return true;
        }
    }
}
