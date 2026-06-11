import { Link } from 'react-router-dom';
import { Download, ExternalLink, Filter, MoreHorizontal, Search } from 'lucide-react';
import { useMemo, useState } from 'react';
import EmptyState from '../../../components/common/EmptyState';
import ProgressBar from '../components/ProgressBar';
import TableSkeleton from '../components/TableSkeleton';
import DashboardErrorState from '../components/DashboardErrorState';
import { DRIVE_STATUS_STYLES } from '../constants/dashboardConstants';
import { formatCurrency, formatPercent } from '../utils/dashboardFormatters';

const columns = [
  { key: 'driveName', label: 'Drive Name' },
  { key: 'vendorName', label: 'Vendor' },
  { key: 'certificationName', label: 'Certification' },
  { key: 'status', label: 'Status' },
  { key: 'registrationCount', label: 'Registrations' },
  { key: 'approvalCount', label: 'Approvals' },
  { key: 'examCompleted', label: 'Exam Completed' },
  { key: 'passRate', label: 'Pass Rate' },
  { key: 'voucherUtilization', label: 'Voucher Utilization' },
  { key: 'targetAchievement', label: 'Target Achievement' },
  { key: 'budgetUtilization', label: 'Budget Utilization' },
];

function DrivePerformanceTable({ query }) {
  const [search, setSearch] = useState('');
  const [sortKey, setSortKey] = useState('registrationCount');
  const [page, setPage] = useState(1);
  const [selectedRows, setSelectedRows] = useState([]);
  const [visibleColumns, setVisibleColumns] = useState(() => Object.fromEntries(columns.map((column) => [column.key, true])));
  const pageSize = 5;

  const rows = useMemo(() => {
    const data = query.data || [];
    return data
      .filter((drive) => `${drive.driveName} ${drive.vendorName} ${drive.certificationName}`.toLowerCase().includes(search.toLowerCase()))
      .sort((a, b) => (b[sortKey] || 0) - (a[sortKey] || 0));
  }, [query.data, search, sortKey]);

  if (query.isLoading) return <TableSkeleton />;
  if (query.isError) return <DashboardErrorState error={query.error} onRetry={query.refetch} />;

  const pagedRows = rows.slice((page - 1) * pageSize, page * pageSize);
  const pageCount = Math.max(1, Math.ceil(rows.length / pageSize));
  const activeColumns = columns.filter((column) => visibleColumns[column.key]);

  const toggleRow = (driveId) => {
    setSelectedRows((current) => (current.includes(driveId) ? current.filter((id) => id !== driveId) : [...current, driveId]));
  };

  const exportRows = () => {
    const csvRows = [
      activeColumns.map((column) => column.label).join(','),
      ...rows.map((drive) => activeColumns.map((column) => `"${drive[column.key] ?? ''}"`).join(',')),
    ];
    const blob = new Blob([csvRows.join('\n')], { type: 'text/csv' });
    const url = URL.createObjectURL(blob);
    const anchor = document.createElement('a');
    anchor.href = url;
    anchor.download = 'drive-performance.csv';
    anchor.click();
    URL.revokeObjectURL(url);
  };

  return (
    <section className="rounded-card border border-slate-200 bg-white p-5 shadow-sm">
      <div className="flex flex-col gap-3 md:flex-row md:items-center md:justify-between">
        <div>
          <h2 className="text-lg font-semibold text-slate-950">Active Drives Performance</h2>
          <p className="text-sm text-slate-500">Operational management of active certification drives</p>
        </div>
        <div className="flex flex-wrap items-center gap-2">
          <label className="inline-flex items-center gap-2 rounded-md border border-slate-300 px-3 py-2 text-sm text-slate-500">
            <Search className="h-4 w-4" />
            <input className="w-48 outline-none" placeholder="Search drives" value={search} onChange={(event) => setSearch(event.target.value)} />
          </label>
          <select className="rounded-md border border-slate-300 px-3 py-2 text-sm" value={sortKey} onChange={(event) => setSortKey(event.target.value)}>
            <option value="registrationCount">Registration Count</option>
            <option value="passRate">Pass Rate</option>
            <option value="voucherUtilization">Voucher Utilization</option>
            <option value="budgetUtilization">Budget Utilization</option>
          </select>
          <details className="relative">
            <summary className="inline-flex cursor-pointer items-center gap-2 rounded-md border border-slate-300 px-3 py-2 text-sm">
              <Filter className="h-4 w-4" />
              Columns
            </summary>
            <div className="absolute right-0 z-20 mt-2 w-56 rounded-card border border-slate-200 bg-white p-3 shadow-md">
              {columns.map((column) => (
                <label key={column.key} className="flex items-center gap-2 py-1 text-sm text-slate-700">
                  <input
                    type="checkbox"
                    checked={visibleColumns[column.key]}
                    onChange={() => setVisibleColumns((current) => ({ ...current, [column.key]: !current[column.key] }))}
                  />
                  {column.label}
                </label>
              ))}
            </div>
          </details>
          <button type="button" className="inline-flex items-center gap-2 rounded-md border border-slate-300 px-3 py-2 text-sm" onClick={exportRows}>
            <Download className="h-4 w-4" />
            Export
          </button>
        </div>
      </div>
      <div className="mt-4 overflow-x-auto">
        {rows.length === 0 ? (
          <EmptyState title="No Active Drives" description="No certification drives match the current dashboard filters." />
        ) : (
          <table className="min-w-full divide-y divide-slate-200 text-sm">
            <thead className="bg-slate-50 text-left text-xs font-semibold uppercase text-slate-500">
              <tr>
                <th className="px-3 py-3">
                  <input
                    type="checkbox"
                    aria-label="Select all visible drives"
                    checked={pagedRows.length > 0 && pagedRows.every((drive) => selectedRows.includes(drive.driveId))}
                    onChange={(event) => {
                      const ids = pagedRows.map((drive) => drive.driveId);
                      setSelectedRows((current) => (event.target.checked ? [...new Set([...current, ...ids])] : current.filter((id) => !ids.includes(id))));
                    }}
                  />
                </th>
                {activeColumns.map((column) => (
                  <th key={column.key} className="px-3 py-3">{column.label}</th>
                ))}
                <th className="px-3 py-3">Actions</th>
              </tr>
            </thead>
            <tbody className="divide-y divide-slate-100">
              {pagedRows.map((drive) => (
                <tr key={drive.driveId} className="hover:bg-slate-50">
                  <td className="px-3 py-3">
                    <input
                      type="checkbox"
                      aria-label={`Select ${drive.driveName}`}
                      checked={selectedRows.includes(drive.driveId)}
                      onChange={() => toggleRow(drive.driveId)}
                    />
                  </td>
                  {activeColumns.map((column) => {
                    if (column.key === 'driveName') {
                      return (
                        <td key={column.key} className="max-w-xs px-3 py-3 font-medium text-slate-900">
                          <p className="truncate">{drive.driveName}</p>
                          <p className="truncate text-xs font-normal text-slate-500">{drive.certificationName}</p>
                        </td>
                      );
                    }

                    if (column.key === 'status') {
                      return (
                        <td key={column.key} className="px-3 py-3">
                          <span className={`rounded-full px-2 py-1 text-xs font-semibold ${DRIVE_STATUS_STYLES[drive.status] || DRIVE_STATUS_STYLES.Draft}`}>{drive.status}</span>
                        </td>
                      );
                    }

                    if (column.key === 'targetAchievement') {
                      return (
                        <td key={column.key} className="min-w-44 px-3 py-3">
                          <ProgressBar current={drive.registrationCount} target={drive.targetCount} label="Target achievement" />
                        </td>
                      );
                    }

                    if (['passRate', 'voucherUtilization', 'budgetUtilization'].includes(column.key)) {
                      return <td key={column.key} className="px-3 py-3">{formatPercent(drive[column.key])}</td>;
                    }

                    return <td key={column.key} className="px-3 py-3 text-slate-600">{drive[column.key]}</td>;
                  })}
                  <td className="px-3 py-3">
                    <div className="flex items-center gap-2">
                      <Link className="text-primary hover:underline" to={`/drives/${drive.driveId}`} aria-label={`View ${drive.driveName}`}><ExternalLink className="h-4 w-4" /></Link>
                      <Link className="text-primary hover:underline" to={`/drives/${drive.driveId}/registrations`}>Reg</Link>
                      <Link className="text-primary hover:underline" to={`/drives/${drive.driveId}/vouchers`}>Vouchers</Link>
                      <Link className="text-primary hover:underline" to={`/drives/${drive.driveId}/reports`}>Reports</Link>
                      <button type="button" aria-label="View audit trail"><MoreHorizontal className="h-4 w-4 text-slate-500" /></button>
                    </div>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        )}
      </div>
      <div className="mt-4 flex items-center justify-between text-sm text-slate-500">
        <span>{selectedRows.length} selected. Budget shown in reports: {formatCurrency((query.data || []).reduce((sum, drive) => sum + (drive.budgetConsumed || 0), 0))}</span>
        <div className="flex items-center gap-2">
          <button type="button" className="rounded-md border border-slate-300 px-3 py-1" disabled={page === 1} onClick={() => setPage((current) => current - 1)}>Previous</button>
          <span>{page} / {pageCount}</span>
          <button type="button" className="rounded-md border border-slate-300 px-3 py-1" disabled={page === pageCount} onClick={() => setPage((current) => current + 1)}>Next</button>
        </div>
      </div>
    </section>
  );
}

export default DrivePerformanceTable;
