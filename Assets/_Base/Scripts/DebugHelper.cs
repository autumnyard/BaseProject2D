using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugHelper : MonoBehaviour
{

	ManagerCamera	managerCamera;
	ManagerEntity	managerEntity;
	ManagerMap		managerMap;


	void Start()
	{
		managerCamera = Director.Instance.managerCamera;
		managerEntity = Director.Instance.managerEntity;
		managerMap = Director.Instance.managerMap;
	}


	#region Lets just make this easy
	public void SetCameraToFollow()
	{
		Director.Instance.managerCamera.cameras[0].SetFollow( managerEntity.playersScript[0].transform );
	}

	public void SetCameraToFixedAxisHorizontal()
	{
		Director.Instance.managerCamera.cameras[0].SetFixedAxis( managerEntity.playersScript[0].transform, true );
	}

	public void SetCameraToFixedAxisVertical()
	{
		Director.Instance.managerCamera.cameras[0].SetFixedAxis( managerEntity.playersScript[0].transform, false );
	}
	#endregion

	#region Lets make this rock
	public void SwitchToLevel1()
	{
		managerMap.LoadMap( 0 );
	}

	public void SwitchToLevel2()
	{
		managerMap.LoadMap( 1 );
	}
	#endregion
}
