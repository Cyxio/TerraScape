using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Localization;
using Terraria.UI;
using OldSchoolRuneScape.UI;
using ReLogic.Content;
using OldSchoolRuneScape.Items.Magic;
using static Terraria.ModLoader.ModContent;

namespace OldSchoolRuneScape
{
    public class OSRSsystem : ModSystem
    {
        public static Dictionary<int, Asset<Texture2D>> runeTextures = new Dictionary<int, Asset<Texture2D>>
        {
            { ItemType<Airrune>(), Request<Texture2D>("OldSchoolRuneScape/Items/Magic/Airrune", AssetRequestMode.ImmediateLoad) },
            { ItemType<Waterrune>(), Request<Texture2D>("OldSchoolRuneScape/Items/Magic/Waterrune", AssetRequestMode.ImmediateLoad) },
            { ItemType<Earthrune>(), Request<Texture2D>("OldSchoolRuneScape/Items/Magic/Earthrune", AssetRequestMode.ImmediateLoad) },
            { ItemType<Firerune>(), Request<Texture2D>("OldSchoolRuneScape/Items/Magic/Firerune", AssetRequestMode.ImmediateLoad) },
            { ItemType<Mindrune>(), Request<Texture2D>("OldSchoolRuneScape/Items/Magic/Mindrune", AssetRequestMode.ImmediateLoad) },
            { ItemType<Bodyrune>(), Request<Texture2D>("OldSchoolRuneScape/Items/Magic/Bodyrune", AssetRequestMode.ImmediateLoad) },
            { ItemType<Cosmicrune>(), Request<Texture2D>("OldSchoolRuneScape/Items/Magic/Cosmicrune", AssetRequestMode.ImmediateLoad) },
            { ItemType<Astralrune>(), Request<Texture2D>("OldSchoolRuneScape/Items/Magic/Astralrune", AssetRequestMode.ImmediateLoad) },
            { ItemType<Naturerune>(), Request<Texture2D>("OldSchoolRuneScape/Items/Magic/Naturerune", AssetRequestMode.ImmediateLoad) },
            { ItemType<Lawrune>(), Request<Texture2D>("OldSchoolRuneScape/Items/Magic/Lawrune", AssetRequestMode.ImmediateLoad) },
            { ItemType<Deathrune>(), Request<Texture2D>("OldSchoolRuneScape/Items/Magic/Deathrune", AssetRequestMode.ImmediateLoad) },
            { ItemType<Bloodrune>(), Request<Texture2D>("OldSchoolRuneScape/Items/Magic/Bloodrune", AssetRequestMode.ImmediateLoad) },
            { ItemType<Soulrune>(), Request<Texture2D>("OldSchoolRuneScape/Items/Magic/Soulrune", AssetRequestMode.ImmediateLoad) },
            { ItemType<Wrathrune>(), Request<Texture2D>("OldSchoolRuneScape/Items/Magic/Wrathrune", AssetRequestMode.ImmediateLoad) }
        };
    }
}
