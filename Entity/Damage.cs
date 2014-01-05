using System;

namespace CirclePhysics.Entity
{
	public class Damage
	{
		public Damage(int amount, IDamager damager, Action onDamaging = null, Action<IDamageable> onKill = null)
		{
			Amount = amount;
			m_damager = damager;
			m_onDamaging = onDamaging;
			m_onKill = onKill;
		}

		public int Amount { get; private set; }
		private IDamager m_damager;
		private Action m_onDamaging;
		private Action<IDamageable> m_onKill;

		public void Damaging(IDamageable damageable)
		{
			int amountDealt = damageable.Health.Damaged(m_damager);
			if (damageable.Health.Amount <= 0 && m_onKill != null)
				m_onKill(damageable);
			else if (m_onDamaging != null)
				m_onDamaging();
		}
	}
}
