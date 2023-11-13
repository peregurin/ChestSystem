using Chest.Types;
using Chest.View;
using UnityEngine;

namespace Chest.SO{
    [CreateAssetMenu(fileName = "Chest", menuName = "Chest System/NewChest")]
    public class ChestSO : ScriptableObject
    {
        public ChestType chestType;
        public Vector2Int coinsRange;
        public Vector2Int gemsRange;
        public float timeToUnlock;
        public ChestView chestPrefab;
        public Sprite chestSprite;
        public Color bgColor; // this can also be replaced with a sprite
    }
}