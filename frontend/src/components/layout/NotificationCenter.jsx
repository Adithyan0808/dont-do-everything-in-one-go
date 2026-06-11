import { Bell } from 'lucide-react';
import { useAuth } from '../../hooks/useAuth';
import { useUnreadNotificationCount } from '../../features/notifications/hooks/useNotifications';
import { useAppStore } from '../../store/appStore';

function NotificationCenter() {
  const { user } = useAuth();
  const storeCount = useAppStore((state) => state.notificationCount);
  const { data } = useUnreadNotificationCount(user?.userId);
  const count = data?.count ?? storeCount;

  return (
    <button
      type="button"
      className="relative inline-flex h-10 w-10 items-center justify-center rounded-md border border-slate-200 bg-white text-slate-600 hover:bg-slate-50"
      aria-label="Notifications"
    >
      <Bell className="h-5 w-5" />
      {count > 0 && (
        <span className="absolute -right-1 -top-1 rounded-full bg-danger px-1.5 py-0.5 text-[10px] font-semibold text-white">
          {count}
        </span>
      )}
    </button>
  );
}

export default NotificationCenter;
