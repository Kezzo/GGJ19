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
		CheckInput();
	}

	private void CheckInput()
	{
		for (int i = 0; i < poseSetups.Count; i++)
		{
			if(Input.GetKeyDown(poseSetups[i].KeyCode))
			{
				if (poseSetups[i].Pose == Pose.WALK1)
				{
					SetNextWalkPose();
				}
				else
				{
					SetPose(poseSetups[i].Pose);
				}
			}
		}
	}

	private void SetNextWalkPose()
	{
		switch (CurrentPose)
		{
			case Pose.WALK1:
				SetPose(Pose.WALK2);
				break;
			case Pose.WALK2:
				SetPose(Pose.WALK3);
				break;
			case Pose.WALK3:
				SetPose(Pose.WALK1);
				break;
			default:
				SetPose(Pose.WALK1);
				break;
		}
	}
}
