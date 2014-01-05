using System;
using System.Collections.Generic;
using System.Linq;
using CirclePhysics.Entity;

namespace CirclePhysics.Observers
{
	public class ObserverCollection
	{
		public ObserverCollection(List<IGrouping<IOverlay, Observer<IOverlay>>> observerGroups)
		{
			m_observerGroups = observerGroups;
		}

		public void Add(IGrouping<IOverlay, Observer<IOverlay>> observerGroup)
		{
			IGrouping<IOverlay, Observer<IOverlay>> groupWithSameKey = m_observerGroups.SingleOrDefault(og => object.ReferenceEquals(og.Key, observerGroup.Key));
			if (groupWithSameKey == null)
				m_observerGroups.Add(observerGroup);
			else
				groupWithSameKey.Concat(observerGroup);
		}

		public void Add(IOverlay observed, Observer<IOverlay> observer)
		{
			Add(new List<Observer<IOverlay>> { observer }.GroupBy(o => observed).Single());
		}

		public void Update()
		{
			bool deleteGroup;
			foreach (IGrouping<IOverlay, Observer<IOverlay>> observerGroup in m_observerGroups)
			{
				deleteGroup = !observerGroup.Any();

				foreach (Observer<IOverlay> observer in observerGroup)
				{
					deleteGroup = observer.IsDeleted();
					if (!deleteGroup)
					{
						observer.Update();
						deleteGroup = observer.IsDeleted();
					}
				}

				if (deleteGroup)
					m_observerGroups.Remove(observerGroup);
			}
		}

		private List<IGrouping<IOverlay, Observer<IOverlay>>> m_observerGroups;
	}
}
