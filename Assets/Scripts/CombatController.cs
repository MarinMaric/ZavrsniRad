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

    AudioSource effectAudio;

    EnemyController enemyScript;
    public string EquippedWeapon { 
        get {
            return inventory[equippedIndex].name;        
        } 
    }
    [SerializeField]
    Text healthText;

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

        combatControls.Combat.Shoot.started -= ctx => FireContinuous(true);
        combatControls.Combat.Shoot.canceled -= ctx => FireContinuous(false);

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

                if (inventory[equippedIndex].itemType == ItemType.Firearm)
                {
                    if (inventory[equippedIndex].name == "Flamethrower")
                        StartCoroutine(FireFlamethrower());
                    else
                        FireOffRound();
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
            firing = false;
        }
        else if(inventory[index]!=null)
        {
            if (equippedIndex != -1)
            {
                inventory[equippedIndex].graphics.gameObject.SetActive(false);

                //Remap controls
                if (inventory[index].name == "Flamethrower")
                {
                    combatControls.Combat.Shoot.performed -= ctx => Fire();
                    combatControls.Combat.Shoot.started += ctx => FireContinuous(true);
                    combatControls.Combat.Shoot.canceled += ctx => FireContinuous(false);
                }
                else 
                {
                    combatControls.Combat.Shoot.performed += ctx => Fire();

                    if (inventory[equippedIndex].name == "Flamethrower")
                    {
                        combatControls.Combat.Shoot.started -= ctx => FireContinuous(true);
                        combatControls.Combat.Shoot.canceled -= ctx => FireContinuous(false);
                    }
                }
            }
            else
            {
                if (inventory[index].name == "Flamethrower")
                {
                    combatControls.Combat.Shoot.started += ctx => FireContinuous(true);
                    combatControls.Combat.Shoot.canceled += ctx => FireContinuous(false);
                }
                else
                {
                    combatControls.Combat.Shoot.performed += ctx => Fire();
                }
            }
            
            effectAudio = inventory[index].graphics.GetComponent<AudioSource>();
            inventory[index].graphics.gameObject.SetActive(true);
            equippedIndex = index;
        }
    }

    void FireContinuous(bool toggle)
    {
        if(equippedIndex!=-1)
            firing = toggle;

        if (firing == false)
        {
            TurnOffEffects();
        }
    }

    void Fire()
    {
        if (equippedIndex != -1)
            firing = true;
    }
    
    IEnumerator FireFlamethrower()
    {
        fired = true;
        inventory[equippedIndex].currentAmmo--;

        if (AimForEnemy())
        {
            //Send signal to enemy to increase priority
            dealtDamageEvent.Invoke(inventory[equippedIndex]);
        }

        yield return new WaitForSeconds(inventory[equippedIndex].delay);
        
        fired = false;
    }

    void FireOffRound()
    {
        inventory[equippedIndex].currentAmmo--;

        if (AimForEnemy())
        {
            dealtDamageEvent.Invoke(inventory[equippedIndex]);
        }

        if (inventory[equippedIndex].currentAmmo <= 0)
        {
            RanOutOfAmmo();
        }
    }

    void Plant()
    {
        inventory[equippedIndex].currentAmmo--;

        GameObject plant = Instantiate(inventory[equippedIndex].graphics.gameObject, transform.position, Quaternion.LookRotation(Vector3.up, transform.forward));
       
        var effScript = plant.AddComponent<SeparateEffect>();
        var collider = plant.AddComponent<BoxCollider>();
        collider.size = new Vector3(.15f, .15f, .15f);
        collider.isTrigger = true;
        plant.tag = "Plant";
        plant.layer = 0;
        
        var plantAudio = plant.GetComponent<AudioSource>();
        plantAudio.playOnAwake = false;

        if (inventory[equippedIndex].name=="Landmine")
        {
            var explosion = Instantiate(effects[2], plant.transform.position, Quaternion.identity);
            effScript.effect = explosion;
            effScript.activateFunction = Explode;
            plantAudio.clip = Resources.Load<AudioClip>("bomb");
        }
        else if(inventory[equippedIndex].name=="Slowdown")
        {
            effScript.activateFunction = SlowDown;
            plantAudio.clip = Resources.Load<AudioClip>("slowdown");
        }
        else if (inventory[equippedIndex].name == "Magnet")
        {
            effScript.activateFunction = Stop;
            plantAudio.clip = Resources.Load<AudioClip>("electricity");
        }

        effScript.audioSrc = plantAudio;

        effectAudio.Play();

        if (inventory[equippedIndex].currentAmmo <= 0)
        {
            RanOutOfAmmo();
        }
    }
    
    void Heal()
    {
        inventory[equippedIndex].currentAmmo--;
        health = Mathf.Clamp(health + inventory[equippedIndex].bonus, health, 100);
        

        StartCoroutine(EndHeal());
    }

    IEnumerator EndHeal()
    {
        effectAudio.Play();
        inventory[equippedIndex].graphics.GetComponent<MeshRenderer>().enabled = false;
        weaponIcons[equippedIndex].GetComponent<Image>().enabled = false;
        healthText.text = health.ToString();

        yield return new WaitForSeconds(1);
        RanOutOfAmmo();
    }

    void Explode()
    {
        enemyScript.TakePassive("Landmine", 50);
    }

    void SlowDown()
    {
        enemyScript.GetComponent<RobotMotor>().moveSpeed /= 5f;
        StartCoroutine(RestoreSpeed());
        enemyScript.recorder.IncreaseImmunity("SlowDown");
    }

    void Stop()
    {
        enemyScript.GetComponent<RobotMotor>().moveSpeed = 0f;
        StartCoroutine(StartMoving());
    }

    void SendSignal(string weaponName)
    {
        Item weapon;

        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i]!=null && inventory[i].name == weaponName)
            {
                weapon = inventory[i];
                dealtDamageEvent.Invoke(weapon);
                break;
            }
        }
    }

    IEnumerator RestoreSpeed() {
        int waitValue = Mathf.Abs(10 - 10 * enemyScript.GetImmunity("SlowDown") / 100);
        yield return new WaitForSeconds(waitValue);
        var motor = enemyScript.gameObject.GetComponent<RobotMotor>();
        if (motor.moveSpeed != motor.ogMoveSpeed)
        {
            motor.moveSpeed = motor.ogMoveSpeed;
        }
    }
    IEnumerator StartMoving()
    {
        int waitValue = Mathf.Abs(10 - 10 * enemyScript.GetImmunity("Magnet") / 100);
        yield return new WaitForSeconds(waitValue);
        var motor = enemyScript.gameObject.GetComponent<RobotMotor>();
        motor.moveSpeed = motor.ogMoveSpeed;
        enemyScript.recorder.IncreaseImmunity("Magnet");
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
            if (!effectAudio.isPlaying)
            {
                effectAudio.Play();
            }
        }
        else if (inventory[equippedIndex].name == "Shotgun")
        {
            effects[1].Play();
            effectAudio.Play();
            Animator anim = transform.GetChild(0).GetChild(0).Find("Shotgun").GetComponent<Animator>();
            anim.SetBool("Fire", true);
            firing = false;
        }
        else
        {
            firing = false;
        }
    }

    public void TurnOffEffects()
    {
        effects[0].Stop();
        if (effectAudio.isPlaying)
        {
            effectAudio.Stop();
        }
    }

    void RanOutOfAmmo()
    {
        //Reset ammo for next round before disabling
        inventory[equippedIndex].currentAmmo = inventory[equippedIndex].totalAmmo;

        //Turn off graphics and remove from inventory
        inventory[equippedIndex].graphics.gameObject.SetActive(false);
        inventory[equippedIndex] = null;

        //Turn off the icon
        weaponIcons[equippedIndex].GetComponent<Image>().enabled = false;
        weaponIcons[equippedIndex].sprite = null;

        //Reduce respective index and reset equippedIndex
        if (equippedIndex < 2)
            firearmIndex = equippedIndex;
        else if (equippedIndex < 5)
            passiveIndex = equippedIndex;

        equippedIndex = -1;
    }

    public void TakeDamage(int damage)
    {
        if (health < damage) { 
            health = 0;
        }
        else
        {
            health -= damage;
        }

        healthText.text = health.ToString();
    }

    public void ResetPlayer()
    {
        health = 100;
        healthText.text = health.ToString();
        if (equippedIndex != -1)
        {
            inventory[equippedIndex].graphics.gameObject.SetActive(false);
            inventory[equippedIndex].currentAmmo = inventory[equippedIndex].totalAmmo;
        }
        inventory = new Item[6];
        equippedIndex = -1;
        firing = false;
        fired = false;
        foreach(var i in weaponIcons)
        {
            i.GetComponent<Image>().enabled = false;
            i.sprite = null;
        }
        firearmIndex = 0;
        passiveIndex = 2;
    }
}
