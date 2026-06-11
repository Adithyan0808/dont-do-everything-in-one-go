import { ChevronRight, LogOut, Menu, Search, UserCircle } from 'lucide-react';
import { useLocation } from 'react-router-dom';
import NotificationCenter from './NotificationCenter';
import { useAuth } from '../../hooks/useAuth';
import { useLogout } from '../../features/auth/hooks/useLogout';
import { getRouteMetadata } from '../../routes/routeConfig';
import { useAppStore } from '../../store/appStore';

function Header() {
  const location = useLocation();
  const route = getRouteMetadata(location.pathname);
  const breadcrumbs = route?.breadcrumb || ['Dashboard'];
  const setMobileSidebarOpen = useAppStore((state) => state.setMobileSidebarOpen);
  const { user } = useAuth();
  const logoutMutation = useLogout();

  return (
    <header className="sticky top-0 z-20 border-b border-slate-200 bg-white/95 px-4 py-3 backdrop-blur sm:px-6 lg:px-8">
      <div className="flex items-center justify-between gap-4">
        <div className="min-w-0">
          <div className="flex items-center gap-2 text-sm text-slate-500">
            <button
              type="button"
              className="rounded-md p-2 text-slate-600 hover:bg-slate-100 lg:hidden"
              onClick={() => setMobileSidebarOpen(true)}
            >
              <Menu className="h-5 w-5" />
            </button>
            {breadcrumbs.map((item, index) => (
              <span key={`${item}-${index}`} className="inline-flex items-center gap-2">
                {index > 0 && <ChevronRight className="h-4 w-4" />}
                <span>{item}</span>
              </span>
            ))}
          </div>
          <h1 className="mt-1 truncate text-lg font-semibold text-slate-900">{breadcrumbs.at(-1)}</h1>
        </div>
        <div className="flex items-center gap-3">
          <label className="hidden items-center gap-2 rounded-md border border-slate-200 bg-white px-3 py-2 text-sm text-slate-500 md:flex">
            <Search className="h-4 w-4" />
            <input className="w-48 outline-none" placeholder="Search" />
          </label>
          <NotificationCenter />
          <div className="hidden items-center gap-2 text-sm text-slate-700 sm:flex">
            <UserCircle className="h-8 w-8 text-slate-400" />
            <span className="max-w-32 truncate">{user?.fullName || user?.email || 'User'}</span>
          </div>
          <button
            type="button"
            className="rounded-md p-2 text-slate-500 hover:bg-slate-100"
            onClick={() => logoutMutation.mutate()}
          >
            <LogOut className="h-5 w-5" />
          </button>
        </div>
      </div>
    </header>
  );
}

export default Header;
