using Microsoft.Xna.Framework.Audio;
using Terraria;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Sounds.Item
{
    public class Goblin : ModSound
    {
        public override SoundEffectInstance PlaySound(ref SoundEffectInstance soundInstance, float volume, float pan, SoundType type)
        {
            soundInstance.Volume = volume;
            soundInstance.Pan = pan;
            soundInstance.Volume = volume * 0.5f;
            return soundInstance;
        }
    }
}
