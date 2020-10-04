using System;
using System.Collections.Generic;
using UnityEngine;

public class BarrackBuilding : Building
{
    [SerializeField] GameObject hologram = null;


    private void Start ( )
    {
        spriteRenderer.enabled = false;
        hologram.SetActive ( true );
    }
}