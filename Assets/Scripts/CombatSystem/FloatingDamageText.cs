using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.CombatSystem
{
	public class FloatingDamageText : MonoBehaviour
	{
		public TextMesh textMesh;

		public float duration = 2f;
		public float speed = 1f;


		private void Update()
		{
			transform.Translate(Vector3.up * speed * Time.deltaTime); //Todo: Add some horizontal movement ? Maybe with perlinNoise

			duration -= Time.deltaTime;
			if (duration <= 0)
			{
				Destroy(gameObject);
			}
		}
	}
}
