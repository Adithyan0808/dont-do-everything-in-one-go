import { useMutation } from '@tanstack/react-query';
import { useLocation, useNavigate } from 'react-router-dom';
import toast from 'react-hot-toast';
import { loginUser } from '../api/authApi';
import { getDefaultRouteForRole } from '../utils/permissions';
import { useAuth } from '../../../hooks/useAuth';

export function useLogin() {
  const auth = useAuth();
  const navigate = useNavigate();
  const location = useLocation();

  return useMutation({
    mutationFn: async (payload) => {
      const { rememberMe, ...credentials } = payload;
      const response = await loginUser(credentials);
      return { ...response, rememberMe };
    },
    onSuccess: (response) => {
      auth.login({
        accessToken: response.accessToken,
        user: response.user,
        rememberMe: response.rememberMe,
      });
      toast.success('Signed in successfully');
      navigate(location.state?.from?.pathname || getDefaultRouteForRole(response.user?.role), { replace: true });
    },
    onError: (error) => {
      toast.error(error?.response?.data?.message || 'Invalid credentials');
    },
  });
}
