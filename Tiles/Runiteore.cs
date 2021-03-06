﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace OldSchoolRuneScape.Tiles
{
    public class Runiteore : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = false;
            TileID.Sets.Ore[Type] = true;
            Main.tileSpelunker[Type] = true; // The tile will be affected by spelunker highlighting
            Main.tileValue[Type] = 410; // Metal Detector value, see https://terraria.gamepedia.com/Metal_Detector
            this.minPick = 100;
            this.soundType = 21;
            dustType = mod.DustType("Runedust");
            drop = mod.ItemType("Runiteore");
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Runite ore");
            AddMapEntry(new Color(81, 108, 119), name);
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 3 : 10;
        }


    }
}