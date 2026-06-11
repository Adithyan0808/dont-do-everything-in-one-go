import { Archive, BarChart3, Edit, Eye, Lock, Play, Square, Ticket } from 'lucide-react';
import { Link } from 'react-router-dom';
import DriveStatusBadge from './DriveStatusBadge';
import { formatCurrency, formatPercent, getBudgetUtilization } from '../utils/driveFormatters';

function MiniBar({ label, value }) {
  return (
    <div>
      <div className="flex items-center justify-between text-xs text-slate-500">
        <span>{label}</span>
        <span>{formatPercent(value)}</span>
      </div>
      <div className="mt-1 h-2 overflow-hidden rounded-full bg-slate-100">
        <div className="h-full rounded-full bg-primary transition-all" style={{ width: `${Math.min(value || 0, 100)}%` }} />
      </div>
    </div>
  );
}

function DriveCard({ drive, onActivate, onClose, onArchive }) {
  const registrationProgress = drive.targetCount ? Math.round(((drive.currentRegistrationCount || 0) / drive.targetCount) * 100) : 0;
  const archived = drive.status === 'Archived';

  return (
    <article className="rounded-card border border-slate-200 bg-white p-5 shadow-sm">
      <div className="flex items-start justify-between gap-3">
        <div className="min-w-0">
          <div className="flex items-center gap-2">
            <span className="flex h-9 w-9 items-center justify-center rounded-card bg-indigo-50 text-sm font-bold text-primary">
              {drive.vendorName?.slice(0, 2).toUpperCase()}
            </span>
            <DriveStatusBadge status={drive.status} />
          </div>
          <h2 className="mt-4 truncate text-lg font-semibold text-slate-950">{drive.driveName}</h2>
          <p className="mt-1 text-sm text-slate-500">{drive.driveCode} · {drive.certificationName}</p>
        </div>
        {archived && <Lock className="h-5 w-5 text-slate-400" aria-label="Archived read-only drive" />}
      </div>

      <dl className="mt-4 grid grid-cols-2 gap-3 text-sm">
        <div>
          <dt className="text-slate-500">Sponsor</dt>
          <dd className="font-medium text-slate-900">{drive.sponsorName}</dd>
        </div>
        <div>
          <dt className="text-slate-500">Window</dt>
          <dd className="font-medium text-slate-900">{drive.registrationStartDate} - {drive.examWindowEndDate}</dd>
        </div>
      </dl>

      <div className="mt-4 space-y-3">
        <MiniBar label="Registration Progress" value={registrationProgress} />
        <MiniBar label="Budget Utilization" value={getBudgetUtilization(drive)} />
        <MiniBar label="Pass Rate" value={drive.passRate} />
        <MiniBar label="Voucher Utilization" value={drive.voucherUtilization} />
      </div>

      <div className="mt-4 rounded-card bg-slate-50 p-3 text-sm text-slate-600">
        Budget: <span className="font-semibold text-slate-900">{formatCurrency(drive.budgetAllocated)}</span>
      </div>

      <div className="mt-4 flex flex-wrap items-center gap-2">
        <Link to={`/drives/${drive.driveId}`} className="inline-flex items-center gap-1 rounded-md bg-primary px-3 py-2 text-sm font-semibold text-white">
          <Eye className="h-4 w-4" /> View
        </Link>
        <Link to={`/drives/new?from=${drive.driveId}`} className="inline-flex items-center gap-1 rounded-md border border-slate-300 px-3 py-2 text-sm font-medium">
          <Edit className="h-4 w-4" /> Edit
        </Link>
        {drive.status === 'Draft' && <button type="button" onClick={() => onActivate(drive.driveId)} className="rounded-md border border-slate-300 p-2" aria-label="Activate drive"><Play className="h-4 w-4" /></button>}
        {drive.status === 'Active' && <button type="button" onClick={() => onClose(drive.driveId)} className="rounded-md border border-slate-300 p-2" aria-label="Close drive"><Square className="h-4 w-4" /></button>}
        {drive.status === 'Closed' && <button type="button" onClick={() => onArchive(drive.driveId)} className="rounded-md border border-slate-300 p-2" aria-label="Archive drive"><Archive className="h-4 w-4" /></button>}
        <Link to={`/drives/${drive.driveId}/reports`} className="rounded-md border border-slate-300 p-2" aria-label="View reports"><BarChart3 className="h-4 w-4" /></Link>
        <Link to={`/drives/${drive.driveId}/vouchers`} className="rounded-md border border-slate-300 p-2" aria-label="Manage vouchers"><Ticket className="h-4 w-4" /></Link>
      </div>
    </article>
  );
}

export default DriveCard;
