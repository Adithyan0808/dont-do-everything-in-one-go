import { createContext, useCallback, useEffect, useMemo, useState } from 'react';
import {
  clearSession,
  persistSession,
  readStoredSession,
  updateStoredUser,
} from '../features/auth/services/sessionService';
import { getCurrentUser, registerUser } from '../features/auth/api/authApi';
import {
  getPermissionsForRoles,
  hasPermission as evaluatePermission,
  hasRole as evaluateRole,
} from '../features/auth/utils/permissions';

export const AuthContext = createContext(null);

export function AuthProvider({ children }) {
  const [user, setUser] = useState(null);
  const [token, setToken] = useState(null);
  const [isLoading, setIsLoading] = useState(true);
  const [isSessionRestoring, setIsSessionRestoring] = useState(true);

  const role = user?.role ?? null;
  const roles = useMemo(() => (role ? [role] : []), [role]);
  const permissions = useMemo(() => getPermissionsForRoles(roles), [roles]);

  const applySession = useCallback(({ accessToken, profile, rememberMe }) => {
    const normalizedProfile = {
      ...profile,
      permissions: getPermissionsForRoles(profile?.role),
    };
    persistSession({ token: accessToken, user: normalizedProfile, rememberMe });
    setToken(accessToken);
    setUser(normalizedProfile);
  }, []);

  const logout = useCallback(() => {
    clearSession();
    sessionStorage.clear();
    setToken(null);
    setUser(null);
  }, []);

  const refreshProfile = useCallback(async () => {
    if (!token) return null;
    const profile = await getCurrentUser();
    const normalizedProfile = {
      ...profile,
      permissions: getPermissionsForRoles(profile?.role),
    };
    updateStoredUser(normalizedProfile);
    setUser(normalizedProfile);
    return normalizedProfile;
  }, [token]);

  const restoreSession = useCallback(async () => {
    setIsSessionRestoring(true);
    const stored = readStoredSession();

    if (!stored.token) {
      setToken(null);
      setUser(null);
      setIsSessionRestoring(false);
      setIsLoading(false);
      return null;
    }

    setToken(stored.token);
    setUser(stored.user);

    try {
      const profile = await getCurrentUser();
      const normalizedProfile = {
        ...profile,
        permissions: getPermissionsForRoles(profile?.role),
      };
      updateStoredUser(normalizedProfile);
      setUser(normalizedProfile);
      return normalizedProfile;
    } catch {
      logout();
      return null;
    } finally {
      setIsSessionRestoring(false);
      setIsLoading(false);
    }
  }, [logout]);

  useEffect(() => {
    restoreSession();
  }, [restoreSession]);

  useEffect(() => {
    const handleExpired = () => logout();
    window.addEventListener('auth:session-expired', handleExpired);
    return () => window.removeEventListener('auth:session-expired', handleExpired);
  }, [logout]);

  const login = useCallback(
    ({ accessToken, user: profile, rememberMe }) => {
      applySession({ accessToken, profile, rememberMe });
    },
    [applySession],
  );

  const register = useCallback(async (request) => registerUser(request), []);

  const value = useMemo(
    () => ({
      user,
      token,
      role,
      roles,
      permissions,
      isAuthenticated: Boolean(token),
      isLoading,
      isSessionRestoring,
      login,
      logout,
      register,
      refreshProfile,
      restoreSession,
      hasRole: (allowedRoles) => evaluateRole(roles, allowedRoles),
      hasPermission: (permission) => evaluatePermission(roles, permission),
    }),
    [user, token, role, roles, permissions, isLoading, isSessionRestoring, login, logout, register, refreshProfile, restoreSession],
  );

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
}
