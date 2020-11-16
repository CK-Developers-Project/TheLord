using UnityEngine;

namespace Developers.Net.Handler
{
    public abstract class BaseHandler : MonoBehaviour
    {
        public abstract void AddListener ( );
        public abstract void RemoveListener ( );

        public virtual void Awake ( )
        {
            AddListener ( );
        }

        public virtual void OnDestroy ( )
        {
            RemoveListener ( );
        }
    }
}
