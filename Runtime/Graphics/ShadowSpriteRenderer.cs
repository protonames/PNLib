using UnityEngine;

namespace PNLib.Graphics
{
	public class ShadowSpriteRenderer : MonoBehaviour
	{
		[SerializeField]
		private Color color = new Color(0, 0, 0, .2f);

		[SerializeField]
		private Vector3 offSet = new Vector3(-.125f, -.125f, 0f);

		private SpriteRenderer parentSpriteRenderer;
		private Transform shadow;
		private SpriteRenderer spriteRenderer;

		private void Start()
		{
			shadow = new GameObject().transform;
			shadow.SetParent(transform);
			parentSpriteRenderer = GetComponent<SpriteRenderer>();
			spriteRenderer = shadow.gameObject.AddComponent<SpriteRenderer>();
			spriteRenderer.sprite = parentSpriteRenderer.sprite;
			spriteRenderer.sortingOrder = parentSpriteRenderer.sortingOrder - 1;
			spriteRenderer.color = color;
			Transform myTransform = transform;
			shadow.rotation = myTransform.rotation;
			shadow.localScale = myTransform.localScale;
		}

		private void LateUpdate()
		{
			Vector3 position = transform.position + offSet;
			shadow.position = position;
			spriteRenderer.sprite = parentSpriteRenderer.sprite;
		}
	}
}