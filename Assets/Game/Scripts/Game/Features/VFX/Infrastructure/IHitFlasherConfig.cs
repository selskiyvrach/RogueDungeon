using UnityEngine;

namespace Game.Features.VFX.Infrastructure
{
    public interface IHitFlasherConfig
    {
        float FlashInDuration { get;}
        float FlashOutDuration { get;}
        Vector3 GetFlashLocalPosition(HitFlashPosition pos);
        float GetFlashSize(HitFlashPosition pos);
        float GetTravelDistance(HitFlashPosition position);
    }
}