using System.Collections.Generic;
using _ScriptableObjects.UI;
using _Scripts.Buildings;
using UnityEngine;
using _Scripts.UI.Resources;

namespace _Scripts.UI
{
    public class UiManager : Singleton<UiManager>
    {
	    public IconsUI iconsUI;
	    
    	public List<ResourceBar> _resourceBars;

	    public void UpdateResourceBar(TownHall townHall)
	    {
		    foreach (var kvp in townHall._resources)
		    {
			    ResourceItem resourceItem = _resourceBars[townHall.id].GetResourceUI(kvp.Key);
			    resourceItem.SetItemUI(kvp.Key, kvp.Value);
		    }
	    }
	}
}