using Terraria.Audio;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace OldSchoolRuneScape.Items.Magic
{
    public class RuneEssence : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rune Essence");
            Tooltip.SetDefault("An uncharged rune stone");
        }
        public override void SetDefaults()
        {
            Item.maxStack = 999;
            Item.width = 28;
            Item.height = 28;
            Item.value = 10;
            Item.useTime = 121;
            Item.useAnimation = 121;
            Item.useStyle = ItemUseStyleID.HoldUp;
        }
        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            IDictionary<int, int> dict = new Dictionary<int, int>();
            dict.Add(ModContent.TileType<Tiles.AirAltar>(), ModContent.ItemType<Airrune>());
            dict.Add(ModContent.TileType<Tiles.MindAltar>(), ModContent.ItemType<Mindrune>());
            dict.Add(ModContent.TileType<Tiles.WaterAltar>(), ModContent.ItemType<Waterrune>());
            dict.Add(ModContent.TileType<Tiles.EarthAltar>(), ModContent.ItemType<Earthrune>());
            dict.Add(ModContent.TileType<Tiles.FireAltar>(), ModContent.ItemType<Firerune>());
            dict.Add(ModContent.TileType<Tiles.BodyAltar>(), ModContent.ItemType<Bodyrune>());
            dict.Add(ModContent.TileType<Tiles.CosmicAltar>(), ModContent.ItemType<Cosmicrune>());
            dict.Add(ModContent.TileType<Tiles.ChaosAltar>(), ModContent.ItemType<Chaosrune>());
            dict.Add(ModContent.TileType<Tiles.NatureAltar>(), ModContent.ItemType<Naturerune>());
            dict.Add(ModContent.TileType<Tiles.LawAltar>(), ModContent.ItemType<Lawrune>());
            dict.Add(ModContent.TileType<Tiles.BloodAltar>(), ModContent.ItemType<Bloodrune>());
            dict.Add(ModContent.TileType<Tiles.SoulAltar>(), ModContent.ItemType<Soulrune>());
            dict.Add(ModContent.TileType<Tiles.DeathAltar>(), ModContent.ItemType<Deathrune>());
            dict.Add(ModContent.TileType<Tiles.WrathAltar>(), ModContent.ItemType<Wrathrune>());
            dict.Add(ModContent.TileType<Tiles.AstralAltar>(), ModContent.ItemType<Astralrune>());

            Point coords = Main.MouseWorld.ToTileCoordinates();
            if (Main.netMode != NetmodeID.MultiplayerClient
                && dict.ContainsKey(Main.tile[coords.X, coords.Y].TileType)
                && player.inventory[player.selectedItem].type == ModContent.ItemType<RuneEssence>())
            {
                if (player.itemAnimation >= 120)
                {
                    SoundEngine.PlaySound(new SoundStyle("OldSchoolRuneScape/Sounds/Item/Runecraft"), player.position);
                }
                if (player.itemAnimation % 30 == 0 && player.itemAnimation >= 60)
                {
                    for (int i = 0; i < 12; i++)
                    {
                        Dust dust;
                        Vector2 position = player.Center + new Vector2(15 * player.direction, -32);
                        dust = Main.dust[Terraria.Dust.NewDust(position, 4, 4, DustID.CrystalPulse2, 0f, -4.473684f, 0, new Color(255, 255, 255), 0.5921053f)];
                    }
                }
                if (player.itemAnimation < 5)
                {
                    int count = player.inventory[player.selectedItem].stack;
                    player.inventory[player.selectedItem].stack = 0;
                    player.QuickSpawnItem(player.GetSource_ItemUse(Item), dict[Main.tile[coords.X, coords.Y].TileType], count);
                }
                if (Main.mouseLeftRelease && player.itemAnimation < 100)
                {
                    player.itemAnimation = 0;
                }
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(10);
            recipe.AddIngredient(ItemID.FallenStar, 1);
            recipe.AddIngredient(ItemID.StoneBlock, 5);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
            recipe = CreateRecipe(5);
            recipe.AddIngredient(ItemID.StoneBlock);
            recipe.AddTile(TileID.AdamantiteForge);
            recipe.Register();
        }
    }
}
