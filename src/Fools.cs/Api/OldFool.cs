﻿// OldFool.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using Fools.cs.Utilities;
using Fools.cs.builtins;

namespace Fools.cs.Api
{
	public class OldFool : MissionOperator
	{
		[NotNull] private readonly MissionSpecification _mission;
		[NotNull] private readonly MissionControl _mission_control;

		public OldFool([NotNull] MissionSpecification mission, [NotNull] MissionControl mission_control)
		{
			_mission = mission;
			_mission_control = mission_control;
		}

		public void schedule_active_missions()
		{
			_mission.execute(this);
		}

		public void execute(SinglePartMission mission)
		{
			if (mission.is_complete) return;
			_mission_control.schedule(() => {
					mission.operation();
					mission.is_complete = true;
				});
		}

		public void execute(SequentialMission mission)
		{
			var next_mission = mission.next_mission;
			if (next_mission == null) return;
			execute(next_mission);
		}
	}
}
