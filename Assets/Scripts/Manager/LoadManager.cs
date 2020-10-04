using System.Collections.Generic;
using Developers.Util;
using Developers.Structure;
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
        Actor,         // Prefab
        End
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

                    case GameObject gameObject:         // Prefab - 1번대
                        index = (int)LoadType.Actor * Multiply_Index;
                        type = GetActor ( gameObject );
                        key = index + type;
                        if( !hashtable.ContainsKey(key) )
                        {
                            hashtable.Add ( key, new List<Object> ( ) );
                        }
                        hashtable[key].Add ( gameObject );
                        break;
                }
            }

            bComplete = true;
        }

        private int GetActor(GameObject gameObject)
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
    }


    private static Dictionary<int, List<Object>> core = new Dictionary<int, List<Object>> ( );
    private static Dictionary<int, List<Object>> hashtable = new Dictionary<int, List<Object>> ( );

    private Queue<AssetLabelReference> labels = new Queue<AssetLabelReference> ( );
    private List<ResourceComplete> works = new List<ResourceComplete> ( );


    /// <summary>로드할 리소스 태그를 등록시킵니다.</summary>
    public void Register ( AssetLabelReference labelReference )
    {
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


    private IEnumerator Runnable()
    {
        if ( labels.Count == 0 )
        {
            while ( works.Count > 0 )
            {
                bool result = false;
                foreach(var work in works)
                {
                    if(!work.bComplete) break;
                    result = true;
                }

                if(result)
                {
                    MonoSingleton<GameManager>.Instance.OnStart ( );
                    yield break;
                }

                yield return null;
            }
            yield break;
        }

        var label = labels.Dequeue ( );
        works.Add ( new ResourceComplete ( label ) );

        yield return StartCoroutine ( Runnable ( ) );
    }
}
