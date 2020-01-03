using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.NPCs.Olm
{
    public class Darkrelic : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dark Relic");
            Tooltip.SetDefault("The gateway to the Chambers of Xeric... \nSummons the Great Olm");
        }
        public override void SetDefaults()
        {
            item.consumable = true;
            item.maxStack = 20;
            item.useStyle = 4;
            item.useTime = 45;
            item.useAnimation = 45;
            item.rare = 8;
        }
        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(mod.NPCType("Olm"));
        }
        public override bool UseItem(Player player)
        {
            NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("Olm"));
            Main.PlaySound(SoundID.Roar, player.position, 0);
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe r = new ModRecipe(mod);
            r.AddIngredient(ItemID.SoulofFlight, 8);
            r.AddIngredient(ItemID.SoulofNight, 6);
            r.AddIngredient(ItemID.SoulofFright, 4);
            r.AddIngredient(ItemID.BeetleHusk, 2);
            r.AddTile(TileID.Anvils);
            r.SetResult(this);
            r.AddRecipe();
        }
    }
}
