using System.Collections.Generic;
using Developers.Util;
using Developers.Structure;
using Developers.Structure.Data;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine;
using System.Collections;

public class LoadManager : MonoSingleton<LoadManager>
{
    private const int Multiply_Index = 100000;
    private const int Multiply_Type = 10000;

    /// <summary>비교할 데이터의 Key Type</summary>
    public enum LoadType
    {
        Actor,          // Prefab
        UI,             // Prefab
        Grave,          // Prefab
        VFX,            // Prefab

        CharacterData,      // ScriptableObject
        AbilityData,    // ScriptableObject
        MusicData,      //ScriptableObject
        SFXData,        //ScriptableObject
    }

    private class ResourceComplete 
    {
        public AsyncOperationHandle handle;
        public bool bComplete = false;

        public ResourceComplete( AssetLabelReference assetLabel )
        {
            var tmpHandle = Addressables.LoadAssetsAsync<Object> ( assetLabel, null );
            tmpHandle.Completed += OnLoadCompleted;
            handle = tmpHandle;
        }

        private void OnLoadCompleted ( AsyncOperationHandle<IList<Object>> handle )
        {
            int index;
            int type;
            int key;
            foreach ( var obj in handle.Result )
            {
                switch ( obj )
                {
                    case Object nullObject when nullObject == null:
                        Debug.LogErrorFormat ( "{0} is Null", nullObject.GetType ( ) );
                        break;
                    case null:
                        Debug.LogErrorFormat ( "Object is Null" );
                        break;

                    case GameObject gameObject:
                        #region Actor
                        if ( gameObject.GetComponent<IActor> ( ) != null )
                        {
                            index = (int)LoadType.Actor * Multiply_Index;
                            type = GetActor ( gameObject );
                            key = index + type;
                            Add ( key, gameObject );
                        }
                        #endregion
                        #region GameUI
                        else if ( gameObject.GetComponent<IGameUI> ( ) != null )
                        {
                            key = (int)LoadType.UI * Multiply_Index;
                            Add ( key, gameObject );
                        }
                        #endregion
                        #region Grave
                        else if(gameObject.GetComponent<Grave>() != null)
                        {
                            key = (int)LoadType.Grave * Multiply_Index;
                            Add ( key, gameObject );
                        }
                        #endregion
                        #region VFX
                        else if(gameObject.GetComponent<VFXObject>() != null)
                        {
                            key = (int)LoadType.VFX * Multiply_Index;
                            Add ( key, gameObject );
                        }
                        #endregion
                        break;

                    case CharacterData character:
                        key = (int)LoadType.CharacterData * Multiply_Index;
                        character.Initialize ( );
                        Add ( key, character );
                        break;

                    case AbilityData ability:
                        key = (int)LoadType.AbilityData * Multiply_Index;
                        ability.Initialize ( );
                        Add ( key, ability );
                        break;

                    case MusicData music:
                        key = (int)LoadType.MusicData * Multiply_Index;
                        Add ( key, music );
                        break;

                    case SFXData sfx:
                        key = (int)LoadType.SFXData * Multiply_Index;
                        Add ( key, sfx );
                        break;
                }
            }

            bComplete = true;
        }

        int GetActor(GameObject gameObject)
        {
            if ( gameObject.GetComponent<BaseCharacter> ( ) != null )
            {
                return (int)ActorType.Character * Multiply_Type;
            }
            else if ( gameObject.GetComponent<Building> ( ) != null )
            {
                return (int)ActorType.Building * Multiply_Type;
            }
            return 0;
        }

        void Add(int key, Object obj)
        {
            if(!hashtable.ContainsKey(key))
            {
                hashtable.Add ( key, new List<Object> ( ) );
            }
            hashtable[key].Add ( obj );
        }
    }


    static List<Object> core = new List<Object> ( );
    static Dictionary<int, List<Object>> hashtable = new Dictionary<int, List<Object>> ( );

    List<AssetLabelReference> isCompletedAsset = new List<AssetLabelReference> ( );
    Queue<AssetLabelReference> labels = new Queue<AssetLabelReference> ( );
    List<ResourceComplete> works = new List<ResourceComplete> ( );

    public bool IsInitialize { get; private set; }
    [SerializeField] AssetLabelReference coreLabel = null;

    public List<Object> Core { get => core; }


    public void Initialize()
    {
        var handle = Addressables.LoadAssetsAsync<Object> ( coreLabel, null );
        handle.Completed += OnInitializeComplete;
    }

    void OnInitializeComplete( AsyncOperationHandle<IList<Object>> handle )
    {
        foreach ( var obj in handle.Result )
        {
            core.Add ( obj );
        }
        IsInitialize = true;
    }


    /// <summary>로드할 리소스 태그를 등록시킵니다.</summary>
    public void Register ( AssetLabelReference labelReference )
    {
        if ( isCompletedAsset.Contains ( labelReference ) )
        {
            return;
        }

        isCompletedAsset.Add ( labelReference );
        labels.Enqueue ( labelReference );
    }

    /// <summary>등록된 리소스 태그들을 로드합니다.</summary>
    public void Run ( )
    {
        StartCoroutine ( Runnable ( ) );
    }

    /// <summary>로드된 리소스 데이터들을 해체합니다.</summary>
    public void Release ( )
    {
        foreach(var list in hashtable.Values)
        {
            foreach(var obj in list)
            {
                Addressables.Release ( obj );
            }
        }
        hashtable.Clear ( );
    }

    public void Release( LoadType type )
    {
        // TODO 선택 제거
    }
    

    public GameObject GetActor<T> ( ActorRecord actorRecord ) where T : IActor
    {
        int index = (int)LoadType.Actor * Multiply_Index;
        int type = (int)actorRecord.type * Multiply_Type;
        int key = index + type;
        if ( !hashtable.ContainsKey ( key ) )
        {
            return null;
        }
        List<Object> actorList = hashtable[key];

        foreach(GameObject actor in actorList)
        {
            T com = actor.GetComponent<T> ( );
            if(com == null)
            {
                continue;
            }
            
            if(com.Index == actorRecord.index)
            {
                return actor;
            }
        }

        return null;
    }

    public GameObject GetGameUI<T> () where T : IGameUI
    {
        List<Object> gameUIList = hashtable[(int)LoadType.UI * Multiply_Index];
        foreach( GameObject gameUI in gameUIList)
        {
            if( gameUI.GetComponent<T> ( ) != null)
            {
                return gameUI;
            }
        }
        return null;
    }

    public GameObject GetGrave(Grave.GraveType type)
    {
        List<Object> graveList = hashtable[(int)LoadType.Grave * Multiply_Index];
        foreach(GameObject grave in graveList)
        {
            if(grave.GetComponent<Grave>().type == type)
            {
                return grave;
            }
        }
        return null;
    }

    public GameObject GetVFX(VFXType type)
    {
        List<Object> vfxList = hashtable[(int)LoadType.VFX * Multiply_Index];
        foreach ( GameObject vfx in vfxList )
        {
            if ( vfx.GetComponent<VFXObject> ( ).type == type )
            {
                return vfx;
            }
        }
        return null;
    }

    public CharacterData GetCharacterData(int index)
    {
        List<Object> CharacterList = hashtable[(int)LoadType.CharacterData * Multiply_Index];
        foreach ( CharacterData character in CharacterList )
        {
            if ( character.index == index )
            {
                return character;
            }
        }
        return null;
    }


    public AbilityData GetAbilityData(int index)
    {
        List<Object> abilityList = hashtable[(int)LoadType.AbilityData * Multiply_Index];
        foreach(AbilityData ability in abilityList)
        {
            if(ability.index == index)
            {
                return ability;
            }
        }
        return null;
    }

    public MusicData GetMusicData(MusicType type)
    {
        List<Object> musicList = hashtable[(int)LoadType.MusicData * Multiply_Index];
        foreach ( MusicData music in musicList )
        {
            if ( music.type == type )
            {
                return music;
            }
        }
        return null;
    }

    public SFXData GetSFXData(SFXType type)
    {
        List<Object> sfxList = hashtable[(int)LoadType.SFXData * Multiply_Index];
        foreach ( SFXData sfx in sfxList )
        {
            if ( sfx.type == type )
            {
                return sfx;
            }
        }
        return null;
    }

    public GameObject Get(System.Type type)
    {
        GameObject obj = core.Find ( x =>
          {
              GameObject o = x as GameObject;
              if(o == null)
              {
                  return false;
              }
              return o.GetComponent ( type ) != null;
          } ) as GameObject;
        return obj;
    }

    private IEnumerator Runnable()
    {
        if ( labels.Count == 0 )
        {
            while ( works.Count > 0 )
            {
                if ( works[0].bComplete )
                {
                    works.RemoveAt ( 0 );
                }
                yield return null;
            }
            StartCoroutine ( GameManager.Instance.GameMode.OnStart ( ) );
            yield break;
        }

        var label = labels.Dequeue ( );
        works.Add ( new ResourceComplete ( label ) );

        yield return StartCoroutine ( Runnable ( ) );
    }

    protected override void Awake()
    {
        base.Awake();
        IsInitialize = false;
    }
}
