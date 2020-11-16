using ExitGames.Client.Photon;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace Developers.Net
{
    using Structure;

    public class EventMedia
    {
        public delegate void Act ( EventData eventData );
        static Dictionary<EventCode, Delegate> table = new Dictionary<EventCode, Delegate> ( );


        public static void AddListener ( EventCode code, Act act )
        {
            if ( !table.ContainsKey ( code ) )
            {
                table.Add ( code, null );
            }

            Delegate dgt = table[code];
            if ( dgt != null && dgt.GetType ( ) != act.GetType ( ) )
            {
                Debug.LogErrorFormat ( "Event Type {0}에 대한 서명이 일치하지 않은 Listener를 추가하려했습니다. 현재 Listener에는 {1} Type이 있고 추가하려는 Listener에는 {2} Type이 있습니다.", code, dgt.GetType ( ).Name, act.GetType ( ).Name );
            }
            else
            {
                table[code] = (Act)table[code] + act;
            }
        }

        public static void RemoveListener ( EventCode code, Act act )
        {
            if ( table.ContainsKey ( code ) )
            {
                Delegate dgt = table[code];

                if ( dgt == null )
                {
                    Debug.LogErrorFormat ( "Event Type {0}에 대한 Listener를 제거하려고 시도했지만 현재 Listener가 null입니다.", code );
                }
                else if ( dgt.GetType ( ) != act.GetType ( ) )
                {
                    Debug.LogErrorFormat ( "Event Type {0}에 대한 서명이 일치하지 않는 Listener를 제거하려 했습니다. 현재 Listener Type은 {1}이고 제거하려는 Listener Type은 {2}입니다.", code, dgt.GetType ( ).Name, act.GetType ( ).Name );
                }
                else
                {
                    table[code] = (Act)table[code] - act;
                    if ( dgt == null )
                    {
                        table.Remove ( code );
                    }
                }
            }
            else
            {
                Debug.LogErrorFormat ( "{0} Type에 대한 Listener를 제거하려 시도했지만 Messenger가 Event Type에 대해 알지 못합니다.", code );
            }
        }

        public static void RemoveAllListener ( EventCode code )
        {
            if ( table.ContainsKey ( code ) )
            {
                table[code] = null;
                table.Remove ( code );
            }
            else
            {
                Debug.LogErrorFormat ( "{0} Type에 대한 Listener를 제거하려 시도했지만 Messenger가 Event Type에 대해 알지 못합니다.", code );
            }
        }

        public static void Dispatch ( EventCode code, EventData eventData )
        {
            Delegate dgt;
            if ( table.TryGetValue ( code, out dgt ) )
            {
                Act callback = dgt as Act;

                if ( callback != null )
                {
                    callback ( eventData );
                }
                else
                {
                    Debug.LogErrorFormat ( "Event Type {0}를 찾지 못했습니다.", code );
                }
            }
        }
    }
}
