import { ArrowDownRight, ArrowUpRight, Info } from 'lucide-react';
import KpiSkeleton from './KpiSkeleton';

function KpiCard({ title, value, previous, trend, icon: Icon, loading, tone = 'primary', tooltip, onClick }) {
  if (loading) return <KpiSkeleton />;

  const isPositive = trend >= 0;
  const toneClass = {
    primary: 'bg-indigo-50 text-primary',
    success: 'bg-emerald-50 text-success',
    warning: 'bg-amber-50 text-warning',
    danger: 'bg-red-50 text-danger',
  }[tone];

  return (
    <button
      type="button"
      onClick={onClick}
      className="rounded-card border border-slate-200 bg-white p-5 text-left shadow-sm transition hover:border-primary/40 hover:shadow-md focus:outline-none focus:ring-2 focus:ring-primary/30"
      aria-label={`${title}: ${value}`}
    >
      <div className="flex items-start justify-between gap-3">
        <div>
          <div className="flex items-center gap-2 text-sm font-medium text-slate-500">
            <span>{title}</span>
            {tooltip && <Info className="h-4 w-4" aria-label={tooltip} />}
          </div>
          <p className="mt-3 text-3xl font-semibold text-slate-950">{value}</p>
        </div>
        {Icon && (
          <span className={`rounded-card p-3 ${toneClass}`}>
            <Icon className="h-5 w-5" />
          </span>
        )}
      </div>
      <div className="mt-4 flex items-center justify-between text-sm">
        <span className="text-slate-500">Previous {previous}</span>
        <span className={`inline-flex items-center gap-1 font-semibold ${isPositive ? 'text-success' : 'text-danger'}`}>
          {isPositive ? <ArrowUpRight className="h-4 w-4" /> : <ArrowDownRight className="h-4 w-4" />}
          {Math.abs(trend)}%
        </span>
      </div>
    </button>
  );
}

export default KpiCard;
