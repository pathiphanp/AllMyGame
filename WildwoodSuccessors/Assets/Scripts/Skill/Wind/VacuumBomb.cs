using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacuumBomb : VotexWaves
{
    public override void StartSound()
    {
        AudioManager.Instance.PlaySFX("Vacum bomb");
    }
}
