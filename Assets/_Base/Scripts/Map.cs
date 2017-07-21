using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
	public List<Vector2> players;
	public List<Vector2> cameraGrabs;

	private void Awake()
	{
		//First, get all the thingies
		MapThingie[] temp = GetComponentsInChildren<MapThingie>();

		// Populate the players
		// Populate the camera grabs
		foreach( MapThingie item in temp )
		{
			switch( item.type )
			{
				case MapThingie.Type.CameraGrab:
					players.Add( item.transform.position );
					break;

				case MapThingie.Type.Player:
					cameraGrabs.Add( item.transform.position );
					break;

				default:
					break;
			}

		}

		// Populate the obstacles, enemies, items, whatever
	}
}
