using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.ClueScroll.ClueRewards.ForAll
{
    [AutoloadEquip(EquipType.HandsOn)]
    public class Holybook : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Holy Book");
        }
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 30;
            Item.rare = ItemRarityID.Orange;
            Item.accessory = true;
            Item.value = Item.sellPrice(0, 10);
            Item.defense = 20;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Summon) += 0.2f;
            player.GetDamage(DamageClass.Throwing) += 0.2f;
            player.GetDamage(DamageClass.Melee) += 0.2f;
            player.GetDamage(DamageClass.Ranged) += 0.2f;
            player.GetDamage(DamageClass.Magic) += 0.2f;
        }
        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ModContent.ItemType<Saradominpage1>());
            r.AddIngredient(ModContent.ItemType<Saradominpage2>());
            r.AddIngredient(ModContent.ItemType<Saradominpage3>());
            r.AddIngredient(ModContent.ItemType<Saradominpage4>());
            r.AddTile(TileID.WorkBenches);
            r.Register();
        }
    }
}
