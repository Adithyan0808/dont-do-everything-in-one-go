import { dashboardApi as sharedDashboardApi } from '../../../api/dashboardApi';
import { driveApi } from '../../../api/driveApi';
import { dashboardMockData } from '../services/dashboardMockData';

const unwrap = (response) => response.data?.data ?? response.data;

async function withFallback(request, fallback) {
  try {
    return unwrap(await request());
  } catch {
    return fallback;
  }
}

export const dashboardFeatureApi = {
  getOverview: () => withFallback(sharedDashboardApi.overview, dashboardMockData.overview),
  getKpis: () => withFallback(sharedDashboardApi.overview, { ...dashboardMockData.overview, quickMetrics: dashboardMockData.quickMetrics }),
  getDrivePerformance: () => withFallback(driveApi.getActive, dashboardMockData.drives),
  getRecentActivity: () => withFallback(() => Promise.reject(), dashboardMockData.activity),
  getPendingApprovals: () => withFallback(() => Promise.reject(), dashboardMockData.approvals),
  getSlaMetrics: () => withFallback(sharedDashboardApi.sla, dashboardMockData.sla),
  getDashboardAlerts: () => withFallback(() => Promise.reject(), dashboardMockData.alerts),
  getCertificationAnalytics: () => withFallback(() => Promise.reject(), dashboardMockData.analytics),
  exportDashboard: (format) => {
    const rows = [
      ['Metric', 'Value'],
      ['Active Drives', dashboardMockData.overview.activeDrives],
      ['Registrations', dashboardMockData.overview.totalRegistrations],
      ['Certified', dashboardMockData.overview.certifiedCount],
    ];
    const csv = rows.map((row) => row.join(',')).join('\n');
    const blob = new Blob([csv], { type: format === 'excel' ? 'application/vnd.ms-excel' : 'text/csv' });
    return URL.createObjectURL(blob);
  },
};
