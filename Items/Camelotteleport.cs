using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.Audio;
using OldSchoolRuneScape.Items.GlobalItems;

namespace OldSchoolRuneScape.Items
{
    public class Camelotteleport : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Camelot Teleport");
            Tooltip.SetDefault("Teleports you to the ocean");
        }
        public override void SetDefaults()
        {
            Item.maxStack = 999;
            Item.width = 28;
            Item.height = 26;
            Item.consumable = true;
            Item.noUseGraphic = true;
            Item.UseSound = new SoundStyle("OldSchoolRuneScape/Sounds/Item/Teleport");
            Item.useAnimation = 114;
            Item.useTime = 114;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.rare = ItemRarityID.Blue;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "Airrune", 5);
            recipe.AddIngredient(null, "Lawrune", 1);
            recipe.AddIngredient(ItemID.ClayBlock);
            recipe.AddTile(null, "Lectern");
            recipe.Register();
        }
        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            Dust.NewDust(player.BottomLeft, 26, 0, DustID.ManaRegeneration, 0, -6f);
            if (player.itemAnimation == 2)
            {
                TeleportClass.HandleTeleport(1);
                player.HeldItem.stack -= 1;
            }
        }
    }
}
