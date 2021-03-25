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

		public void OnTakeDamage(GameObject defender, GameObject attacker)
		{
			var floatingtextObject = Instantiate(FloatingDamageText, TextAnchor.position, Quaternion.identity);
			floatingtextObject.textMesh.text = "Ouch";
		}
	}

	public interface IDamageEffect
	{
		//apply effect only if critifcal
		bool CriticalOnly();
		void OnTakeDamage(GameObject defender, GameObject attacker);
	}
}
