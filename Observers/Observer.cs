using System;
using CirclePhysics.Entity;

namespace CirclePhysics.Observers
{
	public class Observer<TObserved>
		where TObserved : class, IOverlay
	{
		public Observer(Watcher<TObserved> observed, Action<TObserved> action)
		{
			m_observed = observed;
			m_action = action;
		}

		public virtual void Update()
		{
			if (m_observed.TestExpression())
				m_action(m_observed.Watched);
		}

		public virtual bool IsDeleted()
		{
			return m_observed.IsDeleted();
		}

		private Watcher<TObserved> m_observed;
		private Action<TObserved> m_action;
	}
}
