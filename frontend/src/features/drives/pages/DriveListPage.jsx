import { Download, Grid2X2, List, Plus, RefreshCw, Search } from 'lucide-react';
import { useMemo, useState } from 'react';
import { Link } from 'react-router-dom';
import EmptyState from '../../../components/common/EmptyState';
import { useAppStore } from '../../../store/appStore';
import DriveCard from '../components/DriveCard';
import DriveCardSkeleton from '../components/DriveCardSkeleton';
import DriveErrorState from '../components/DriveErrorState';
import DriveKpiStrip from '../components/DriveKpiStrip';
import DrivePortfolioTable from '../tables/DrivePortfolioTable';
import { DRIVE_STATUSES } from '../constants/driveConstants';
import { useActivateDrive, useArchiveDrive, useCloseDrive, useDrives } from '../hooks/useDriveQueries';

function calculateMetrics(drives) {
  const totalDrives = drives.length;
  const activeDrives = drives.filter((drive) => drive.status === 'Active').length;
  const openRegistrations = drives.reduce((sum, drive) => sum + (drive.currentRegistrationCount || 0), 0);
  const totalBudget = drives.reduce((sum, drive) => sum + (drive.budgetAllocated || 0), 0);
  const totalCertifications = drives.reduce((sum, drive) => sum + (drive.passedCount || 0), 0);
  const passed = drives.reduce((sum, drive) => sum + (drive.passedCount || 0), 0);
  const failed = drives.reduce((sum, drive) => sum + (drive.failedCount || 0), 0);
  const assigned = drives.reduce((sum, drive) => sum + (drive.voucherAssignedCount || 0), 0);
  const redeemed = drives.reduce((sum, drive) => sum + (drive.voucherRedeemedCount || 0), 0);
  const registrations = drives.reduce((sum, drive) => sum + (drive.currentRegistrationCount || 0), 0);

  return {
    totalDrives,
    activeDrives,
    openRegistrations,
    totalBudget,
    totalCertifications,
    overallPassRate: passed + failed ? Math.round((passed / (passed + failed)) * 100) : 0,
    voucherUtilization: registrations ? Math.round(((assigned + redeemed) / registrations) * 100) : 0,
  };
}

function DriveListPage() {
  const { preferences, setPreference } = useAppStore();
  const viewMode = preferences.driveViewMode || 'card';
  const [search, setSearch] = useState('');
  const [status, setStatus] = useState('All');
  const [vendor, setVendor] = useState('All');
  const [sortBy, setSortBy] = useState('newest');
  const drivesQuery = useDrives();
  const activateDrive = useActivateDrive();
  const closeDrive = useCloseDrive();
  const archiveDrive = useArchiveDrive();

  const drives = drivesQuery.data || [];
  const vendors = ['All', ...new Set(drives.map((drive) => drive.vendorName))];

  const filteredDrives = useMemo(() => {
    const term = search.toLowerCase();
    return drives
      .filter((drive) => {
        const haystack = `${drive.driveName} ${drive.certificationName} ${drive.sponsorName} ${drive.vendorName} ${drive.driveCode}`.toLowerCase();
        return haystack.includes(term);
      })
      .filter((drive) => (status === 'All' ? true : drive.status === status))
      .filter((drive) => (vendor === 'All' ? true : drive.vendorName === vendor))
      .sort((a, b) => {
        if (sortBy === 'oldest') return new Date(a.createdDate) - new Date(b.createdDate);
        if (sortBy === 'name') return a.driveName.localeCompare(b.driveName);
        if (sortBy === 'status') return a.status.localeCompare(b.status);
        if (sortBy === 'budget') return (b.budgetAllocated || 0) - (a.budgetAllocated || 0);
        if (sortBy === 'passRate') return (b.passRate || 0) - (a.passRate || 0);
        if (sortBy === 'registrations') return (b.currentRegistrationCount || 0) - (a.currentRegistrationCount || 0);
        return new Date(b.createdDate) - new Date(a.createdDate);
      });
  }, [drives, search, sortBy, status, vendor]);

  const exportDrives = () => {
    const rows = [['Drive', 'Code', 'Vendor', 'Status'], ...filteredDrives.map((drive) => [drive.driveName, drive.driveCode, drive.vendorName, drive.status])];
    const blob = new Blob([rows.map((row) => row.join(',')).join('\n')], { type: 'text/csv' });
    const url = URL.createObjectURL(blob);
    const anchor = document.createElement('a');
    anchor.href = url;
    anchor.download = 'certification-drives.csv';
    anchor.click();
    URL.revokeObjectURL(url);
  };

  if (drivesQuery.isError) {
    return <DriveErrorState error={drivesQuery.error} onRetry={drivesQuery.refetch} />;
  }

  return (
    <div className="space-y-5">
      <section className="flex flex-col gap-4 rounded-card border border-slate-200 bg-white p-5 shadow-sm lg:flex-row lg:items-center lg:justify-between">
        <div>
          <h1 className="text-2xl font-semibold text-slate-950">Certification Drives</h1>
          <p className="mt-1 text-sm text-slate-500">Manage and monitor certification initiatives.</p>
        </div>
        <div className="flex flex-wrap items-center gap-2">
          <Link to="/drives/new" className="inline-flex items-center gap-2 rounded-md bg-primary px-3 py-2 text-sm font-semibold text-white">
            <Plus className="h-4 w-4" /> Launch Drive
          </Link>
          <button type="button" className="inline-flex items-center gap-2 rounded-md border border-slate-300 px-3 py-2 text-sm" onClick={exportDrives}>
            <Download className="h-4 w-4" /> Export
          </button>
          <button type="button" className="inline-flex items-center gap-2 rounded-md border border-slate-300 px-3 py-2 text-sm" onClick={drivesQuery.refetch}>
            <RefreshCw className="h-4 w-4" /> Refresh
          </button>
        </div>
      </section>

      <DriveKpiStrip metrics={calculateMetrics(drives)} />

      <section className="rounded-card border border-slate-200 bg-white p-4 shadow-sm">
        <div className="grid gap-3 lg:grid-cols-[1fr_repeat(4,auto)] lg:items-center">
          <label className="inline-flex items-center gap-2 rounded-md border border-slate-300 px-3 py-2 text-sm text-slate-500">
            <Search className="h-4 w-4" />
            <input className="w-full outline-none" placeholder="Search drives, vendors, sponsors, codes" value={search} onChange={(event) => setSearch(event.target.value)} />
          </label>
          <select className="rounded-md border border-slate-300 px-3 py-2 text-sm" value={status} onChange={(event) => setStatus(event.target.value)}>
            <option>All</option>
            {DRIVE_STATUSES.map((option) => <option key={option}>{option}</option>)}
          </select>
          <select className="rounded-md border border-slate-300 px-3 py-2 text-sm" value={vendor} onChange={(event) => setVendor(event.target.value)}>
            {vendors.map((option) => <option key={option}>{option}</option>)}
          </select>
          <select className="rounded-md border border-slate-300 px-3 py-2 text-sm" value={sortBy} onChange={(event) => setSortBy(event.target.value)}>
            <option value="newest">Newest</option>
            <option value="oldest">Oldest</option>
            <option value="name">Name</option>
            <option value="status">Status</option>
            <option value="budget">Budget</option>
            <option value="passRate">Pass Rate</option>
            <option value="registrations">Registrations</option>
          </select>
          <div className="flex rounded-md border border-slate-300 p-1">
            <button type="button" className={`rounded px-2 py-1 ${viewMode === 'card' ? 'bg-primary text-white' : 'text-slate-600'}`} onClick={() => setPreference('driveViewMode', 'card')} aria-label="Card view">
              <Grid2X2 className="h-4 w-4" />
            </button>
            <button type="button" className={`rounded px-2 py-1 ${viewMode === 'table' ? 'bg-primary text-white' : 'text-slate-600'}`} onClick={() => setPreference('driveViewMode', 'table')} aria-label="Table view">
              <List className="h-4 w-4" />
            </button>
          </div>
        </div>
      </section>

      {drivesQuery.isLoading ? (
        <div className="grid gap-4 xl:grid-cols-3">
          <DriveCardSkeleton />
          <DriveCardSkeleton />
          <DriveCardSkeleton />
        </div>
      ) : filteredDrives.length === 0 ? (
        <EmptyState title="No Drives" description="No certification drives match the current filters." />
      ) : viewMode === 'card' ? (
        <div className="grid gap-4 xl:grid-cols-3">
          {filteredDrives.map((drive) => (
            <DriveCard
              key={drive.driveId}
              drive={drive}
              onActivate={(driveId) => activateDrive.mutate(driveId)}
              onClose={(driveId) => closeDrive.mutate(driveId)}
              onArchive={(driveId) => archiveDrive.mutate(driveId)}
            />
          ))}
        </div>
      ) : (
        <DrivePortfolioTable drives={filteredDrives} />
      )}
    </div>
  );
}

export default DriveListPage;
