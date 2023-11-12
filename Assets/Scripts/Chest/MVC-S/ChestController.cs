using System.Threading.Tasks;
using UnityEngine;

public class ChestController
{
    public ChestModel ChestModel { get; }
    public ChestView ChestView { get; }

    public ChestController(ChestSO chestData){
        ChestModel = new ChestModel(chestData);
        ChestView = GameObject.Instantiate<ChestView>(chestData.chestPrefab);
        ChestView.SetController(this);
        ChestView.UpdateView(ChestModel);
    }

    public void OnChestButtonClicked(){ 
        var currentState = ChestModel.ChestState;
        if(currentState == ChestState.Locked){
            _ = StartUnlockingChest(ChestModel.TimeToUnlock);
        }else if(currentState == ChestState.Unlocking){
            UIService.Instance.ShowOpenChestNowPopup(OpenChestPopupCallback);
        }
        else if(currentState == ChestState.Unlocked){
            CollectChestReward();
        }
    }

    private void OpenChestPopupCallback(bool useGems){
        var gemsCost = CalculateGemCost(ChestView.RemainingTime);
        PlayerService.Instance.UseGems(gemsCost);
        // change state and stop the awaited task
    }

    private async Task StartUnlockingChest(float timeToUnlock){
        ChangeChestState(ChestState.Unlocking);
        Debug.Log($"{((int)timeToUnlock)*1000}");
        await Task.Delay(((int)timeToUnlock)*1000);
        ChangeChestState(ChestState.Unlocked);
    }

    private void CollectChestReward(){
        PlayerService.Instance.CollectChestReward(ChestModel.ChestReward);
        ChangeChestState(ChestState.Collected);
    }

    private void ChangeChestState(ChestState chestState){
        ChestModel.UpdateChestState(chestState);
        ChestView.UpdateView(ChestModel);
    }

    public int CalculateGemCost(float remainingTime)
    {
        float minutes = remainingTime / 60;
        return Mathf.CeilToInt(minutes / 10);
    }
}