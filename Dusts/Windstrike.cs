using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace OldSchoolRuneScape.Dusts
{
    public class Windstrike : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.noGravity = true;
            dust.noLight = true;
            dust.alpha = 0;
        }

        public override bool Update(Dust dust)
        {
            dust.position += dust.velocity;
            dust.rotation += dust.velocity.X * 0.15f;
            dust.velocity *= 0.97f;
            dust.scale *= 0.97f;
            Vector3 light = dust.color.ToVector3() * 1f * dust.scale;
            Lighting.AddLight(dust.position, light);
            if (dust.scale < 0.4f)
            {
                dust.active = false;
            }
            return false;
        }
    }
}