using UnityEngine;
// Doing a weapon manager to try keep things nice and organised
public class WeaponManager : MonoBehaviour
{
    // our 2 current weapons
    public GameObject holdingSword;
    public GameObject holdingGun;

    private bool hasGun = false;
    private bool usingGun = false;
    // 2 booleans

    void Start()
    {
        // Hank starts with the sword
        holdingSword.SetActive(true);
        holdingGun.SetActive(false);
    }

    void Update()
    {
        // Switch to sword
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquipSword();
        }

        // Switch to gun, if collected, it may not be yet
        if (Input.GetKeyDown(KeyCode.Alpha2) && hasGun)
        {
            EquipGun();
        }
    }

    public void CollectGun()
    {
        hasGun = true;
        EquipGun(); // function call
        Debug.Log("Gun collected, idea is to Press 1 for sword or 2 for gun.");
    }

    void EquipSword()
    {
        usingGun = false;

        holdingSword.SetActive(true);
        holdingGun.SetActive(false);
        Debug.Log("Sword is equipped by default");
    }

    void EquipGun()
    {
        usingGun = true;

        holdingSword.SetActive(false);
        holdingGun.SetActive(true);
        Debug.Log("Gun equipped, now it can be used instead of the sword weapon");
    }

    public bool IsUsingGun()
    {
        return usingGun;
    }
}
