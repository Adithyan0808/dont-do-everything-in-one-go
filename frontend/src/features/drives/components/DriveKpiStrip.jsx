import { Award, BadgeDollarSign, ClipboardList, TicketPercent, Trophy, UsersRound } from 'lucide-react';
import { formatCurrency, formatNumber, formatPercent } from '../utils/driveFormatters';

const metricConfig = [
  { key: 'totalDrives', label: 'Total Drives', icon: ClipboardList, format: formatNumber },
  { key: 'activeDrives', label: 'Active Drives', icon: UsersRound, format: formatNumber },
  { key: 'openRegistrations', label: 'Open Registrations', icon: Trophy, format: formatNumber },
  { key: 'totalBudget', label: 'Total Budget', icon: BadgeDollarSign, format: formatCurrency },
  { key: 'totalCertifications', label: 'Certifications', icon: Award, format: formatNumber },
  { key: 'overallPassRate', label: 'Pass Rate', icon: Award, format: formatPercent },
  { key: 'voucherUtilization', label: 'Voucher Utilization', icon: TicketPercent, format: formatPercent },
];

function DriveKpiStrip({ metrics }) {
  return (
    <section className="grid gap-3 sm:grid-cols-2 lg:grid-cols-4 2xl:grid-cols-7">
      {metricConfig.map(({ key, label, icon: Icon, format }) => (
        <article key={key} className="rounded-card border border-slate-200 bg-white p-4 shadow-sm">
          <div className="flex items-center justify-between gap-3">
            <p className="text-xs font-medium uppercase text-slate-500">{label}</p>
            <Icon className="h-4 w-4 text-primary" aria-hidden="true" />
          </div>
          <p className="mt-2 text-xl font-semibold text-slate-950">{format(metrics[key])}</p>
        </article>
      ))}
    </section>
  );
}

export default DriveKpiStrip;
