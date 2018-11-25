
/**
 * Network.cs
 * Networking related snippets for Unity
 */

/* using */
using UnityEngine;
using UnityEngine.Networking; // for UnityWebRequest, SendWebRequest, asyncResult
using System.IO; // for BinaryWriter, FileMode


/* -----------------------------------------
   Request data from server
----------------------------------------- */

// execute the request using GET method
string data = "?name=Alex&score=1300"; //urlencoded data

UnityWebRequest request = UnityWebRequest.Get("http://httpbin.org/get"+data);
request.SendWebRequest().completed += (asyncResult) => {

	if (request.isDone && !request.isNetworkError && !request.isHttpError) {
		
		// success
		Debug.Log("Request success, data: "+request.downloadHandler.text);
		// use JsonUtility.FromJson() to parse the data from request.downloadHandler.text, see Json.cs ...
		
	} else {
		// error ...
		Debug.Log("Request error: "+request.error);
	}

};

// OR 
// execute the request using POST method
string data = "name=Alex&score=1300"; //urlencoded data

// execute the request using POST
UnityWebRequest request = UnityWebRequest.Post("http://httpbin.org/post", data);
request.SendWebRequest().completed += (asyncResult) => {

	if (request.isDone && !request.isNetworkError && !request.isHttpError) {
		
		// success
		Debug.Log("Request success, data: "+request.downloadHandler.text);
		// use JsonUtility.FromJson() to parse the data from request.downloadHandler.text, see Json.cs ...
		
	} else {
		// error ...
		Debug.Log("Request error: "+request.error);
	}

};


/* -----------------------------------------
   Download file from server
----------------------------------------- */

// request the remote file using GET method
UnityWebRequest request = UnityWebRequest.Get("http://httpbin.org/gzip");

request.SendWebRequest().completed += (asyncResult) => {

	if (request.isDone && !request.isNetworkError && !request.isHttpError) {
		
		// success, save the file data on disk
		using (BinaryWriter binaryWriter = new BinaryWriter(File.Open(Application.persistentDataPath+"/downloadedData.zip", FileMode.Create))) {
			binaryWriter.Write(request.downloadHandler.data);
		}
		
		Debug.Log("File Downloaded successfully in: "+Application.persistentDataPath+"/downloadedData.zip");
		
	} else {
		// error ...
		Debug.Log("Error downloading file: "+request.error);
	}

};


