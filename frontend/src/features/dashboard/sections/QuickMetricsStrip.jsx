import { formatPercent } from '../utils/dashboardFormatters';

const metricLabels = {
  issuedThisMonth: 'Certifications Issued This Month',
  averageApprovalTime: 'Average Approval Time',
  averageAssessmentScore: 'Average Assessment Score',
  openRegistrations: 'Open Registrations',
  pendingApprovals: 'Pending Approvals',
  slaCompliance: 'SLA Compliance',
  expiredVouchers: 'Expired Vouchers',
  budgetUtilization: 'Budget Utilization',
};

function QuickMetricsStrip({ metrics }) {
  return (
    <section className="grid gap-3 rounded-card border border-slate-200 bg-white p-4 shadow-sm sm:grid-cols-2 lg:grid-cols-4 2xl:grid-cols-8">
      {Object.entries(metricLabels).map(([key, label]) => (
        <div key={key} className="min-w-0">
          <p className="truncate text-xs font-medium text-slate-500">{label}</p>
          <p className="mt-1 text-lg font-semibold text-slate-950">
            {key.includes('Compliance') || key.includes('Utilization') ? formatPercent(metrics?.[key]) : metrics?.[key] ?? 0}
          </p>
        </div>
      ))}
    </section>
  );
}

export default QuickMetricsStrip;
