import apiClient from './axios';

export const registrationApi = {
  create: (payload) => apiClient.post('/registrations', payload),
  getById: (registrationId) => apiClient.get(`/registrations/${registrationId}`),
  getByUser: (userId) => apiClient.get(`/registrations/user/${userId}`),
  getByDrive: (driveId) => apiClient.get(`/registrations/drive/${driveId}`),
  updateStatus: (registrationId, status) => apiClient.put(`/registrations/${registrationId}/status`, { status }),
  schedule: (registrationId, preferredSlot) => apiClient.post(`/registrations/${registrationId}/schedule`, { preferredSlot }),
  approve: (registrationId, payload) => apiClient.post(`/registrations/${registrationId}/approve`, payload),
  reject: (registrationId, payload) => apiClient.post(`/registrations/${registrationId}/reject`, payload),
};
