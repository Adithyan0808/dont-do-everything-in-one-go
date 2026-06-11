import { useQuery } from '@tanstack/react-query';
import { getCurrentUser, validateSession } from '../api/authApi';
import { useAuth } from '../../../hooks/useAuth';

export function useCurrentUser({ enabled = true } = {}) {
  const auth = useAuth();

  return useQuery({
    queryKey: ['auth', 'current-user'],
    queryFn: async () => {
      const profile = await getCurrentUser();
      await validateSession();
      return profile;
    },
    enabled: enabled && auth.isAuthenticated,
  });
}
