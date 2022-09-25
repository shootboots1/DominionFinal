using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class abilityButton : MonoBehaviour
{
    public enum ButtonAbility
    {
        Duplicate,
        Dismantle,
        Enlarge,
        Rush,
        CellRush
    }

    public abilityManager manager;

    public ButtonAbility buttonAbility;

    public GameObject description;

    // Start is called before the first frame update
    void Start()
    {
        description.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeCurrentAbility()
    {
        if(buttonAbility == ButtonAbility.Duplicate)
        {
            manager.selectedAbility = SelectedAbility.Duplicate;
        }
        if (buttonAbility == ButtonAbility.Dismantle)
        {
            manager.selectedAbility = SelectedAbility.Dismantle;
        }
        if (buttonAbility == ButtonAbility.Enlarge)
        {
            manager.selectedAbility = SelectedAbility.Enlarge;
        }
        if (buttonAbility == ButtonAbility.Rush)
        {
            manager.selectedAbility = SelectedAbility.Rush;
        }
        if (buttonAbility == ButtonAbility.CellRush)
        {
            manager.selectedAbility = SelectedAbility.CellRush;
        }
    }
}
