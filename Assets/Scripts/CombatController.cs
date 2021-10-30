using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    PlayerTriggers combatControls;
    [SerializeField]
    CinemachineVirtualCamera freeLookCam;
    public Item[] inventory = new Item[6];
    int firearmIndex, passiveIndex=2;

    int health;

    public GameObject equipText;
    public bool showText;

    public LayerMask itemLayer;
    ItemTrigger targetedItem;

    public bool equippedGeneral; //referenced in the FPS controller script

    void Start()
    {
        combatControls = new PlayerTriggers();
        combatControls.Enable();
        combatControls.Combat.PickUp.performed += ctx=> AddToInventory();
        combatControls.Combat.Primary.performed += ctx=> EquipItem(0);
    }

    private void OnDisable()
    {
        combatControls.Combat.PickUp.performed -= ctx=> AddToInventory();
        combatControls.Combat.Primary.performed -= ctx=> EquipItem(0);
    }

    private void Update()
    {
        if (showText && CheckForItem())
        {
            equipText.SetActive(true);
        }
        else
        {
            equipText.SetActive(false);
        }
    }

    void AddToInventory()
    {
        if (CheckForItem())
        {
            var temp = targetedItem.item;
            temp.graphics = targetedItem.graphics;

            switch (temp.itemType)
            {
                case ItemType.Firearm:
                    inventory[firearmIndex]=temp;
                    firearmIndex++;
                    break;
                case ItemType.Passive:
                    inventory[passiveIndex] = temp;
                    passiveIndex++;
                    break;
                case ItemType.Throwable:
                    inventory[4] = temp;
                    break;
                case ItemType.Heal:
                    inventory[5] = temp;
                    break;
                default:
                    break;
            }

            //turn off target item
            targetedItem.gameObject.SetActive(false);
        }
    }

    bool CheckForItem()
    {
        if (showText)
        {
            var direction = freeLookCam.State.CorrectedOrientation * Vector3.forward;
            RaycastHit hitInfo;
            if (Physics.Raycast(freeLookCam.transform.position, direction, out hitInfo, 5f, itemLayer, QueryTriggerInteraction.Ignore))
            {
                targetedItem = hitInfo.collider.transform.parent.GetComponent<ItemTrigger>();
                return true;
            }
            else
            {
                targetedItem = null;
            }
        }

        return false;
    }

    void EquipItem(int index) {
        inventory[index].equipped = !inventory[index].equipped;
        inventory[index].graphics.gameObject.SetActive(inventory[index].equipped);
        equippedGeneral = inventory[index].equipped;
    }

}
