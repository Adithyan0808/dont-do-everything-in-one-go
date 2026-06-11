import { Check, Circle, X } from 'lucide-react';
import { JOURNEY_STAGES } from '../constants/registrationConstants';

const failedStatuses = ['Rejected', 'Failed', 'Ineligible', 'Cancelled'];

function getStageState(status, stage) {
  if (failedStatuses.includes(status)) return stage.key === 'approval' || stage.key === 'eligibility' || stage.key === 'result' ? 'failed' : 'pending';
  if (stage.statuses.includes(status)) {
    const activeIndex = JOURNEY_STAGES.findLastIndex((item) => item.statuses.includes(status));
    const stageIndex = JOURNEY_STAGES.findIndex((item) => item.key === stage.key);
    if (stageIndex < activeIndex) return 'completed';
    if (stageIndex === activeIndex) return status === 'Certified' ? 'completed' : 'current';
  }
  return 'pending';
}

function RegistrationJourneyTracker({ status }) {
  return (
    <div className="overflow-x-auto">
      <ol className="flex min-w-[720px] items-start gap-3" aria-label="Registration journey tracker">
        {JOURNEY_STAGES.map((stage) => {
          const state = getStageState(status, stage);
          const styles = {
            completed: 'bg-success text-white border-success',
            current: 'bg-blue-600 text-white border-blue-600',
            failed: 'bg-danger text-white border-danger',
            pending: 'bg-white text-slate-400 border-slate-300',
          }[state];
          const Icon = state === 'failed' ? X : state === 'pending' ? Circle : Check;
          return (
            <li key={stage.key} className="flex min-w-28 flex-1 flex-col items-center text-center">
              <span className={`flex h-9 w-9 items-center justify-center rounded-full border ${styles}`}>
                <Icon className="h-4 w-4" />
              </span>
              <span className="mt-2 text-xs font-medium text-slate-700">{stage.label}</span>
            </li>
          );
        })}
      </ol>
    </div>
  );
}

export default RegistrationJourneyTracker;
