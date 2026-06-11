import { Navigate, Outlet, useLocation } from 'react-router-dom';
import FullScreenLoader from '../common/FullScreenLoader';
import { useAuth } from '../../hooks/useAuth';

function ProtectedRoute() {
  const { isAuthenticated, isLoading, isSessionRestoring } = useAuth();
  const location = useLocation();

  if (isLoading || isSessionRestoring) {
    return <FullScreenLoader />;
  }

  if (!isAuthenticated) {
    return <Navigate to="/login" replace state={{ from: location }} />;
  }

  return <Outlet />;
}

export default ProtectedRoute;
