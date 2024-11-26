using Common.UnityUtils;
using UnityEngine;

namespace RogueDungeon.Game
{
    public class GameplayRootObject : MonoBehaviour, IRootObject<Player.Player>
    {
        [SerializeField] private GameObject _playerRoot;
        GameObject IRootObject<Player.Player>.GameObject => _playerRoot;
    }
}