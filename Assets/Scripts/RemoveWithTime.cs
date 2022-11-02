using System.Collections;
using UnityEngine;

public class RemoveWithTime : MonoBehaviour
{
	[SerializeField] private float time = float.PositiveInfinity;
	
	private IEnumerator Start()
	{
		yield return new WaitForSeconds(time);
		Destroy(gameObject);
	}
}
