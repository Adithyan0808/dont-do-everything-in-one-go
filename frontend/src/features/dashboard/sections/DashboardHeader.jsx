import { Download, Plus, RefreshCw } from 'lucide-react';
import { useNavigate } from 'react-router-dom';
import { DATE_RANGES } from '../constants/dashboardConstants';
import { dashboardFeatureApi } from '../api/dashboardApi';

function DashboardHeader({ dateRange, onDateRangeChange, onRefresh }) {
  const navigate = useNavigate();

  const exportDashboard = (format) => {
    const url = dashboardFeatureApi.exportDashboard(format);
    const anchor = document.createElement('a');
    anchor.href = url;
    anchor.download = `dashboard.${format === 'excel' ? 'xls' : 'csv'}`;
    anchor.click();
    URL.revokeObjectURL(url);
  };

  return (
    <section className="flex flex-col gap-4 rounded-card border border-slate-200 bg-white p-5 shadow-sm lg:flex-row lg:items-center lg:justify-between">
      <div>
        <h1 className="text-2xl font-semibold text-slate-950">Certification Operations Dashboard</h1>
        <p className="mt-1 text-sm text-slate-500">Real-time certification program overview</p>
      </div>
      <div className="flex flex-wrap items-center gap-2">
        <select
          className="rounded-md border border-slate-300 px-3 py-2 text-sm"
          value={dateRange}
          onChange={(event) => onDateRangeChange(event.target.value)}
          aria-label="Date range filter"
        >
          {DATE_RANGES.map((range) => (
            <option key={range.value} value={range.value}>{range.label}</option>
          ))}
        </select>
        <button type="button" className="inline-flex items-center gap-2 rounded-md border border-slate-300 px-3 py-2 text-sm font-medium" onClick={onRefresh}>
          <RefreshCw className="h-4 w-4" />
          Refresh
        </button>
        <button type="button" className="inline-flex items-center gap-2 rounded-md border border-slate-300 px-3 py-2 text-sm font-medium" onClick={() => exportDashboard('csv')}>
          <Download className="h-4 w-4" />
          CSV
        </button>
        <button type="button" className="inline-flex items-center gap-2 rounded-md border border-slate-300 px-3 py-2 text-sm font-medium" onClick={() => exportDashboard('excel')}>
          <Download className="h-4 w-4" />
          Excel
        </button>
        <button type="button" className="inline-flex items-center gap-2 rounded-md bg-primary px-3 py-2 text-sm font-semibold text-white" onClick={() => navigate('/drives/new')}>
          <Plus className="h-4 w-4" />
          Launch New Drive
        </button>
      </div>
    </section>
  );
}

export default DashboardHeader;
