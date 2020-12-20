using UnityEngine;

namespace Developers.Net.Handler
{
    public abstract class BaseHandler : MonoBehaviour
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
