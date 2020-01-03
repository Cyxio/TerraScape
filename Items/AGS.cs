using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace OldSchoolRuneScape.Items
{
    public class AGS : ModItem
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Armadyl Godsword");
            Tooltip.SetDefault("A beautiful, heavy sword. \n[c/5cdb7d:Special attack (18 second cooldown): The Judgement]\n[c/5cdb7d:Strike upward dealing 375% increased damage with doubled critical strike chance and %-health damage on bosses]");
        }
        public override void SetDefaults()
        {
            item.damage = 200;
            item.melee = true;
            item.crit = 4;
            item.width = 76;
            item.height = 86;
            item.useTime = 30;
            item.useAnimation = 30;
            item.useStyle = 1;
            item.knockBack = 10f;
            item.value = Item.sellPrice(1, 0, 0, 0);
            item.rare = 8;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = false;
            item.shootSpeed = 0;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2 && player.GetModPlayer<OSRSplayer>().SpecCD == false)
            {
                item.noMelee = true;
                Projectile.NewProjectile(player.Center, new Vector2(player.direction, 0), mod.ProjectileType("AGSspec"), (int)(item.damage * 3.75f), item.knockBack, player.whoAmI, 0, 0);
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/AGSspec"), player.position);
                item.noUseGraphic = true;
                player.AddBuff(mod.BuffType("SpecCD"), 60*18);
            }
            else if (player.altFunctionUse == 2)
            {
                item.noMelee = false;
                item.UseSound = SoundID.Item1;
                item.noUseGraphic = false;
                return false;
            }
            else
            {
                item.noMelee = false;
                item.UseSound = SoundID.Item1;
                item.noUseGraphic = false;
            }
            return base.CanUseItem(player);
        }
        
    }
}

