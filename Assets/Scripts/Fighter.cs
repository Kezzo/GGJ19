﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
	[SerializeField]
	private SpriteRenderer spriteRenderer;
	
	[Serializable]
	private class PoseSetup
	{
		public Pose Pose;
		public Sprite Sprite;
	}

	[SerializeField]
	private List<PoseSetup> poseSetups;

	public Pose CurrentPose;

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
			spriteRenderer.sprite = poseSetupToUse.Sprite;
		}
	}
}