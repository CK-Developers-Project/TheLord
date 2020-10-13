using System;
using System.Collections.Generic;
using UnityEngine;
using Developers.Util;

namespace Assets.Scripts.GameMode
{
    public class LoginGameMode : BaseGameMode
    {
        [SerializeField] LoginPage loginPage = null;
        [SerializeField] RaceSelectPage raceSelectPage = null;


        // 코어 로드
        public override void Load()
        {
            MonoSingleton<GameManager>.Instance.OnStart();
        }


        public override void OnEnter ( )
        {
            SetPage ( loginPage );
        }
    }
}
