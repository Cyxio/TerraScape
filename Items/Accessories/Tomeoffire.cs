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
            Item.accessory = true;
            Item.width = 22;
            Item.height = 31;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.rare = ItemRarityID.Yellow;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Magic) += 0.1f;
            player.GetModPlayer<OSRSplayer>().TomeFire = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Torch, 99);
            recipe.AddIngredient(ItemID.LivingFireBlock, 20);
            recipe.AddIngredient(ItemID.LivingDemonFireBlock, 20);
            recipe.AddIngredient(ItemID.LivingCursedFireBlock, 20);
            recipe.AddIngredient(ItemID.LivingFrostFireBlock, 20);
            recipe.AddIngredient(ItemID.SpellTome);
            recipe.AddIngredient(ItemID.SorcererEmblem);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Torch, 99);
            recipe.AddIngredient(ItemID.LivingFireBlock, 20);
            recipe.AddIngredient(ItemID.LivingDemonFireBlock, 20);
            recipe.AddIngredient(ItemID.LivingIchorBlock, 20);
            recipe.AddIngredient(ItemID.LivingFrostFireBlock, 20);
            recipe.AddIngredient(ItemID.SpellTome);
            recipe.AddIngredient(ItemID.SorcererEmblem);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}