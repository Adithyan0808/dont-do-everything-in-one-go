import { useMutation, useQueryClient } from '@tanstack/react-query';
import { useNavigate } from 'react-router-dom';
import toast from 'react-hot-toast';
import { logoutUser } from '../api/authApi';
import { useAuth } from '../../../hooks/useAuth';

export function useLogout() {
  const auth = useAuth();
  const navigate = useNavigate();
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: logoutUser,
    onSettled: () => {
      auth.logout();
      queryClient.clear();
      toast.success('Signed out');
      navigate('/login', { replace: true });
    },
  });
}
