using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class MystichatL : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Light Mystic Hat");
            Tooltip.SetDefault("Increases maximum mana by 40");
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == Mod.Find<ModItem>("MystictopL").Type && legs.type == Mod.Find<ModItem>("MysticbottomL").Type;
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "10% increased magic damage\n25% reduced mana usage";
            player.GetDamage(DamageClass.Magic) += 0.1f;
            player.manaCost -= 0.25f;
        }

        public override void SetDefaults()
        {
            Item.width = 19;
            Item.height = 30;
            Item.value = Item.sellPrice(0, 3, 0, 0);
            Item.rare = ItemRarityID.LightRed;
            Item.defense = 7;
        }

        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawOutlines = true;
        }

        public override void UpdateEquip(Player player)
        {
            player.statManaMax2 += 40;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "Mystichat");
            recipe.AddIngredient(ItemID.SoulofLight, 14);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}