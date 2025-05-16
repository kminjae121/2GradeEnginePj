using _01.Scipt.Player.Player;
using Blade.Entities;
using GondrLib.Dependencies;
using UnityEngine;

namespace Blade.Managers
{
    [DefaultExecutionOrder(-1)]
    public class PlayerManager : MonoBehaviour
    {
        [Inject, SerializeField] private Player player;
        [SerializeField] private EntityFinderSO playerFinder;

        private void Awake()
        {
            playerFinder.SetTarget(player);
        }
    }
}