using UnityEngine;
using Developers.Structure;

public class AnimVFX : MonoBehaviour
{
    public void Anim_VFX ( VFXType type )
    {
        GameObject vfx = LoadManager.Instance.GetVFX ( type );
        Instantiate ( vfx, transform.position, Quaternion.identity );
    }
}
