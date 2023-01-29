using UnityEngine;

public class ManagerTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Managers managers = Managers.Instance;
        InventoryManager inventory = managers.Inventory;

        if (!inventory.LoadInventory())
        {
            inventory.SaveInventory();
        }

        if (!inventory.LoadInventory())
        {
            Debug.Log("Error");
        }

    }

}
