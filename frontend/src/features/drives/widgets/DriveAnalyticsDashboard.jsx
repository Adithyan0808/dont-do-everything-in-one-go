import { Bar, BarChart, CartesianGrid, Cell, Line, LineChart, Pie, PieChart, ResponsiveContainer, Tooltip, XAxis, YAxis } from 'recharts';
import AnalyticsSkeleton from '../components/AnalyticsSkeleton';
import DriveErrorState from '../components/DriveErrorState';

const colors = ['#4F46E5', '#10B981', '#F59E0B', '#EF4444'];

function DriveAnalyticsDashboard({ query }) {
  if (query.isLoading) return <AnalyticsSkeleton />;
  if (query.isError) return <DriveErrorState error={query.error} onRetry={query.refetch} />;
  const data = query.data || {};

  return (
    <section className="grid gap-5 xl:grid-cols-2">
      <article className="h-72 rounded-card border border-slate-200 bg-white p-5 shadow-sm">
        <h2 className="text-lg font-semibold text-slate-950">Registration Trend</h2>
        <ResponsiveContainer width="100%" height="85%">
          <LineChart data={data.registrationTrend || []}><CartesianGrid strokeDasharray="3 3" /><XAxis dataKey="date" /><YAxis /><Tooltip /><Line dataKey="registrations" stroke="#4F46E5" strokeWidth={3} /></LineChart>
        </ResponsiveContainer>
      </article>
      <article className="h-72 rounded-card border border-slate-200 bg-white p-5 shadow-sm">
        <h2 className="text-lg font-semibold text-slate-950">Pass Rate Trend</h2>
        <ResponsiveContainer width="100%" height="85%">
          <LineChart data={data.passRateTrend || []}><CartesianGrid strokeDasharray="3 3" /><XAxis dataKey="date" /><YAxis /><Tooltip /><Line dataKey="passRate" stroke="#10B981" strokeWidth={3} /></LineChart>
        </ResponsiveContainer>
      </article>
      <article className="h-72 rounded-card border border-slate-200 bg-white p-5 shadow-sm">
        <h2 className="text-lg font-semibold text-slate-950">Department Performance</h2>
        <ResponsiveContainer width="100%" height="85%">
          <BarChart data={data.departmentPerformance || []}><CartesianGrid strokeDasharray="3 3" /><XAxis dataKey="department" /><YAxis /><Tooltip /><Bar dataKey="passed" fill="#10B981" /><Bar dataKey="failed" fill="#EF4444" /></BarChart>
        </ResponsiveContainer>
      </article>
      <article className="h-72 rounded-card border border-slate-200 bg-white p-5 shadow-sm">
        <h2 className="text-lg font-semibold text-slate-950">Voucher Utilization</h2>
        <ResponsiveContainer width="100%" height="85%">
          <PieChart><Pie data={data.voucherUtilization || []} dataKey="value" nameKey="name" outerRadius={86}>{(data.voucherUtilization || []).map((entry, index) => <Cell key={entry.name} fill={colors[index % colors.length]} />)}</Pie><Tooltip /></PieChart>
        </ResponsiveContainer>
      </article>
    </section>
  );
}

export default DriveAnalyticsDashboard;
