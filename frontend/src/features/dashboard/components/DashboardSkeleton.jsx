import KpiSkeleton from './kpi/KpiSkeleton';

function DashboardSkeleton() {
  return (
    <div className="space-y-6">
      <div className="h-20 animate-pulse rounded-card bg-white" />
      <div className="grid gap-4 sm:grid-cols-2 xl:grid-cols-4">
        {[1, 2, 3, 4].map((item) => <KpiSkeleton key={item} />)}
      </div>
      <div className="grid gap-4 xl:grid-cols-3">
        <div className="h-80 animate-pulse rounded-card bg-white xl:col-span-2" />
        <div className="h-80 animate-pulse rounded-card bg-white" />
      </div>
    </div>
  );
}

export default DashboardSkeleton;
