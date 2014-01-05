using System;

namespace CirclePhysics.Entity
{
	public class Health
	{
		public Health(int amount, int timeOutDuration, Action onDeath, Func<IDamager, int> onDamaged = null)
		{
			Amount = amount;
			m_onDeath = onDeath;
			m_onDamaged = onDamaged ?? new Func<IDamager, int>(d => d.Damage.Amount);

			m_timeOutDuration = timeOutDuration;
			m_elapsedTimeOut = 0;
		}

		public int Amount { get; private set; }
		private Action m_onDeath;
		private Func<IDamager, int> m_onDamaged;

		private int m_timeOutDuration;
		private int m_elapsedTimeOut;

		public int Damaged(IDamager damager)
		{
			int damage = 0;
			if (m_elapsedTimeOut != m_timeOutDuration && Amount > 0)
			{
				damage = m_onDamaged(damager);
				Amount -= damage;

				if (Amount <= 0)
					m_onDeath();
				else
					m_elapsedTimeOut = 0;
			}
			else
			{
				++m_elapsedTimeOut;
			}

			return damage;
		}
	}
}
