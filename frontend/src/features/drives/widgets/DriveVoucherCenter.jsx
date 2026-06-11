import EmptyState from '../../../components/common/EmptyState';
import DriveErrorState from '../components/DriveErrorState';
import DriveTableSkeleton from '../components/DriveTableSkeleton';

function DriveVoucherCenter({ query }) {
  if (query.isLoading) return <DriveTableSkeleton />;
  if (query.isError) return <DriveErrorState error={query.error} onRetry={query.refetch} />;

  const vouchers = query.data || [];
  const metrics = ['Available', 'Assigned', 'Redeemed', 'Expired', 'Revoked'].map((status) => ({
    status,
    count: vouchers.filter((voucher) => voucher.status === status).length,
  }));

  return (
    <section className="space-y-4">
      <div className="grid gap-3 sm:grid-cols-5">
        {metrics.map((metric) => (
          <article key={metric.status} className="rounded-card border border-slate-200 bg-white p-4 shadow-sm">
            <p className="text-xs font-medium uppercase text-slate-500">{metric.status}</p>
            <p className="mt-2 text-2xl font-semibold text-slate-950">{metric.count}</p>
          </article>
        ))}
      </div>
      <section className="rounded-card border border-slate-200 bg-white p-5 shadow-sm">
        <div className="flex flex-wrap items-center justify-between gap-3">
          <h2 className="text-lg font-semibold text-slate-950">Voucher Pool</h2>
          <div className="flex gap-2">
            {['Assign', 'Revoke', 'Reissue', 'Export'].map((action) => <button key={action} type="button" className="rounded-md border border-slate-300 px-3 py-2 text-sm">{action}</button>)}
          </div>
        </div>
        {vouchers.length === 0 ? (
          <div className="mt-4"><EmptyState title="No Vouchers" description="No vouchers are configured for this drive." /></div>
        ) : (
          <div className="mt-4 overflow-x-auto">
            <table className="min-w-full divide-y divide-slate-200 text-sm">
              <thead className="bg-slate-50 text-left text-xs font-semibold uppercase text-slate-500">
                <tr>{['Masked Code', 'Assigned User', 'Status', 'Assigned Date', 'Redeemed Date', 'Expiry Date'].map((column) => <th key={column} className="px-3 py-3">{column}</th>)}</tr>
              </thead>
              <tbody className="divide-y divide-slate-100">
                {vouchers.map((voucher) => (
                  <tr key={voucher.id}>
                    <td className="px-3 py-3 font-mono">{voucher.maskedCode}</td>
                    <td className="px-3 py-3">{voucher.assignedUser || '-'}</td>
                    <td className="px-3 py-3">{voucher.status}</td>
                    <td className="px-3 py-3">{voucher.assignedDate || '-'}</td>
                    <td className="px-3 py-3">{voucher.redeemedDate || '-'}</td>
                    <td className="px-3 py-3">{voucher.expiryDate}</td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        )}
      </section>
    </section>
  );
}

export default DriveVoucherCenter;
