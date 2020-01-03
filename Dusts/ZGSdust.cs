using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace OldSchoolRuneScape.Dusts
{
    public class ZGSdust : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.velocity *= 0.001f;
            dust.noGravity = true;
            dust.noLight = false;
            dust.alpha = 150;
        }
        public override bool Update(Dust dust)
        {
            dust.GetAlpha(new Color(255, 255, 255, dust.alpha));
            dust.frame = new Rectangle(0, 0, 48, 48);
            dust.rotation = dust.velocity.ToRotation();
            dust.scale = 1f;
            dust.alpha += 5;
            float strength = (250f - dust.alpha) / 100f;
            Lighting.AddLight(dust.position, new Vector3(0.8f * strength, 0.388f * strength, 0.286f * strength));
            if (dust.alpha > 249)
            {
                dust.active = false;
            }
            return false;
        }

    }
}