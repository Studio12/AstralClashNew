using UnityEngine;
using System.Collections;

public class DrawCameraViewport : MonoBehaviour {
	
	public Color color = Color.yellow;
	
	//Gizmos to show viewport rectangle in Edit mode
	void OnDrawGizmos() {
		Gizmos.color = color;
		Gizmos.DrawLine(GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.0f, 0.0f, GetComponent<Camera>().nearClipPlane)), GetComponent<Camera>().ViewportToWorldPoint(new Vector3(1.0f, 0.0f, GetComponent<Camera>().nearClipPlane)));
		Gizmos.DrawLine(GetComponent<Camera>().ViewportToWorldPoint(new Vector3(1.0f, 0.0f, GetComponent<Camera>().nearClipPlane)), GetComponent<Camera>().ViewportToWorldPoint(new Vector3(1.0f, 1.0f, GetComponent<Camera>().nearClipPlane)));
		Gizmos.DrawLine(GetComponent<Camera>().ViewportToWorldPoint(new Vector3(1.0f, 1.0f, GetComponent<Camera>().nearClipPlane)), GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.0f, 1.0f, GetComponent<Camera>().nearClipPlane)));
		Gizmos.DrawLine(GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.0f, 1.0f, GetComponent<Camera>().nearClipPlane)), GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.0f, 0.0f, GetComponent<Camera>().nearClipPlane)));
		
		Gizmos.color = Color.green;
		var boxCollider2D = GetComponent<BoxCollider2D> ();
		if (boxCollider2D != null) {
			var boxCollider2Dpos = boxCollider2D.transform.position;
			var gizmoPos = new Vector2 (boxCollider2Dpos.x + boxCollider2D.offset.x, boxCollider2Dpos.y + boxCollider2D.offset.y);
			Gizmos.DrawWireCube (gizmoPos, new Vector2 (boxCollider2D.size.x, boxCollider2D.size.y));
		}
	}
}
