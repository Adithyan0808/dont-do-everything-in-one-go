import { useMemo, useState } from 'react';
import EmptyState from '../../../components/common/EmptyState';
import DriveErrorState from '../components/DriveErrorState';
import DriveTableSkeleton from '../components/DriveTableSkeleton';

const columns = ['Employee', 'Department', 'Manager', 'Registration Date', 'Status', 'Eligibility', 'Assessment', 'Voucher', 'Certification'];

function DriveRegistrationsTable({ query }) {
  const [search, setSearch] = useState('');
  const [selected, setSelected] = useState([]);
  const rows = useMemo(() => (query.data || []).filter((row) => `${row.employee} ${row.department} ${row.manager}`.toLowerCase().includes(search.toLowerCase())), [query.data, search]);

  if (query.isLoading) return <DriveTableSkeleton />;
  if (query.isError) return <DriveErrorState error={query.error} onRetry={query.refetch} />;

  return (
    <section className="rounded-card border border-slate-200 bg-white p-5 shadow-sm">
      <div className="flex flex-col gap-3 lg:flex-row lg:items-center lg:justify-between">
        <div>
          <h2 className="text-lg font-semibold text-slate-950">Registrations</h2>
          <p className="text-sm text-slate-500">Sort, filter, export, and execute bulk actions.</p>
        </div>
        <div className="flex flex-wrap gap-2">
          {['Approve', 'Reject', 'Export', 'Assign Voucher', 'Send Notification'].map((action) => (
            <button key={action} type="button" className="rounded-md border border-slate-300 px-3 py-2 text-sm">{action}</button>
          ))}
        </div>
      </div>
      <input className="mt-4 w-full rounded-md border border-slate-300 px-3 py-2 text-sm" placeholder="Filter registrations" value={search} onChange={(event) => setSearch(event.target.value)} />
      {rows.length === 0 ? (
        <div className="mt-4"><EmptyState title="No Registrations" description="No registrations match the current filters." /></div>
      ) : (
        <div className="mt-4 overflow-x-auto">
          <table className="min-w-full divide-y divide-slate-200 text-sm">
            <thead className="bg-slate-50 text-left text-xs font-semibold uppercase text-slate-500">
              <tr>
                <th className="px-3 py-3"><input type="checkbox" aria-label="Select all registrations" onChange={(event) => setSelected(event.target.checked ? rows.map((row) => row.id) : [])} /></th>
                {columns.map((column) => <th key={column} className="px-3 py-3">{column}</th>)}
              </tr>
            </thead>
            <tbody className="divide-y divide-slate-100">
              {rows.map((row) => (
                <tr key={row.id}>
                  <td className="px-3 py-3"><input type="checkbox" checked={selected.includes(row.id)} onChange={() => setSelected((current) => current.includes(row.id) ? current.filter((id) => id !== row.id) : [...current, row.id])} /></td>
                  <td className="px-3 py-3 font-medium text-slate-900">{row.employee}</td>
                  <td className="px-3 py-3">{row.department}</td>
                  <td className="px-3 py-3">{row.manager}</td>
                  <td className="px-3 py-3">{row.registrationDate}</td>
                  <td className="px-3 py-3">{row.status}</td>
                  <td className="px-3 py-3">{row.eligibility}</td>
                  <td className="px-3 py-3">{row.assessment}</td>
                  <td className="px-3 py-3">{row.voucher}</td>
                  <td className="px-3 py-3">{row.certification}</td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      )}
    </section>
  );
}

export default DriveRegistrationsTable;
