using System;
using CirclePhysics.Entity;

namespace CirclePhysics.Observers
{
	public class Watcher<TWatched>
		where TWatched : class, IOverlay
	{
		public Watcher(TWatched watched, Func<TWatched, bool> watchFor)
		{
			Watched = watched;
			m_watchFor = watchFor;
		}

		public TWatched Watched { get; private set; }
		private Func<TWatched, bool> m_watchFor;

		public bool TestExpression()
		{
			return m_watchFor(Watched);
		}

		public bool IsDeleted()
		{
			return Watched.IsDeleted;
		}
	}
}
