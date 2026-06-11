import { DECISION_STYLES } from '../constants/eligibilityConstants';

export default function DecisionBanner({ decision }) {
  const styles = DECISION_STYLES[decision] || 'bg-slate-50 border-slate-200 text-slate-700';
  const label = decision || 'Unknown';
  const subtext = {
    Eligible: 'Candidate satisfies all certification drive requirements.',
    ConditionallyEligible: 'Candidate meets most requirements; some conditions require attention.',
    PendingApproval: 'Awaiting manager or approver decision.',
    Rejected: 'Candidate does not meet eligibility requirements.',
    Ineligible: 'Candidate is not eligible for this drive.',
  }[decision] || 'Eligibility state unknown.';

  return (
    <div className={`rounded-card border p-4 ${styles}`}>
      <h2 className="text-lg font-semibold">{label}</h2>
      <p className="mt-1 text-sm">{subtext}</p>
    </div>
  );
}
