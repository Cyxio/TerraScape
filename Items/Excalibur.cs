using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace OldSchoolRuneScape.Items
{
    public class Excalibur : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Excalibur");
            Tooltip.SetDefault("'For Camelot!' \n[c/5cdb7d:Special attack (60 second cooldown): Grants ironskin buff for 1 minute on hit]");
        }
        public override void SetDefaults()
        {
            item.damage = 40;
            item.melee = true;
            item.crit = 11;
            item.width = 34;
            item.height = 58;
            item.useTime = 24;
            item.useAnimation = 24;
            item.useStyle = 1;
            item.knockBack = 8f;
            item.value = Item.sellPrice(0, 3, 0, 0);
            item.rare = 3;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2 && player.GetModPlayer<OSRSplayer>(mod).SpecCD)
            {
                return false;
            }
            return base.CanUseItem(player);
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (player.altFunctionUse == 2)
            {
                Dust.NewDust(hitbox.TopLeft(), hitbox.Width, hitbox.Height, 135);
            }
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            if (player.altFunctionUse == 2)
            {
                player.AddBuff(BuffID.Ironskin, 3600);
                player.AddBuff(mod.BuffType<Buffs.SpecCD>(), 3600);
                player.GetModPlayer<OSRSplayer>(mod).MessageTime = 90;
                player.GetModPlayer<OSRSplayer>(mod).MessageDust = mod.GetDust<Dusts.ExcaliburMessage>();
            }
        }
        public override void UseItemHitbox(Player player, ref Rectangle hitbox, ref bool noHitbox)
        {
            hitbox.X += 24 * player.direction;
        }
    }
}