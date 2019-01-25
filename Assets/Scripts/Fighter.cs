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
		public float Duration;
	}

	[SerializeField]
	private List<PoseSetup> poseSetups;

	public Pose CurrentPose;

	[SerializeField] private float Health = 1;
	private float lastPoseFinishTime = 0f;

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
			lastPoseFinishTime = Time.time + poseSetupToUse.Duration;
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
		if (Time.time <= lastPoseFinishTime)
		{
			return;
		}
		
		for (int i = 0; i < poseSetups.Count; i++)
		{
			if (poseSetups[i].Pose == Pose.WALK1 || 
				poseSetups[i].Pose == Pose.WALK2 || 
				poseSetups[i].Pose == Pose.WALK3)
			{
				if (Input.GetKey(poseSetups[i].KeyCode))
				{
					SetNextWalkPose();
				}
			}
			else
			{
				if (Input.GetKeyDown(poseSetups[i].KeyCode))
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
