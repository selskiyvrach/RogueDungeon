using UnityEngine;

namespace Game.Features.Player.App.UseCases.Instance
{
    public interface IPlayerPovCameraPoint
    {
        Vector3 Position { get; }
        Vector3 Rotation { get; }
    }
}