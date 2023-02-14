using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int[] playerResourceList;
    public int[] containerResourceList;
    public int dayCount;
    public int playerCurrentLife;

    public GameData()
    {
        playerResourceList = ResourceManage.resourceManage.GetInventoryList();
        containerResourceList = Container.container.containerInventoryList;
        dayCount = DayController.dayController.dayCount;
        playerCurrentLife = PlayerStatus.playerObj.GetComponent<PlayerLife>().GetCurrentLife();
    }
}
