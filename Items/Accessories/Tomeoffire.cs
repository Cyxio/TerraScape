using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;

namespace OldSchoolRuneScape.Items.Accessories
{
    public class Tomeoffire : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tome of Fire");
            Tooltip.SetDefault("10% increased magic damage\nImbues your magical attacks with various flames");
        }
        public override void SetDefaults()
        {
            item.accessory = true;
            item.width = 22;
            item.height = 31;
            item.value = Item.sellPrice(0, 10, 0, 0);
            item.rare = 8;
        }
        public override void UpdateEquip(Player player)
        {
            player.magicDamage += 0.1f;
            player.GetModPlayer<OSRSplayer>().TomeFire = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Torch, 99);
            recipe.AddIngredient(ItemID.LivingFireBlock, 20);
            recipe.AddIngredient(ItemID.LivingDemonFireBlock, 20);
            recipe.AddIngredient(ItemID.LivingCursedFireBlock, 20);
            recipe.AddIngredient(ItemID.LivingFrostFireBlock, 20);
            recipe.AddIngredient(ItemID.SpellTome);
            recipe.AddIngredient(ItemID.SorcererEmblem);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Torch, 99);
            recipe.AddIngredient(ItemID.LivingFireBlock, 20);
            recipe.AddIngredient(ItemID.LivingDemonFireBlock, 20);
            recipe.AddIngredient(ItemID.LivingIchorBlock, 20);
            recipe.AddIngredient(ItemID.LivingFrostFireBlock, 20);
            recipe.AddIngredient(ItemID.SpellTome);
            recipe.AddIngredient(ItemID.SorcererEmblem);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}