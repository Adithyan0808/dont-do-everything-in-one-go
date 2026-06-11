import DriveFunnelChart from '../widgets/DriveFunnelChart';
import DriveActivityTimeline from '../widgets/DriveActivityTimeline';
import { mockDriveAudit } from '../services/driveMockData';
import { formatCurrency, formatPercent, getBudgetUtilization } from '../utils/driveFormatters';

function InfoCard({ title, items }) {
  return (
    <article className="rounded-card border border-slate-200 bg-white p-5 shadow-sm">
      <h3 className="font-semibold text-slate-950">{title}</h3>
      <dl className="mt-3 space-y-2 text-sm">
        {items.map(([label, value]) => (
          <div key={label} className="flex justify-between gap-3">
            <dt className="text-slate-500">{label}</dt>
            <dd className="text-right font-medium text-slate-900">{value}</dd>
          </div>
        ))}
      </dl>
    </article>
  );
}

function DriveOverviewTab({ drive }) {
  const kpis = [
    ['Registered', drive.currentRegistrationCount],
    ['Approved', drive.approvedCount],
    ['Exam Taken', drive.examTakenCount],
    ['Passed', drive.passedCount],
    ['Failed', drive.failedCount],
    ['Voucher Assigned', drive.voucherAssignedCount],
    ['Voucher Redeemed', drive.voucherRedeemedCount],
  ];

  return (
    <div className="space-y-5">
      <section className="grid gap-3 sm:grid-cols-2 lg:grid-cols-4 2xl:grid-cols-7">
        {kpis.map(([label, value]) => (
          <article key={label} className="rounded-card border border-slate-200 bg-white p-4 shadow-sm">
            <p className="text-xs font-medium uppercase text-slate-500">{label}</p>
            <p className="mt-2 text-2xl font-semibold text-slate-950">{value || 0}</p>
          </article>
        ))}
      </section>
      <section className="grid gap-4 lg:grid-cols-4">
        <InfoCard title="Drive Metadata" items={[['Sponsor', drive.sponsorName], ['Business Unit', drive.businessUnit], ['Owner', drive.owner], ['Status', drive.status]]} />
        <InfoCard title="Timeline" items={[['Registration', `${drive.registrationStartDate} - ${drive.registrationEndDate}`], ['Exam Window', `${drive.examWindowStartDate} - ${drive.examWindowEndDate}`], ['Closure', drive.closureDate]]} />
        <InfoCard title="Budget" items={[['Allocated', formatCurrency(drive.budgetAllocated)], ['Consumed', formatCurrency(drive.budgetConsumed)], ['Utilization', formatPercent(getBudgetUtilization(drive))]]} />
        <InfoCard title="Certification" items={[['Vendor', drive.vendorName], ['Certification', drive.certificationName], ['Level', drive.certificationLevel], ['Pass Rate', formatPercent(drive.passRate)]]} />
      </section>
      <div className="grid gap-5 xl:grid-cols-2">
        <DriveFunnelChart drive={drive} />
        <DriveActivityTimeline events={mockDriveAudit} />
      </div>
    </div>
  );
}

export default DriveOverviewTab;
