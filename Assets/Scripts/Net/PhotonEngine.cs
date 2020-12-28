using ExitGames.Client.Photon;
using UnityEngine;
using System.Collections;

namespace Developers.Net
{
    using Util;
    using Structure;
    using UnityEngine.SceneManagement;

    public class PhotonEngine : MonoSingleton<PhotonEngine>, IPhotonPeerListener
    {
        public static PhotonPeer Peer { get; private set; }

        public string serverAddress = "127.0.0.1:5055";
        public string applicationName = "TheLord";
        public bool isServerConnect { get => Peer != null && Peer.PeerState == PeerStateValue.Connected; }

        public void DebugReturn ( DebugLevel level, string message )
        {
            string msg = string.Format ( "[{0}] - {1}", level, message );
            switch(level)
            {
                case DebugLevel.ALL:
                case DebugLevel.INFO:
                    Debug.Log ( msg );
                    break;
                case DebugLevel.WARNING:
                case DebugLevel.OFF:
                    Debug.LogWarning ( msg );
                    break;
                case DebugLevel.ERROR:
                    Debug.LogError ( msg );
                    break;
            }
        }

        ///<summary> 
        ///클라이언트가 요청을 시작하지 않았지만 서버가 클라이언트에게 데이터를 보낼 때 OnEvent을 통해 응답합니다.
        ///</summary>
        public void OnEvent ( EventData eventData )
        {
            EventCode code = (EventCode)eventData.Code;
            EventMedia.Dispatch ( code, eventData );
        }

        ///<summary> 
        ///클라이언트에서 서버로 요청이 시작할 때, 서버는 요청을 수락 및 처리하고 클라이언트에게 응답을 보내면 처리합니다.
        ///</summary>
        public void OnOperationResponse ( OperationResponse operationResponse )
        {
            OperationCode code = (OperationCode)operationResponse.OperationCode;
            HandlerMedia.Dispatch ( code, operationResponse );
        }

        /// <summary>
        /// <para>이 메서드는 연결 상태가 변경 될 때 트리거가 됩니다.</para>
        /// <para>연결 상태는 연결 (PeerStateValue.Connecting)</para>
        /// <para>연결됨 (PeerStateValue.Connected)</para>
        /// <para>연결 해제 (PeerStateValue.Disconnecting)</para>
        /// <para>연결 해제 (PeerStateValue.Disconnected)</para>
        /// </summary>
        /// <param name="statusCode">연결상태</param>
        public void OnStatusChanged ( StatusCode statusCode )
        {
            Debug.LogFormat ( "[OnStatusChanged] - {0}", statusCode );

            if (!SceneManager.GetActiveScene().name.Equals(SceneName.Login) &&
                (statusCode == StatusCode.Disconnect || statusCode == StatusCode.DisconnectByServer
                || statusCode == StatusCode.DisconnectByServerLogic || statusCode == StatusCode.DisconnectByServerUserLimit
                || statusCode == StatusCode.TimeoutDisconnect))
            {
                TransitionManager.instance.OnSceneTransition(SceneName.Login, TransitionType.Slide, () => {

                    GameManager.instance.gamePlayers.Clear ( );
                    for(int i = 0; i < GameManager.instance.transform.childCount; ++i )
                    {
                        Destroy ( GameManager.instance.transform.GetChild ( i ).gameObject );
                    }
                } );
            }    
        }


        public bool Connect()
        {
            if ( isServerConnect )
            {
                return isServerConnect;
            }

            if ( Peer == null )
            {
                Peer = new PhotonPeer ( this, ConnectionProtocol.Udp );
            }
            return Peer.Connect ( serverAddress, applicationName );
        }

        public void Disconnect()
        {
            // Peer가 비어있지 않고 연결중인 경우 연결을 끊습니다.
            if ( Peer != null && Peer.PeerState == PeerStateValue.Connected )
            {
                Destroy ( GameManager.instance.LocalPlayer.gameObject );
                Peer.Disconnect ( );
            }
        }

        private void Update ( )
        {
            Peer?.Service ( );
        }

        void OnDestroy ( )
        {
            Disconnect ( );
        }
    }
}