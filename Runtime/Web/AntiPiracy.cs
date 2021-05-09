using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

namespace PNLib.Web
{
	public class AntiPiracy : MonoBehaviour
	{
		[SerializeField]
		private string[] allowedHosts =
		{
			"protonam.es",
			"github.io",
			"itch.io",
			"newgrounds.com",
			"ungrounded.net",
			"kongregate.com",
			"konggames.com",
			"itch.zone",
			"v6p9d9t4.ssl.hwcdn.net",
			"file://",
			"localhost",
			"127.0.0.1",
		};

		private const string RedirectURL = "https://i.giphy.com/media/lgcUUCXgC8mEo/giphy.webp";

		private void Start()
		{
			if (!Application.isEditor)
				ValidateURL();
		}

		[DllImport("__Internal")]
		private static extern void ExternalEval(string str);

		private void ValidateURL()
		{
			ValidateURLWithJavaScript();

			if (IsValidURL(allowedHosts) == false)
				Redirect();
		}

		private static void Redirect()
		{
			Application.OpenURL(RedirectURL);
		}

		private static bool IsValidURL(IEnumerable<string> urls)
		{
			return urls.Any(url => Application.absoluteURL.ToLower().Contains(url));
		}

		private static string CompileHosts(IReadOnlyList<string> allowedUrLs)
		{
			StringBuilder urls = new StringBuilder();

			for (int i = 0; i < allowedUrLs.Count; i++)
			{
				urls.Append("(document.location.host.includes('");
				string url = allowedUrLs[i];

				if (url.IndexOf("http://", StringComparison.Ordinal) == 0)
					url = url.Substring(7);
				else if (url.IndexOf("https://", StringComparison.Ordinal) == 0)
					url = url.Substring(8);

				urls.Append(url);
				urls.Append("'))");

				if (i < (allowedUrLs.Count - 1))
					urls.Append(" && ");
			}

			return urls.ToString();
		}

		private void ValidateURLWithJavaScript()
		{
			StringBuilder javascriptTest = new StringBuilder();
			javascriptTest.Append("if (");
			javascriptTest.Append("(document.location.host != 'localhost') && (document.location.host != '')");

			if (allowedHosts.Length > 0)
				javascriptTest.Append(" && ");

			javascriptTest.Append(CompileHosts(allowedHosts));
			javascriptTest.Append("){ document.location='");
			javascriptTest.Append(RedirectURL);
			javascriptTest.Append("'; }");
			ExternalEval(javascriptTest.ToString());
		}
	}
}