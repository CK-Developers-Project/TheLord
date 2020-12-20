using UnityEngine;

namespace Developers.Net.Event
{
    public abstract class BaseEvent : MonoBehaviour
    {
        public abstract void AddListener ( );
        public abstract void RemoveListener ( );

        void Awake ( )
        {
            AddListener ( );
        }

        void OnDestroy ( )
        {
            RemoveListener ( );
        }
    }
}
