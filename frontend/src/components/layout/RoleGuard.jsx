import { Navigate, Outlet } from 'react-router-dom';
import { useAuth } from '../../hooks/useAuth';
import { hasRole } from '../../utils/permissions';

function RoleGuard({ roles = [] }) {
  const { roles: userRoles, isSessionRestoring } = useAuth();

  if (isSessionRestoring) {
    return null;
  }

  if (!hasRole(userRoles, roles)) {
    return <Navigate to="/unauthorized" replace />;
  }

  return <Outlet />;
}

export default RoleGuard;
