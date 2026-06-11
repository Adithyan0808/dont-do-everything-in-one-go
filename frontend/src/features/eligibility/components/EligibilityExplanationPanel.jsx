import { getEligibilityGuidance } from '../EligibilityGuidanceEngine';

export default function EligibilityExplanationPanel({ decision }) {
  const text = getEligibilityGuidance(decision);
  return (
    <section className="rounded-card border p-4">
      <h3 className="font-semibold">Eligibility Explanation</h3>
      <p className="mt-2 text-sm text-slate-600">{text}</p>
    </section>
  );
}
