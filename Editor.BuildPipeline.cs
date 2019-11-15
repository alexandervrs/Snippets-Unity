
/**
 * Editor.BuildPipeline.cs
 * Editor BuildPipeline related snippets for Unity
 */

/* using */
using UnityEditor;
using UnityEngine;


/*
	Note: Editor-only scripts should be placed inside an "Editor" folder
		  The contents of the "Editor" folder will not be included in a Build

		  Otherwise if functionality is need in a runtime script
		  enclose the UnityEditor methods within a #if UNITY_EDITOR flag

		  If UnityEditor methods are used in runtime without the above
		  the game will fail to build!
*/


/* -----------------------------------------
   Build AssetBundles
----------------------------------------- */ 

// note: place script in "Editor" folder
//       also add more menu items for other platforms and changing the platform name (e.g. "StandaloneWindows" to "Android")
//       Different Asset Bundles need to be generated for each target platform
//       Alternatively you can use the "Asset Bundle Browser" package from the Package Manager

// create all asset bundles for a certain platform
[MenuItem("Asset Bundles/Build StandaloneWindows")]
static void BuildAssetBundlesWin()
{
	string assetBundleDirectory = "Assets/StreamingAssets/AssetBundles/StandaloneWindows/";

	if (!Directory.Exists(assetBundleDirectory)) {
		Directory.CreateDirectory(assetBundleDirectory);
	}

	BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.StrictMode | BuildAssetBundleOptions.ForceRebuildAssetBundle, BuildTarget.StandaloneWindows);

	Debug.Log($"Asset Bundles were built in {assetBundleDirectory}");
	
}

// create specific asset bundle for a certain platform
[MenuItem("Asset Bundles/Build Windows Specified")]
static void BuildSpecifiedAssetBundles() 
{

	List<AssetBundleBuild> assetBundleBuilds = new List<AssetBundleBuild>();

	string assetBundleDirectory = "Assets/StreamingAssets/AssetBundles/Windows/";

	if (!Directory.Exists(assetBundleDirectory)) {
		Directory.CreateDirectory(assetBundleDirectory);
	}

	// create a new asset bundle build
	AssetBundleBuild build = new AssetBundleBuild();

	build.assetBundleName = "commonassets"; // build the Asset Bundle labeled as "commonassets"
	build.assetNames      = AssetDatabase.GetAssetPathsFromAssetBundle(build.assetBundleName);

	// add "commonassets" to build list
	assetBundleBuilds.Add(build);

	// build the bundle
	BuildPipeline.BuildAssetBundles(assetBundleDirectory, assetBundleBuilds.ToArray(), BuildAssetBundleOptions.StrictMode, BuildTarget.StandaloneWindows);

	// calculate CRC
	uint crc = 0;
	BuildPipeline.GetCRCForAssetBundle(assetBundleDirectory+build.assetBundleName, out crc);
	if (crc == 0) {
		Debug.LogError($"Asset Bundles were not built successfully");
	} else {
		Debug.Log($"Asset Bundles were built in {assetBundleDirectory}  CRC: {crc}");
	}

	// refresh the asset database so the new files show up in the Editor
	AssetDatabase.Refresh();

}


/* -----------------------------------------
   Switch Platform
----------------------------------------- */
EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, BuildTarget.Android);


/* -----------------------------------------
   Build Game
----------------------------------------- */
// build and run the Windows Player
[MenuItem("Build/Build and Run Windows Player")]
public static void BuildAndRunWindowsPlayer()
{
	
	// note: you can also build the project's Asset Bundles here ...

	BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();

	// specify the included scenes
	//buildPlayerOptions.scenes = new[] { "Assets/Scene1.unity", "Assets/Scene2.unity" };

	buildPlayerOptions.locationPathName = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop)+"/WindowsBuild/MyGame.exe";
	buildPlayerOptions.target  = BuildTarget.StandaloneWindows;
	buildPlayerOptions.options = BuildOptions.ShowBuiltPlayer | BuildOptions.AutoRunPlayer; // build, show folder and run

	BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
	BuildSummary summary = report.summary;

	if (summary.result == BuildResult.Succeeded) {
		Debug.Log("Build succeeded: " + summary.totalSize + " bytes");
	}

	if (summary.result == BuildResult.Failed) {
		Debug.LogError("Build failed with " + summary.totalErrors+" errors");
	}

}
