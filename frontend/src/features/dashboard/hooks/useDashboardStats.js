import { useQuery } from '@tanstack/react-query';
import { dashboardApi } from '../../../api/dashboardApi';
export * from './useDashboardQueries';

const unwrap = (response) => response.data?.data ?? response.data;

export function useDashboardStats() {
  return useQuery({
    queryKey: ['dashboard', 'overview'],
    queryFn: async () => unwrap(await dashboardApi.overview()),
  });
}

export function useDriveDashboard(driveId) {
  return useQuery({
    queryKey: ['dashboard', 'drive', driveId],
    queryFn: async () => unwrap(await dashboardApi.drive(driveId)),
    enabled: Boolean(driveId),
  });
}
