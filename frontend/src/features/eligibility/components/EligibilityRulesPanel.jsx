import EligibilityRuleCard from './EligibilityRuleCard';

export default function EligibilityRulesPanel({ rules = [] }) {
  return (
    <section className="space-y-3">
      <h3 className="text-lg font-semibold text-slate-900">Rule Evaluations</h3>
      <div className="space-y-2">
        {rules.map((r) => (
          <EligibilityRuleCard key={r.ruleId} rule={r} />
        ))}
      </div>
    </section>
  );
}
