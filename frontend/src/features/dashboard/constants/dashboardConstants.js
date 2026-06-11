export const DASHBOARD_QUERY_OPTIONS = {
  staleTime: 60 * 1000,
  gcTime: 5 * 60 * 1000,
  refetchInterval: 60 * 1000,
  refetchOnReconnect: true,
  retry: 1,
};

export const DATE_RANGES = [
  { label: 'Last 7 Days', value: '7d' },
  { label: 'Last 30 Days', value: '30d' },
  { label: 'Quarter', value: 'quarter' },
  { label: 'Year', value: 'year' },
  { label: 'Custom', value: 'custom' },
];

export const ALERT_SEVERITY_STYLES = {
  Critical: 'border-red-200 bg-red-50 text-red-800',
  High: 'border-orange-200 bg-orange-50 text-orange-800',
  Medium: 'border-amber-200 bg-amber-50 text-amber-800',
  Low: 'border-blue-200 bg-blue-50 text-blue-800',
};

export const DRIVE_STATUS_STYLES = {
  Draft: 'bg-slate-100 text-slate-700',
  Open: 'bg-emerald-100 text-emerald-700',
  Active: 'bg-emerald-100 text-emerald-700',
  Closed: 'bg-amber-100 text-amber-700',
  Cancelled: 'bg-red-100 text-red-700',
  Archived: 'bg-red-100 text-red-700',
};
