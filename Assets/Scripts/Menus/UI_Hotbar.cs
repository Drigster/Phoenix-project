using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Hotbar : UI_Inventory
{
    void Start()
    {
        Player player = FindFirstObjectByType<Player>();
        if (player != null)
        {
            Set(player.Hotbar);
        }
    }
}
