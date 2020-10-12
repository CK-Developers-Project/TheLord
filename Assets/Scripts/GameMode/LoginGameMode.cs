using System;
using System.Collections.Generic;
using UnityEngine;
using Developers.Util;

namespace Assets.Scripts.GameMode
{
    public class LoginGameMode : BaseGameMode
    {


        // 코어 로드
        public override void Load()
        {
            MonoSingleton<GameManager>.Instance.OnStart();
        }



    }
}
