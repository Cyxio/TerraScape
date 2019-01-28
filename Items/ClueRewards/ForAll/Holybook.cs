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
            item.width = 32;
            item.height = 30;
            item.rare = 3;
            item.accessory = true;
            item.value = Item.sellPrice(0, 10);
            item.defense = 20;
        }
        public override void UpdateEquip(Player player)
        {
            player.minionDamage += 0.2f;
            player.thrownDamage += 0.2f;
            player.meleeDamage += 0.2f;
            player.rangedDamage += 0.2f;
            player.magicDamage += 0.2f;
        }
        public override void AddRecipes()
        {
            ModRecipe r = new ModRecipe(mod);
            r.AddIngredient(mod.ItemType<Saradominpage1>());
            r.AddIngredient(mod.ItemType<Saradominpage2>());
            r.AddIngredient(mod.ItemType<Saradominpage3>());
            r.AddIngredient(mod.ItemType<Saradominpage4>());
            r.AddTile(TileID.WorkBenches);
            r.SetResult(this);
            r.AddRecipe();
        }
    }
}
