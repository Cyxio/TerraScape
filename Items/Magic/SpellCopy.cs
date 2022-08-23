using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using OldSchoolRuneScape.Items;
using OldSchoolRuneScape.Projectiles;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using static Terraria.ModLoader.ModContent;
using Terraria.Audio;
using Terraria.GameContent;
using ReLogic.Content;
using ReLogic.Graphics;

namespace OldSchoolRuneScape.Items.Magic
{
    public abstract class SpellCopy : ModItem
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public Dictionary<int, int> Runecost { get; set; }
        public abstract void AdditionalDefaults();
        public abstract bool Unlocked();
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.value = 0;
            Item.autoReuse = false;
            Item.DamageType = DamageClass.Magic;
            AdditionalDefaults();
        }
        public bool PlayerHasRunes(Player player)
        {
            var result = true;
            foreach (KeyValuePair<int, int> pair in Runecost)
            {
                if (player.CountItem(pair.Key) < pair.Value)
                {
                    result = false;
                }
            }
            return result;
        }
        public bool ConsumeRunes(Player player) 
        {
            if (PlayerHasRunes(player))
            {
                foreach (KeyValuePair<int, int> pair in Runecost)
                {
                    for (int i = 0; i < player.inventory.Length; i++)
                    {
                        if (player.inventory[i].type == pair.Key)
                        {
                            player.inventory[i].stack -= pair.Value;
                        }
                    }
                }
                return true;
            }
            return false;
        }
        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            return true;
        }
        public override void UpdateInventory(Player player)
        {
            Item.TurnToAir();
        }
        public override string Texture => "OldSchoolRuneScape/emptyImage";
        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Item.active = false;
            return false;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            var str = "";
            foreach (KeyValuePair<int, int> pair in Runecost)
            {
                str += $"[i/s{pair.Value}:{pair.Key}]";
            }
            var line = new TooltipLine(Mod, "RunecostIcons", str);
            tooltips.Add(line);
            var line2 = new TooltipLine(Mod, "RunecostNumbers", str);
            tooltips.Add(line2);
        }
        private void DrawRuneIcon(DrawableTooltipLine line, int xOffset, int id, int amount)
        {
            Main.spriteBatch.Draw(OSRSsystem.runeTextures[id].Value, new Vector2(line.X + xOffset, line.Y), Color.White);
        }        
        private void DrawRuneText(DrawableTooltipLine line, int xOffset, int id, int amount)
        {
            var font = FontAssets.ItemStack.Value;
            var playerAmount = Main.LocalPlayer.CountItem(id, 999);
            string text = $"{playerAmount}/{amount}";
            Vector2 size = font.MeasureString(text);
            Color color = playerAmount >= amount ? Color.Green : Color.Red;
            Main.spriteBatch.DrawString(font, text, new Vector2(line.X + xOffset + 14 - (size.X / 2), line.Y), color);
        }
        public override bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
        {
            var allowedLines = new List<string> { "ItemName", "Damage", "CritChance", "Speed", "Knockback" };
            if (line.Name == "RunecostIcons")
            {
                var xOffset = 0;
                foreach (KeyValuePair<int, int> pair in Runecost)
                {
                    DrawRuneIcon(line, xOffset, pair.Key, pair.Value);
                    xOffset += 56;
                }
            }            
            if (line.Name == "RunecostNumbers")
            {
                var xOffset = 0;
                foreach (KeyValuePair<int, int> pair in Runecost)
                {
                    DrawRuneText(line, xOffset, pair.Key, pair.Value);
                    xOffset += 56;
                }
            }
            if (allowedLines.Contains(line.Name) || line.Name.StartsWith("Tooltip"))
            {
                return base.PreDrawTooltipLine(line, ref yOffset);
            }
            return false;
        }
    }
    // Spells start here
    public class WindStrike : SpellCopy
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wind Strike");
            Tooltip.SetDefault("Deals more damage to airborne enemies");
        }
        public override bool Unlocked()
        {
            return true;
        }
        public override void AdditionalDefaults()
        {
            Item.damage = 8;
            Item.knockBack = 4f;
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = new SoundStyle("OldSchoolRuneScape/Sounds/Item/WindStrikeCast");
            Item.shoot = Mod.Find<ModProjectile>("WindstrikeP").Type;
            Item.shootSpeed = 12;
            Row = 0;
            Column = 1;
            Runecost = new Dictionary<int, int>
            {
                {ItemType<Airrune>(), 1},
                {ItemType<Mindrune>(), 1},
            };
        }
    }
    public class WaterStrike : SpellCopy
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Water Strike");
            Tooltip.SetDefault("Travels faster");
        }
        public override bool Unlocked()
        {
            return true;
        }
        public override void AdditionalDefaults()
        {
            Item.damage = 8;
            Item.knockBack = 4f;
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = new SoundStyle("OldSchoolRuneScape/Sounds/Item/WaterStrikeCast");
            Item.shoot = Mod.Find<ModProjectile>("WaterstrikeP").Type;
            Item.shootSpeed = 18;
            Row = 0;
            Column = 4;
            Runecost = new Dictionary<int, int>
            {
                {ItemType<Airrune>(), 1},
                {ItemType<Waterrune>(), 1},
                {ItemType<Mindrune>(), 1},
            };
        }
    }
    public class EarthStrike : SpellCopy
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Earth Strike");
            Tooltip.SetDefault("Deals more damage to grounded enemies");
        }
        public override bool Unlocked()
        {
            return true;
        }
        public override void AdditionalDefaults()
        {
            Item.damage = 8;
            Item.knockBack = 4f;
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = new SoundStyle("OldSchoolRuneScape/Sounds/Item/EarthStrikeCast");
            Item.shoot = Mod.Find<ModProjectile>("EarthstrikeP").Type;
            Item.shootSpeed = 12;
            Row = 0;
            Column = 6;
            Runecost = new Dictionary<int, int>
            {
                {ItemType<Airrune>(), 1},
                {ItemType<Earthrune>(), 2},
                {ItemType<Mindrune>(), 1},
            };
        }
    }
    public class FireStrike : SpellCopy
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fire Strike");
            Tooltip.SetDefault("Might set the target on fire");
        }
        public override bool Unlocked()
        {
            return true;
        }
        public override void AdditionalDefaults()
        {
            Item.damage = 8;
            Item.knockBack = 4f;
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = new SoundStyle("OldSchoolRuneScape/Sounds/Item/FireStrikeCast");
            Item.shoot = Mod.Find<ModProjectile>("FirestrikeP").Type;
            Item.shootSpeed = 12;
            Row = 1;
            Column = 1;
            Runecost = new Dictionary<int, int>
            {
                {ItemType<Airrune>(), 2},
                {ItemType<Firerune>(), 3},
                {ItemType<Mindrune>(), 1},
            };
        }
    }
}
