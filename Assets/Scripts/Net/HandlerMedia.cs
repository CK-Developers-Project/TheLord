using System;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;

namespace Developers.Net
{
    using Structure;

    public class HandlerMedia
    {
        public delegate void Act ( OperationResponse response );
        static Dictionary<OperationCode, Delegate> table = new Dictionary<OperationCode, Delegate> ( );


        public static void AddListener ( OperationCode type, Act act )
        {
            if ( !table.ContainsKey ( type ) )
            {
                table.Add ( type, null );
            }

            Delegate dgt = table[type];
            if ( dgt != null && dgt.GetType ( ) != act.GetType ( ) )
            {
                Debug.LogErrorFormat (
                    "Event Type {0}에 대한 서명이 일치하지 않은 Listener를 추가하려했습니다.\n" +
                    "현재 Listener에는 {1} Type이 있으며 추가 요청중인 Listener {2} Type이 있습니다."
                    , type, dgt.GetType ( ).Name, act.GetType ( ).Name
                );
            }
            else
            {
                table[type] = (Act)table[type] + act;
            }
        }

        public static void RemoveListener ( OperationCode type, Act act )
        {
            if ( table.ContainsKey ( type ) )
            {
                Delegate dgt = table[type];

                if ( dgt == null )
                {
                    Debug.LogErrorFormat (
                        "Event Type {0}에 대한 Listener 제거를 시도했지만 현재 Listener가 null입니다.", type );
                }
                else if ( dgt.GetType ( ) != act.GetType ( ) )
                {
                    Debug.LogErrorFormat (
                        "Event Type {0}에 대한 서명이 일치하지 않는 Listener를 제거하려고 합니다." +
                        "현재 Listener에는 {1} Type가 있으며 제가하려는 Listener에는 {2} Type이 있습니다.",
                        type, dgt.GetType ( ).Name, act.GetType ( ).Name );
                }
                else
                {
                    table[type] = (Act)table[type] - act;
                    if ( dgt == null )
                    {
                        table.Remove ( type );
                    }
                }
            }
            else
            {
                Debug.LogErrorFormat ( "{0} Type에 대한 Listener를 제거하려고 시도했지만 Messenger에 해당 Event Type이 없습니다.", type );
            }
        }

        public static void RemoveAllListener ( OperationCode code )
        {
            if ( table.ContainsKey ( code ) )
            {
                table[code] = null;
                table.Remove ( code );
            }
            else
            {
                Debug.LogErrorFormat ( "{0} Type에 대한 Listener를 제거하려 시도했지만 Messenger에 해당 Event Type이 없습니다.", code );
            }
        }

        public static void Dispatch ( OperationCode code, OperationResponse response )
        {
            Delegate dgt;
            Debug.Log ( table.Count );
            if ( table.TryGetValue ( code, out dgt ) )
            {
                Act callback = dgt as Act;

                if ( callback != null )
                {
                    callback ( response );
                }
                else
                {
                    Debug.LogErrorFormat ( "Event Type {0}를 찾기 못했습니다.", code );
                }
            }
        }
    }
}
