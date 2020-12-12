using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace Developers.Structure
{
    [Serializable]
    public class DevButton
    {
        public Button button;
        public TextMeshProUGUI text;
    }

    [Serializable]
    public class AnimationButton
    {
        public enum State
        {
            Default = 0,
            Select = 1,
        }

        public Button button;
        Animator animator;
        public Animator Animator {
            get
            {
                if(animator == null)
                {
                    animator = button.GetComponent<Animator> ( );
                }
                return animator;
            }
        }

        public void Initialize()
        {
            animator = button.GetComponent<Animator> ( );
        }

        public void SetAnimation(State state)
        {
            SetAnimation ( (int)state );
        }

        public void SetAnimation(int state)
        {
            Animator.SetTrigger ( "Action" );
            Animator.SetInteger ( "State", state );
        }
    }
}