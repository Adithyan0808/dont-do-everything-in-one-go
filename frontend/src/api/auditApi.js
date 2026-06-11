import apiClient from './axios';

export const auditApi = {
  logs: () => apiClient.get('/audit/logs'),
  entity: (entityName, entityId) => apiClient.get(`/audit/entity/${entityName}/${entityId}`),
  export: (format = 'csv') => apiClient.get('/audit/export', { params: { format }, responseType: 'blob' }),
};
