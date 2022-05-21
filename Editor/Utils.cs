using System;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;
namespace zimj
{

    [CustomEditor(typeof(ScriptablePipeline))]
    public class Utils : Editor
    {

        public override void OnInspectorGUI()
        {
    
              
            base.OnInspectorGUI();
            var pipeline = (ScriptablePipeline) target; 
            
            GUILayout.BeginVertical();
            GUI.color = Color.green;
            if (GUILayout.Button("Make a build"))
            {
               MakeAbuild(pipeline);
               
            }
            GUI.color = Color.yellow;
            if (GUILayout.Button("SETUP"))
            {
                SetupScene(pipeline);
            }
            GUILayout.EndVertical();//make a build here

        }
        
        private void MakeAbuild(ScriptablePipeline pipeline)
        {
            if (!CheckBasicSetUp(pipeline) || !CheckNameSetup(pipeline))
                return; 
            try
            {
                var buildPlayerOptions = new BuildPlayerOptions
                {
                    scenes = pipeline.SceneNames.ToArray(),
                    locationPathName = EditorUtility.SaveFilePanel("Choose Location of Built Game","D:/Build", "" , "exe"),
                    target = pipeline.target,
                    options = pipeline.options,
                    
                };
                
                StartBuildProcess(ref buildPlayerOptions);
            }
            catch (Exception e)
            {
               Debug.LogError(e. Message);
            }
            
        }

        private void SetupScene(ScriptablePipeline pipeline)
        {
            foreach (var item in EditorBuildSettings.scenes)
            {
                if (pipeline.SceneNames.Contains(item.path))
                    break;
                pipeline.SceneNames.Add(item.path);
            }
            
    
            CheckNameSetup(pipeline);

        }

        //final build process
       private void StartBuildProcess(ref BuildPlayerOptions options)
       { BuildReport report = BuildPipeline.BuildPlayer(options);
           BuildSummary summary = report.summary;
           if (summary.result == BuildResult.Succeeded)
               Debug.Log("Build succeeded" + summary.totalSize);
           if(summary.result ==  BuildResult.Failed )
               Debug.Log("build failed");
           
       }

       private bool CheckNameSetup(ScriptablePipeline pipeline)
       {
           if (string.IsNullOrEmpty(pipeline.productName) || string.IsNullOrEmpty(pipeline.CompanyName))
           {
               Debug.Log("they are empty");
               EditorUtility.DisplayDialog("Empty", "plz enter name ", "OK");
               return false; 
           }
           else
           {
               PlayerSettings.productName = pipeline.productName;
               PlayerSettings.companyName = pipeline.CompanyName;
               return true;
           }
       }
       

       private bool CheckBasicSetUp(ScriptablePipeline pipeline)
       {
           if (pipeline.SceneNames.Count == 0)
           {
               EditorUtility.DisplayDialog("Error", "Please setup your scene", "OK");
               return false;
           }
           else return true; 
           
       }

    }

}