import { Link } from 'react-router-dom';
import DriveStatusBadge from '../components/DriveStatusBadge';
import { formatCurrency, formatPercent, getBudgetUtilization } from '../utils/driveFormatters';

function DrivePortfolioTable({ drives }) {
  return (
    <section className="overflow-hidden rounded-card border border-slate-200 bg-white shadow-sm">
      <div className="overflow-x-auto">
        <table className="min-w-full divide-y divide-slate-200 text-sm">
          <thead className="bg-slate-50 text-left text-xs font-semibold uppercase text-slate-500">
            <tr>
              <th className="px-4 py-3">Drive</th>
              <th className="px-4 py-3">Vendor</th>
              <th className="px-4 py-3">Sponsor</th>
              <th className="px-4 py-3">Status</th>
              <th className="px-4 py-3">Registrations</th>
              <th className="px-4 py-3">Budget</th>
              <th className="px-4 py-3">Pass Rate</th>
              <th className="px-4 py-3">Voucher Utilization</th>
              <th className="px-4 py-3">Actions</th>
            </tr>
          </thead>
          <tbody className="divide-y divide-slate-100">
            {drives.map((drive) => (
              <tr key={drive.driveId} className="hover:bg-slate-50">
                <td className="max-w-sm px-4 py-3">
                  <p className="truncate font-semibold text-slate-900">{drive.driveName}</p>
                  <p className="truncate text-xs text-slate-500">{drive.driveCode} · {drive.certificationName}</p>
                </td>
                <td className="px-4 py-3 text-slate-600">{drive.vendorName}</td>
                <td className="px-4 py-3 text-slate-600">{drive.sponsorName}</td>
                <td className="px-4 py-3"><DriveStatusBadge status={drive.status} /></td>
                <td className="px-4 py-3">{drive.currentRegistrationCount}/{drive.targetCount}</td>
                <td className="px-4 py-3">{formatCurrency(drive.budgetAllocated)} · {formatPercent(getBudgetUtilization(drive))}</td>
                <td className="px-4 py-3">{formatPercent(drive.passRate)}</td>
                <td className="px-4 py-3">{formatPercent(drive.voucherUtilization)}</td>
                <td className="px-4 py-3">
                  <Link className="font-medium text-primary hover:underline" to={`/drives/${drive.driveId}`}>Open</Link>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </section>
  );
}

export default DrivePortfolioTable;
