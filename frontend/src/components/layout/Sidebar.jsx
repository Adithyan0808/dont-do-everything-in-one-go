import { Menu, PanelLeftClose, X } from 'lucide-react';
import { NavLink } from 'react-router-dom';
import { routeConfig } from '../../routes/routeConfig';
import { useAuth } from '../../hooks/useAuth';
import { hasRole } from '../../utils/permissions';
import { useAppStore } from '../../store/appStore';

function Sidebar() {
  const { roles } = useAuth();
  const isCollapsed = useAppStore((state) => state.isSidebarCollapsed);
  const isMobileOpen = useAppStore((state) => state.isMobileSidebarOpen);
  const toggleSidebar = useAppStore((state) => state.toggleSidebar);
  const setMobileSidebarOpen = useAppStore((state) => state.setMobileSidebarOpen);
  const navigation = routeConfig.filter((route) => route.menu && hasRole(roles, route.roles));
  const groupedNavigation = navigation.reduce((groups, route) => {
    groups[route.group] = [...(groups[route.group] || []), route];
    return groups;
  }, {});

  const content = (
    <div className="flex h-full flex-col bg-white">
      <div className="flex h-16 items-center justify-between border-b border-slate-200 px-4">
        {!isCollapsed && <span className="text-sm font-semibold text-slate-900">Maverick Hub</span>}
        <button type="button" className="rounded-md p-2 text-slate-500 hover:bg-slate-100" onClick={toggleSidebar}>
          {isCollapsed ? <Menu className="h-5 w-5" /> : <PanelLeftClose className="h-5 w-5" />}
        </button>
        <button
          type="button"
          className="rounded-md p-2 text-slate-500 hover:bg-slate-100 lg:hidden"
          onClick={() => setMobileSidebarOpen(false)}
        >
          <X className="h-5 w-5" />
        </button>
      </div>
      <nav className="flex-1 overflow-y-auto px-3 py-4">
        {Object.entries(groupedNavigation).map(([group, routes]) => (
          <div key={group} className="mb-5">
            {!isCollapsed && <p className="mb-2 px-3 text-xs font-semibold uppercase text-slate-400">{group}</p>}
            <div className="space-y-1">
              {routes.map((route) => {
                const Icon = route.icon;
                return (
                  <NavLink
                    key={route.path}
                    to={route.path}
                    className={({ isActive }) =>
                      `flex items-center gap-3 rounded-md px-3 py-2 text-sm font-medium ${
                        isActive ? 'bg-primary text-white' : 'text-slate-700 hover:bg-slate-100'
                      }`
                    }
                  >
                    <Icon className="h-5 w-5 shrink-0" />
                    {!isCollapsed && <span>{route.label}</span>}
                  </NavLink>
                );
              })}
            </div>
          </div>
        ))}
      </nav>
    </div>
  );

  return (
    <>
      <aside className={`fixed inset-y-0 left-0 z-30 hidden border-r border-slate-200 lg:block ${isCollapsed ? 'w-20' : 'w-72'}`}>
        {content}
      </aside>
      {isMobileOpen && (
        <div className="fixed inset-0 z-40 lg:hidden">
          <button
            type="button"
            className="absolute inset-0 bg-slate-900/40"
            aria-label="Close navigation"
            onClick={() => setMobileSidebarOpen(false)}
          />
          <aside className="relative h-full w-72 border-r border-slate-200">{content}</aside>
        </div>
      )}
    </>
  );
}

export default Sidebar;
