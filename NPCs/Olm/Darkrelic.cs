using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
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
            Item.consumable = true;
            Item.maxStack = 20;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTime = 45;
            Item.useAnimation = 45;
            Item.rare = ItemRarityID.Yellow;
        }
        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(Mod.Find<ModNPC>("Olm").Type);
        }
        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>("Olm").Type);
            SoundEngine.PlaySound(SoundID.Roar, player.position);
            return true;
        }
        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ItemID.SoulofFlight, 8);
            r.AddIngredient(ItemID.SoulofNight, 6);
            r.AddIngredient(ItemID.SoulofFright, 4);
            r.AddIngredient(ItemID.BeetleHusk, 2);
            r.AddTile(TileID.Anvils);
            r.Register();
        }
    }
}
