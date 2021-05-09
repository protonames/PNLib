using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

namespace PNLib.Utility
{
	public class Newsletter : MonoBehaviour
	{
		[SerializeField]
		private TMP_InputField textInput;

		private const string GoogleFormBaseURL = "";
		private const string GoogleFormEntryId = "";

		public void Submit()
		{
			if (!textInput.text.Contains("@"))
				return;

			StartCoroutine(SubmitGoogleFormData(textInput.text));
		}

		private static IEnumerator SubmitGoogleFormData<T>(T dataContainer)
		{
			string jsonData = dataContainer is string ? dataContainer.ToString() : JsonUtility.ToJson(dataContainer);
			WWWForm form = new WWWForm();
			form.AddField(GoogleFormEntryId, jsonData);
			const string URLGFormResponse = GoogleFormBaseURL + "formResponse";
			using UnityWebRequest www = UnityWebRequest.Post(URLGFormResponse, form);
			yield return www.SendWebRequest();
		}
	}
}