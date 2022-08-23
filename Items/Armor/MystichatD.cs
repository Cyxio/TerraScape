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
            return body.type == Mod.Find<ModItem>("MystictopD").Type && legs.type == Mod.Find<ModItem>("MysticbottomD").Type;
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "20% increased magic damage\n10% reduced mana usage";
            player.GetDamage(DamageClass.Magic) += 0.2f;
            player.manaCost -= 0.1f;           
        }

        public override void SetDefaults()
        {
            Item.width = 19;
            Item.height = 30;
            Item.value = Item.sellPrice(0, 3, 0, 0);
            Item.rare = ItemRarityID.LightRed;
            Item.defense = 6;
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
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "Mystichat");
            recipe.AddIngredient(ItemID.SoulofNight, 14);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}