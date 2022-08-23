﻿using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.Audio;
using OldSchoolRuneScape.Items.GlobalItems;

namespace OldSchoolRuneScape.Items
{
    public class Lumbridgeteleport : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lumbridge Teleport");
            Tooltip.SetDefault("Teleports you to hell");
        }
        public override void SetDefaults()
        {
            Item.maxStack = 999;
            Item.width = 56;
            Item.height = 56;
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
            recipe.AddIngredient(null, "Airrune", 3);
            recipe.AddIngredient(null, "Earthrune", 1);
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
                TeleportClass.HandleTeleport(2);
                player.HeldItem.stack -= 1;
            }
        }
    }
}
