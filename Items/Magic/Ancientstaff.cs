using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace OldSchoolRuneScape.Items.Magic
{
    public class Ancientstaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ancient Staff");
            Tooltip.SetDefault("'For the masters of the ancient magicks' \n[c/5cdb7d:Special attack (30 second cooldown): Channels a massive volley of barrages towards your cursor]");
        }
        public override void SetDefaults()
        {
            item.damage = 34;
            item.magic = true;
            item.mana = 12;
            item.width = 52;
            item.height = 50;
            item.useTime = 12;
            item.useAnimation = 12;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 2;
            item.value = Item.sellPrice(0, 30, 0, 0);
            item.rare = 5;
            item.UseSound = SoundID.Item20;
            item.autoReuse = true;
            item.shoot = ModContent.ProjectileType<Projectiles.Shadowburst>();
            item.shootSpeed = 8;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-2, 0);
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

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2 && !player.GetModPlayer<OSRSplayer>().SpecCD)
            {
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Ancientmagick"), position);
                for (int i = 0; i < 100; i++)
                {
                    int meme = Main.rand.Next(4);
                    switch (meme)
                    {
                        case 0:
                            meme = ModContent.ProjectileType<Projectiles.Bloodburst>();
                            break;
                        case 1:
                            meme = ModContent.ProjectileType<Projectiles.Shadowburst>();
                            break;
                        case 2:
                            meme = ModContent.ProjectileType<Projectiles.Iceburst>();
                            break;
                        default:
                            meme = ModContent.ProjectileType<Projectiles.Smokeburst>();
                            break;
                    }
                    Vector2 posi = new Vector2(0, Main.rand.Next(500, 750)).RotateRandom(2*Math.PI);
                    Vector2 spede = posi;
                    spede.Normalize();
                    spede *= -8;
                    Projectile.NewProjectile(Main.MouseWorld + posi, spede, meme, damage, knockBack, player.whoAmI, 1, 0);
                }
                player.AddBuff(ModContent.BuffType<Buffs.SpecCD>(), 1800);
                return false;
            }
            Vector2 pos = new Vector2(Main.MouseWorld.X, Main.screenPosition.Y);
            Vector2 velocity = new Vector2(0, 14);
            for (int i = 0; i < 5; i++)
            {
                int meme = Main.rand.Next(4);
                switch (meme)
                {
                    case 0:
                        meme = ModContent.ProjectileType<Projectiles.Bloodburst>();
                        break;
                    case 1:
                        meme = ModContent.ProjectileType<Projectiles.Shadowburst>();
                        break;
                    case 2:
                        meme = ModContent.ProjectileType<Projectiles.Iceburst>();
                        break;
                    default:
                        meme = ModContent.ProjectileType<Projectiles.Smokeburst>();
                        break;
                }
                Vector2 spawn = pos + new Vector2(Main.rand.Next(-50, 50), Main.rand.Next(-100, 0));
                Vector2 distance = Main.MouseWorld - spawn;
                Vector2 spawnspeed = velocity * Main.rand.NextFloat(0.8f, 1.2f);
                float timeto = distance.Length() / spawnspeed.Y;
                Projectile.NewProjectile(spawn, spawnspeed, meme, damage, knockBack, player.whoAmI, 1, timeto);
            }
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Bloodbarrage");
            recipe.AddIngredient(null, "Bloodburst");
            recipe.AddIngredient(null, "Icebarrage");
            recipe.AddIngredient(null, "Iceburst");
            recipe.AddIngredient(null, "Shadowbarrage");
            recipe.AddIngredient(null, "Shadowburst");
            recipe.AddIngredient(null, "Smokebarrage");
            recipe.AddIngredient(null, "Smokeburst");
            recipe.AddIngredient(ItemID.Ectoplasm, 15);
            recipe.SetResult(this);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.AddRecipe();
        }
    }
}