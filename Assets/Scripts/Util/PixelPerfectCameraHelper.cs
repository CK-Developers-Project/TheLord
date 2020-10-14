using UnityEngine;
using UnityEngine.U2D;

namespace Developers.Util
{
    [RequireComponent(typeof( PixelPerfectCamera ) ), DisallowMultipleComponent]
#if UNITY_EDITOR
    [ExecuteInEditMode]
#endif
    public class PixelPerfectCameraHelper : MonoBehaviour
    {
        PixelPerfectCamera  PPCamera { get; set; }
        [SerializeField] Vector2Int fixedScreen = new Vector2Int ( 16, 9 );
        [SerializeField] float orthoSize = 1f;
        Vector2Int preScreen;

        public void UpdateResolution ( )
        {
            float ratio = Mathf.Max ( 1f, fixedScreen.x ) / Mathf.Max ( 1f, fixedScreen.y );
            PPCamera.refResolutionX = Screen.width;
            PPCamera.refResolutionY = Mathf.FloorToInt ( Screen.width / ratio );
            float ScreenAspectRatio = PPCamera.refResolutionX / (float)PPCamera.refResolutionY;
            PPCamera.assetsPPU = Mathf.FloorToInt ( ( ( PPCamera.refResolutionX / ScreenAspectRatio ) * 0.5f ) / orthoSize );
            preScreen.x = PPCamera.refResolutionX;
            preScreen.y = PPCamera.refResolutionY;
        }

        private void Awake ( )
        {
            PPCamera = GetComponent<PixelPerfectCamera> ( );
#if UNITY_ANDROID || UNITY_IOS
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
#endif
        }

        private void FixedUpdate ( )
        {
            if( PPCamera.refResolutionX != preScreen.x || PPCamera.refResolutionY != preScreen.y )
            {
                UpdateResolution ( );
            }
        }

#if UNITY_EDITOR
        private void LateUpdate ( )
        {
            if ( PPCamera.runInEditMode )
            {
                UpdateResolution ( );
            }
        }
#endif
    }
}
