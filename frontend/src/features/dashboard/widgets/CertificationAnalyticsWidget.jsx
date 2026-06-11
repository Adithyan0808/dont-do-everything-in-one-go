import {
  Area,
  AreaChart,
  Bar,
  BarChart,
  CartesianGrid,
  Cell,
  Line,
  LineChart,
  Pie,
  PieChart,
  ResponsiveContainer,
  Tooltip,
  XAxis,
  YAxis,
} from 'recharts';
import DashboardErrorState from '../components/DashboardErrorState';
import WidgetSkeleton from '../components/WidgetSkeleton';

const chartColors = ['#4F46E5', '#10B981', '#F59E0B', '#EF4444'];

function CertificationAnalyticsWidget({ query }) {
  if (query.isLoading) return <WidgetSkeleton title="Certification Analytics" />;
  if (query.isError) return <DashboardErrorState error={query.error} onRetry={query.refetch} />;

  const analytics = query.data || {};

  return (
    <section className="rounded-card border border-slate-200 bg-white p-5 shadow-sm">
      <div>
        <h2 className="text-lg font-semibold text-slate-950">Certification Analytics</h2>
        <p className="text-sm text-slate-500">Pass rate, vendor mix, certification distribution, and monthly outcomes</p>
      </div>

      <div className="mt-5 grid gap-5 xl:grid-cols-2">
        <div className="h-64 rounded-card border border-slate-200 p-4">
          <p className="mb-3 text-sm font-semibold text-slate-800">Pass Rate Trend</p>
          <ResponsiveContainer width="100%" height="85%">
            <LineChart data={analytics.passRateTrend || []}>
              <CartesianGrid strokeDasharray="3 3" stroke="#E2E8F0" />
              <XAxis dataKey="month" tickLine={false} axisLine={false} />
              <YAxis tickLine={false} axisLine={false} />
              <Tooltip />
              <Line type="monotone" dataKey="passRate" stroke="#4F46E5" strokeWidth={3} dot={{ r: 3 }} />
            </LineChart>
          </ResponsiveContainer>
        </div>

        <div className="h-64 rounded-card border border-slate-200 p-4">
          <p className="mb-3 text-sm font-semibold text-slate-800">Vendor Distribution</p>
          <ResponsiveContainer width="100%" height="85%">
            <PieChart>
              <Pie data={analytics.vendorDistribution || []} dataKey="value" nameKey="name" innerRadius={48} outerRadius={82} paddingAngle={3}>
                {(analytics.vendorDistribution || []).map((entry, index) => (
                  <Cell key={entry.name} fill={chartColors[index % chartColors.length]} />
                ))}
              </Pie>
              <Tooltip />
            </PieChart>
          </ResponsiveContainer>
        </div>

        <div className="h-64 rounded-card border border-slate-200 p-4">
          <p className="mb-3 text-sm font-semibold text-slate-800">Monthly Certifications</p>
          <ResponsiveContainer width="100%" height="85%">
            <BarChart data={analytics.monthlyCertifications || []}>
              <CartesianGrid strokeDasharray="3 3" stroke="#E2E8F0" />
              <XAxis dataKey="month" tickLine={false} axisLine={false} />
              <YAxis tickLine={false} axisLine={false} />
              <Tooltip />
              <Bar dataKey="count" fill="#10B981" radius={[6, 6, 0, 0]} />
            </BarChart>
          </ResponsiveContainer>
        </div>

        <div className="h-64 rounded-card border border-slate-200 p-4">
          <p className="mb-3 text-sm font-semibold text-slate-800">Certification Distribution</p>
          <ResponsiveContainer width="100%" height="85%">
            <AreaChart data={analytics.passRateTrend || []}>
              <CartesianGrid strokeDasharray="3 3" stroke="#E2E8F0" />
              <XAxis dataKey="month" tickLine={false} axisLine={false} />
              <YAxis tickLine={false} axisLine={false} />
              <Tooltip />
              <Area type="monotone" dataKey="passRate" stroke="#F59E0B" fill="#FEF3C7" strokeWidth={2} />
            </AreaChart>
          </ResponsiveContainer>
        </div>
      </div>
    </section>
  );
}

export default CertificationAnalyticsWidget;
