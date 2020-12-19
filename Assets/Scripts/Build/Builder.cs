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
        var record = new ActorRecord ( ActorType.Building, (int)buildingType );
        GameManager.Create<Building> ( record, transform.position, GameManager.Instance.LocalPlayer );
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