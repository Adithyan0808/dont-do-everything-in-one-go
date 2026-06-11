import apiClient from './axios';

export const eligibilityApi = {
  check: (payload) => apiClient.post('/eligibility/check', payload),
  getByRegistration: (registrationId) => apiClient.get(`/eligibility/registration/${registrationId}`),
  manualApprove: (payload) => apiClient.post('/eligibility/manual-approve', payload),
};
