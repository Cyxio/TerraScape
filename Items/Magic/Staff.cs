using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Terraria.ModLoader.IO;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework.Graphics;

namespace OldSchoolRuneScape.Items.Magic
{
    public class SpellStaff : ModItem
    {
        // TODO: rune consumption
        public SpellCopy? BoundSpell { get; set; } = null;

        public void SelectSpell(SpellCopy spell)
        {
            BoundSpell = spell;
            var item = spell.Item;
            Item.shootSpeed = item.shootSpeed;
            Item.shoot = item.shoot;
            Item.damage = item.damage;
            Item.knockBack = item.knockBack;
            Item.UseSound = item.UseSound;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
        }

        public override void SaveData(TagCompound tag)
        {
            tag["BoundSpellType"] = BoundSpell?.Item.type ?? 0;
        }
        public override void LoadData(TagCompound tag)
        {
            int spellType = tag.Get<int>("BoundSpellType");
            if (spellType != 0)
            {
                SelectSpell((SpellCopy)GetModItem(spellType));
            }
        }
        public override bool CanRightClick()
        {
            return true;
        }
        public override void RightClick(Player player)
        {
            Item.stack += 1;
            BoundSpell = null;
            Item.shoot = ProjectileID.None;
            Item.damage = 5;
            Item.knockBack = 1f;
            Item.UseSound = SoundID.Item1;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noMelee = false;
        }
        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            if (BoundSpell != null) 
            {
                Texture2D t = UI.SpellbookUI.spellbookActiveTexture.Value;
                int x = 13 + 26 * BoundSpell.Column;
                int y = 8 + 24 * BoundSpell.Row;
                spriteBatch.Draw(t, position + new Vector2(scale * 20f), new Rectangle(x, y, 20, 20), drawColor, 0, origin, scale * 2f, SpriteEffects.None, 0f);
            }
            base.PostDrawInInventory(spriteBatch, position, frame, drawColor, itemColor, origin, scale);
        }
    }
    public class Staff : SpellStaff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Staff");
            Tooltip.SetDefault("Can be used to autocast spells from the Standard Spellbook");
        }
        public override void SetDefaults()
        {
            Item.damage = 5;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 8;
            Item.width = 42;
            Item.height = 40;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noMelee = false;
            Item.knockBack = 1f;
            Item.value = Item.sellPrice(0, 0, 5, 0);
            Item.rare = ItemRarityID.White;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.None;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-2, 0);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            return BoundSpell != null;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddRecipeGroup(RecipeGroupID.Wood, 12)
                .AddIngredient<RuneEssence>(50)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}