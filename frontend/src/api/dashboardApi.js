import apiClient from './axios';

export const dashboardApi = {
  overview: () => apiClient.get('/dashboard/overview'),
  drive: (driveId) => apiClient.get(`/dashboard/drive/${driveId}`),
  funnel: (driveId) => apiClient.get(`/dashboard/funnel/${driveId}`),
  utilization: (driveId) => apiClient.get('/dashboard/utilization', { params: { driveId } }),
  sla: () => apiClient.get('/dashboard/sla'),
};
