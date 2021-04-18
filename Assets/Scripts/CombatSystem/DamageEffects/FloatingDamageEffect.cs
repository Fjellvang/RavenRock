using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.CombatSystem.DamageEffects
{
	public class FloatingDamageEffect : MonoBehaviour, IDamageEffect
	{
		public Transform TextAnchor;
		public FloatingDamageText FloatingDamageText;
		public bool CriticalOnly() => false;

		public void OnTakeDamage(GameObject attacker, float damage)
		{
			var randomCircle = UnityEngine.Random.insideUnitCircle * .5f;
			var floatingtextObject = Instantiate(FloatingDamageText, TextAnchor.position + (Vector3)randomCircle, Quaternion.identity);
			floatingtextObject.textMesh.text = damage.ToString();
		}
	}

	public interface IDamageEffect
	{
		//apply effect only if critifcal
		bool CriticalOnly();
		void OnTakeDamage(GameObject attacker, float damage);
	}
}
