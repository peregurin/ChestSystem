using UnityEngine;

public struct ChestReward{
    public int coins;
    public int gems;
}

public class ChestModel
{
    private Vector2Int coinsRange;
    private Vector2Int gemsRange;

    private ChestReward chestReward;

    public ChestState ChestState { get; private set; }

    public ChestType ChestType { get; }
    public Vector2Int CoinsRange => coinsRange;
    public Vector2Int GemsRange => gemsRange;
    public float TimeToUnlock { get; }
    public Sprite ChestSprite { get; }
    public Color BgColor { get; }
    public ChestReward ChestReward => chestReward;

    public ChestModel(ChestSO chestData){
        ChestState = ChestState.Locked;
        ChestType = chestData.chestType;
        coinsRange = chestData.coinsRange;
        gemsRange = chestData.gemsRange;
        TimeToUnlock = chestData.timeToUnlock;
        ChestSprite = chestData.chestSprite;
        BgColor = chestData.bgColor;
        chestReward = new ChestReward{
            coins = Random.Range(coinsRange.x, coinsRange.y),
            gems = Random.Range(gemsRange.x, gemsRange.y)
        };
    }

    public void UpdateChestState(ChestState chestState){
        ChestState = chestState;
    }
}
