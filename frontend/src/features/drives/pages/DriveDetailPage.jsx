import { Archive, Download, History, Play, Square, Pencil } from 'lucide-react';
import { useState } from 'react';
import { useParams } from 'react-router-dom';
import DriveDetailSkeleton from '../components/DriveDetailSkeleton';
import DriveErrorState from '../components/DriveErrorState';
import DriveStatusBadge from '../components/DriveStatusBadge';
import DriveOverviewTab from '../sections/DriveOverviewTab';
import DriveRegistrationsTable from '../tables/DriveRegistrationsTable';
import DriveAnalyticsDashboard from '../widgets/DriveAnalyticsDashboard';
import DriveAuditTimeline from '../widgets/DriveAuditTimeline';
import DriveVoucherCenter from '../widgets/DriveVoucherCenter';
import {
  useActivateDrive,
  useArchiveDrive,
  useCloseDrive,
  useDrive,
  useDriveAnalytics,
  useDriveRegistrations,
  useDriveVouchers,
} from '../hooks/useDriveQueries';

const tabs = ['Overview', 'Registrations', 'Eligibility', 'Assessments', 'Vouchers', 'Analytics', 'Reports', 'Audit'];

function PlaceholderTab({ title, description }) {
  return (
    <section className="rounded-card border border-slate-200 bg-white p-8 text-center shadow-sm">
      <h2 className="text-lg font-semibold text-slate-950">{title}</h2>
      <p className="mt-2 text-sm text-slate-500">{description}</p>
    </section>
  );
}

function ReportsTab() {
  return (
    <section className="rounded-card border border-slate-200 bg-white p-5 shadow-sm">
      <h2 className="text-lg font-semibold text-slate-950">Reports</h2>
      <p className="mt-1 text-sm text-slate-500">Export drive portfolio, registration, voucher, budget, and outcome reports.</p>
      <div className="mt-4 flex flex-wrap gap-2">
        {['CSV', 'Excel', 'PDF'].map((format) => <button key={format} type="button" className="rounded-md border border-slate-300 px-3 py-2 text-sm">Export {format}</button>)}
      </div>
    </section>
  );
}

function DriveDetailPage() {
  const { driveId } = useParams();
  const [activeTab, setActiveTab] = useState('Overview');
  const driveQuery = useDrive(driveId);
  const registrationsQuery = useDriveRegistrations(driveId);
  const vouchersQuery = useDriveVouchers(driveId);
  const analyticsQuery = useDriveAnalytics(driveId);
  const activateDrive = useActivateDrive();
  const closeDrive = useCloseDrive();
  const archiveDrive = useArchiveDrive();

  if (driveQuery.isLoading) return <DriveDetailSkeleton />;
  if (driveQuery.isError) return <DriveErrorState error={driveQuery.error} onRetry={driveQuery.refetch} />;

  const drive = driveQuery.data;
  const readOnly = drive.status === 'Archived';

  return (
    <div className="space-y-5">
      <section className="rounded-card border border-slate-200 bg-white p-5 shadow-sm">
        <div className="flex flex-col gap-4 lg:flex-row lg:items-start lg:justify-between">
          <div>
            <div className="flex flex-wrap items-center gap-2">
              <DriveStatusBadge status={drive.status} />
              <span className="text-sm text-slate-500">{drive.driveCode}</span>
            </div>
            <h1 className="mt-3 text-2xl font-semibold text-slate-950">{drive.driveName}</h1>
            <p className="mt-1 text-sm text-slate-500">Owner {drive.owner} · Created {drive.createdDate}</p>
          </div>
          <div className="flex flex-wrap gap-2">
            <button type="button" disabled={readOnly} className="inline-flex items-center gap-2 rounded-md border border-slate-300 px-3 py-2 text-sm"><Pencil className="h-4 w-4" /> Edit</button>
            {drive.status === 'Draft' && <button type="button" onClick={() => activateDrive.mutate(drive.driveId)} className="inline-flex items-center gap-2 rounded-md bg-primary px-3 py-2 text-sm font-semibold text-white"><Play className="h-4 w-4" /> Activate</button>}
            {drive.status === 'Active' && <button type="button" onClick={() => closeDrive.mutate(drive.driveId)} className="inline-flex items-center gap-2 rounded-md border border-slate-300 px-3 py-2 text-sm"><Square className="h-4 w-4" /> Close</button>}
            {drive.status === 'Closed' && <button type="button" onClick={() => archiveDrive.mutate(drive.driveId)} className="inline-flex items-center gap-2 rounded-md border border-slate-300 px-3 py-2 text-sm"><Archive className="h-4 w-4" /> Archive</button>}
            <button type="button" className="inline-flex items-center gap-2 rounded-md border border-slate-300 px-3 py-2 text-sm"><Download className="h-4 w-4" /> Export</button>
            <button type="button" onClick={() => setActiveTab('Audit')} className="inline-flex items-center gap-2 rounded-md border border-slate-300 px-3 py-2 text-sm"><History className="h-4 w-4" /> Audit Trail</button>
          </div>
        </div>
      </section>

      <nav className="flex gap-2 overflow-x-auto rounded-card border border-slate-200 bg-white p-2 shadow-sm" aria-label="Drive workspace tabs">
        {tabs.map((tab) => (
          <button key={tab} type="button" className={`shrink-0 rounded-md px-3 py-2 text-sm font-medium ${activeTab === tab ? 'bg-primary text-white' : 'text-slate-600 hover:bg-slate-100'}`} onClick={() => setActiveTab(tab)}>
            {tab}
          </button>
        ))}
      </nav>

      {activeTab === 'Overview' && <DriveOverviewTab drive={drive} />}
      {activeTab === 'Registrations' && <DriveRegistrationsTable query={registrationsQuery} />}
      {activeTab === 'Eligibility' && <PlaceholderTab title="Eligibility" description="Eligibility rules and review queues for this drive." />}
      {activeTab === 'Assessments' && <PlaceholderTab title="Assessments" description="Assessment tracking and imports for this drive." />}
      {activeTab === 'Vouchers' && <DriveVoucherCenter query={vouchersQuery} />}
      {activeTab === 'Analytics' && <DriveAnalyticsDashboard query={analyticsQuery} />}
      {activeTab === 'Reports' && <ReportsTab />}
      {activeTab === 'Audit' && <DriveAuditTimeline />}
    </div>
  );
}

export default DriveDetailPage;
