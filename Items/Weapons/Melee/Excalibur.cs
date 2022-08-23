using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace OldSchoolRuneScape.Items.Weapons.Melee
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
            Item.damage = 40;
            Item.DamageType = DamageClass.Melee;
            Item.crit = 11;
            Item.width = 34;
            Item.height = 58;
            Item.useTime = 24;
            Item.useAnimation = 24;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 8f;
            Item.value = Item.sellPrice(0, 3, 0, 0);
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = true;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2 && player.GetModPlayer<OSRSplayer>().SpecCD)
            {
                return false;
            }
            return base.CanUseItem(player);
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (player.altFunctionUse == 2)
            {
                Dust.NewDust(hitbox.TopLeft(), hitbox.Width, hitbox.Height, DustID.IceTorch);
            }
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            if (player.altFunctionUse == 2)
            {
                player.AddBuff(BuffID.Ironskin, 3600);
                player.AddBuff(ModContent.BuffType<Buffs.SpecCD>(), 3600);
                player.GetModPlayer<OSRSplayer>().MessageTime = 90;
                player.GetModPlayer<OSRSplayer>().MessageDust = ModContent.GetInstance<Dusts.ExcaliburMessage>();
            }
        }
        public override void UseItemHitbox(Player player, ref Rectangle hitbox, ref bool noHitbox)
        {
            hitbox.X += 24 * player.direction;
        }
    }
}