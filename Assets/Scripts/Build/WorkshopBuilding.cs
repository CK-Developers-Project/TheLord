using System;
using System.Collections.Generic;
using Developers.Structure;

public class WorkshopBuilding : Building
{

    public override void OnSelect ( )
    {
        switch ( info.state )
        {
            case BuildingState.Empty:

                break;
            case BuildingState.Work:

                break;
            case BuildingState.Completion:

                break;
        }
    }

}