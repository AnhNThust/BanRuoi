﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BulletPro;

// This script is supported by the BulletPro package for Unity.
// Template author : Simon Albou <albou.simon@gmail.com>

namespace BulletPro.DemoScripts
{
	public class BPDemo_AsteroidBehaviour : BaseBulletBehaviour
	{
		// You can access this.bullet to get the parent bullet script.
		// After bullet's death, you can delay this script's death : use this.lifetimeAfterBulletDeath.

		public BulletReceiver receiver;

		// Split this bullet by firing mini-asteroids when hit.
		public void OnAsteroidHit()
		{
			if (bullet.modulePatterns.isEnabled)
				bullet.modulePatterns.Play();
			
			StartCoroutine(DieNextFrame());
		}

		IEnumerator DieNextFrame()
		{
			yield return new WaitForEndOfFrame();
			yield return new WaitForEndOfFrame();
			bullet.Die();
		}

		// Use this for initialization (instead of Start)
		public override void OnBulletBirth ()
		{
			base.OnBulletBirth();

			// Your code here
			receiver.enabled = true;
			bullet.moduleRenderer.SetFrame(Random.Range(0, bullet.moduleRenderer.animationSprites.Length));
		}
		
		// Update is (still) called once per frame
		public override void Update ()
		{
			base.Update();

			// Your code here
		}

		// This gets called when the bullet dies
		public override void OnBulletDeath()
		{
			base.OnBulletDeath();

			// Your code here
			receiver.enabled = false;
		}

		// This gets called after the bullet has died, it can be delayed.
		public override void OnBehaviourDeath()
		{
			base.OnBehaviourDeath();

			// Your code here
		}

		// This gets called whenever the bullet collides with a BulletReceiver. The most common callback.
		public override void OnBulletCollision(BulletReceiver br, Vector3 collisionPoint)
		{
			base.OnBulletCollision(br, collisionPoint);

			// Your code here
		}

		// This gets called whenever the bullet collides with a BulletReceiver AND was not colliding during the previous frame.
		public override void OnBulletCollisionEnter(BulletReceiver br, Vector3 collisionPoint)
		{
			base.OnBulletCollisionEnter(br, collisionPoint);

			// Your code here
		}

		// This gets called whenever the bullet stops colliding with any BulletReceiver.
		public override void OnBulletCollisionExit()
		{
			base.OnBulletCollisionExit();

			// Your code here
		}

		// This gets called whenever the bullet shoots a pattern.
		public override void OnBulletShotAnotherBullet(int patternIndex)
		{
			base.OnBulletShotAnotherBullet(patternIndex);

			// Your code here
		}
	}
}