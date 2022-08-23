using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
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
            Item.damage = 34;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 12;
            Item.width = 52;
            Item.height = 50;
            Item.useTime = 12;
            Item.useAnimation = 12;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 2;
            Item.value = Item.sellPrice(0, 30, 0, 0);
            Item.rare = ItemRarityID.Pink;
            Item.UseSound = SoundID.Item20;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Shadowburst>();
            Item.shootSpeed = 8;
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

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2 && !player.GetModPlayer<OSRSplayer>().SpecCD)
            {
                SoundEngine.PlaySound(new SoundStyle("OldSchoolRuneScape/Sounds/Item/Ancientmagick"), position);
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
                    Projectile.NewProjectile(source, Main.MouseWorld + posi, spede, meme, damage, knockback, player.whoAmI, 1, 0);
                }
                player.AddBuff(ModContent.BuffType<Buffs.SpecCD>(), 1800);
                return false;
            }
            Vector2 pos = new Vector2(Main.MouseWorld.X, Main.screenPosition.Y);
            Vector2 vel = new Vector2(0, 14);
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
                Vector2 spawnspeed = vel * Main.rand.NextFloat(0.8f, 1.2f);
                float timeto = distance.Length() / spawnspeed.Y;
                Projectile.NewProjectile(source, spawn, spawnspeed, meme, damage, knockback, player.whoAmI, 1, timeto);
            }
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "Bloodbarrage");
            recipe.AddIngredient(null, "Bloodburst");
            recipe.AddIngredient(null, "Icebarrage");
            recipe.AddIngredient(null, "Iceburst");
            recipe.AddIngredient(null, "Shadowbarrage");
            recipe.AddIngredient(null, "Shadowburst");
            recipe.AddIngredient(null, "Smokebarrage");
            recipe.AddIngredient(null, "Smokeburst");
            recipe.AddIngredient(ItemID.Ectoplasm, 15);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}