using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public List<Image> weaponIcons;
    public List<ParticleSystem> effects;

    public LayerMask itemLayer;
    ItemTrigger targetedItem;

    public delegate void DealtDamage(Item weapon);
    public event DealtDamage dealtDamageEvent;

    EnemyController enemyScript;
    public string EquippedWeapon { 
        get {
            return inventory[equippedIndex].name;        
        } 
    }

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

        for (int i = 0; i < 6; i++)
        {
            if (inventory[i] != null)
            {
                inventory[i].currentAmmo = inventory[i].totalAmmo;
            }
        }
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
                DisplayEffect();

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
                    weaponIcons[firearmIndex].GetComponent<Image>().enabled = true;
                    weaponIcons[firearmIndex].sprite = Resources.Load<Sprite>(temp.name);
                    firearmIndex++;
                    break;
                case ItemType.Passive:
                    inventory[passiveIndex] = temp;
                    weaponIcons[passiveIndex].GetComponent<Image>().enabled = true;
                    weaponIcons[passiveIndex].sprite = Resources.Load<Sprite>(temp.name);
                    passiveIndex++;
                    break;
                case ItemType.Heal:
                    inventory[5] = temp;
                    weaponIcons[5].GetComponent<Image>().enabled = true;
                    weaponIcons[5].sprite = Resources.Load<Sprite>(temp.name);
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
        else if(inventory[index]!=null)
        {
            if(equippedIndex!=-1)
                inventory[equippedIndex].graphics.gameObject.SetActive(false);
            inventory[index].graphics.gameObject.SetActive(true);
            equippedIndex = index;
        }
    }

    void Fire(bool toggle)
    {
        if(equippedIndex!=-1)
            firing = toggle;

        if (firing == false)
        {
            TurnOffEffects();
        }
    }

    IEnumerator DealDamage()
    {
        fired = true;

        //Send signal to enemy to increase priority
        dealtDamageEvent.Invoke(inventory[equippedIndex]);

        yield return new WaitForSeconds(inventory[equippedIndex].delay);
        fired = false;
    }

    void Plant()
    {
        inventory[equippedIndex].currentAmmo--;
        GameObject plant = Instantiate(inventory[equippedIndex].graphics.gameObject, transform.position, Quaternion.LookRotation(Vector3.up, transform.forward));
       
        var effScript = plant.AddComponent<SeparateEffect>();
        var collider = plant.AddComponent<BoxCollider>();
        collider.size = new Vector3(.15f, .15f, .15f);
        collider.isTrigger = true;

        if (inventory[equippedIndex].name=="Landmine")
        {
            var explosion = Instantiate(effects[2], plant.transform.position, Quaternion.identity);
            effScript.effect = explosion;
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
        SendSignal("Landmine");
    }

    void SlowDown()
    {
        enemyScript.gameObject.GetComponent<RobotMotor>().moveSpeed /= 5f;
        SendSignal("SlowDown");
        StartCoroutine(RestoreSpeed());
    }

    void Stop()
    {
        enemyScript.gameObject.GetComponent<RobotMotor>().moveSpeed = 0f;
        SendSignal("Magnet");
        StartCoroutine(StartMoving());
    }

    void SendSignal(string weaponName)
    {
        Item weapon;

        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i].name == weaponName)
            {
                weapon = inventory[i];
                dealtDamageEvent.Invoke(weapon);
                break;
            }
        }
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
        Physics.Raycast(freeLookCam.transform.position, direction, out hitInfo, float.MaxValue, ~itemLayer, QueryTriggerInteraction.Ignore);
        Debug.Log(hitInfo.collider.name);
        return hitInfo.collider.tag == "Robot";
    }

    void DisplayEffect()
    {
        if (inventory[equippedIndex].name == "Flamethrower")
        {
            effects[0].Play();
        }
        else if (inventory[equippedIndex].name == "Shotgun")
        {
            effects[1].Play();
        }
    }

    void TurnOffEffects()
    {
        effects[0].Stop();
    }

    public void ResetPlayer()
    {
        health = 100;
        inventory = new Item[6];
        equippedIndex = -1;
        weaponIcons.Clear();
    }
}
