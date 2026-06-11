import { Link } from 'react-router-dom';
import RegistrationJourneyTracker from './RegistrationJourneyTracker';
import RegistrationStatusBadge from './RegistrationStatusBadge';
import { resolveRegistrationAction } from '../utils/RegistrationActionResolver';
import { getRegistrationGuidance } from '../utils/RegistrationGuidanceEngine';

function Metric({ label, value }) {
  return (
    <div className="rounded-card bg-slate-50 p-3">
      <p className="text-xs font-medium text-slate-500">{label}</p>
      <p className="mt-1 text-sm font-semibold text-slate-900">{value}</p>
    </div>
  );
}

function RegistrationCard({ registration }) {
  const action = resolveRegistrationAction(registration);
  return (
    <article className="rounded-card border border-slate-200 bg-white p-5 shadow-sm">
      <div className="flex flex-col gap-4 lg:flex-row lg:items-start lg:justify-between">
        <div>
          <div className="flex flex-wrap items-center gap-2">
            <RegistrationStatusBadge status={registration.status} />
            <span className="text-sm text-slate-500">{registration.registrationId}</span>
          </div>
          <h2 className="mt-3 text-lg font-semibold text-slate-950">{registration.certificationName}</h2>
          <p className="mt-1 text-sm text-slate-500">{registration.vendorName} · {registration.driveName} · Registered {registration.registeredDate}</p>
        </div>
        <Link to={action.to} className="inline-flex items-center justify-center rounded-md bg-primary px-3 py-2 text-sm font-semibold text-white">
          {action.label}
        </Link>
      </div>
      <div className="mt-5">
        <RegistrationJourneyTracker status={registration.status} />
      </div>
      <div className="mt-5 grid gap-3 sm:grid-cols-2 lg:grid-cols-5">
        <Metric label="Eligibility" value={registration.eligibilityResult} />
        <Metric label="Approval" value={registration.approvalResult} />
        <Metric label="Assessment" value={registration.assessmentResult} />
        <Metric label="Voucher" value={registration.voucherStatus} />
        <Metric label="Certification" value={registration.certificationStatus} />
      </div>
      <p className="mt-4 rounded-card bg-blue-50 p-3 text-sm text-blue-800">{getRegistrationGuidance(registration.status)}</p>
    </article>
  );
}

export default RegistrationCard;
