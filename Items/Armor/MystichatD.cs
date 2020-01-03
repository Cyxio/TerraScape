using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class MystichatD : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dark Mystic Hat");
            Tooltip.SetDefault("Increases maximum mana by 20");
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("MystictopD") && legs.type == mod.ItemType("MysticbottomD");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "20% increased magic damage\n10% reduced mana usage";
            player.magicDamage += 0.2f;
            player.manaCost -= 0.1f;           
        }

        public override void SetDefaults()
        {
            item.width = 19;
            item.height = 30;
            item.value = Item.sellPrice(0, 3, 0, 0);
            item.rare = 4;
            item.defense = 6;
        }

        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawOutlines = true;
        }

        public override void UpdateEquip(Player player)
        {
            player.statManaMax2 += 20;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Mystichat");
            recipe.AddIngredient(ItemID.SoulofNight, 14);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}