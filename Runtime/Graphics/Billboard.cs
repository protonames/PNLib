using PNLib.Utility;
using UnityEngine;

namespace PNLib.Graphics
{
	public class Billboard : MonoBehaviour
	{
		private void LateUpdate()
		{
			transform.forward = Helper.Camera.transform.forward;
		}
	}
}