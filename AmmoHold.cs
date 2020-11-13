using System.Collections;
using UnityEngine;

public class AmmoHold : MonoBehaviour
{
    public int pistolAmmo;
    public int rifleAmmo;
    public int shotgunAmmo;
    public int sniperAmmo;


    public IEnumerator Reload(WeaponData weaponData, float reloadTime)
    {
        var mc = weaponData.maxCapacity;
        var cc = weaponData.currentCapacity;

        var na = mc - cc;

        yield return new WaitForSeconds(reloadTime);
            
        switch (weaponData.ammoType)
        {
            case WeaponData.AmmoType.Pistol:

                if (pistolAmmo > na)
                {
                    weaponData.currentCapacity += na;
                    pistolAmmo -= na;
                }

                if (pistolAmmo < na)
                {
                    weaponData.currentCapacity += pistolAmmo;
                    pistolAmmo = 0;
                }

                break;
            case WeaponData.AmmoType.Rifle:
                if (rifleAmmo > na)
                {
                    weaponData.currentCapacity += na;
                    rifleAmmo -= na;
                }

                if (rifleAmmo < na)
                {
                    weaponData.currentCapacity += pistolAmmo;
                    rifleAmmo = 0;
                }

                break;
            case WeaponData.AmmoType.Shotgun:
                if (shotgunAmmo > na)
                {
                    weaponData.currentCapacity += na;
                    shotgunAmmo -= na;
                }

                if (shotgunAmmo < na)
                {
                    weaponData.currentCapacity += pistolAmmo;
                    shotgunAmmo = 0;
                }
                break;
            case WeaponData.AmmoType.Sniper:
                break;
            case WeaponData.AmmoType.None:
                break;
        }

        weaponData.UpdateUI();
    }
    public IEnumerator AIReload(WeaponData weaponData, float reloadTime)
    {
        var mc = weaponData.maxCapacity;
        var cc = weaponData.currentCapacity;

        var na = mc - cc;

        yield return new WaitForSeconds(reloadTime);
            
        switch (weaponData.ammoType)
        {
            case WeaponData.AmmoType.Pistol:

                if (pistolAmmo > na)
                {
                    weaponData.currentCapacity += na;
                    pistolAmmo -= na;
                }

                if (pistolAmmo < na)
                {
                    weaponData.currentCapacity += pistolAmmo;
                    pistolAmmo = 0;
                }

                break;
            case WeaponData.AmmoType.Rifle:
                if (rifleAmmo > na)
                {
                    weaponData.currentCapacity += na;
                    rifleAmmo -= na;
                }

                if (rifleAmmo < na)
                {
                    weaponData.currentCapacity += pistolAmmo;
                    rifleAmmo = 0;
                }

                break;
            case WeaponData.AmmoType.Shotgun:
                if (shotgunAmmo > na)
                {
                    weaponData.currentCapacity += na;
                    shotgunAmmo -= na;
                }

                if (shotgunAmmo < na)
                {
                    weaponData.currentCapacity += pistolAmmo;
                    shotgunAmmo = 0;
                }
                break;
            case WeaponData.AmmoType.Sniper:
                break;
            case WeaponData.AmmoType.None:
                break;
        }
    }
}