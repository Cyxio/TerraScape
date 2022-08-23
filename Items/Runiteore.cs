using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items
{
    public class Runiteore : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Runite Ore");
        }
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = 999;
            Item.useTurn = true;
            Item.rare = ItemRarityID.Orange;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.value = 5000;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.createTile = Mod.Find<ModTile>("Runiteore").Type;
        }
    }
}