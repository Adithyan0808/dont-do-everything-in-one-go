import { useParams } from 'react-router-dom';
import DecisionBanner from '../components/DecisionBanner';
import EligibilityRulesPanel from '../components/EligibilityRulesPanel';
import EligibilityExplanationPanel from '../components/EligibilityExplanationPanel';
import { useCheckEligibility, useEligibilityRules } from '../hooks/useEligibilityQueries';

export default function EligibilityPage() {
  const { registrationId } = useParams();
  const { data: eligibility, isLoading, error, refetch } = useCheckEligibility(registrationId);
  const { data: rules = [], refetch: refetchRules } = useEligibilityRules(registrationId);

  const handleExport = () => {
    const payload = { eligibility, rules };
    const blob = new Blob([JSON.stringify(payload, null, 2)], { type: 'application/json' });
    const url = URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.href = url;
    a.download = `eligibility-${registrationId || 'export'}.json`;
    a.click();
    URL.revokeObjectURL(url);
  };

  if (isLoading) return <div className="rounded-card p-4">Loading eligibility...</div>;
  if (error) return <div className="rounded-card p-4 text-red-700">Unable to load eligibility data.</div>;

  return (
    <div className="space-y-5">
      <section className="rounded-card border border-slate-200 bg-white p-5 shadow-sm">
        <div className="flex items-center justify-between">
          <div>
            <h1 className="text-2xl font-semibold text-slate-950">Eligibility Evaluation</h1>
            <p className="mt-1 text-sm text-slate-500">Registration decision engine results</p>
          </div>
          <div className="flex items-center gap-2">
            <button type="button" className="rounded-md border px-3 py-2 text-sm" onClick={() => refetch()}>Refresh Evaluation</button>
            <button type="button" className="rounded-md border px-3 py-2 text-sm" onClick={() => refetchRules()}>Re-run Eligibility</button>
            <button type="button" className="rounded-md bg-primary px-3 py-2 text-sm font-semibold text-white" onClick={handleExport}>Export Evaluation</button>
          </div>
        </div>
      </section>

      <section className="grid gap-4 lg:grid-cols-3">
        <div className="lg:col-span-2 space-y-4">
          <DecisionBanner decision={eligibility?.overallDecision} />
          <EligibilityRulesPanel rules={eligibility?.rules || rules} />
        </div>

        <aside className="space-y-4">
          <EligibilityExplanationPanel decision={eligibility?.overallDecision} />
        </aside>
      </section>
    </div>
  );
}
