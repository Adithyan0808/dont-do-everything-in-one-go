import { AlertTriangle, CheckCircle2, XCircle } from 'lucide-react';

const iconMap = {
  Pass: CheckCircle2,
  Warning: AlertTriangle,
  Fail: XCircle,
};

const styleMap = {
  Pass: 'border-emerald-200 bg-emerald-50 text-emerald-800',
  Warning: 'border-amber-200 bg-amber-50 text-amber-800',
  Fail: 'border-red-200 bg-red-50 text-red-800',
};

function EligibilityPreviewWidget({ checks }) {
  return (
    <section className="rounded-card border border-slate-200 bg-white p-5 shadow-sm">
      <h2 className="text-lg font-semibold text-slate-950">Eligibility Preview</h2>
      <div className="mt-4 space-y-3">
        {checks.map((check) => {
          const Icon = iconMap[check.status] || AlertTriangle;
          return (
            <article key={check.ruleName} className={`rounded-card border p-3 ${styleMap[check.status] || styleMap.Warning}`}>
              <div className="flex items-start gap-3">
                <Icon className="mt-0.5 h-4 w-4" />
                <div>
                  <p className="text-sm font-semibold">{check.ruleName}</p>
                  <p className="mt-1 text-sm opacity-90">{check.reason}</p>
                </div>
              </div>
            </article>
          );
        })}
      </div>
    </section>
  );
}

export default EligibilityPreviewWidget;
