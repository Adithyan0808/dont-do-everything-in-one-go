import apiClient from './axios';

export const notificationApi = {
  getByUser: (userId) => apiClient.get(`/notifications/user/${userId}`),
  markRead: (notificationId) => apiClient.put(`/notifications/${notificationId}/read`),
  unreadCount: (userId) => apiClient.get(`/notifications/unread-count/${userId}`),
};
