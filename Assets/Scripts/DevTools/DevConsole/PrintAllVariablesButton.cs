using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintAllVariablesButton : MonoBehaviour
{
    VariableManager vm;

    void Start()
    {
        vm = FindAnyObjectByType<VariableManager>();
    }

    public void PrintVariables()
    {
        vm.PrintVariables();
    }
}
