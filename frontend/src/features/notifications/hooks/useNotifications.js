import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query';
import { notificationApi } from '../../../api/notificationApi';

const unwrap = (response) => response.data?.data ?? response.data;

export function useNotifications(userId) {
  return useQuery({
    queryKey: ['notifications', userId],
    queryFn: async () => unwrap(await notificationApi.getByUser(userId)),
    enabled: Boolean(userId),
  });
}

export function useUnreadNotificationCount(userId) {
  return useQuery({
    queryKey: ['notifications', 'unread-count', userId],
    queryFn: async () => unwrap(await notificationApi.unreadCount(userId)),
    enabled: Boolean(userId),
  });
}

export function useMarkNotificationRead() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: async (notificationId) => unwrap(await notificationApi.markRead(notificationId)),
    onSuccess: () => queryClient.invalidateQueries({ queryKey: ['notifications'] }),
  });
}
