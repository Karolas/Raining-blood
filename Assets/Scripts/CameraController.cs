using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	private Vector3 botLeft = new Vector3(0.0f, 0.0f, -10.0f);
	private Vector3 topRight = new Vector3(0.0f, 0.0f, -10.0f);
	
	private Camera camera;
	private Transform cameraTransform;
	
	// Use this for initialization
	void Start () {
		camera = GetComponent<Camera>();
		cameraTransform = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		
		float minX = float.MaxValue;
		float minY = float.MaxValue;
		float maxX = float.MinValue;
		float maxY = float.MinValue;
		foreach(GameObject player in players) {
			Transform playerPos = player.GetComponent<Transform>();
			if(playerPos.position.x < minX) minX = playerPos.position.x;
			if(playerPos.position.y < minY) minY = playerPos.position.y;
			if(playerPos.position.x > maxX) maxX = playerPos.position.x;
			if(playerPos.position.y > maxY) maxY = playerPos.position.y;
		}
		
		botLeft.x = minX;
		botLeft.y = minY - 2.0f;
		
		topRight.x = maxX;
		topRight.y = maxY + 2.0f;
		
		Debug.Log("Top right:" + topRight.x + "    " + topRight.y);
		Debug.Log("Bot left:" + botLeft.x + "    " + botLeft.y);
		
		Vector3 middlePoint = (topRight + botLeft) * 0.5f;
		middlePoint.z = -10.0f;
		
		cameraTransform.position = middlePoint;
		
		float cameraSizeY = (maxY - minY) / 2;
		float cameraSizeX = (maxX - minX) / camera.aspect;
		Debug.Log("aspect:" + camera.aspect);
		
		if(cameraSizeY > 5.0f || cameraSizeX > 5.0f) {
			camera.orthographicSize = Mathf.Max(cameraSizeY, cameraSizeX);
		}
		else{
			camera.orthographicSize = 5.0f;
		}
	}
}
