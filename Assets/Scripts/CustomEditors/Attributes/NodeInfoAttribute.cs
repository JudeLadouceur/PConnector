using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeInfoAttribute : Attribute
{
    private string m_nodeTitle;
    private string m_menuItem;
    private bool m_hasFlowInput;
    private bool m_hasMultipleFlowInputs;
    private bool m_hasFlowOutput;
    private bool m_hasMultipleFlowOutputs;

    public string title => m_nodeTitle;
    public string menuItem => m_menuItem;
    public bool hasFlowInput => m_hasFlowInput;
    public bool hasMultipleFlowInputs => m_hasMultipleFlowInputs;
    public bool hasFlowOutput => m_hasFlowOutput;
    public bool hasMultipleFlowOutputs => m_hasMultipleFlowOutputs;


    public NodeInfoAttribute(string title, string menuItem = "", bool hasFlowInput = true, bool hasMultipleFlowInputs = false, bool hasFlowOuput = true, bool hasMultipleFlowOutputs = false)
    {
        m_nodeTitle = title;
        m_menuItem = menuItem;
        m_hasFlowInput = hasFlowInput;
        m_hasMultipleFlowInputs = hasMultipleFlowInputs;
        m_hasFlowOutput = hasFlowOuput;
        m_hasMultipleFlowOutputs = hasMultipleFlowOutputs;
    }
}
