import { Outlet } from 'react-router-dom';
import Header from '../components/layout/Header';
import Sidebar from '../components/layout/Sidebar';
import { useAppStore } from '../store/appStore';

function MainLayout() {
  const isSidebarCollapsed = useAppStore((state) => state.isSidebarCollapsed);

  return (
    <div className="min-h-screen bg-app-bg">
      <Sidebar />
      <div className={isSidebarCollapsed ? 'lg:pl-20' : 'lg:pl-72'}>
        <Header />
        <main className="px-4 py-6 sm:px-6 lg:px-8">
          <Outlet />
        </main>
      </div>
    </div>
  );
}

export default MainLayout;
