using System.Collections;
using UnityEngine;
using Developers.Structure;
using Developers.Util;

public class Builder : MonoBehaviour
{
    private SpriteRenderer spriteRenderer = null;
    public BuildingType buildingType;


    public void Load()
    {
         GameObject building = GameManager.Create<Building> ( new ActorRecord ( ActorType.Building, (int)buildingType ) );

        if(building == null)
        {
            Debug.LogErrorFormat ( "{0} Type의 건물 생성에 실패하였습니다.", buildingType );
            return;
        }
        Instantiate ( building, transform.position, Quaternion.identity );
        Destroy ( gameObject );
    }


    private void Awake ( )
    {
        spriteRenderer = GetComponent<SpriteRenderer> ( );
        spriteRenderer.enabled = false;
    }

    private IEnumerator Start()
    {
        yield return new WaitUntil ( ( ) => MonoSingleton<GameManager>.Instance.IsGameStart );
        Load ( );
    }
}