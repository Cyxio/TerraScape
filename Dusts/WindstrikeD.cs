using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace OldSchoolRuneScape.Dusts
{
    public class WindstrikeD : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.velocity *= 0f;
            dust.noGravity = true;
            dust.noLight = true;
            dust.alpha = 0;
            dust.frame = new Rectangle(0, 0, 38, 38);
            dust.scale = 0.2f;
            dust.alpha = 50;
        }
        private const float TimeToLive = 16f;
        public override bool Update(Dust dust)
        {
            float changePerTick = 0.8f / TimeToLive;
            dust.scale += changePerTick;
            float sizeChange = dust.frame.Width * changePerTick;
            dust.position -= new Vector2(sizeChange) * dust.scale / 2f;
            Vector3 light = Color.White.ToVector3() * 0.5f * dust.scale;
            Lighting.AddLight(dust.position, light);
            if (dust.scale > 1f)
            {
                dust.active = false;
            }
            return false;
        }
    }
}