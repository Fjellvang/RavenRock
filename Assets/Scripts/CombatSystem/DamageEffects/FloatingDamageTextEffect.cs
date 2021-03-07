using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.CombatSystem.DamageEffects
{
	[CreateAssetMenu(fileName = nameof(FloatingDamageTextEffect) + ".asset", menuName = "ScriptableObjects/" + nameof(FloatingDamageTextEffect) + "ScriptableObject")]
	public class FloatingDamageTextEffect : DamageEffect
	{
		public FloatingDamageText FloatingDamageText;
		public override void OnTakeDamage(GameObject defender, GameObject attacker)
		{
			var floatingtextObject = Instantiate(FloatingDamageText, defender.transform.position, Quaternion.identity);
			floatingtextObject.textMesh.text = "Ouch";
		}
	}
}
