import { useQuery, useQueryClient } from '@tanstack/react-query';
import { DASHBOARD_QUERY_OPTIONS } from '../constants/dashboardConstants';
import { dashboardFeatureApi } from '../api/dashboardApi';

export function useDashboardOverview() {
  return useQuery({
    queryKey: ['dashboard', 'overview'],
    queryFn: dashboardFeatureApi.getOverview,
    ...DASHBOARD_QUERY_OPTIONS,
  });
}

export function useKpiMetrics() {
  return useQuery({
    queryKey: ['dashboard', 'kpis'],
    queryFn: dashboardFeatureApi.getKpis,
    ...DASHBOARD_QUERY_OPTIONS,
  });
}

export function useDrivePerformance() {
  return useQuery({
    queryKey: ['dashboard', 'drive-performance'],
    queryFn: dashboardFeatureApi.getDrivePerformance,
    ...DASHBOARD_QUERY_OPTIONS,
  });
}

export function useRecentActivity() {
  return useQuery({
    queryKey: ['dashboard', 'recent-activity'],
    queryFn: dashboardFeatureApi.getRecentActivity,
    ...DASHBOARD_QUERY_OPTIONS,
  });
}

export function usePendingApprovals() {
  return useQuery({
    queryKey: ['dashboard', 'pending-approvals'],
    queryFn: dashboardFeatureApi.getPendingApprovals,
    ...DASHBOARD_QUERY_OPTIONS,
  });
}

export function useSlaMetrics() {
  return useQuery({
    queryKey: ['dashboard', 'sla'],
    queryFn: dashboardFeatureApi.getSlaMetrics,
    ...DASHBOARD_QUERY_OPTIONS,
  });
}

export function useCertificationAnalytics() {
  return useQuery({
    queryKey: ['dashboard', 'analytics'],
    queryFn: dashboardFeatureApi.getCertificationAnalytics,
    ...DASHBOARD_QUERY_OPTIONS,
  });
}

export function useDashboardAlerts() {
  return useQuery({
    queryKey: ['dashboard', 'alerts'],
    queryFn: dashboardFeatureApi.getDashboardAlerts,
    ...DASHBOARD_QUERY_OPTIONS,
  });
}

export function useRefreshDashboard() {
  const queryClient = useQueryClient();
  return () => queryClient.invalidateQueries({ queryKey: ['dashboard'] });
}
