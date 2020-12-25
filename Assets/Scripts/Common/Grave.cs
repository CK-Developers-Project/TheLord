using UnityEngine;
using System.Collections;

public class Grave : MonoBehaviour
{
    public enum GraveType
    {
        Elf,
        Human,
        Undead,
    }

    public GraveType type;

    public static void Create(GraveType type, Vector3 position, float lifeTimed)
    {
        GameObject obj = Instantiate ( LoadManager.Instance.GetGrave ( type ), position, Quaternion.identity );
        var grave = obj.GetComponent<Grave> ( );
        grave.StartCoroutine ( grave.Destroy ( lifeTimed ) );
    }

    IEnumerator Destroy( float time )
    {
        yield return new WaitForSeconds ( time );
        Destroy ( gameObject );
    }
}
