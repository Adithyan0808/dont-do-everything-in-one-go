import apiClient from './axios';

export const voucherApi = {
  createPool: (payload) => apiClient.post('/vouchers/pool', payload),
  assign: (payload) => apiClient.post('/vouchers/assign', payload),
  deliver: (voucherId) => apiClient.post(`/vouchers/${voucherId}/deliver`),
  revoke: (voucherId) => apiClient.post(`/vouchers/${voucherId}/revoke`),
  reissue: (voucherId) => apiClient.post(`/vouchers/${voucherId}/reissue`),
  getByDrive: (driveId) => apiClient.get(`/vouchers/drive/${driveId}`),
  getStatus: (voucherId) => apiClient.get(`/vouchers/${voucherId}/status`),
  sendReminders: (payload) => apiClient.post('/vouchers/send-reminders', payload),
};
