using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace PNLib.Utility
{
	public static class CSVDownloader
	{
		private const string SheetId = "";
		private const string URL = "https://docs.google.com/spreadsheets/d/" + SheetId + "/export?format=csv";

		public static IEnumerator DownloadDataEnumerator(Action<string> onDownloadComplete)
		{
			yield return new WaitForEndOfFrame();

			string data;

			using (UnityWebRequest webRequest = UnityWebRequest.Get(URL))
			{
				yield return webRequest.SendWebRequest();

				if (webRequest.result == UnityWebRequest.Result.ConnectionError
					|| webRequest.result == UnityWebRequest.Result.ProtocolError)
				{
					data = PlayerPrefs.GetString("LastData", null);
				}
				else
				{
					data = webRequest.downloadHandler.text;
					PlayerPrefs.SetString("LastData", data);
				}
			}

			onDownloadComplete(data);
		}
	}
}