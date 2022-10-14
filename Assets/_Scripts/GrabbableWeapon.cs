using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableWeapon : MonoBehaviour
{
    [HideInInspector]
    public Weapon weaponDetails;
    private MeshRenderer objectMesh;

    // Start is called before the first frame update
    public void Setup(Weapon weapon)
    {
        this.objectMesh = GetComponent<MeshRenderer>();
        this.weaponDetails = weapon;

        switch (weapon.rarity)
        {
            case Rarity.Common:
                this.objectMesh.material = Resources.Load<Material>("Materials/Common");
                break;
            case Rarity.Uncommon:
                this.objectMesh.material = Resources.Load<Material>("Materials/Uncommon");
                break;
            case Rarity.Rare:
                this.objectMesh.material = Resources.Load<Material>("Materials/Rare");
                break;
            case Rarity.Legendary:
                this.objectMesh.material = Resources.Load<Material>("Materials/Legendary");
                break;
            default:
                Debug.LogError("Unknown Rarity: " + weapon.rarity + " Unable to set object mesh");
                break;
        }
    }
}
