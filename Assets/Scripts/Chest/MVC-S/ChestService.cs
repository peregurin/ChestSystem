using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestService : GenericMonoSingleton<ChestService>
{
    private List<ChestController> chestControllers;
    [SerializeField] private ChestSO[] chestsSO;
    [SerializeField] private Button generateChestsButton;

    private void Start(){
        chestControllers = new List<ChestController>();
        generateChestsButton.onClick.AddListener(OnGenerateChestButtonClicked);
    }

    private void OnGenerateChestButtonClicked(){
        if(SlotsService.Instance.IsAnySlotEmpty){
            int randomSelection = Random.Range(0, chestsSO.Length);
            var chestController = new ChestController(chestsSO[randomSelection]);
            SlotsService.Instance.PopulateSlot(chestController.ChestView);
        }else{
            Debug.Log("Chest slots are full, cannot create more");
        }
    }
}
