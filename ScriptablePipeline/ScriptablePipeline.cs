using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "BuildSettings")]
public class ScriptablePipeline : ScriptableObject
{

            
            public string productName; 
            public string CompanyName;
            public List<string> SceneNames;
            public BuildTarget target;
            public BuildOptions options;
            public LogState logState = LogState.None;
            

            public Image icon; 
            public ScriptingImplementation Implementation;



}

public enum LogState
{
    
    LogsDisabled ,
    LogsEnabled ,
    None
}
