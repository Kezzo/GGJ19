using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fighter : MonoBehaviour
{
	[SerializeField]
	private Image image;
	
	[Serializable]
	private class PoseSetup
	{
		public Pose Pose;
		public Sprite Sprite;
		public KeyCode KeyCode;
	}

	[SerializeField]
	private List<PoseSetup> poseSetups;

	public Pose CurrentPose;

	[SerializeField] private float Health = 1;

	private void Awake()
	{
		SetPose(Pose.IDLE);
	}

	private void SetPose(Pose pose)
	{
		CurrentPose = pose;
		var poseSetupToUse = poseSetups.Find(setup => setup.Pose == pose);

		if (poseSetupToUse == null || poseSetupToUse.Sprite == null)
		{
			Debug.LogError("Pose: " + pose + " is not setup!!! TATU PLIS");
		}
		else
		{
			image.sprite = poseSetupToUse.Sprite;
		}
	}

	public bool TakeDamage( float dmg )
	{
		Health = Math.Max( Health - dmg, 0f );
		return Health == 0f;
	}

	private void Update()
	{
		for (int i = 0; i < poseSetups.Count; i++)
		{
			if(Input.GetKeyDown(poseSetups[i].KeyCode))
			{
				SetPose(poseSetups[i].Pose);
				break;
			}
		}
	}
}
