using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;
using System.Diagnostics;

namespace UnityToolbarExtender.Examples
{
	static class ToolbarStyles
	{
		public static readonly GUIStyle commandButtonStyle;

		static ToolbarStyles()
		{
			commandButtonStyle = new GUIStyle("Command")
			{
				fontSize = 16,
				alignment = TextAnchor.MiddleCenter,
				imagePosition = ImagePosition.ImageAbove,
				fontStyle = FontStyle.Bold
			};
		}
	}


	[InitializeOnLoad]
	public class GameRunnerLeftButton
	{
        static void BuildGame()
        {
            // Define the build target and path
            BuildTarget buildTarget = BuildTarget.StandaloneWindows; // Change to your desired target platform
            string buildPath = "Build/CartoonCombat_MultiPlayer.exe"; // Change to your desired output path and filename

            // Specify the scene path to build
            string scenePath = "Assets/Scenes/GameScene.unity"; // Change to the path of your desired scene

            // Ensure that the scene path is valid
            if (SceneManager.GetSceneByPath(scenePath) != null)
            {
                // Build the project with the specified scene
                BuildPipeline.BuildPlayer(
                    new[] { scenePath },
                    buildPath,
                    buildTarget,
                    BuildOptions.None
                );

                UnityEngine.Debug.Log("Build completed successfully!");
            }
            else
            {
                UnityEngine.Debug.LogError("The specified scene does not exist: " + scenePath);
            }
        }

		static GameRunnerLeftButton()
		{
			ToolbarExtender.LeftToolbarGUI.Add(OnToolbarGUI);
		}

		static void OnToolbarGUI()
		{
			GUILayout.FlexibleSpace();

			if(GUILayout.Button(new GUIContent("1", "Run a single instance of the game"), ToolbarStyles.commandButtonStyle))
			{
				BuildGame();
                UnityEngine.Debug.LogWarning(Environment.CurrentDirectory);
				Process.Start(Environment.CurrentDirectory+ "\\Build\\CartoonCombat_MultiPlayer.exe");

            }

			if(GUILayout.Button(new GUIContent("xN", "Run any number of  instances of the game"), ToolbarStyles.commandButtonStyle))
			{
                BuildGame();
                UnityEngine.Debug.LogWarning(Environment.CurrentDirectory);
                Process.Start(Environment.CurrentDirectory + "\\Build\\runGame.bat");
            }
        }
	}
}