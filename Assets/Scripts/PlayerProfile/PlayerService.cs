public class PlayerService : GenericMonoSingleton<PlayerService>
{
    private int coinsBalance;
    private int gemsBalance;

    public int GemsBalance { 
        get => gemsBalance;
        private set {
            gemsBalance = value;
            UIService.Instance.UpdateGemsBalance(gemsBalance);
        } 
    }
    public int CoinsBalance { 
        get => coinsBalance;
        private set {
            coinsBalance = value;
            UIService.Instance.UpdateCoinsBalance(coinsBalance);   
        } 
    }

    public void UseGems(int gemsUsed){
        GemsBalance -= gemsUsed;
    }

    public void Start(){
        CoinsBalance = 1000;
        GemsBalance = 20;
    }

    public void CollectChestReward(ChestReward chestReward){
        CoinsBalance += chestReward.coins;
        GemsBalance += chestReward.gems;
    }
}