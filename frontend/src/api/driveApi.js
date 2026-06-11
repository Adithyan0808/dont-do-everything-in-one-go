import apiClient from './axios';

export const driveApi = {
  getAll: () => apiClient.get('/drives'),
  getActive: () => apiClient.get('/drives/active'),
  getArchived: () => apiClient.get('/drives/archived'),
  getById: (driveId) => apiClient.get(`/drives/${driveId}`),
  getDashboard: (driveId) => apiClient.get(`/drives/${driveId}/dashboard`),
  create: (payload) => apiClient.post('/drives', payload),
  update: (driveId, payload) => apiClient.put(`/drives/${driveId}`, payload),
  updateStatus: (driveId, status) => apiClient.put(`/drives/${driveId}/status`, { status }),
  remove: (driveId) => apiClient.delete(`/drives/${driveId}`),
};
