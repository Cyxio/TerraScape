using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using OldSchoolRuneScape.Projectiles;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using static Terraria.ModLoader.ModContent;
using Terraria.Audio;
using OldSchoolRuneScape.Items.GlobalItems;

namespace OldSchoolRuneScape.Items.Magic
{
    public class VarrockTeleSpell : Spell
    {
        public override int Row()
        {
            return 2;
        }
        public override int Column()
        {
            return 1;
        }
        public override void AdditionalDefaults()
        {
            Item.useTime = 120;
            Item.useAnimation = 120;
            Item.UseSound = new SoundStyle("OldSchoolRuneScape/Sounds/Item/TeleportSpell");
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.consumable = false;
            Item.rare = ItemRarityID.Blue;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Varrock Teleport");
        }
        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            if (player.itemAnimation < 67 && player.itemAnimation > 2)
            {
                player.GetModPlayer<OSRSplayer>().HidePlayer = true;
                if (player.itemAnimation % 6 == 0)
                {
                    for (int i = 0; i < 36; i++)
                    {
                        Dust dust;
                        Vector2 position = player.Center + ((player.itemAnimation / 2) * Vector2.UnitX.RotatedBy(MathHelper.TwoPi * (i / 36f)));
                        dust = Terraria.Dust.NewDustPerfect(position, 86, new Vector2(0f, 0f), 0, new Color(255, 255, 255), 0.5f);
                        dust.noGravity = true;
                        dust.fadeIn = 1.2f;
                    }
                }
            }
            if (player.itemAnimation == 2)
            {
                TeleportClass.HandleTeleport(3);
                player.HeldItem.stack -= 1;
            }
        }
    }
    public class CamelotTeleSpell : Spell
    {
        public override int Row()
        {
            return 3;
        }
        public override int Column()
        {
            return 5;
        }
        public override void AdditionalDefaults()
        {
            Item.useTime = 120;
            Item.useAnimation = 120;
            Item.UseSound = new SoundStyle("OldSchoolRuneScape/Sounds/Item/TeleportSpell");
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.consumable = false;
            Item.rare = ItemRarityID.Blue;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Camelot Teleport");
        }
        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            if (player.itemAnimation < 67 && player.itemAnimation > 2)
            {
                player.GetModPlayer<OSRSplayer>().HidePlayer = true;
                if (player.itemAnimation % 6 == 0)
                {
                    for (int i = 0; i < 36; i++)
                    {
                        Dust dust;
                        Vector2 position = player.Center + ((player.itemAnimation / 2) * Vector2.UnitX.RotatedBy(MathHelper.TwoPi * (i / 36f)));
                        dust = Terraria.Dust.NewDustPerfect(position, 86, new Vector2(0f, 0f), 0, new Color(255, 255, 255), 0.5f);
                        dust.noGravity = true;
                        dust.fadeIn = 1.2f;
                    }
                }
            }
            if (player.itemAnimation == 2)
            {
                TeleportClass.HandleTeleport(1);
                player.HeldItem.stack -= 1;
            }
        }
    }
    public class FaladorTeleSpell : Spell
    {
        public override int Row()
        {
            return 3;
        }
        public override int Column()
        {
            return 0;
        }
        public override void AdditionalDefaults()
        {
            Item.useTime = 120;
            Item.useAnimation = 120;
            Item.UseSound = new SoundStyle("OldSchoolRuneScape/Sounds/Item/TeleportSpell");
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.consumable = false;
            Item.rare = ItemRarityID.Blue;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Falador Teleport");
        }
        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            if (player.itemAnimation < 67 && player.itemAnimation > 2)
            {
                player.GetModPlayer<OSRSplayer>().HidePlayer = true;
                if (player.itemAnimation % 6 == 0)
                {
                    for (int i = 0; i < 36; i++)
                    {
                        Dust dust;
                        Vector2 position = player.Center + ((player.itemAnimation / 2) * Vector2.UnitX.RotatedBy(MathHelper.TwoPi * (i / 36f)));
                        dust = Terraria.Dust.NewDustPerfect(position, 86, new Vector2(0f, 0f), 0, new Color(255, 255, 255), 0.5f);
                        dust.noGravity = true;
                        dust.fadeIn = 1.2f;
                    }
                }
            }
            if (player.itemAnimation == 2)
            {
                TeleportClass.HandleTeleport(0);
                player.HeldItem.stack -= 1;
            }
        }
    }
    public class LumbridgeTeleSpell : Spell
    {
        public override int Row()
        {
            return 2;
        }
        public override int Column()
        {
            return 4;
        }
        public override void AdditionalDefaults()
        {
            Item.useTime = 120;
            Item.useAnimation = 120;
            Item.UseSound = new SoundStyle("OldSchoolRuneScape/Sounds/Item/TeleportSpell");
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.consumable = false;
            Item.rare = ItemRarityID.Blue;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lumbridge Teleport");
        }
        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            if (player.itemAnimation < 67 && player.itemAnimation > 2)
            {
                player.GetModPlayer<OSRSplayer>().HidePlayer = true;
                if (player.itemAnimation % 6 == 0)
                {
                    for (int i = 0; i < 36; i++)
                    {
                        Dust dust;
                        Vector2 position = player.Center + ((player.itemAnimation / 2) * Vector2.UnitX.RotatedBy(MathHelper.TwoPi * (i / 36f)));
                        dust = Terraria.Dust.NewDustPerfect(position, 86, new Vector2(0f, 0f), 0, new Color(255, 255, 255), 0.5f);
                        dust.noGravity = true;
                        dust.fadeIn = 1.2f;
                    }
                }
            }
            if (player.itemAnimation == 2)
            {
                TeleportClass.HandleTeleport(2);
                player.HeldItem.stack -= 1;
            }
        }
    }
}
