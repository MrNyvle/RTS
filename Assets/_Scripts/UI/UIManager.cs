using System;
using System.Collections.Generic;
using _ScriptableObjects.UI;
using _Scripts.Buildings;
using UnityEngine;
using _Scripts.UI.Resources;
using _Scripts.Unit;
using Unity.VisualScripting;
using UnityEngine.Serialization;

namespace _Scripts.UI
{
    public class UiManager : Singleton<UiManager>
    {
	    public IconsUI iconsUI;
	    public BuildingUI activeBuildingUI;
	    
    	[FormerlySerializedAs("_resourceBars")] public List<ResourceBar> resourceBars;
	    [FormerlySerializedAs("_eBuildingUI")] public List<BuildingUI>  eBuildingUIs;
	    public GameObject cheatScreen;

	    public void UpdateResourceBar(TownHall townHall)
	    {
		    foreach (var kvp in townHall.GetVillageResources())
		    {
			    ResourceItem resourceItem = resourceBars[townHall.id].GetResourceUI(kvp.Key);
			    resourceItem.SetItemUI(kvp.Key, kvp.Value);
		    }
	    }

	    public void BuildTimberLodger()
	    {
		    BuildManager.Instance.BuildBuilding(EBuilding.TimberLodger);
	    }

	    public void ShowUI(Building building)
	    {
		    activeBuildingUI?.gameObject.SetActive(false);
		    
		    activeBuildingUI = ShowBuildingUI(building);
		    
		    activeBuildingUI.gameObject.SetActive(true);
	    }

	    public void HideUI()
	    {
		    if (activeBuildingUI != null)
			    activeBuildingUI.gameObject.SetActive(false);
		    activeBuildingUI = null;
	    }

	    private BuildingUI GetBuildingUI(EBuilding buildingType)
	    {
		    foreach (BuildingUI eBuildingUI in eBuildingUIs)
		    {
			    if (eBuildingUI.BuildingType == buildingType)
			    {
				    return eBuildingUI;
			    }
		    }
		    return null;
	    }
	    
	    private BuildingUI ShowBuildingUI(Building building)
	    {
		    BuildingUI buildingUI = GetBuildingUI(building.EBuildingType);
		    buildingUI.FillUI(building);
		    return buildingUI;
	    }

	    public void ShowCheatScreen()
	    {
		    cheatScreen.SetActive(true);
	    }
    }
}