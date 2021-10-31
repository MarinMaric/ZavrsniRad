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
    [SerializeField]
    List<SeparateEffect> instantiationList; 
    int firearmIndex, passiveIndex = 2;
    int equippedIndex=-1;

    public int health = 100;
    bool firing, fired;

    public GameObject equipText;
    public bool showText;

    public LayerMask itemLayer;
    ItemTrigger targetedItem;

    EnemyController enemyScript;

    void Start()
    {
        enemyScript = FindObjectOfType<EnemyController>();

        combatControls = new PlayerTriggers();
        combatControls.Enable();
        combatControls.Combat.PickUp.performed += ctx => AddToInventory();
        combatControls.Combat.Primary.performed += ctx => EquipItem(0);
        combatControls.Combat.Secondary.performed += ctx => EquipItem(1);
        combatControls.Combat.PassivePrimary.performed += ctx => EquipItem(2);
        combatControls.Combat.PassiveSecondary.performed += ctx => EquipItem(3);
        combatControls.Combat.PassiveThird.performed += ctx => EquipItem(4);
        combatControls.Combat.Heal.performed += ctx => EquipItem(5);

        combatControls.Combat.Shoot.started += ctx => Fire(true);
        combatControls.Combat.Shoot.canceled += ctx => Fire(false);
    }

    private void OnDisable()
    {
        combatControls.Combat.PickUp.performed -= ctx => AddToInventory();
        combatControls.Combat.Primary.performed -= ctx => EquipItem(0);
        combatControls.Combat.PassivePrimary.performed -= ctx => EquipItem(1);
        combatControls.Combat.PassivePrimary.performed -= ctx => EquipItem(2);
        combatControls.Combat.PassiveSecondary.performed -= ctx => EquipItem(3);
        combatControls.Combat.PassiveThird.performed -= ctx => EquipItem(4);
        combatControls.Combat.Heal.performed -= ctx => EquipItem(5);

        combatControls.Combat.Shoot.started -= ctx => Fire(true);
        combatControls.Combat.Shoot.canceled -= ctx => Fire(false);
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

        if (firing && !fired)
        {
            if(inventory[equippedIndex].currentAmmo > 0)
            {
                if (inventory[equippedIndex].itemType == ItemType.Firearm && AimForEnemy())
                {
                    StartCoroutine(DealDamage());
                }
                else if (inventory[equippedIndex].itemType == ItemType.Passive)
                {
                    Plant();
                }
                else if(inventory[equippedIndex].itemType == ItemType.Heal)
                {
                    Heal();
                }
            }
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
                    inventory[firearmIndex] = temp;
                    firearmIndex++;
                    break;
                case ItemType.Passive:
                    inventory[passiveIndex] = temp;
                    passiveIndex++;
                    break;
                case ItemType.Heal:
                    inventory[4] = temp;
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
        if (equippedIndex == index)
        {
            inventory[index].graphics.gameObject.SetActive(false);
            equippedIndex = -1;
        }
        else
        {
            inventory[equippedIndex].graphics.gameObject.SetActive(false);
            inventory[index].graphics.gameObject.SetActive(true);
            equippedIndex = index;
        }
    }

    void Fire(bool toggle)
    {
        if(equippedIndex!=-1)
            firing = toggle;
    }

    IEnumerator DealDamage()
    {
        fired = true;
        enemyScript.health -= inventory[equippedIndex].damage;
        yield return new WaitForSeconds(inventory[equippedIndex].delay);
        fired = false;
    }

    void Plant()
    {
        inventory[equippedIndex].currentAmmo--;
        GameObject plant = Instantiate(inventory[equippedIndex].graphics.gameObject, transform.position, Quaternion.identity);
        var effScript = plant.AddComponent<SeparateEffect>();
        var collider = plant.GetComponent<BoxCollider>();
        collider.isTrigger = true;
        collider.size = new Vector3(2f, 2f, 2f);

        if (inventory[equippedIndex].name=="Landmine")
        {
            effScript.activateFunction = Explode;
        }
        else if(inventory[equippedIndex].name=="Slowdown")
        {
            effScript.activateFunction = SlowDown;
        }
        else if (inventory[equippedIndex].name == "Magnet")
        {
            effScript.activateFunction = Stop;
        }
    }
    
    void Heal()
    {
        inventory[equippedIndex].currentAmmo--;
        health = Mathf.Clamp(health + inventory[equippedIndex].bonus, health, 100);
    }

    void Explode()
    {
        enemyScript.health -= inventory[equippedIndex].damage;
    }

    void SlowDown()
    {
        enemyScript.gameObject.GetComponent<RobotMotor>().moveSpeed /= 5f;
        StartCoroutine(RestoreSpeed());
    }

    void Stop()
    {
        enemyScript.gameObject.GetComponent<RobotMotor>().moveSpeed = 0f;
        StartCoroutine(StartMoving());
    }

    IEnumerator RestoreSpeed() {
        yield return new WaitForSeconds(inventory[equippedIndex].delay);
        enemyScript.gameObject.GetComponent<RobotMotor>().moveSpeed *= 5f;
    }
    IEnumerator StartMoving()
    {
        yield return new WaitForSeconds(inventory[equippedIndex].delay);
        enemyScript.gameObject.GetComponent<RobotMotor>().moveSpeed = 2f;
    }

    bool AimForEnemy()
    {
        Vector3 direction = freeLookCam.State.CorrectedOrientation * Vector3.forward;
        RaycastHit hitInfo;
        Physics.Raycast(freeLookCam.transform.position, direction, out hitInfo, float.MaxValue);
        Debug.Log(hitInfo.collider.name);
        return hitInfo.collider.tag == "Robot";
    }
}
